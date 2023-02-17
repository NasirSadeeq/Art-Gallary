using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Art_Gallery.Interface;
using Art_Gallery.DbContextClasses;
using Microsoft.IdentityModel.Tokens;

namespace Art_Gallery.Repository;
public class LoginRepository : Ilogin
{
    private readonly IConfiguration configuration;
    private readonly GallaryDbContext dbContext;

    public LoginRepository(IConfiguration configuration,GallaryDbContext dbContext )
    {
        this.configuration = configuration;
        this.dbContext = dbContext;
    }
    public string Authenticate(string Email, string Password, string role)
    {
        var data=dbContext.registrations.Where(e=>e.Email==Email && e.role==role).FirstOrDefault();
            if (data != null)
            {
                bool isValid = (data.Email == Email && decryptPassword(data.Password) == Password);
                if (isValid)
                {
                    var Key = configuration.GetValue<string>("JwtConfig:Key");
                    var KeyBytes = Encoding.ASCII.GetBytes(Key);

                    var tokenHandler = new JwtSecurityTokenHandler();

                    var tokenDescription = new SecurityTokenDescriptor()
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {

                    new Claim(ClaimTypes.NameIdentifier, Email),
                     new Claim(ClaimTypes.Role,data.role)
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(30),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(KeyBytes), SecurityAlgorithms.HmacSha256)

                    };
                    var token = tokenHandler.CreateToken(tokenDescription);
                    return tokenHandler.WriteToken(token);

                }
                else
                {
                    return "Invalid Password";
                }

            }
            else
                return "Email Not Found";

           

        }
        public static string decryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] encryptpassword =Convert.FromBase64String(password);
                string decryptedPassword=ASCIIEncoding.ASCII.GetString(encryptpassword);
                return decryptedPassword;
            }
        }
    }
