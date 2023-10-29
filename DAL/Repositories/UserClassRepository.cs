using Core.Abstractions.IRepositories;
using Core.Entities;
using Core.Models;
using DAL.Context;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace LogixTestProject.Controllers
{
    public class UserClassRepository : BaseRepository<UserClass>, IUserClassRepository
    {
        public UserClassRepository(LogixDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<UserClass>> GetAllByIdAsync(int id)
        {
            try
            {
                return await _context.Set<UserClass>().Where(m => m.ClassId == id).ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
