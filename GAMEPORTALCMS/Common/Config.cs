namespace GAMEPORTALCMS.Common
{
    public static class Config
    {

        public static string SaltKey = "M2Fixed#SaltV@lue";
        public static string BaseImageURL = "https://gakk-content.sgp1.digitaloceanspaces.com/gameportal/";
        //public static string BannerImageURL = "https://gakk-content.sgp1.digitaloceanspaces.com/gameportal/";
        public static string PhysicalFileURL = "https://sgp1.digitaloceanspaces.com/gameportal/";

        //JWT
        public static string JWT_SecretKey = "a_CustomCode@#$FOR@@GAME";
        public static string JWT_Issuer = "GAMEPORTAL";
        public static string JWT_Audience = "CLINETPLAYZONE";

        // Client
        public static int Play2Win = 1;// 
    }
}
