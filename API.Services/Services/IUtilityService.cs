namespace API.Services.Services
{
    public interface IUtilityService
    {
        bool CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);

        string GenerateJWT(string UserID, string Secret);
    }
}
