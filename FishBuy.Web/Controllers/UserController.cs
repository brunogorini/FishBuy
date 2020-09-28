using FishBuy.Application.ViewModels.UserModels;
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
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;

        }


        [AllowAnonymous]
        [HttpPost("authentication")]
        public IActionResult AuthenticateUser([FromBody] AuthenticateViewModel model)
        {
            try
            {
                var response = _userAppService.AuthenticateUser(model);
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
                        ex.Message.ToString()
                    };
                return BadRequest(error);
            }
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public IActionResult RegisterUser([FromBody] RegisterViewModel model)
        {

            try
            {
                var response = _userAppService.RegisterUser(model);
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
                            ex.Message.ToString()
                        };
                return BadRequest(error);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllUsers()
        {

            try
            {
                var response = _userAppService.GetAllUsers();
                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }

        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UpdateViewModel model)
        {
            try
            {
                var response = _userAppService.UpdateUser(model);
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
                        ex.Message.ToString()
                    };
                return BadRequest(error);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var response = _userAppService.DeleteUser(id);
                return Ok(response.Result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

    }
}
