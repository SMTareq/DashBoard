using System.ComponentModel.DataAnnotations;

namespace GAMEPORTALCMS.Models.Request
{
    public class UserUpdateRequest
    {
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        //public IFormFile Image { get; set; }
    }
}
