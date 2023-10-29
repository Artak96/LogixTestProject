using Core.Abstractions.IRepositories;
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
    public class BaseRepository<T> : IBaseRepository<T> where T : class 
    {
        protected readonly LogixDbContext _context;

        public BaseRepository(LogixDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                var user = await _context.Set<T>().AddAsync(entity);
                if (user != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public void AddRange(IEnumerable<T> entities)
        {
            try
            {
                _context.Set<T>().AddRange(entities);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _context.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

     

        public async Task<T> GetById(int id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _context.Set<T>().FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task RemoveAsync(T entity)
        {
            try
            {
                 _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task SaveChangeAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
