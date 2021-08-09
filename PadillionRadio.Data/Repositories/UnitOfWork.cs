using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PadillionRadio.Data.Contexts;
using PadillionRadio.Data.Interfaces;

namespace PadillionRadio.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public DatabaseContext Context { get; }

        public UnitOfWork(DatabaseContext context)
        {
            Context = context;
        }

        public async Task Save()
        {
            await Context.SaveChangesAsync();
            Dispose();
        }

        public async void Dispose()
        {
            await Context.DisposeAsync();
        }
    }
}