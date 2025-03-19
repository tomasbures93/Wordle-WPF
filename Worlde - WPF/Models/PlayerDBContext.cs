using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Worlde___WPF.Models
{
    internal class PlayerDBContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // This needs to be reworked !!!
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Database.db");
            optionsBuilder.UseSqlite($"Filename={path}");
        }
    }
}
