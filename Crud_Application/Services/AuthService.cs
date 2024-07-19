using Crud_Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Crud_Application.Services
{
    public class AuthService : IAuthService
    {
        // This code initializes instances of UserManager and
        // IConfiguration in the AuthService's constructor.
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        public AuthService(UserManager<IdentityUser> userManager , IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        // This async method 'RegisterUser' creates a new IdentityUser with
        // the username and email from the provided 'LoginUser'. It then uses
        // the UserManager to create the user asynchronously with the provided
        // password. The method returns true if the user creation succeeded, otherwise false.
        public async Task<bool> RegisterUser(LoginUser user)
        {
            var identityUser = new IdentityUser
            {
                UserName = user.UserName,
                Email = user.UserName
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);

            return result.Succeeded;
        }

        // This async method 'Login' first finds the user by their email using
        // the UserManager. If the user is not found, it returns false. If the
        // user is found, it checks if the provided password is correct for the
        // user using the UserManager's CheckPasswordAsync method. The method
        // returns true if the password is correct, otherwise false
        public async Task<bool> Login(LoginUser user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.UserName);
            if(identityUser == null)
                return false;

            return await _userManager.CheckPasswordAsync(identityUser,user.Password);
        }

        // This method 'GenerateTokenString' creates a JWT for the user.
        // It sets up claims, creates a security key from the config, and
        // establishes signing credentials. A JWT is then created with these
        // details, an expiration time of 60 minutes, and issuer and audience
        // from the config. The JWT is written to a string and returned.
        public string GenerateTokenString(LoginUser user)
        {
            IdentityUser identity = new IdentityUser();
            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,identity.Id),
                new Claim(ClaimTypes.Email,user.UserName),
                new Claim(ClaimTypes.Role,"Admin"),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (_config.GetSection("Jwt:Key").Value));

            var singingcred = new SigningCredentials
                (securityKey, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken
            (
                claims : claims,
                expires : DateTime.Now.AddMinutes(60),
                issuer : _config.GetSection("Jwt:Issuer").Value,
                audience : _config.GetSection("Jwt:Audience").Value,
                signingCredentials : singingcred
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }
    }
}
