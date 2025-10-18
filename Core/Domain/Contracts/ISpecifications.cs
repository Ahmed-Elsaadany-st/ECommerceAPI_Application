﻿using Domain.Models;
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
        public Expression<Func<TEntity,bool>>?Criteria { get; } // Condition of the Query
        public List<Expression<Func<TEntity,object>>>IncludeExpressions { get; } // Include statement

        public Expression<Func<TEntity,object>>OrderBy {  get; }
        public Expression<Func<TEntity,object>>OrderByDesceding {  get; }



    }
}
