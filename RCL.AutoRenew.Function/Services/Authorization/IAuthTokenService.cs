namespace RCL.AutoRenew.Function
{
    public interface IAuthTokenService
    {
        Task<AuthToken> GetAuthTokenAsync(string resource);
    }
}
