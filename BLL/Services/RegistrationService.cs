using Core.Abstractions;
using Core.Abstractions.IServices;
using Core.Entities;
using Core.Helpers;
using Core.Models;
using Core.Roles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RegistrationService> _logger;

        public RegistrationService(IUnitOfWork unitOfWork, ILogger<RegistrationService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<BaseResult> Register(RegistrationModel model)
        {
            try
            {

                var userName = await _unitOfWork.User.GetSingleAsync(m => m.UserName == model.UserName);
                var email = await _unitOfWork.User.GetSingleAsync(m => m.Email == model.Email);
                var phoneNumber = await _unitOfWork.User.GetSingleAsync(m => m.PhoneNumber == model.PhoneNumber);

                if (userName != null)
                {
                    return new BaseResult
                    {
                        Message = "User name already exist!",
                        Success = false
                    };
                }
                else if (email != null)
                {
                    return new BaseResult
                    {
                        Message = "Email already exist!",
                        Success = false
                    };
                }
                else if (phoneNumber != null)
                {

                    return new BaseResult
                    {
                        Message = "Phone Number name already exist!",
                        Success = false
                    };
                }

                string[] addressArray = model.Address.Replace(".", " ").Split(' ');

                for (int i = 0; i < addressArray.Length; i++)
                {
                    switch (addressArray[i].ToLower())
                    {
                        case "apartment":
                            addressArray[i] = "APTS";
                            break;
                        case "avenue":
                            addressArray[i] = "AVES";
                            break;
                        case "road":
                            addressArray[i] = "RDS";
                            break;
                        case "street":
                            addressArray[i] = "STS";
                            break;
                        case "boulevardeet":
                            addressArray[i] = "BLVD";
                            break;
                        default:
                            break;
                    }
                }

                string Address = string.Join(" ", addressArray);

                User user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.UserName,
                    PhoneNumber = model.PhoneNumber,
                    Password = PasswordHash.HashPassword(model.Password),
                    Address = Address,
                    DateOfBirth = model.DateOfBirth,
                    Role = Roles.User.ToString(),

                };

                await _unitOfWork.User.AddAsync(user);
                await _unitOfWork.User.SaveChangeAsync();

                return new BaseResult
                {
                    Message = "Congratulation you are succesfully registr",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Registr service -> exaption -> {ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }

}
