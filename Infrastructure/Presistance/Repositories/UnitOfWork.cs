using Domain.Contracts;
using Domain.Models;
using Presistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Repositories
{
    public class UnitOfWork(StoreDbContext _Context) : IUntiOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var TypeName= typeof(TEntity).Name;
            if (_repositories.ContainsKey(TypeName))
            {
                return (GenericRepository<TEntity,Tkey>) _repositories[TypeName]; // We Need Casitn Cause the Repo is stored as an object
            }
            else
            {
                var Repo = new GenericRepository<TEntity, Tkey>(_Context);
                _repositories.Add(TypeName, Repo); //_repositories[TypeName] = Repo;
                return Repo;
            }

        }

        public async Task<int> SaveChangesAsync() =>  await _Context.SaveChangesAsync();
        
    }
}
