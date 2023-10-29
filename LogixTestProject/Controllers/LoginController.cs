using Core.Abstractions.IServices;
using Core.Models;
using LogixTestProject.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace LogixTestProject.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;
        private ILoginService _loginService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IConfiguration configuration, ILoginService loginService, ILogger<LoginController> logger)
        {
            _configuration = configuration;
            _loginService = loginService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<JsonResult> Login([FromBody] LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _loginService.Login(model.UserName, model.Password);
                    if (result.Success)
                    {
                        var token = BerareTokem.TokenGeneratorWithClaims(result.UserInfo, _configuration);
                        var jsonSuccessResult = JsonSerializer.Serialize(new { Token = token, Message = result.Message, Succes = result.Success });

                        return new JsonResult(jsonSuccessResult);

                    }
                    var jsonErrorResult = JsonSerializer.Serialize(new { Message = result.Message, Succes = result.Success });
                    return new JsonResult(jsonErrorResult);
                }
                return new JsonResult(JsonSerializer.Serialize(new { Message = "Model state is not valid", Succes = false }));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Login controller -> Login -> exaption -> {ex.Message}");
                throw new Exception(ex.Message);
            }

        }
    }
}
