using Core.Abstractions;
using Core.Abstractions.IServices;
using Core.Entities;
using Core.Helpers;
using Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IUnitOfWork unitOfWork, ILogger<AccountService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<BaseResult> AddClassForUserAsync(ClassUserModel model)
        {
            try
            {
                var result = await _unitOfWork.UserClass.GetSingleAsync(m => m.ClassId == model.ClassId && m.UserId == model.UserId);

                if (result != null)
                {
                    return new BaseResult
                    {
                        Message = "User already have this class",
                        Success = false
                    };
                }

                UserClass userClass = new UserClass
                {
                    ClassId = model.ClassId,
                    UserId = model.UserId,
                };

                var addClassUser = await _unitOfWork.UserClass.AddAsync(userClass);
                if (addClassUser)
                {
                    await _unitOfWork.UserClass.SaveChangeAsync();
                    return new BaseResult
                    {
                        Message = "UserClass successfully add",
                        Success = true
                    };
                }
                return new BaseResult
                {
                    Message = "User Class not add",
                    Success = false
                };

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Account service AddClassForUser  -> exaption -> {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                var users = await _unitOfWork.User.GetAllAsync();
                if (users == null)
                {
                    return Enumerable.Empty<User>();
                }

                return users;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Account service GetAll  -> exaption -> {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseResult> RemoveUserAsync(int id)
        {
            try
            {
                var getUser = await _unitOfWork.User.GetById(id);
                var getUsers = await _unitOfWork.UserClass.GetAllByIdAsync(id);


                if (getUser == null)
                {
                    return new BaseResult
                    {
                        Success = false,
                        Message = "User Not Found"
                    };
                }

                await _unitOfWork.User.RemoveAsync(getUser);
                foreach (var item in getUsers)
                {
                    await _unitOfWork.UserClass.RemoveAsync(item);
                }

                return new BaseResult
                {
                    Success = true,
                    Message = "User successfully delete"
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Account service Remove  -> exaption -> {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseResult> UpdateUserAsync(UserUpdateModel model)
        {
            try
            {
                var getUser = _unitOfWork.User.GetById(model.UserId);
                if (getUser == null)
                {
                    return new BaseResult
                    {
                        Success = false,
                        Message = "User Not Found"
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
                    Address = Address,
                    DateOfBirth = model.DateOfBirth,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = PasswordHash.HashPassword(model.Password),
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.UserName
                };

                await _unitOfWork.User.UpdateAsync(user);

                return new BaseResult
                {
                    Success = true,
                    Message = "User successfully uopdate"
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Account service Update user -> exaption -> {ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
