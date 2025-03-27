using FamApp.Interfaces;
using FamApp.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;

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
    }
}
