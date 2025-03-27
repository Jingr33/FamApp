using FamApp.Areas.Identity.Data;

namespace FamApp.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<bool> UpdateUserAsync(ApplicationUser user);
    }
}
