using BackEndWeb.Controller.Model.Acceptance.FilterModel;
using BackEndWeb.Controller.Model.Acceptance.LoginModel;
using BackEndWeb.Controller.Model.Sending.UserModel;
using BackEndWeb.LDAPService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace BackEndWeb.Controller.AuthController;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private LdapService _ldap;
    private IConfiguration _configuration;
    
    public AuthController(LdapService ldap,  IConfiguration configuration)
    {
        _ldap = ldap;
        _configuration = configuration;
    }
    
    [HttpPost]
    public IActionResult Login([FromBody] LoginModel request) // Проверка авторизации
    {
        UserModel? userModel = _ldap.LoginVerification(request);
        
        if (userModel == null) 
            return Unauthorized();
        
        userModel.Token = GenerateJwtToken(userModel.Uid);

        if (userModel.Token == null)
            return BadRequest("Token was not created");
        
        return Ok(userModel);
    }
    
    [HttpPost("filter")]
    public IActionResult Filter([FromBody] FilterModel request) // Получение данных по фильтру 
    {
        List<UserModel>? models = _ldap.GttingStudentsByCn(request);
        
        if (models != null) 
            return Ok(models);
        
        return Unauthorized();
    }

    private string GenerateJwtToken(string? uid)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"];
        var issuer = jwtSettings["ValidIssuer"];
        var audience = jwtSettings["ValidAudience"];
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, uid), // ID пользователя
            new Claim(ClaimTypes.Email, "test@example.com"),  // пример Email
            new Claim(ClaimTypes.Role, "User")               // пример роли пользователя
        };
        
        var token = new JwtSecurityToken(
            issuer: issuer,       // Издатель
            audience: audience,     // Получатель
            claims: claims,         // Утверждения
            expires: DateTime.Now.AddMinutes(1), // Время жизни токена (например, 30 минут)
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}