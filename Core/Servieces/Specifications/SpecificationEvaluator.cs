using Domain.Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Servieces.Specifications
{
    public static class SpecificationEvaluator
    {
        //Query Building 
        //_dbContext.Product.Where(P=>P.Id==id).Include(p=>p.ProductType).Include(p=>p.ProductuBrand);
         public static IQueryable<TEntity> BuildQuery<TEntity, Tkey>(IQueryable<TEntity> QueryEntry, ISpecifications<TEntity, Tkey> specifications) where TEntity : BaseEntity<Tkey>
        {
            var Query = QueryEntry;
            #region Filteration
            if (specifications.Criteria is not null)
            {
                Query = Query.Where(specifications.Criteria);
            } 
            #endregion
            #region Sorting
            if (specifications.OrderBy is not null)
            {
                Query = Query.OrderBy(specifications.OrderBy);
            }
            if (specifications.OrderByDesceding is not null)
            {
                Query = Query.OrderByDescending(specifications.OrderByDesceding);
            }
            #endregion
            #region Loading Related Data
            if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count > 0)
            {
                Query = specifications.IncludeExpressions.Aggregate(Query, (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));
            }
            #endregion
            #region Pagination
            if (specifications.IsPaginated)
            {
                Query=Query.Skip(specifications.Skip).Take(specifications.Take);
            }
            #endregion
            return Query;
        }
    }
}
