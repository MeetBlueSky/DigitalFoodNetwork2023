using DFN2023.Infrastructure.Context;
using DFN2023.Infrastructure.Repositories;
using System;

namespace DFN2023.Infrastructure.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public EFUnitOfWork(ApplicationContext context)
        {
            _context = context;
        }
        public IRepository<T> GetRepostory<T>() where T : class
        {
            return new EFRepository<T>(_context);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        #region IDisposable Members
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

