using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace keep.Models
{
    public class keepContext : DbContext
    {
        public keepContext (DbContextOptions<keepContext> options)
            : base(options)
        {
        }

        public DbSet<keep.Models.Note> Note { get; set; }
    }
}
