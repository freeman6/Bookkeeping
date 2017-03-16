using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Bookkeeping.Models;

namespace Bookkeeping.Repositories
{
    
    public class EFUnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; set; }
        public EFUnitOfWork()
        {
            Context = new DBModel();
        }
        public void Save()
        {
            Context.SaveChanges();
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}