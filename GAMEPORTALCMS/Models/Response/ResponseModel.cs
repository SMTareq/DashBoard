namespace GAMEPORTALCMS.Models.Response
{
    public class ResponseModel
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "Saved successfully";
        public string Details { get; set; }
        public Object Data { get; set; }
    }
}
