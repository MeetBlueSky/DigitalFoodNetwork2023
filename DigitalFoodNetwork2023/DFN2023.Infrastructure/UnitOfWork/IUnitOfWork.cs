
using DFN2023.Infrastructure.Repositories;
using System;

namespace DFN2023.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepostory<T>() where T : class;
        void SaveChanges();
    }
}

