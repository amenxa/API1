using System.ComponentModel.DataAnnotations;

namespace ApiTest.Data.DTOS
{
    public class UserDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }


    }
}
