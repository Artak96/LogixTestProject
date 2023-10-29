using BLL.Services;
using Core.Abstractions.IServices;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LogixTestProject.Controllers
{
    [Route("api/Account")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        private IClassService _classService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger, IClassService classService)
        {
            _accountService = accountService;
            _logger = logger;
            _classService = classService;   
        }

        [HttpPut("UpdateUser")]
        public async Task<JsonResult> UpdateUser([FromBody] UserUpdateModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountService.UpdateUserAsync(model);

                    return new JsonResult(JsonSerializer.Serialize(new { Message = result.Message, Succes = result.Success }));
                }
                return new JsonResult(JsonSerializer.Serialize(new { Message = "Model is not valid", Succes = false }));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Account controller Update -> Login -> exaption -> {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("RemoveUser")]
        public async Task<JsonResult> RemoveUser(int id)
        {
            try
            {
                var result = await _accountService.RemoveUserAsync(id);

                return new JsonResult(JsonSerializer.Serialize(new { Message = result.Message, Succes = result.Success }));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Accoutn controller Remove User -> Login -> exaption -> {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> GetAllUsers()
        {
            try
            {
                var results = await _accountService.GetAllUsersAsync();

                return new JsonResult(results);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Account controller  Get All Users -> exaption -> {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("AddClassForUser")]
        public async Task<JsonResult> AddClassForUser([FromBody] ClassUserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountService.AddClassForUserAsync(model);
                    return new JsonResult(result);
                }
                return new JsonResult(JsonSerializer.Serialize(new { Message = "Model is not valid", Succes = false }));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Account controller AddClassForUser -> exaption -> {ex.Message}");
                throw new Exception(ex.Message);
            }

        }

        [HttpPut("UpdateClass")]
        public async Task<JsonResult> UpdateClass([FromBody] ClassModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _classService.UpdateClass(model);

                    return new JsonResult(JsonSerializer.Serialize(new { Message = result.Message, Succes = result.Success }));
                }
                return new JsonResult(JsonSerializer.Serialize(new { Message = "Model is not valid", Succes = false }));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Account controller Update Class -> exaption -> {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("AddClass")]
        public async Task<JsonResult> AddClass([FromBody] ClassModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _classService.AddClass(model);
                    return new JsonResult(JsonSerializer.Serialize(new { Message = result.Message, Succes = result.Success }));
                }
                return new JsonResult(JsonSerializer.Serialize(new { Message = "Model is not valid", Succes = false }));

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Account controller AddClass -> exaption -> {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("RemoveClass")]
        [Authorize]
        public async Task<JsonResult> RemoveClass(int id)
        {
            try
            {
                var result = await _classService.RemoveClass(id);

                return new JsonResult(JsonSerializer.Serialize(new { Message = result.Message, Succes = result.Success }));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Accoutn controller Remove Class -> Login -> exaption -> {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

    }
}
