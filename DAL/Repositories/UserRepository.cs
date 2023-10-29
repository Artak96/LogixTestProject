using Core.Abstractions.IRepositories;
using Core.Entities;
using DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(LogixDbContext context) : base(context)
        {

        }
    }
}
