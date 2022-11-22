using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using Domain;

namespace Repository
{
    public class Context : DbContext
    {
        public DbSet<Lancamento> Lancamento { get; set; }

        public void Save()
        {
            base.SaveChanges();
        }
        
    }
}
