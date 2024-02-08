using System.ComponentModel.DataAnnotations.Schema;

namespace GAMEPORTALCMS.Models.Entity
{
    [Table("GamePortalClient", Schema = "dbo")]
    public class GamePortalClient
    {    
        public int Id { get; set; }
        public string ClientName { get; set; }
        public int ClientValue { get; set; }
        public bool IsActive {  get; set; }
    }
}
