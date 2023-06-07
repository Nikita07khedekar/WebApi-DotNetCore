using Microsoft.AspNetCore.Identity;
using SharedProject;

namespace WebApi.Services
{
    public interface IUserService
    {
        Task<UserManageResponse> RegisterUserAsync(RegisterViewModel model);
    }

    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;
        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<UserManageResponse> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Register Model is Null!!");

            if (model.Password != model.ConfirmPassword)
                return new UserManageResponse
                {
                    Message = "Confirm Password must same as Password !!",
                    IsSuccess = false
                };

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                return new UserManageResponse
                {
                    IsSuccess = true,
                    Message = "User created succesfully!!",

                };
            }
            return new UserManageResponse
            {
                Message = "User not created!!",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)

            };
        }
    }
}


