namespace WebAuth;

public interface IUserInterface
{
    Task<AuthenticateResponse?> Authenticate(AuthenticateReq model);
     Task<Users> GetUserByUser_Id(Guid user_id);

    
}
