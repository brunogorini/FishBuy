using FishBuy.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace FishBuy.Application.Services
{
    public class ApplicationService
    {
        public ApplicationServiceResponse Ok()
        {
            return new ApplicationServiceResponse()
            {
                Result = new object(),
                StatusCode = StatusCodes.Status200OK,
                Messages = new List<string>(),
            };
        }

        public ApplicationServiceResponse Ok(object obj)
        {
            return new ApplicationServiceResponse()
            {
                Result = obj,
                StatusCode = StatusCodes.Status200OK,
                Messages = new List<string>(),
            };
        }

        public ApplicationServiceResponse Ok(object obj, List<string> messages)
        {
            return new ApplicationServiceResponse()
            {
                Result = obj,
                StatusCode = StatusCodes.Status200OK,
                Messages = messages,
            };
        }

        public ApplicationServiceResponse BadRequest(List<string> messages)
        {

            return new ApplicationServiceResponse()
            {
                Result = null,
                StatusCode = StatusCodes.Status400BadRequest,
                Messages = messages,
            };
        }
    }

}
