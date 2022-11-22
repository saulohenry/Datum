using System;
using System.Data.Entity;

namespace Repository1
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        { }
    }
}
