using Microsoft.EntityFrameworkCore;
using Project.Core.Entities;

namespace Project.Infrastructure.Data
{
    public class DataContextConfig
    {
        public static void Config(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.HasMany(e => e.Reservations)
                .WithMany(e => e.Equipments)
                .UsingEntity<ReservationEquipment>();
            });
        }
    }
}