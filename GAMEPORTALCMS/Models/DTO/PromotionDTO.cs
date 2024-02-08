namespace GAMEPORTALCMS.Models.DTO
{
    public class PromotionDTO
    {
        public int Id { get; set; }
        public string? PromotionName { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? ImageMockURL { get; set; }
        public string? EventUrl { get; set; }

        public int Serial { get; set; }
        public int PortalValue { get; set; }
        public bool IsActive { get; set; }

        public string? ClientValueDetails { get;set; }


    }
}
