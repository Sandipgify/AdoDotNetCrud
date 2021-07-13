using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.EntityModels.Dto;
using IdentityUser = User.EntityModels.Entity;

namespace User.EntityModels
{
    public static class MappingConfig
    {
        public static IdentityUser.User ToEntity(this UserDto userDto)
            => new IdentityUser.User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                ContactNo = userDto.ContactNo
            };


        public static UserDto ToDto(this IdentityUser.User user)
            => new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                ContactNo = user.ContactNo,
                Email=user.Email
            };


    }
}
