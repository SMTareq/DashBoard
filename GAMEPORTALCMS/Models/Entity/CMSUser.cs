using System.ComponentModel.DataAnnotations.Schema;

namespace GAMEPORTALCMS.Models.Entity
{
    [Table("CMSUser", Schema = "dbo")]
    public class CMSUser 
    {
        public int Id { get; set; } 
        public string UserCode { get; set; } 
        public string Username { get; set; } 
        public string Password { get; set; } 
        public bool IsBlock { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public string CreatedBy { get; set; }
    }
}
