using FamApp.Areas.Identity.Data;
using FamApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FamApp.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<bool> UpdateUserAsync(ApplicationUser user);
        Task<ApplicationUser?> GetCurrentUserAsync();
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, ChangePasswordViewModel model);
        Task RefreshSignInAsync(ApplicationUser user);
        Task<string> GetUserIdAsync(ApplicationUser user);
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<List<ApplicationUser>> GetUsersByIdsAsync(List<string> userIds);
    }
}
