namespace GAMEPORTALCMS.Models.DTO
{
    public class GameInfoDTO
    {
        public int Id { get; set; } 
        public int PatchId { get; set; }    
        public int GameId { get; set; }    
        public string? GameName { get; set; }    
        public string? Name { get; set; }    
        public string? Description { get; set; }    
        public string? PortraitImage { get; set; }    
        public string? BannerImage { get; set; }    
        public string? PreviewImage { get; set; }    
        public int CategoryId { get; set; }    
        public int? GameSerial { get; set; }    
        public int? GameType { get; set; }    

        public string? CategoryName { get; set; }
        public string? GamePrice { get; set; }
        public string? AndroidVersion { get; set; }
        public string? Size { get; set; }
        public bool Status { get; set; }

        public string? APKLocation { get; set; }    

        public string? GameURL { get; set; }   
        

        public string? APKMockURL { get; set; }    
        public string? BannerImageMockURL { get; set; }    
        public string? PreviewImageMockURL { get; set; }    
        public string? PortraitImageMockURL { get; set; }    

       
    }
}
