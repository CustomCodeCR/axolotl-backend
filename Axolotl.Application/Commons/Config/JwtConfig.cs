namespace Axolotl.Application.Commons.Config;

public class JwtConfig
{
    public string? Expires { get; set; }
    public string? Issuer { get; set; }
    public string? Secret { get; set; }
}
