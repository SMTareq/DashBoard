namespace GAMEPORTALCMS.Models.DTO
{
    public class CMSuserDTO
    {
        public int Id { get; set; }
        public string UserCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsBlock { get; set; }
    }
}
