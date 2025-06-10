using Azure.Core;
using GamePal.Data.Entities;
using GamePal.Models.AuthContracts;
using GamePal.Repositories.AuthProviderRepo;
using GamePal.Repositories.UserAuthProviders;
using LadleMeThis.Services.TokenService;
using Microsoft.AspNetCore.Identity;

namespace GamePal.Services.UserServices
{
    public class UserService : IUserService
    {
        UserManager<User> _userManager;
        RoleManager<IdentityRole> _roleManager;
        ITokenService _tokenService;
        IUserAuthProviderRepo _userAuthProviderRepo;
        IAuthProviderRepo _authProviderRepo;
        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService, IUserAuthProviderRepo userAuthProviderRepo, IAuthProviderRepo authProviderRepo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _userAuthProviderRepo = userAuthProviderRepo;
            _authProviderRepo = authProviderRepo;
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
                return new AuthResult("", false, request.Email, request.Username, "")
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
            return new AuthResult(user.Id, true, request.Email, request.Username, "");
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

            return new AuthResult(user.Id, true, user.Email, user.UserName, accessToken);
        }

        public async Task<AuthResult> UpsertUserAsync(ExternalAuthRequest extAuthReq)
        {
            string userProviderUsername = extAuthReq.Name;
            string userProviderId = extAuthReq.ProviderAccountId;
            string userProviderName = extAuthReq.ProviderName;
            string? userEmail = extAuthReq.Email;
            User? userByEmail = null;

            // 1. Try finding the user by external provider ID
            var userByProvider = await _userAuthProviderRepo.GetUserByProviderIdAsync(userProviderId, userProviderName);

            if (userByProvider != null)
            {
                var token2 = await _tokenService.CreateToken(userByProvider);

                return new AuthResult(userByProvider.Id, true, userEmail, userProviderUsername, token2);
            }

            // 2. Try finding the user by email
            if (userEmail != null)
            {
                userByEmail = await _userManager.FindByEmailAsync(userEmail);
            }

            if (userByEmail == null)
            {
                // 3. Create a new user
                var newUser = new User
                {
                    Email = extAuthReq.Email,
                    UserName = extAuthReq.Name,
                };

                var result = await _userManager.CreateAsync(newUser);

                if (!result.Succeeded)
                {
                    return FailedRegistration(result, userEmail, userProviderUsername);
                }

                userByEmail = newUser;
            }

            // 4. Link external provider to user if not already linked
            var allProviders = await _userAuthProviderRepo.GetAllAsync();
            var existingLink = allProviders.Any(p =>
                p.AuthProvider.Name == extAuthReq.ProviderName &&
                p.ProviderUserId == extAuthReq.ProviderAccountId);


            if (!existingLink)
            {
                var authProvider = new AuthProvider() { Name = extAuthReq.ProviderName };
                authProvider = await _authProviderRepo.AddProviderAsync(authProvider);


                var userAuthProvider = new UserAuthProvider
                {
                    AuthProvider = authProvider,
                    ProviderUserId = extAuthReq.ProviderAccountId,
                    User = userByEmail
                };

                await _userAuthProviderRepo.AddUserAuthProviderAsync(userAuthProvider);
            }

            var token = await _tokenService.CreateToken(userByEmail);

            return new AuthResult(userByEmail.Id, true, userEmail, userProviderUsername, token);
        }



        private static AuthResult InvalidEmail(string email)
        {
            var result = new AuthResult("", false, email, "", "");
            result.ErrorMessages.Add("Bad credentials", "Invalid email/username");
            return result;
        }


        private static AuthResult InvalidPassword(string email, string userName)
        {
            var result = new AuthResult("", false, email, userName, "");
            result.ErrorMessages.Add("Bad credentials", "Invalid password");
            return result;
        }


        private static AuthResult FailedRegistration(IdentityResult result, string email, string username)
        {
            var authResult = new AuthResult("", false, email, username, "");

            foreach (var error in result.Errors)
            {
                authResult.ErrorMessages.Add(error.Code, error.Description);
            }

            return authResult;
        }


    }
}
