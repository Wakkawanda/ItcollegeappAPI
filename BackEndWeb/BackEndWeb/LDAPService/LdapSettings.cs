namespace BackEndWeb.LDAPService;

public class LdapSettings
{
    public string? Host { get; set; }
    public int Port { get; set; }
    public string? BaseDc { get; set; }
    public int ProtocolVersion { get; set; }
}