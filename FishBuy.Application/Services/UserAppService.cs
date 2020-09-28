using AutoMapper;
using FishBuy.Application.Interfaces;
using FishBuy.Application.ViewModels.UserModels;
using FishBuy.Domain.Contracts;
using FishBuy.Domain.Entities;
using Sesc.IAD.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FishBuy.Application.Services
{
    public class UserAppService : ApplicationService, IUserAppService
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IMapper _mapper;

        public UserAppService(IUserBusiness userBusiness, IMapper mapper)
        {
            _userBusiness = userBusiness;
            _mapper = mapper;
        }

        public ApplicationServiceResponse AuthenticateUser(AuthenticateViewModel model)
        {
            model.Validate();
            if (!model.IsValid)
            {
                return BadRequest(model.GetValidationMessagesList());
            }

            var user = _userBusiness.AuthenticateUser(model.Email, model.Password);
            if (user == null)
            {
                return BadRequest(new List<string> { "Email or password is incorrect" });
            }
            var tokenString = _userBusiness.GenerateToken(user);

            var authenticatedUser = new AuthenticatedUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Administrator = user.Administrator,
                Token = tokenString
            };

            return Ok(authenticatedUser);
        }

        public ApplicationServiceResponse RegisterUser(RegisterViewModel model)
        {
            model.Validate();
            if (!model.IsValid)
            {
                return BadRequest(model.GetValidationMessagesList());
            }
            var user = _mapper.Map<User>(model);
            var messages = _userBusiness.CreateUser(user, model.Password);
            if (messages != null)
            {
                return BadRequest(messages);
            }
            return Ok();
        }

        public ApplicationServiceResponse GetAllUsers()
        {
            var users = _userBusiness.GetAllUsers().ToList();
            var model = _mapper.Map<IList<UserViewModel>>(users);
            return Ok(model);
        }

        public ApplicationServiceResponse UpdateUser(UpdateViewModel model)
        {
            model.Validate();
            if (!model.IsValid)
            {
                return BadRequest(model.GetValidationMessagesList());
            }
            var user = _mapper.Map<User>(model);

            _userBusiness.UpdateUser(user, model.Password);
            return Ok();
        }

        public ApplicationServiceResponse DeleteUser(int id)
        {
            _userBusiness.DeleteUser(id);
            var users = _userBusiness.GetAllUsers().ToList();
            var model = _mapper.Map<IList<UserViewModel>>(users);
            return Ok(model);
        }
    }
}

