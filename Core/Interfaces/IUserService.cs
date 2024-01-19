using Domain.Common;
using Domain.Dto;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<UserTokenResponse> GetAuthentication(UserTokenRequest userTokenRequest);

        Task<UserResponse> CreateUser(UserRequest userRequest);

        Task<BaseResponse> UpdateUser(UserRequest userRequest);

        Task<List<UserResponse>> GetUser();

        Task<UserResponse> GetUserId(int idUser);

        Task<List<SPProcessByUserResponse>> GetProccesByUser(int idUser);

        Task<ReportingRejectedCandidatesResponse> GetRejectedCandidatesByUser(int idUser);
    }
}