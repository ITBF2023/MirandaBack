﻿using AutoMapper;
using Core.Common;
using Core.Interfaces;
using DataAccess.Interface;
using Domain.Common;
using Domain.Common.Enum;
using Domain.Dto;
using Domain.Entities;
using Domain.Entities.StoreProcedure;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace Core.Repository
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Configuracion> configuiuracionRepository;
        private readonly IRepository<Cliente> clienteRepository;
        private readonly IRepository<Proceso> procesoRepository;
        private readonly IStoreProcedureRepository storeProcedureRepository;
        private readonly IRepository<Vacante> vacanteRepository;
        private readonly IRepository<RolUsuario> RolUsuarioRepository;

        private readonly IMapper mapper;

        public ClientService(IRepository<Configuracion> configuiuracionRepository, IRepository<Cliente> clienteRepository, IMapper mapper,
            IStoreProcedureRepository storeProcedureRepository, IRepository<Proceso> procesoRepository, IRepository<Vacante> vacanteRepository,
            IRepository<RolUsuario> rolUsuarioRepository)
        {
            this.storeProcedureRepository = storeProcedureRepository;
            this.configuiuracionRepository = configuiuracionRepository;
            this.clienteRepository = clienteRepository;
            this.mapper = mapper;
            this.procesoRepository = procesoRepository;
            this.vacanteRepository = vacanteRepository;
            this.RolUsuarioRepository = rolUsuarioRepository;
        }

        public async Task<BaseResponse> CreateClient(ClientRequest clientRequest)
        {
            var baseRe = new BaseResponse();

            try
            {
                if (!string.IsNullOrEmpty(clientRequest.Name) && !string.IsNullOrEmpty(clientRequest.Nit))
                {
                    if (await InsertClient(clientRequest))
                        baseRe = GetResponse("Cliente Creado con exito");
                    else
                        baseRe = GetResponseFailed();
                }
            }
            catch (Exception ex)
            {
                baseRe.StatusCode = HttpStatusCode.InternalServerError;
                baseRe.Message = ex.Message;
            }
            return baseRe;
        }

        private async Task<bool> InsertClient(ClientRequest clientRequest)
        {
            var cliente = await clienteRepository.GetByParam(x => x.Nit.Equals(clientRequest.Nit));
            if (cliente is null)
            {
                await clienteRepository.Insert(new Cliente
                {
                    Name = clientRequest.Name,
                    Nit = clientRequest.Nit,
                    PathLogo = string.IsNullOrEmpty(clientRequest.Base64File) ? string.Empty : await GetPathLogo(clientRequest.Base64File, clientRequest.Name),
                    UrlEmpresa = clientRequest.UrlEmpresa,
                    IdUserCreated = clientRequest.IdUser,
                    DateCreated = DateTime.Now,
                    Estado = true
                });

                return true;
            }

            return false;
        }

        private async Task<string> GetPathLogo(string base64File, string clientName)
        {
            var saveFile = new SaveFiles();
            var pathLogos = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.PathLogoCliente.ToString())))?.Value ?? string.Empty;

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

            objectFileSave.FileName = $"{clientName}.jpg";
            var pathFile = saveFile.SaveFileBase64(objectFileSave);
            return objectFileSave.FileName;
        }

        private BaseResponse GetResponse(string mensaje)
        {
            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Message = mensaje,
            };
        }

        private BaseResponse GetResponseFailed()
        {
            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "El cliente ya ha sido creado",
            };
        }

        public async Task<ClientResponse> GetClientByDocument(string document)
        {
            var clientResponse = new ClientResponse();
            try
            {
                var cliente = await clienteRepository.GetByParam(x => x.Nit.Equals(document));

                if (cliente == null)
                {
                    clientResponse.Message = "No existe cliente asociado al nit ingresado.";
                }
                else {
                    clientResponse = mapper.Map<ClientResponse>(cliente);
                }
                
                clientResponse.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                clientResponse.Message = ex.Message;
                clientResponse.StatusCode = HttpStatusCode.InternalServerError;
            }

            return clientResponse;
        }

        public async Task<ClientResponse> GetClientByID(int id)
        {
            var clientResponse = new ClientResponse();
            try
            {
                var cliente = await clienteRepository.GetById(id);

                if (cliente == null)
                {
                    clientResponse.Message = "No existe cliente asociado al id ingresado.";
                }
                else
                {
                    clientResponse = mapper.Map<ClientResponse>(cliente);

                    var roleslUsuario = await RolUsuarioRepository.GetAllByParamIncluding(p => p.IdUsuario == id, (p => p.Rol));

                    clientResponse.ListaRoles = mapper.Map<List<RolResponse>>(roleslUsuario);

                }

                clientResponse.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                clientResponse.Message = ex.Message;
                clientResponse.StatusCode = HttpStatusCode.InternalServerError;
            }

            return clientResponse;
        }

        public async Task<List<ClientResponse>> GetListClients()
        {
            try
            {
                List<ClientResponse> clientResponse;
                var cliente = await clienteRepository.GetAll();

                clientResponse = mapper.Map<List<ClientResponse>>(cliente);

                return clientResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SPEmployeesByClientResponse>> GetEmployeesByClient(int idClient)
        {
            try
            {
                List<SPEmployeesByClientResponse> sPEmployeesByClientResponses;
                var empployess = await storeProcedureRepository.GetByEmploiesId(idClient);
                sPEmployeesByClientResponses = MapperSPEmployeesByClientResponse(empployess);
                return sPEmployeesByClientResponses;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<SPEmployeesByClientResponse> MapperSPEmployeesByClientResponse(List<SPEmployeesByClient> sPEmployeesByClients)
        {
            var sPEmployeesByClientResponses = new List<SPEmployeesByClientResponse>();
            if (sPEmployeesByClients is not null || sPEmployeesByClients?.Count > 0)
            {
                sPEmployeesByClients.ForEach(s =>
                {
                    var sPEmployeesByClientResponse = mapper.Map<SPEmployeesByClientResponse>(s);
                    sPEmployeesByClientResponses.Add(sPEmployeesByClientResponse);
                });
            }
            return sPEmployeesByClientResponses;
        }

        public async Task<List<VacantesEmpresaResponse>> GetVacantsByClient(int idClient)
        {
            try
            {
                List<VacantesEmpresaResponse> sPEmployeesByClientResponses;
                var vacantes = await vacanteRepository.GetAllByParamIncluding((x => x.IdCliente == idClient), (x => x.EstadoVacante));
                sPEmployeesByClientResponses = MapperVacantesEmpresaResponse(vacantes);
                return sPEmployeesByClientResponses;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<VacantesEmpresaResponse> MapperVacantesEmpresaResponse(List<Vacante> vacantes)
        {
            var vacantesResponse = new List<VacantesEmpresaResponse>();
            if (vacantes is not null && vacantes.Count > 0)
            {
                foreach (var item in vacantes)
                {
                    var vacanteResponse = new VacantesEmpresaResponse();
                    vacanteResponse.IdVacante = item.IdVacante;
                    vacanteResponse.DescripcionCargo = item.DescripcionCargo;
                    vacanteResponse.IsEstadoVacante = item.IdEstadoVacante;
                    vacanteResponse.EstadoVacante = item?.EstadoVacante?.Description ?? string.Empty;
                    vacantesResponse.Add(vacanteResponse);
                }
            }
            return vacantesResponse;
        }

        public async Task<BaseResponse> UpdateCliet(ClientRequest clientRequest)
        {
            var clientUpdate = new BaseResponse();
            try
            {
                await UpdateCliente(clientRequest);
                clientUpdate = GetResponse("Cliente actualizado con exito");
            }
            catch (Exception ex)
            {
                clientUpdate.StatusCode = HttpStatusCode.InternalServerError;
                clientUpdate.Message = ex.Message;
            }

            return clientUpdate;
        }

        private async Task UpdateCliente(ClientRequest clientRequest)
        {
            var client = await clienteRepository.GetById(clientRequest.IdCliente);

            if (client is not null)
            {
                client.Name = clientRequest.Name;
                client.Nit = clientRequest.Nit;
                client.UrlEmpresa = clientRequest.UrlEmpresa;
                client.UserIdModified = clientRequest.IdUser;
                client.DateModified = DateTime.Now;
                client.Estado = clientRequest.Estado;

                if (!string.IsNullOrEmpty(clientRequest.Base64File))
                    client.PathLogo = await GetPathLogo(clientRequest.Base64File, clientRequest.Name);

                await clienteRepository.Update(client);
            }
        }

        public async Task<BaseResponse> CancelProccessCandidateByClient(CancelProcessClientRequest cancelProcessClientRequest)
        {
            var clientUpdate = new BaseResponse();
            try
            {
                var sPProcessCandidateByClients = await GetProccessCandidateByClient(cancelProcessClientRequest.IdClient, cancelProcessClientRequest.IdCandidato);
                var processId = sPProcessCandidateByClients.Select(x => x.IdProceso).ToList();
                var process = await GetProccessIdClient(processId);
                await CancelProcessCandidate(process);
                clientUpdate = GetResponse("Candidato ha sido rechazado de todos los procesos de la empresa");
            }
            catch (Exception ex)
            {
                clientUpdate.StatusCode = HttpStatusCode.InternalServerError;
                clientUpdate.Message = ex.Message;
            }
            return clientUpdate;
        }

        private async Task<List<SPProcessCandidateByClient>> GetProccessCandidateByClient(int idClient, int idCandidato)
        {
            return await storeProcedureRepository.GetProcessCandidateByClient(idClient, idCandidato);
        }

        private async Task<List<Proceso>> GetProccessIdClient(List<long> sPProcessCandidateByClients)
        {
            return await procesoRepository.GetListByParam(x => sPProcessCandidateByClients.Contains(x.IdProceso));
        }

        private async Task CancelProcessCandidate(List<Proceso> procesos)
        {
            foreach (var item in procesos)
            {
                item.IdEstadoProceso = TipoEstadoProceso.Rechazado.GetIdEstadoProceso();
                await procesoRepository.Update(item);
            }
        }
    }
}