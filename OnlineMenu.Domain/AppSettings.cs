namespace OnlineMenu.Domain
{
    public class AppSettings
    {
        public Secrets Secrets { get; set; }
        public int JwtExpirationTimeHours { get; set; }
    }

    public class Secrets
    {
        public string JwtKey { get; set; }
    }
}