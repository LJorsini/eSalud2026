using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using eSalud.Models;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>

{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)

        : base(options)

    {

    }

 

    // Se van a agregar los dbset
    public DbSet<Administrador> Administradores {get; set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>()
            .HasIndex(u => u.Dni)
            .IsUnique();
    }

}