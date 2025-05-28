using Microsoft.EntityFrameworkCore;

namespace Kanye4King.Database
{
    public class Kanye4KingDbContext : DbContext
    {
        private const string _connectionString = "Data Source=data.db";

        public DbSet<DbPacket> Packets { get; set; }
        public DbSet<DbLog> Log { get; set; }

        public Kanye4KingDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options
                .UseSqlite(_connectionString)
                .EnableSensitiveDataLogging();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbPacket>().ToTable("packets");
            modelBuilder.Entity<DbPacket>().HasIndex(c => c.CreatedAt);

            modelBuilder.Entity<DbLog>().ToTable("log");
            modelBuilder.Entity<DbLog>().HasIndex(m => m.CreatedAt);
        }

        public void DropAll()
        {
            Log.ExecuteDelete();
            Packets.ExecuteDelete();
        }
    }
}