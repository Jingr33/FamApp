using FamApp.Areas.Identity.Data;
using FamApp.ViewModels;

namespace FamApp.Interfaces
{
    public interface IUserService
    {
        Task<bool> UpdateUserAsync(UpdatePersonalDataViewModel model);
        Task<ApplicationUser?> ChangePasswordAsync(ChangePasswordViewModel model);
        Task<ApplicationUser?> GetCurrentUserAsync();
        Task RefreshSignInAsync(ApplicationUser user);
    }
}
