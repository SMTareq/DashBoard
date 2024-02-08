using System.ComponentModel.DataAnnotations.Schema;

namespace GAMEPORTALCMS.Models.Entity
{
    [Table("OnlineGames", Schema = "dbo")]
    public class OnlineGames : BaseEntity<int>
    {
        public string? GameName { get; set; }
        public string? GameCode { get; set; }
        public string? PreviewURL { get; set; }
        public string? PortraitURL { get; set; }
        public string? BannerURL { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public int Type { get; set; }
        public string? GameURL { get; set; }

    }
}
