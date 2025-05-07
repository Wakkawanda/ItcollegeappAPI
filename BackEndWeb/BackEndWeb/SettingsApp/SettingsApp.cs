namespace BackEndWeb.SettingsApp;

public class SettingsApp
{
    //Ldap Settings
    private string Host;
    private int Port;
    private string BaseDC;

    public SettingsApp(string host,int port,string baseDc)
    {
        Host = host;
        Port = port;
        BaseDC = baseDc;
    }

    public string getHost() => Host;
    public int getPort() => Port;
    public string getBaseDc() => BaseDC;
}