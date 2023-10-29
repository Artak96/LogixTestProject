using Core.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions.IRepositories
{
    public interface IUserClassRepository : IBaseRepository<UserClass>
    {
        Task<IEnumerable<UserClass>> GetAllByIdAsync(int id);
    }
}
