using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Bookkeeping.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; set; }

        void Save();
    }
}