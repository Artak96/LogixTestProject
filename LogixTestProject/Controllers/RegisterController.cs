using Core.Abstractions.IServices;
using Core.Entities;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace LogixTestProject.Controllers
{
    [Route("api/Register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private IRegistrationService _registrationService;
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(IRegistrationService registrationService, ILogger<RegisterController> logger)
        {
            _registrationService = registrationService;
            _logger = logger;
        }


        [HttpPost("register")]
        public async Task<JsonResult> Register([FromBody] RegistrationModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _registrationService.Register(model);

                    return new JsonResult(JsonSerializer.Serialize(new { Message = result.Message, Succes = result.Success }));
                }

                return new JsonResult(JsonSerializer.Serialize(new { Message = "Model state is not valid", Succes = false }));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Register controller -> Register -> exaption -> {ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
