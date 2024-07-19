using Crud_Application.Services;
using Crud_Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crud_Project.Controllers
{
    public class AuthController : Controller
    {
        // This code initializes an instance of IAuthService
        // in the AuthController's constructor.
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        // This HTTP POST method registers a user. If successful,
        // it returns a 200 OK status, otherwise a 400 Bad Request.
        [HttpPost]
        [Route("Register User")]
        public async Task<IActionResult> Register(LoginUser user)
        {
            if (await _authService.RegisterUser(user))
                return Ok("Register is successfuly done");

            return BadRequest();
           
        }

        // This HTTP POST method logs in a user. If successful,
        // it generates a token and returns it with a 200 OK status.
        // If the model state is invalid or login fails, it returns a 400 Bad Request.
        [HttpPost]
        [Route("Login User")]
        public async Task<IActionResult> Login(LoginUser user)
        {
            if(!ModelState.IsValid)
                return BadRequest();    

            if (await _authService.Login(user))
            {
                var tokenString = _authService.GenerateTokenString(user);
                return Ok(tokenString);
            }
            return BadRequest();
        }

    }
}
