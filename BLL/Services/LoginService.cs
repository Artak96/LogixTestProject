using Core.Abstractions;
using Core.Abstractions.IServices;
using Core.Helpers;
using Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LoginService> _logger;

        public LoginService(IUnitOfWork unitOfWork, ILogger<LoginService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task<BaseResult> Login(string userName, string password)
        {
            try
            {
                var username = await _unitOfWork.User.GetSingleAsync(m => m.UserName == userName);

                if (username != null)
                {
                    var user = await _unitOfWork.User.GetSingleAsync(m => m.Password == PasswordHash.HashPassword(password));

                    if (user != null)
                    {
                        return  new BaseResult
                        {
                            Success = true,
                            Message = "Congratulation you are successful LOG IN",
                            UserInfo = user
                        };
                    }
                    else
                    {
                        _logger.LogInformation("Login service -> Incorrect password");
                        return new BaseResult
                        {
                            Success = false,
                            Message = "Incorrect password"
                        };
                    }
                }
                else
                {
                    _logger.LogInformation("Login service -> Incorrect user name");
                    return new BaseResult
                    {
                        Message = "Incorrect UserName",
                        Success = false
                    };

                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Login service -> exaption -> {ex.Message}");
                throw new Exception(ex.Message);
            }

        }

     
       
    }
}
