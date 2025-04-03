using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Template.Application;
using Template.Application.Abstraction;
using Template.Application.DTOs.User;
using Template.Application.Results;
using Template.Application.Services;

namespace Template.WebAPI.Controllers
{
    [EnableCors]
    public class AutoController(ITemplateClient client) : ControllerBase
    {
        private readonly ITemplateClient _client = client;
       // private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [HttpGet]

        public async Task<ActionResult<ServiceResponse<List<User_ResponseDTO>>>> GetAutoUsers()
        {


            ServiceResponse<List<User_ResponseDTO>> response = new();

            response.Payload = await _client.Service.GetAllCars();

            return Ok(response);

        }


        [HttpGet]
        public async Task<ActionResult<Result<List<int>>>> GetAutoById(int id)
        {
            var result = await _client.Service.GetUserById(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error); 
            }
           
            return Ok(result);

        }

      
      
    }
}