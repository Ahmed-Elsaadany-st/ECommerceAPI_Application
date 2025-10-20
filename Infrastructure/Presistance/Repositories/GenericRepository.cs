using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servieces.Specifications;


namespace Presistance.Repositories
{
    public class GenericRepository<TEntity, Tkey>(StoreDbContext _Context) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public async Task AddAsync(TEntity entity)=> await _Context.Set<TEntity>().AddAsync(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _Context.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(int id) => await _Context.Set<TEntity>().FindAsync(id);

        public void Remove(TEntity entity) => _Context.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) => _Context.Set<TEntity>().Update(entity);

        #region With Specification
        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> specifications)
        {
            return await SpecificationEvaluator.BuildQuery(_Context.Set<TEntity>(), specifications).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specifications)
        {
            return await SpecificationEvaluator.BuildQuery(_Context.Set<TEntity>(), specifications).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecifications<TEntity, Tkey> specifications)
        {
            return await SpecificationEvaluator.BuildQuery(_Context.Set<TEntity>(),specifications).CountAsync();
            // I Do not Understand the the job of this function(I think it Gets All Products that meet the criteria (in all pages))
        }
        #endregion

    }
}
