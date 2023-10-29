using Core.Abstractions;
using Core.Abstractions.IServices;
using Core.Entities;
using Core.Helpers;
using Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly ILogger<ClassService> _logger;

        public ClassService(IUnitOfWork unitOfWork, ILogger<ClassService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<BaseResult> AddClass(ClassModel model)
        {
            try
            {
              

                var result = await _unitOfWork.Class.GetByClassName(model.ClassName);
                if (result != null)
                {
                    return new BaseResult
                    {
                        Message = "Class already exist",
                        Success = false,
                    };
                }

                Class clas = new Class
                {
                    ClassName = model.ClassName
                };

                await _unitOfWork.Class.AddAsync(clas);
                await _unitOfWork.Class.SaveChangeAsync();

                return new BaseResult
                {
                    Message = "Class succesfully add",
                    Success = true,
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<BaseResult> RemoveClass(int id)
        {
            try
            {
                var getClass = await _unitOfWork.Class.GetById(id);
                var getClasses = await _unitOfWork.UserClass.GetAllByIdAsync(id);

                if (getClass == null)
                {
                    return new BaseResult
                    {
                        Success = false,
                        Message = "Class Not Found"
                    };
                }

                await _unitOfWork.Class.RemoveAsync(getClass);
                foreach (var item in getClasses)
                {
                    await _unitOfWork.UserClass.RemoveAsync(item);
                }

                return new BaseResult
                {
                    Success = true,
                    Message = "Class successfully delete"
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<BaseResult> UpdateClass(ClassModel model)
        {
            try
            {
                var getClass = await _unitOfWork.Class.GetById(model.ClassId);
                if (getClass == null)
                {
                    return new BaseResult
                    {
                        Success = false,
                        Message = "Class Not Found"
                    };
                }

                Class clas = new Class
                {
                  ClassName = model.ClassName
                };

                await _unitOfWork.Class.UpdateAsync(clas);

                return new BaseResult
                {
                    Success = true,
                    Message = "Class successfully uopdate"
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Class service Update  -> exaption -> {ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
