using FamApp.Areas.Identity.Data;
using FamApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FamApp.Interfaces
{
    public interface IUserService
    {
        Task<bool> UpdateUserAsync(UpdatePersonalDataViewModel model);
        Task<ApplicationUser?> ChangePasswordAsync(ChangePasswordViewModel model);
        Task<ApplicationUser?> GetCurrentUserAsync();
        Task RefreshSignInAsync(ApplicationUser user);
        Task<string> GetUserIdAsync(ApplicationUser? user);
        Task<List<SelectListItem>> GetUserSelectListAsync();
        Task<string> GetUserNickByIdAsync(string userId);
    }
}
