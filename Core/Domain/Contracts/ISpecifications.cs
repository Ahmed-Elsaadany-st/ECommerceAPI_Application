using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISpecifications<TEntity,Tkey>where TEntity : BaseEntity<Tkey>
    {
        #region Filtering
        public Expression<Func<TEntity, bool>>? Criteria { get; } // Condition of the Query
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } // Include statement

        #endregion
        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; }
        public Expression<Func<TEntity, object>> OrderByDesceding { get; }
        #endregion
        #region Pagination
        public int Skip { get; }
        public int Take { get;}
        public bool IsPaginated { get; set; }
        #endregion



    }
}
