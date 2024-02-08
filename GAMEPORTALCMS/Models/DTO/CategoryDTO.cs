namespace GAMEPORTALCMS.Models.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public string? ImageUrl { get; set; }
        public string? IconUrl { get; set; }

        public string? Mock_ImageUrl { get; set; }
        public string? Mock_IconUrl { get; set; }
        public int Seial { get; set; }
        public int PortalValue { get; set; } = 1;

        public bool IsActive { get; set; }



    }
}
