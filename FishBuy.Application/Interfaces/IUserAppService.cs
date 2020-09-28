using FishBuy.Application.Interfaces;
using FishBuy.Application.ViewModels.UserModels;

namespace Sesc.IAD.Application.Interfaces
{
    public interface IUserAppService
    {
        ApplicationServiceResponse AuthenticateUser(AuthenticateViewModel model);
        ApplicationServiceResponse RegisterUser(RegisterViewModel model);
        ApplicationServiceResponse GetAllUsers();
        ApplicationServiceResponse DeleteUser(int id);
        ApplicationServiceResponse UpdateUser(UpdateViewModel model);
    }
}
