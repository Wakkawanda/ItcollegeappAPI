using System.DirectoryServices.Protocols;
using System.Text.RegularExpressions;
using BackEndWeb.Controller.Model.Acceptance.FilterModel;
using BackEndWeb.Controller.Model.Acceptance.LoginModel;
using BackEndWeb.Controller.Model.Sending.UserModel;
using Microsoft.Extensions.Options;

namespace BackEndWeb.LDAPService;

public class LdapService
{
    private readonly LdapSettings _ldapSettings;
    public LdapService(IOptions<LdapSettings> ldapSettings)
    {
        _ldapSettings = ldapSettings.Value ?? throw new ArgumentNullException(nameof(ldapSettings), "LdapSettings cannot be null.");
        
        // Обработчики ошибок
        if (string.IsNullOrEmpty(_ldapSettings.Host))
            throw new ArgumentException("LDAP Host cannot be null or empty.", nameof(_ldapSettings.Host));

        if (string.IsNullOrEmpty(_ldapSettings.BaseDc))
            throw new ArgumentException("LDAP BaseDc cannot be null or empty.", nameof(_ldapSettings.BaseDc));
    }
    
    public LdapConnection? LdapConnect(string? userName, string? password) // Создание + Проверка подключение к LDAP
    {
        string dn = $"uid={userName},ou=People,{_ldapSettings.BaseDc}"; // DN пользователя для подключения
        
        LdapConnection connection = new LdapConnection(new LdapDirectoryIdentifier(_ldapSettings.Host, _ldapSettings.Port));
        connection.Credential = new System.Net.NetworkCredential(dn, password);
        connection.AuthType = AuthType.Basic;
        connection.SessionOptions.ProtocolVersion = _ldapSettings.ProtocolVersion;
        
        try
        {
            connection.Bind();
            return connection;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public List<UserModel>? GttingStudentsByCn(FilterModel request) // Вывод студентов по определённой группе или специальности.
    {
        List<UserModel> users = new List<UserModel>();

        string filter = $"(&(objectClass=inetOrgPerson)(memberOf=cn={request.Filter},ou=groups,{_ldapSettings.BaseDc}))";

        try
        {
            using (LdapConnection? connection = LdapConnect(request.Username, request.Password))
            {
                Console.WriteLine(connection == null);
                if (connection == null) return null; 
                
                SearchRequest searchRequest = new SearchRequest(
                    $"ou=People,{_ldapSettings.BaseDc}",
                    filter,
                    SearchScope.Subtree,
                    new string[] { "uid", "cn" }
                );

                SearchResponse searchResponse = (SearchResponse)connection.SendRequest(searchRequest);

                foreach (SearchResultEntry entry in searchResponse.Entries)
                {
                    if (entry.Attributes["uid"] != null && entry.Attributes["cn"] != null)
                    {
                        string? uid = entry.Attributes["uid"][0].ToString();
                        string? cn = entry.Attributes["cn"][0].ToString();
                        users.Add(new UserModel{Uid = uid, Cn = cn});
                    }
                    else
                    {
                        Console.WriteLine($"Missing uid or cn attribute (or both) for DN: {entry.DistinguishedName}");
                        return null;
                    }
                }
                
                return users;
            }
        }
        catch (LdapException ex)
        {
            Console.WriteLine($"LDAP Search Error: {ex.Message}");
            Console.WriteLine($"LDAP Error Code: {ex.ErrorCode}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error during search: {ex.Message}");
        }
        return null;
    }

    public UserModel? LoginVerification(LoginModel request)
    {
        try
        {
            using (LdapConnection? connection = LdapConnect(request.Username, request.Password))
            {
                if (connection == null) return null;

                SearchRequest searchRequest = new SearchRequest(
                    $"uid={request.Username},ou=People,{_ldapSettings.BaseDc}",
                    "(objectClass=*)",
                    SearchScope.Base,
                    "cn", "memberOf"
                );
                
                SearchResponse searchResponse = (SearchResponse)connection.SendRequest(searchRequest);
                
                if (searchResponse.Entries.Count > 0)
                {
                    SearchResultEntry entry = searchResponse.Entries[0];
                    string cn = "";
                    
                    List<string> memberOfList = new List<string>();
                    
                    if (entry.Attributes.Contains("cn")) // Вывод имени и фамилии
                        cn += entry.Attributes["cn"][0] + " ";
                    
                    for (int i = 0; i < entry.Attributes["memberOf"].Count; i++) // Вывод групп
                    {
                        Match match = Regex.Match(entry.Attributes["memberOf"][i].ToString(), @"cn=([^,]+),ou=groups.*");
                        
                        if (match.Success)
                            cn += match.Groups[1].Value + ", ";
                    }

                    return new UserModel(){Uid = request.Username, Cn = cn};
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"LDAP Search Error: {ex.Message}");
        }
        
        return null;
    }
}