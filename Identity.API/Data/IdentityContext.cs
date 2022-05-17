using Identity.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Data;

public class IdentityContext : DbContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {

    }
    public virtual DbSet<appUsers> appUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<appUsers>(entity =>
        {
            entity.HasKey(e => e.usId).HasName("PK_usId");
            entity.Property(e=> e.usId).HasColumnName("usId").HasColumnType("uniqueidentifier").IsRequired(true).ValueGeneratedOnAdd();
            entity.Property(e=>e.Fname).HasColumnName("Fname").HasColumnType("nvarchar(30)").IsRequired();
            entity.Property(e=>e.Mname).HasColumnName("Mname").HasColumnType("nvarchar(30)").IsRequired(false);
            entity.Property(e=>e.Lname).HasColumnName("Lname").HasColumnType("nvarchar(30)").IsRequired();
            entity.Property(e=>e.EmailId).HasColumnName("EmailId").HasColumnType("nvarchar(40)").IsRequired();
            entity.Property(e=>e.Password).HasColumnName("Password").HasColumnType("nvarchar(100)").IsRequired();
            entity.Property(e=>e.Role).HasColumnName("Role").HasColumnType("varchar(10)").IsRequired();
            entity.Property(e=>e.Dob).HasColumnName("Dob").HasColumnType("datetime").IsRequired();
            entity.Property(e=>e.CreatedDate).HasColumnName("CreatedDate").HasColumnType("datetime").HasDefaultValueSql("getdate()");
            entity.Property(e=>e.ModifiedDate).HasColumnName("ModifiedDate").HasColumnType("datetime").HasDefaultValueSql("getdate()");
        });
    }
}