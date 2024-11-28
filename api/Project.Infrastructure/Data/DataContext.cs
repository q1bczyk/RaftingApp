using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Core.Entities;

namespace Project.Infrastructure.Data
{
    public class DataContext : IdentityDbContext<
    User, 
    IdentityRole<string>, 
    string, 
    IdentityUserClaim<string>, 
    IdentityUserRole<string>, 
    IdentityUserLogin<string>, 
    IdentityRoleClaim<string>, 
    IdentityUserToken<string>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<Discount> Discounts { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationEquipment> ReservationsEquipment { get; set; }
        public DbSet<Settings> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            DataContextConfig.Config(modelBuilder);
        }   
    }
}