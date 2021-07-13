using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.EntityModels.Dto;

namespace User.Service.Interface
{
    public interface IUserService
    {
        Task<int> SaveAsync(UserDto userDto);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int Id);
        Task<UserDto> UpdateAsync(int Id, UserDto userDto);
        Task<bool> CheckEmailExist(string email);
        Task<IEnumerable<UserDto>> UserList();
        int GetCurrentUserId();
        string GetCurrentUserName();
    }
}
