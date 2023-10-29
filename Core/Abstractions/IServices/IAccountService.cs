using Core.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions.IServices
{
    public interface IAccountService
    {
        Task<BaseResult> UpdateUserAsync(UserUpdateModel model);
        Task<BaseResult> RemoveUserAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<BaseResult> AddClassForUserAsync(ClassUserModel model);
    }
}
