using Core.Abstractions;
using Core.Abstractions.IRepositories;
using Core.Entities;
using DAL.Context;
using DAL.Repositories;
using LogixTestProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LogixDbContext _dbContext;

        public UnitOfWork(LogixDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private UserRepository? _user;
        public IUserRepository User => _user ?? (_user = new UserRepository(_dbContext));

        private ClassRepository _class;
        public IClassRepository Class => _class ?? (_class = new ClassRepository(_dbContext));

        private UserClassRepository? _userClass;
        public IUserClassRepository UserClass => _userClass ?? (_userClass = new UserClassRepository(_dbContext));
    }
}
