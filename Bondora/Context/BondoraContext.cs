using Bondora.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bondora.Context
{
    public class BondoraContext : DbContext
    {
		public DbSet<Equipment> Equipments { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Rental> Rentals { get; set; }

		public BondoraContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Order>().Ignore(o => o.TotalPrice);
			modelBuilder.Entity<Order>().Ignore(o => o.TotalBonus);

			modelBuilder.Entity<Rental>().Ignore(x => x.TotalPrice);
			modelBuilder.Entity<Rental>().Ignore(x => x.Bonus);

			base.OnModelCreating(modelBuilder);
		}
	}
}
