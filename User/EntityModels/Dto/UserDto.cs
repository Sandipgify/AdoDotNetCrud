using System.ComponentModel;

namespace User.EntityModels.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string FullName { get; set; }
    }
}
