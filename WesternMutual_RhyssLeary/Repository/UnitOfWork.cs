
using WesternMutual_RhyssLeary.Domain;

namespace WesternMutual_RhyssLeary.Repository
{
    public interface IUnitOfWork : IDisposable { 
        #region Repositories
        IGenericRepository<Entry> Entries { get; }
        #endregion
        #region Methods
        void Commit();

        Task  CommitAsync();
        #endregion
    }
    public class UnitOfWork:IUnitOfWork
    {
        #region Properties & Fields
        private ApplicationDbContext Context { get; set; } = default!;
        
        #endregion
        #region Events
        public UnitOfWork(ApplicationDbContext context)
        {
            Context = context;
            Entries = new GenericRepository<Entry>(context);
        }
        #endregion
        #region Repositories
        public IGenericRepository<Entry> Entries { get; private set; }

        #endregion
        #region Methods


        public void Commit()
        {
            Context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
