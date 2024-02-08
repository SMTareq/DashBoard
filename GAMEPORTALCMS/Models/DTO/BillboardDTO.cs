namespace GAMEPORTALCMS.Models.DTO
{
    public class BillboardDTO
    {
        public int Id { get; set; } 
        public int GameId { get; set; }
        public string? GameName { get; set; }
        public int GameTypeId { get; set; }
        public string? ImageUrl { get; set; }
        public int Serial { get; set; }
        public bool IsActive { get; set; }
        public int? Portal { get; set; }
        public string? BannerImageMockURL { get; set; }
        public string? ClientValueDetails { get; set; }
    }
}
