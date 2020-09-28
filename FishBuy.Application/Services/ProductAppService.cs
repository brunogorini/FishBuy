using AutoMapper;
using FishBuy.Application.Interfaces;
using FishBuy.Application.ViewModels.ProductModels;
using FishBuy.Domain.Contracts;
using FishBuy.Domain.Entities;
using Sesc.IAD.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FishBuy.Application.Services
{
    public class ProductAppService : ApplicationService, IProductAppService
    {
        private readonly IProductBusiness _productBusiness;
        private readonly IMapper _mapper;

        public ProductAppService(IProductBusiness productBusiness, IMapper mapper)
        {
            _productBusiness = productBusiness;
            _mapper = mapper;
        }

        public ApplicationServiceResponse GetAllProducts()
        {
            var productsList = _productBusiness.GetAllProducts().ToList();
            return Ok(productsList);
        }

        public ApplicationServiceResponse UpdateProduct(ProductViewModel model)
        {
            model.Validate();
            if (!model.IsValid)
            {
                return BadRequest(model.GetValidationMessagesList());
            }
            var product = _mapper.Map<Product>(model);

            try
            {
                var updatedProduct = _productBusiness.UpdateProduct(product);
                return Ok(_mapper.Map<ProductViewModel>(updatedProduct));
            }
            catch (Exception ex)
            {
                var error = new List<string>
                    {
                        ex.Message.ToString()
                    };
                return BadRequest(error);
            }

        }

        public ApplicationServiceResponse AddProduct(ProductViewModel model)
        {
            model.Validate();
            if (!model.IsValid)
            {
                return BadRequest(model.GetValidationMessagesList());
            }

            var product = _mapper.Map<Product>(model);
            _productBusiness.AddProduct(product);

            return Ok(product);
        }

        public ApplicationServiceResponse DeleteProduct(int id)
        {
            _productBusiness.DeleteProduct(id);
            var products = _productBusiness.GetAllProducts().ToList();
            var model = _mapper.Map<IList<ProductViewModel>>(products);
            return Ok(model);

        }

        public ApplicationServiceResponse UploadFile()
        {
            var newFileName = _productBusiness.UploadFile();
            return Ok(newFileName);
        }
    }
}
