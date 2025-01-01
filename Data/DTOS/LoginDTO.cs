using System.ComponentModel.DataAnnotations;

namespace ApiTest.Data.DTOS
{
    public class LoginDTO
    {
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
