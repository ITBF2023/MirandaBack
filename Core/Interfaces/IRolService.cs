using Domain.Common;
using Domain.Dto;

namespace Core.Interfaces
{
    public interface IRolService
    {
        Task<List<RolResponse>> GetAll();
    }
}