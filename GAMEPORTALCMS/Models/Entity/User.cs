using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAMEPORTALCMS.Models.Entity
{
    [Table("User", Schema = "dbo")]
    public class User:BaseEntity<int>
    {
        [Key]
        public string UserCode { get; set; }
        public string MSISDN { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Image { get; set; }
        public int Client { get; set; }
        public bool IsBlock { get; set; }
    }
}
