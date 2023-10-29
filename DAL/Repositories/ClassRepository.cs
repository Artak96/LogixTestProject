using Core.Abstractions.IRepositories;
using Core.Entities;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ClassRepository : BaseRepository<Class>, IClassRepository
    {
        public ClassRepository(LogixDbContext context) : base(context)
        {

        }

        public async Task<Class> GetByClassName(string className)
        {
            try
            {
                return await _context.Classes.FirstOrDefaultAsync(m => m.ClassName.Equals(className));
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
