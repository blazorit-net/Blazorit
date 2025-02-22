using Blazorit.Domain.EShop.EF.ident;
using Blazorit.Infrastructure.Configurations.EShop.EF.Ident;
using Microsoft.EntityFrameworkCore;

namespace Blazorit.Infrastructure.DBStorages.EShop.EF;

public class IdentDbContext : DbContext
{
    public IdentDbContext(DbContextOptions<IdentDbContext> options) : base(options) {
    }

    //################################################################
    //  #######################--IDENT--############################
    //################################################################
    public virtual DbSet<User> User { get; set; } = null!;
    //################################################################
    //  ############################################################
    //################################################################

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new UserConfiguration());
    }
}