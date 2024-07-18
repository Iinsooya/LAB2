using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace LAB_2
{
    internal class AppContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public AppContext() : base("DefaultConnection") { }

    }
}
