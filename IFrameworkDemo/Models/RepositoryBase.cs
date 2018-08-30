using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IFramework.EntityFrameworkCore.Repositories;
using IFramework.UnitOfWork;

namespace IFrameworkDemo.Models
{
    public class RepositoryBase<TEntity> : Repository<TEntity> where TEntity: class
    {
        public RepositoryBase(DemoDbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork) { }

    }
}
