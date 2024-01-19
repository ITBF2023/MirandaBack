using AutoMapper;
using Core.Common;
using Core.Interfaces;
using DataAccess.Interface;
using Domain.Common;
using Domain.Common.Enum;
using Domain.Dto;
using Domain.Entities;
using Domain.Entities.StoreProcedure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Core.Repository
{
    public class UserService : IUserService
    {
        private readonly IRepository<Configuracion> configuiuracionRepository;
        private readonly IRepository<Usuario> usuarioRepository;
        private readonly IStoreProcedureRepository storeProcedureRepository;
        private readonly IMapper mapper;

        public UserService(IRepository<Configuracion> configuiuracionRepository, IRepository<Usuario> usuarioRepository, IMapper mapper, IStoreProcedureRepository storeProcedureRepository)
        {
            this.configuiuracionRepository = configuiuracionRepository;
            this.usuarioRepository = usuarioRepository;
            this.mapper = mapper;
            this.storeProcedureRepository = storeProcedureRepository;
        }

        public async Task<UserTokenResponse> GetAuthentication(UserTokenRequest userTokenRequest)
        {
            var UserTokenResponse = new UserTokenResponse();
            try
            {
                var user = await ValidarUsuarioCorreo(userTokenRequest.Correo);

                if (user is null)
                {
                    UserTokenResponse.StatusCode = HttpStatusCode.Unauthorized;
                    UserTokenResponse.Message = "Usuario no encontrado";
                    return UserTokenResponse;
                }

                string pass = DecodeBase64Password(userTokenRequest.Contraseña);

                if (!await ValidatePassword(pass, user.Password))
                {
                    UserTokenResponse.StatusCode = HttpStatusCode.Unauthorized;
                    UserTokenResponse.Message = "Password no valido";
                    return UserTokenResponse;
                }

                UserTokenResponse = await MapperUserTokenResponse(user);
            }
            catch (Exception ex)
            {
                UserTokenResponse.StatusCode = HttpStatusCode.InternalServerError;
                UserTokenResponse.Message = ex.Message;
            }

            return UserTokenResponse;
        }

        private async Task<Usuario?> ValidarUsuarioCorreo(string? correo)
        {
            var user = await usuarioRepository.GetByParam(x => x.Correo.Equals(correo));
            return user;
        }

        private static string DecodeBase64Password(string password)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(password);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public async Task<bool> ValidatePassword(string? password, string encryptedPassword)
        {
            var keyEncrypted = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.KeyEncrypted.ToString())))?.Value ?? string.Empty;
            var iVEncrypted = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.IVEncrypted.ToString())))?.Value ?? string.Empty;
            byte[] key = Encoding.UTF8.GetBytes(keyEncrypted);
            byte[] iv = Encoding.UTF8.GetBytes(iVEncrypted);
            using (TripleDES aes = TripleDES.Create())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
                byte[] encryptedPasswordBytes = Convert.FromBase64String(encryptedPassword);
                byte[] decryptedPasswordBytes = decryptor.TransformFinalBlock(encryptedPasswordBytes, 0, encryptedPasswordBytes.Length);
                string decryptedPassword = Encoding.UTF8.GetString(decryptedPasswordBytes);
                return decryptedPassword == password;
            }
        }

        private async Task<UserTokenResponse> MapperUserTokenResponse(Usuario user)
        {
            UserTokenResponse UserTokenResponse;
            UserTokenResponse = mapper.Map<UserTokenResponse>(user);
            UserTokenResponse.IdSesion = 1;
            UserTokenResponse.StatusCode = HttpStatusCode.OK;
            UserTokenResponse.Token = await GenerateToken(user.Correo);
            return UserTokenResponse;
        }

        private async Task<string> GenerateToken(string? correo = "")
        {
            var secretKey = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtSecretKey.ToString())))?.Value ?? string.Empty;
            var jwtIssuerToken = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtIssuerToken.ToString())))?.Value ?? string.Empty;
            var jwtAudienceToken = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtIssuerToken.ToString())))?.Value ?? string.Empty;
            var jwtExpireTime = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.JwtExpireTime.ToString())))?.Value ?? string.Empty;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            ClaimsIdentity claimsIdentity = new(new[] { new Claim(ClaimTypes.Name, correo) });
            var currentDate = DateTime.Now;
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: jwtAudienceToken,
                issuer: jwtIssuerToken,
                subject: claimsIdentity,
                notBefore: currentDate,
                expires: currentDate.AddMinutes(Convert.ToInt32(jwtExpireTime)),
                signingCredentials: signingCredentials);
            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }

        /// <summary>
        /// Creacion de usuario
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        public async Task<UserResponse> CreateUser(UserRequest userRequest)
        {
            var userResponse = new UserResponse();

            try
            {
                var validateUser = await ValidarUsuarioCorreo(userRequest.Correo);
                if (validateUser is null)
                {
                    var usuario = mapper.Map<Usuario>(userRequest);
                    string pass = DecodeBase64Password(userRequest.Contraseña);

                    usuario.Nombres = userRequest.Nombres;
                    usuario.Apellidos = userRequest.Apellidos;
                    usuario.Correo = userRequest.Correo;
                    usuario.Password = await EncryptedPassword(pass);
                    usuario.Foto = string.IsNullOrEmpty(userRequest.FotoBase64) ? string.Empty : await GetPathFoto(userRequest.FotoBase64, userRequest.Correo);

                    await InsertUser(usuario);
                    //userResponse.IdRol = usuario.IdRol;
                    userResponse.StatusCode = HttpStatusCode.OK;
                    userResponse.Message = $"El usuario {usuario.Correo} a sido creado con exito";
                }
                else
                {
                    userResponse.StatusCode = HttpStatusCode.Conflict;
                    userResponse.Message = $"Ya existe un usuario con el nombre {userRequest.Correo}";
                }
            }
            catch (Exception ex)
            {
                userResponse.StatusCode = HttpStatusCode.InternalServerError;
                userResponse.Message = ex.Message;
            }
            return userResponse;
        }

        private async Task<string> GetPathFoto(string base64File, string name)
        {
            var saveFile = new SaveFiles();
            var pathLogos = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.PathFotoUsuario.ToString())))?.Value ?? string.Empty;

            var objectFileSave = new ObjectFileSave();
            objectFileSave.FilePath = pathLogos;

            if (base64File.Contains(","))
            {
                string[] data = base64File.Split(',');
                objectFileSave.Base64String = data[1];
            }
            else
            {
                objectFileSave.Base64String = base64File;
            }

            objectFileSave.FileName = $"{name}.jpg";
            var pathFile = saveFile.SaveFileBase64(objectFileSave);
            return pathFile;
        }

        private async Task<string> EncryptedPassword(string password)
        {
            var keyEncrypted = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.KeyEncrypted.ToString())))?.Value ?? string.Empty;
            var iVEncrypted = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.IVEncrypted.ToString())))?.Value ?? string.Empty;

            byte[] key = Encoding.UTF8.GetBytes(keyEncrypted);
            byte[] iv = Encoding.UTF8.GetBytes(iVEncrypted);

            using (TripleDES aes = TripleDES.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);

                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] encryptedPasswordBytes = encryptor.TransformFinalBlock(passwordBytes, 0, passwordBytes.Length);

                string encryptedPassword = Convert.ToBase64String(encryptedPasswordBytes);
                return encryptedPassword;
            }
        }

        private async Task InsertUser(Usuario usuario)
        {
            await usuarioRepository.Insert(usuario);
        }

        public async Task<BaseResponse> UpdateUser(UserRequest userRequest)
        {
            var baseResponse = new BaseResponse();
            try
            {
                var user = await GetUserById(userRequest.Id);
                if (user is not null)
                {
                    await UpdateUser(user, userRequest);
                    baseResponse = GetResponse("Usuario actualizado con exito", HttpStatusCode.OK);
                }
                else
                {
                    baseResponse = GetResponse("Id Usuario no encontrado", HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                baseResponse.StatusCode = HttpStatusCode.InternalServerError;
                baseResponse.Message = ex.Message;
            }
            return baseResponse;
        }

        private async Task<Usuario> GetUserById(int idUser)
        {
            return await usuarioRepository.GetById(idUser);
        }

        private async Task UpdateUser(Usuario usuario, UserRequest userRequest)
        {
            usuario.Correo = userRequest.Correo;
            if (!string.IsNullOrEmpty(userRequest.Contraseña))
            {
                string pass = DecodeBase64Password(userRequest.Contraseña);
                usuario.Password = await EncryptedPassword(pass);
            }
            //usuario.IdRol = userRequest.IdRol;
            await usuarioRepository.Update(usuario);
        }

        private BaseResponse GetResponse(string mensaje, HttpStatusCode httpStatusCode)
        {
            return new BaseResponse()
            {
                StatusCode = httpStatusCode,
                Message = mensaje,
            };
        }

        public async Task<List<UserResponse>> GetUser()
        {
            List<UserResponse> list;
            try
            {
                var users = await usuarioRepository.GetAll();
                list = MapperUserResponse(users);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<UserResponse> MapperUserResponse(List<Usuario> usuarios)
        {
            var list = new List<UserResponse>();
            if (usuarios is not null && usuarios.Count > 0)
            {
                foreach (var item in usuarios)
                {
                    //userResponse.DescripcionRol = item.Rol.Description;
                    //userResponse.IdRol = item.IdRol;

                    list.Add(new UserResponse()
                    {
                        Nombres = item.Nombres,
                        Apellidos = item.Apellidos,
                        Telefono = item.Telefono,
                        Correo = item.Correo
                    });
                }
            }

            return list;
        }

        public async Task<UserResponse> GetUserId(int idUser)
        {
            var userResponse = new UserResponse();
            try
            {
                var user = await GetUserById(idUser);
                if (user is not null)
                {
                    userResponse = mapper.Map<UserResponse>(user);
                    userResponse.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    userResponse.StatusCode = HttpStatusCode.NotFound;
                    userResponse.Message = "El id no se encuentra";
                }
            }
            catch (Exception ex)
            {
                userResponse.StatusCode = HttpStatusCode.InternalServerError;
                userResponse.Message = ex.Message;
            }
            return userResponse;
        }

        public async Task<List<SPProcessByUserResponse>> GetProccesByUser(int idUser)
        {
            List<SPProcessByUserResponse> list;
            try
            {
                var users = await GetSPProcessByUser(idUser);
                list = MapperSPProcessByUserResponse(users);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<List<SPProcessByUser>> GetSPProcessByUser(int idUser)
        {
            return await storeProcedureRepository.GetProcessCandidateByUser(idUser);
        }

        private List<SPProcessByUserResponse> MapperSPProcessByUserResponse(List<SPProcessByUser> sPProcessByUsers)
        {
            var list = new List<SPProcessByUserResponse>();
            if (sPProcessByUsers is not null && sPProcessByUsers.Count > 0)
            {
                foreach (var item in sPProcessByUsers)
                {
                    var sPProcessByUserResponse = mapper.Map<SPProcessByUserResponse>(item);
                    list.Add(sPProcessByUserResponse);
                }
            }
            return list;
        }

        public async Task<ReportingRejectedCandidatesResponse> GetRejectedCandidatesByUser(int idUser)
        {
            var reportingRejectedCandidatesResponse = new ReportingRejectedCandidatesResponse();
            try
            {
                var proceesRejected = await GetSPRejectedCandidatesByUser(idUser);
                reportingRejectedCandidatesResponse.UrlExcel = await SaveExcel(proceesRejected, idUser);
                reportingRejectedCandidatesResponse.StatusCode = HttpStatusCode.OK;
                reportingRejectedCandidatesResponse.Message = "Descarga Exitosa";
            }
            catch (Exception ex)
            {
                reportingRejectedCandidatesResponse.Message = ex.Message;
                reportingRejectedCandidatesResponse.StatusCode = HttpStatusCode.InternalServerError;
            }
            return reportingRejectedCandidatesResponse;
        }

        private async Task<List<SPRejectedCandidatesByUser>> GetSPRejectedCandidatesByUser(int idUser)
        {
            return await storeProcedureRepository.GetRejectedCandidatesByUser(idUser);
        }

        private async Task<string> SaveExcel(List<SPRejectedCandidatesByUser> sPRejectedCandidatesByUsers, int idUser)
        {
            var pathExcel = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.PathExcelRejected.ToString())))?.Value ?? string.Empty;

            var Savefile = new SaveFiles();
            if (sPRejectedCandidatesByUsers is not null && sPRejectedCandidatesByUsers.Count > 0)
            {
                var objectFileSaveExcel = new ObjectFileSaveExcel();
                objectFileSaveExcel.Lista = sPRejectedCandidatesByUsers;
                objectFileSaveExcel.IdUser = idUser;
                objectFileSaveExcel.Path = pathExcel;
                return Savefile.SaveExcel(objectFileSaveExcel);
            }
            return string.Empty;
        }
    }
}