using FishBuy.Application.ViewModels.OrderModels;
using FishBuy.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sesc.IAD.Application.Interfaces;
using System;

namespace FishBuy.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderAppService _orderAppService;


        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            try
            {
                var response = _orderAppService.GetAllOrders();
                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetOrdersByUserId(int id)
        {
            try
            {
                var response = _orderAppService.GetOrdersByUserId(id);
                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpPost]
        public ActionResult PlaceOrder([FromBody] OrderViewModel model)
        {
            try
            {
                var response = _orderAppService.PlaceOrder(model);
                if (response.StatusCode != StatusCodes.Status200OK)
                {
                    return BadRequest(response.Messages);
                }

                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateOrderStatus(int id, [FromBody] StatusEnum orderStatus)
        {
            try
            {
                var response = _orderAppService.UpdateOrderStatus(id, orderStatus);
                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
