using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAuth.Helpers;

namespace WebAuth.Helpers
{
public class JWTMiddleware
{
    private readonly  RequestDelegate _request;
    private readonly Appsettings _appsettings;

    private readonly IUserInterface _userService;
    public JWTMiddleware(RequestDelegate next, IOptions<Appsettings> appSettings , IUserInterface userService)
        {
            _request = next;
            _appsettings = appSettings.Value;
            _userService = userService;
        }

       public async Task Invoke(HttpContext context, IUserInterface userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await attachUserToContext(context, userService, token);

            await _request(context);
        }  


        private async Task attachUserToContext(HttpContext context, IUserInterface userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appsettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clock skew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = new Guid(jwtToken.Claims.First(x => x.Type == "id").Value);

                //Attach user to context on successful JWT validation
                context.Items["User"] = await userService.GetUserByUser_Id(userId);
            }
            catch
            {
                //Do nothing if JWT validation fails
                // user is not attached to context so the request won't have access to secure routes
            }
        }

}
}
