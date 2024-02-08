
using System.ComponentModel.DataAnnotations;

namespace GAMEPORTALCMS.Models.Request
{
    public class UserRequestCreate
    {
        [Required]
        [StringLength(13)]
   
        public string MobileNumber { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
