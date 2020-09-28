using FishBuy.Application.Interfaces;
using FishBuy.Application.ViewModels.ProductModels;

namespace Sesc.IAD.Application.Interfaces
{
    public interface IProductAppService
    {
        ApplicationServiceResponse GetAllProducts();
        ApplicationServiceResponse UpdateProduct(ProductViewModel model);
        ApplicationServiceResponse AddProduct(ProductViewModel model);
        ApplicationServiceResponse DeleteProduct(int id);
        ApplicationServiceResponse UploadFile();
    }
}
