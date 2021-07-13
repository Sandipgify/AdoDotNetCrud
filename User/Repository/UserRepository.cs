using Database.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.EntityModels.Dto;
using User.Repository.Interface;
using EntityUser = User.EntityModels.Entity;

namespace User.Repository
{

    public class UserRepository:IUserRepository
    {
        private readonly IDbAccessManager _dbAccessManager;
        public UserRepository(IDbAccessManager dbAccessManager)
        {
            _dbAccessManager = dbAccessManager;
        }

        public async Task<EntityUser.User> GetUserAsync(int Id)
        {
            var query = "select * from users where id=@id";
            var users = (await _dbAccessManager.ReadDataAsync<EntityUser.User>(query, new { id = Id })).FirstOrDefault();
            return users;
        }

        public async Task<IEnumerable<EntityUser.User>> GetUserAsync()
        {
            var query = "select * from users";
            var users = await _dbAccessManager.ReadDataAsync<EntityUser.User>(query);
            return users;
        }

        public async Task<EntityUser.User> GetUserAsync(string email)
        {
            var query = "select * from users where email=@email";
            var users = (await _dbAccessManager.ReadDataAsync<EntityUser.User>(query, new { email = email })).FirstOrDefault();
            return users;
        }

        public async Task<int> CreateAsync(EntityUser.User user)
        {
            var query = @"INSERT INTO [dbo].[users]
           ([FirstName]
           ,[LastName]
           ,[ContactNo]
           ,[Email]
           ,[Password])
     VALUES
           (@firstName
           ,@lastName
           ,@contactNo
           ,@email
           ,@pass) select scope_identity()";
            var insertedRowId =await _dbAccessManager.GetScalarAsync<int>(query, new { firstName = user.FirstName, lastName = user.LastName, contactNo = user.ContactNo, email = user.Email, pass = user.Password });
            return (int)insertedRowId;
        }

        public async Task<int> UpdateAsync(EntityUser.User user)
        {
            var query = "Update users set FirstName=@FirstName,LastName=@lastName,ContactNo=@contactNo where id=@id";
            var rowAffected = await _dbAccessManager.UpdateAsync(query, new { FirstName = user.FirstName, LastName = user.LastName, contactNo = user.ContactNo, id = user.Id });
            return rowAffected;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = "delete from users where id=@id";
            var rowAffected = await _dbAccessManager.DeleteAsync(query, new {id = id });
            return rowAffected;
        }

        public async Task<bool> CheckEmailExist(string email)
        {
            var query = "select count(*) from users where email=@email";
            var emailCount = await _dbAccessManager.GetScalarAsync<int>(query, new { email = email });
           return (int)emailCount > 0 ? true : false;
        }

        public async Task<IEnumerable<UserDto>> GetUserList()
        {
            var query = "select Id,FirstName+' '+LastName as FullName from Users";
            var task = await _dbAccessManager.ReadDataAsync<UserDto>(query);
            return task;
        }
    }
}
