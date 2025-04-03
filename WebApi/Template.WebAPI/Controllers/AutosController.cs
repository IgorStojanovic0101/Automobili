using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Template.Application;
using Template.Application.Abstraction;
using Template.Application.DTOs.User;
using Template.Application.Results;
using Template.Application.Services;
using Template.Domain.DataModels;

namespace Template.WebAPI.Controllers
{
    [EnableCors]
    public class AutoController(ITemplateClient client) : ControllerBase
    {
        private readonly ITemplateClient _client = client;
       // private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [HttpGet]

        public async Task<ActionResult<List<Auto>>> GetAutos()
        {

            var result = await _client.Service.GetAllCars();


            return Ok(result);

        }


        [HttpGet]
        public async Task<ActionResult<List<int>>> GetAutoById(int id)
        {
            var result = await _client.Service.GetAutoById(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error); 
            }
           
            return Ok(result.Value);

        }

        [HttpPut]
        public async Task<ActionResult<List<int>>> UpdateAuto(Auto auto)
        {
            var result = await _client.Service.UpdateAuto(auto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);

        }




    }
}