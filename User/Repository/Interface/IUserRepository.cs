using System.Collections.Generic;
using System.Threading.Tasks;
using EntityUser = User.EntityModels.Entity;
using User.EntityModels.Dto;

namespace User.Repository.Interface
{
    public interface IUserRepository
    {
        Task<EntityUser.User> GetUserAsync(int Id);
        Task<IEnumerable<EntityUser.User>> GetUserAsync();
        Task<EntityUser.User> GetUserAsync(string email);
        Task<int> CreateAsync(EntityUser.User user);
        Task<int> UpdateAsync(EntityUser.User user);
        Task<int> DeleteAsync(int id);
        Task<bool> CheckEmailExist(string email);
        Task<IEnumerable<UserDto>> GetUserList();
    }
}
