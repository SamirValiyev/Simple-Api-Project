namespace SimpleApiProjectUI.Models
{
    public class JwtTokenResponseModel
    {
        public string? AccessToken { get; set; }
        // public string RefreshToken { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
