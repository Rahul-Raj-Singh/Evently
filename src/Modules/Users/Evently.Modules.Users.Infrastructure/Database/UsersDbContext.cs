using Evently.Modules.Users.Application.Abstractions.Data;
using Evently.Modules.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Users.Infrastructure.Database;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(Schemas.Users);

        // cardinality

        modelBuilder.Entity<Role>(builder => 
        {
            builder.ToTable("roles");
            builder.HasKey(x => x.Name);
        });

        modelBuilder.Entity<Role>()
        .HasMany<User>()
        .WithMany(x => x.Roles)
        .UsingEntity(joinBuilder => 
        {
            joinBuilder.ToTable("user_roles");
            joinBuilder.Property("RolesName").HasColumnName("role_name");
        });

        modelBuilder.Entity<Permission>(builder => 
        {
            builder.ToTable("permissions");
            builder.HasKey(x => x.Code);
        });

        modelBuilder.Entity<Permission>()
        .HasMany<Role>()
        .WithMany()
        .UsingEntity(joinBuilder => 
        {
            joinBuilder.ToTable("role_permissions");
        });
    }

}
