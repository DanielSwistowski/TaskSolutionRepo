using DataAccessLayer.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace DataAccessLayer
{
    public interface ICompanyDbContext
    {
        DbSet<Company> Companies { get; set; }
        DbSet<Address> Adresses { get; set; }
        DbSet<SearchDetail> SearchDetails { get; set; }

        void SetModified(object entity);
        Task<int> SaveChangesAsync();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry Entry<T>(T entity) where T : class;
    }

    public class CompanyDbContext : DbContext,ICompanyDbContext
    {
        public CompanyDbContext() : base("CompanyDetailsDb")
        { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Address> Adresses { get; set; }
        public DbSet<SearchDetail> SearchDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        DbEntityEntry ICompanyDbContext.Entry<T>(T entity)
        {
            return Entry(entity);
        }
    }
}
