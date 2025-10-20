using Domain.Contracts;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Servieces.Specifications
{
    public abstract class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public BaseSpecifications(Expression<Func<TEntity, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }
        #region Filtering
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }
        #endregion
        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> OrderByAsc) => OrderBy = OrderByAsc;

        public Expression<Func<TEntity, object>> OrderByDesceding { get; private set; }

        protected void AddOrderByDesceding(Expression<Func<TEntity, object>> _OrderByDesceding) => OrderByDesceding = _OrderByDesceding;
        #endregion
        #region Pagination
        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPaginated { get; set; }
        protected void ApplyPagination(int PageSize, int PageIndex)
        {
            IsPaginated= true;
            Take= PageSize;
            Skip = (PageIndex - 1) * PageSize;
        }
 
        #endregion

    }
}
