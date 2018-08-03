using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GoogleKeepLike.Models
{
    public class GoogleKeepLikeContext : DbContext
    {
        public GoogleKeepLikeContext (DbContextOptions<GoogleKeepLikeContext> options)
            : base(options)
        {
        }

        public DbSet<GoogleKeepLike.Models.Note> Note { get; set; }
        //public DbSet<Label> Label { get; set; }
        //public DbSet<CheckList> CheckList { get; set; }
//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Note>()
//                .HasMany()
//.WillCascadeOnDelete(true);
//        }
    }
}
