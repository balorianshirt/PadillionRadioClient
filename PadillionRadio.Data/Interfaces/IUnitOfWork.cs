using System;
using System.Threading.Tasks;
using PadillionRadio.Data.Contexts;

namespace PadillionRadio.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DatabaseContext Context { get; }

        Task Save();
    }
}