using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lancamento2.Model;

namespace Lancamento2.Data
{
    public class Lancamento2Context : DbContext
    {
        public Lancamento2Context (DbContextOptions<Lancamento2Context> options)
            : base(options)
        {
        }

        public DbSet<Lancamento2.Model.Lancamento> Lancamento { get; set; }
    }
}
