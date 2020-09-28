using FishBuy.Domain.Contracts;
using FishBuy.Domain.Entities;
using FishBuy.Domain.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FishBuy.Domain.Business
{
    public class ProductBusiness : IProductBusiness
    {

        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductBusiness(
            IProductRepository productRepository,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor)

        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;

        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        public Product UpdateProduct(Product product)
        {

            var registeredProduct = _productRepository.GetById(product.Id);
            if (registeredProduct == null)
                throw new Exception("Product was not found");

            if (!string.IsNullOrWhiteSpace(registeredProduct.FileName) && !string.IsNullOrWhiteSpace(product.FileName))
            {
                var fileName = registeredProduct.FileName;
                var filesFolder = _webHostEnvironment.WebRootPath + "\\files\\";
                var path = filesFolder + fileName;
                System.IO.File.Delete(path);
            }

            registeredProduct.Name = product.Name;
            registeredProduct.Description = product.Description;
            registeredProduct.Price = product.Price;
            if (!string.IsNullOrWhiteSpace(product.FileName))
                registeredProduct.FileName = product.FileName;

            _productRepository.Update(registeredProduct);

            return product;
        }

        public void AddProduct(Product product)
        {
            _productRepository.Add(product);

        }

        public void DeleteProduct(int id)
        {
            var product = _productRepository.GetById(id);
            if (product != null)
            {
                _productRepository.Delete(product);

                if (product.FileName != null)
                {
                    var fileName = product.FileName;
                    var filesFolder = _webHostEnvironment.WebRootPath + "\\files\\";
                    var path = filesFolder + fileName;
                    System.IO.File.Delete(path);
                }
            }

        }

        public string UploadFile()
        {
            var formFile = _httpContextAccessor.HttpContext.Request.Form.Files["FileToUpload"];
            var fileName = formFile.FileName;
            var extension = fileName.Split(".").Last();
            string newFileName = CreateNewFileName(ref fileName, extension);
            var FilesFolder = _webHostEnvironment.WebRootPath + "\\files\\";
            var path = FilesFolder + newFileName;

            using (var streamArquivo = new FileStream(path, FileMode.Create))
            {
                formFile.CopyTo(streamArquivo);
            }
            return newFileName;
        }

        private static string CreateNewFileName(ref string fileName, string extension)
        {
            var arrayCompactName = Path.GetFileNameWithoutExtension(fileName).Take(10).ToArray();
            var newFileName = new string(arrayCompactName).Replace(" ", "-");
            newFileName = $"{newFileName}_{DateTime.Now.Year}" +
                          $"{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}" +
                          $"{DateTime.Now.Minute}{DateTime.Now.Second}.{extension}";
            return newFileName;
        }
    }

}
