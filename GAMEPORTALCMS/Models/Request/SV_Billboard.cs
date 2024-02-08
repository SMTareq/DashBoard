namespace GAMEPORTALCMS.Models.Request
{
    public class SV_Billboard
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int GameTypeId { get; set; }
        public string BannerImageMockURL { get; set; }
        public int Serial { get; set; }
        public bool IsActive { get; set; }
        public int Portal { get; set; }

        public string? ClientDetails { get; set; }

    }
}
