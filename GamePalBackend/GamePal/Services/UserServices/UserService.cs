using GamePal.Data.Entities;
using GamePal.Models.AuthContracts;
using LadleMeThis.Services.TokenService;
using Microsoft.AspNetCore.Identity;

namespace GamePal.Services.UserServices
{
    public class UserService : IUserService
    {
        UserManager<User> _userManager;
        RoleManager<IdentityRole> _roleManager;
        ITokenService _tokenService;
        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        public async Task<AuthResult> RegisterAsync(RegistrationRequest request, string role)
        {
            var user = new User
            {
                UserName = request.Username,
                Email = request.Email,
            };

            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                return new AuthResult(false, request.Email, request.Username, "")
                {
                    ErrorMessages = { { "RoleError", "The specified role does not exist." } }
                };
            }

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return FailedRegistration(result, request.Email, request.Username);
            }

            await _userManager.AddToRoleAsync(user, role);
            return new AuthResult(true, request.Email, request.Username, "");
        }

        public async Task<AuthResult> LoginAsync(AuthRequest authRequest)
        {
            var user = await _userManager.FindByEmailAsync(authRequest.EmailOrUsername) ??
                       await _userManager.FindByNameAsync(authRequest.EmailOrUsername);

            if (user == null)
            {
                return InvalidEmail(authRequest.EmailOrUsername);
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, authRequest.Password);
            if (!isPasswordValid)
            {
                return InvalidPassword(authRequest.EmailOrUsername, user.UserName);
            }


            var accessToken = await _tokenService.CreateToken(user);

            return new AuthResult(true, user.Email, user.UserName, accessToken);
        }



        private static AuthResult InvalidEmail(string email)
        {
            var result = new AuthResult(false, email, "", "");
            result.ErrorMessages.Add("Bad credentials", "Invalid email/username");
            return result;
        }


        private static AuthResult InvalidPassword(string email, string userName)
        {
            var result = new AuthResult(false, email, userName, "");
            result.ErrorMessages.Add("Bad credentials", "Invalid password");
            return result;
        }


        private static AuthResult FailedRegistration(IdentityResult result, string email, string username)
        {
            var authResult = new AuthResult(false, email, username, "");

            foreach (var error in result.Errors)
            {
                authResult.ErrorMessages.Add(error.Code, error.Description);
            }

            return authResult;
        }


    }
}
