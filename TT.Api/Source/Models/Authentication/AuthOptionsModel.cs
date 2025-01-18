namespace TT.Models.Authentication;

public class AuthOptionsModel
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public string Key { get; set; }
    public uint Lifetime { get; set; }
}
