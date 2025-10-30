using FirebaseAdmin.Auth;
using Library.Application.Interfaces;

namespace Library.Application.Services;

public class FirebaseAuthService : IFirebaseAuthService
{
    public async Task<string> CreateUserAsync(string email, string password, string displayName)
    {
        try
        {
            var userArgs = new UserRecordArgs
            {
                Email = email,
                Password = password,
                DisplayName = displayName,
                EmailVerified = false
            };

            var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);
            return userRecord.Uid;
        }
        catch (FirebaseAuthException ex)
        {
            throw new Exception($"Erro ao criar usuário: {ex.Message}", ex);
        }
    }

    public async Task<string> VerifyIdTokenAsync(string idToken)
    {
        try
        {
            var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
            return decodedToken.Uid;
        }
        catch (FirebaseAuthException ex)
        {
            throw new Exception($"Token inválido: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteUserAsync(string uid)
    {
        try
        {
            await FirebaseAuth.DefaultInstance.DeleteUserAsync(uid);
            return true;
        }
        catch (FirebaseAuthException ex)
        {
            throw new Exception($"Erro ao deletar usuário: {ex.Message}", ex);
        }
    }

    public async Task<string> GeneratePasswordResetLinkAsync(string email)
    {
        try
        {
            var link = await FirebaseAuth.DefaultInstance.GeneratePasswordResetLinkAsync(email);
            return link;
        }
        catch (FirebaseAuthException ex)
        {
            throw new Exception($"Erro ao gerar link de recuperação: {ex.Message}", ex);
        }
    }

    public async Task<bool> UpdateUserAsync(string uid, string? email = null, string? displayName = null, string? password = null)
    {
        try
        {
            var userArgs = new UserRecordArgs
            {
                Uid = uid
            };

            if (!string.IsNullOrEmpty(email))
                userArgs.Email = email;
            
            if (!string.IsNullOrEmpty(displayName))
                userArgs.DisplayName = displayName;
            
            if (!string.IsNullOrEmpty(password))
                userArgs.Password = password;

            await FirebaseAuth.DefaultInstance.UpdateUserAsync(userArgs);
            return true;
        }
        catch (FirebaseAuthException ex)
        {
            throw new Exception($"Erro ao atualizar usuário: {ex.Message}", ex);
        }
    }
}
