using FishBuy.Application.ViewModels.ProductModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sesc.IAD.Application.Interfaces;
using System;
using System.Collections.Generic;

namespace FishBuy.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductAppService _productAppService;


        public ProductController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var response = _productAppService.GetAllProducts();
                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductViewModel model)
        {
            try
            {
                var response = _productAppService.AddProduct(model);

                if (response.StatusCode != StatusCodes.Status200OK)
                {
                    return BadRequest(response.Messages);
                }

                return Created("api/product", response.Result);
            }
            catch (Exception ex)
            {
                var error = new List<string>
                    {
                        ex.ToString()
                    };
                return BadRequest(error);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult UpdateProduct([FromBody] ProductViewModel model)
        {
            try
            {
                var response = _productAppService.UpdateProduct(model);

                if (response.StatusCode != StatusCodes.Status200OK)
                {
                    return BadRequest(response.Messages);
                }
                return Ok(response.Result);

            }
            catch (Exception ex)
            {
                var error = new List<string>
                    {
                        ex.ToString()
                    };
                return BadRequest(error);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var response = _productAppService.DeleteProduct(id);
                return Ok(response.Result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("file-upload")]
        public IActionResult UploadFile()
        {
            try
            {
                var response = _productAppService.UploadFile();
                return Json(response.Result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}
