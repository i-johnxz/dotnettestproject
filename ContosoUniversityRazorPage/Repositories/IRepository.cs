using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ContosoUniversityRazorPage.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Delete(TEntity entityToDelete);

        Task Delete(object id);

        Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = ""
        );

        Task<TEntity> GetById(object id);

        Task<IEnumerable<TEntity>> GetWithRawSql(string query,
            params object[] parameters);

        Task Insert(TEntity entity);

        void Update(TEntity entityToUpdate);
    }
}