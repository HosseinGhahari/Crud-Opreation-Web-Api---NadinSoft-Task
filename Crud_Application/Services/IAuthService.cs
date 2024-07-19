using Crud_Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Crud_Application.Services
{
    // This interface defines the contract for the authentication service,
    // including methods for user registration, login, and token generation.
    public interface IAuthService
    {
        Task<bool> RegisterUser(LoginUser user);
        Task<bool> Login(LoginUser user);
        string GenerateTokenString(LoginUser user);
    }
}