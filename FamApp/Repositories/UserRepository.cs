using FamApp.Areas.Identity.Data;
using FamApp.Data;
using FamApp.Interfaces;
using FamApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FamApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(ApplicationDbContext db, 
                              UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<bool> UpdateUserAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<ApplicationUser?> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, ChangePasswordViewModel model)
        {
            return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }

        public async Task RefreshSignInAsync(ApplicationUser user)
        {
            await _signInManager.RefreshSignInAsync(user);
        }

        public async Task<string> GetUserIdAsync(ApplicationUser user)
        {
            return await _userManager.GetUserIdAsync(user);
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetUsersByIdsAsync(List<string> userIds)
        {
            return await _db.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();
        }
    }
}
