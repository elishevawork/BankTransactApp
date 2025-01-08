using Microsoft.EntityFrameworkCore;
using TransactionsAPI.Data.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TransactionsAPI.Data
{
    public class TransactionsDbContext : DbContext
    {
        public TransactionsDbContext()
        {

        }

        public TransactionsDbContext(DbContextOptions<TransactionsDbContext> options) : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId);
                entity.Property(e => e.OrderId).ValueGeneratedOnAdd();
                entity.Property(e => e.CustomerFullNameHebrew)
                .IsRequired()
                .HasMaxLength(50); //Scallable - Validations in Dtos

                entity.Property(e => e.CustomerFullName)
                .IsRequired()
                .HasMaxLength(50);

                entity.Property(e => e.OrderType).IsRequired();
                entity.Property(e => e.Amount).IsRequired();
                entity.Property(e => e.AccountNumber).IsRequired();

                entity.Property(e => e.Amount)
                .HasColumnType("decimal(18,2)");

                entity.Property(e => e.UpdatedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
