namespace WebAuth;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAuth.Entity;
using WebAuth.Helpers;

public class UsersServices : IUserInterface
{
    private readonly WebAuthdbContext _db;
    private readonly Appsettings _appsettings;
    public UsersServices(WebAuthdbContext db, IOptions<Appsettings> appsettings)
    {
        _db = db;
        _appsettings = appsettings.Value;
    }
        public async Task<Users> GetUserByUser_Id(Guid id)
        {
            return await _db.Users.FirstOrDefaultAsync(x=>x.User_Id == id);
        }

         public async Task<AuthenticateResponse?> Authenticate(AuthenticateReq model)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.UserName == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = await generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }
        private async Task<string> generateJwtToken(Users user)
        {
            //Generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = await Task.Run(() =>
            {

                var key = Encoding.ASCII.GetBytes(_appsettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.User_Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                return tokenHandler.CreateToken(tokenDescriptor);
            });

            return tokenHandler.WriteToken(token);
        }
}
