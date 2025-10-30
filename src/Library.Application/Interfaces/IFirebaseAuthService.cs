namespace Library.Application.Interfaces;

public interface IFirebaseAuthService
{
    Task<string> CreateUserAsync(string email, string password, string displayName);
    Task<string> VerifyIdTokenAsync(string idToken);
    Task<bool> DeleteUserAsync(string uid);
    Task<string> GeneratePasswordResetLinkAsync(string email);
    Task<bool> UpdateUserAsync(string uid, string? email = null, string? displayName = null, string? password = null);
}
