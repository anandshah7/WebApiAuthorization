using System.ComponentModel;

namespace WebAuth;

public class AuthenticateReq
{
    [DefaultValue("System")]
        public required string Username { get; set; }

        [DefaultValue("System")]
        public required string Password { get; set; }

}
