namespace ServicesExample.Configurations.Options;

public class JwtOptions
{
    public int ExpiredAtMinutes { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string IssuerSecretKey { get; set; }
}
