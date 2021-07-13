using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.EntityModels;
using User.EntityModels.Dto;
using User.Repository.Interface;
using User.Service.Interface;

namespace User.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> CheckEmailExist(string email)
           => await _userRepository.CheckEmailExist(email);


        public async Task<IEnumerable<UserDto>> GetAllAsync()
        
            =>(await _userRepository.GetUserAsync()).Select(x => new UserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                ContactNo = x.ContactNo,
                Email = x.Email
            });
           

        
        public async Task<UserDto> GetByIdAsync(int Id)
          => (await _userRepository.GetUserAsync(Id)).ToDto();

        public int GetCurrentUserId() => Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("Id").Value);


        public string GetCurrentUserName() => _httpContextAccessor.HttpContext.User.FindFirst("UserName").Value.ToString();
        

        public async Task<int> SaveAsync(UserDto userDto)
        {
            var userEntity = userDto.ToEntity();
            userEntity.Email = userDto.Email;
            userEntity.Password = userDto.Password;
            var id = await _userRepository.CreateAsync(userEntity);
            return id;
        }

        public async Task<UserDto> UpdateAsync(int Id, UserDto userDto)
        {

            var existingUser = await _userRepository.GetUserAsync(Id);
            if (existingUser == null)
                return null;
            existingUser.FirstName = userDto.FirstName;
            existingUser.LastName = userDto.LastName;
            existingUser.ContactNo = userDto.ContactNo;
            await _userRepository.UpdateAsync(existingUser);
            return existingUser.ToDto();
        }

        public async Task<IEnumerable<UserDto>> UserList()
       =>await _userRepository.GetUserList();
    }
}
