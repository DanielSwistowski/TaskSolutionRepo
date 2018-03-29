using DataAccessLayer;
using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace BusinessLogicLayer.Tests.FakeContext
{
    public class FakeDbContext : ICompanyDbContext
    {
        public FakeDbContext()
        {
            Companies = new FakeCompaniesSet();
            Adresses = new FakeAddresesSet();
            SearchDetails = new FakeSearchDetailsSet();
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Address> Adresses { get; set; }
        public DbSet<SearchDetail> SearchDetails { get; set; }

        public DbEntityEntry Entry<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(0);
        }

        DbSet<TEntity> ICompanyDbContext.Set<TEntity>()
        {
            if (typeof(TEntity) == typeof(Company))
                return Companies as DbSet<TEntity>;

            if (typeof(TEntity) == typeof(Address))
                return Adresses as DbSet<TEntity>;

            if (typeof(TEntity) == typeof(SearchDetail))
                return SearchDetails as DbSet<TEntity>;

            throw new NullReferenceException("Please provide DbSet for selected entity if you try to use method from generic BaseService");
        }

        public void SetModified(object entity)
        { }
    }

    public class FakeCompaniesSet : FakeDbSet<Company>
    {
        public override Company Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.ComanyId == (int)keyValues.Single());
        }

        public override Task<Company> FindAsync(params object[] keyValues)
        {
            return Task.FromResult(Find(keyValues));
        }
    }

    public class FakeAddresesSet : FakeDbSet<Address>
    {
        public override Address Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.CompanyId == (int)keyValues.Single());
        }

        public override Task<Address> FindAsync(params object[] keyValues)
        {
            return Task.FromResult(Find(keyValues));
        }
    }

    public class FakeSearchDetailsSet : FakeDbSet<SearchDetail>
    {
        public override SearchDetail Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.SearchDetailId == (int)keyValues.Single());
        }

        public override Task<SearchDetail> FindAsync(params object[] keyValues)
        {
            return Task.FromResult(Find(keyValues));
        }
    }
}