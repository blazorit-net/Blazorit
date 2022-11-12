using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.ident;
using Microsoft.EntityFrameworkCore;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF {
    public partial class BlazoritContext : DbContext {
        public BlazoritContext(DbContextOptions<BlazoritContext> options) : base(options) {

        }


        public virtual DbSet<User> Users { get; set; } = null!;





        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>(entity => {
                entity.Property(e => e.DateCreated).HasColumnType("timestamp without time zone");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}