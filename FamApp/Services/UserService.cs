using FamApp.Areas.Identity.Data;
using FamApp.Interfaces;
using FamApp.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FamApp.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UpdatePersonalDataViewModel> GetUserForUpdateAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                return null;

            return new UpdatePersonalDataViewModel
            {
                Id = user.Id,
                Nick = user.Nick,
                Color = user.Color
            };
        }

        public async Task<bool> UpdateUserAsync(UpdatePersonalDataViewModel model)
        {
            var user = await _userRepository.GetUserByIdAsync(model.Id);
            if (user == null)
                return false;

            user.Nick = model.Nick;
            user.Color = model.Color;
            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<ApplicationUser?> GetCurrentUserAsync()
        {
            return await _userRepository.GetCurrentUserAsync();
        }

        public async Task<ApplicationUser?> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
                return null;

            var result = await _userRepository.ChangePasswordAsync(user, model);
            return result.Succeeded ? user : null;
        }

        public async Task RefreshSignInAsync(ApplicationUser user)
        {
            await _userRepository.RefreshSignInAsync(user);
        }

        public async Task<string> GetUserIdAsync(ApplicationUser? user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null");

            return await _userRepository.GetUserIdAsync(user);
        }

        public async Task<List<SelectListItem>> GetUserSelectListAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(u => new SelectListItem { Value = u.Id, Text = u.Nick}).ToList();
        }

        public async Task<string> GetUserNickByIdAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            return user.Nick;
        }

    }
}
