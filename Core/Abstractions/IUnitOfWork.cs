using Core.Abstractions.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IClassRepository Class { get; }
        IUserClassRepository UserClass { get; }

    }
}
