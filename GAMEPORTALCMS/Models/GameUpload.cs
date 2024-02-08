namespace GAMEPORTALCMS.Models
{
    public class GameUpload
    {
        public string? GameName { get; set; }
        public string? GameCode { get; set; }
        public string? PreviewURL { get; set; }
        public string? PortraitURL { get; set; }
        public string? BannerURL { get; set; }
        public int CategoryId { get; set; }
        public string? Category { get; set; }
        public string? PhysicalLocation { get; set; }
        public string? GamePrice { get; set; }
        public string? AndroidVersion { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public bool IsActive { get; set; }
        public string? OwnerCode { get; set; }
        public int Type { get; set; }
        public string?  GameType { get; set; }
    }
}
