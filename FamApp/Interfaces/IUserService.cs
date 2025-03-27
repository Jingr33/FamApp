using FamApp.ViewModels;

namespace FamApp.Interfaces
{
    public interface IUserService
    {
        Task<bool> UpdateUserAsync(UpdatePersonalDataViewModel model);
    }
}
