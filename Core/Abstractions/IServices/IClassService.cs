using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions.IServices
{
    public interface IClassService
    {
        Task<BaseResult> AddClass(ClassModel model);
        Task<BaseResult> RemoveClass(int id);
        Task<BaseResult> UpdateClass(ClassModel model);
    }
}
