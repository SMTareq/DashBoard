namespace GAMEPORTALCMS.Models.DTO
{
    public class GamePatchDTO
    {
        public int Id { get; set; }
        public string? PatchName { get; set; }
        public int PatchType { get; set; }
        public string PatchTypeName { get; set; }
        public int Serial { get; set; }

        public bool IsActive { get; set; }

        public int PortalValue { get; set; }


    }
}
