using FishBuy.Domain.Entities;
using System.Collections.Generic;

namespace FishBuy.Domain.Contracts
{
    public interface IProductBusiness
    {
        IEnumerable<Product> GetAllProducts();
        Product UpdateProduct(Product product);
        void AddProduct(Product product);
        void DeleteProduct(int id);
        string UploadFile();
    }
}
