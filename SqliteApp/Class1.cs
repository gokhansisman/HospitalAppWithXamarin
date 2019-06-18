using App2;
using Microsoft.EntityFrameworkCore;


namespace SqliteApp
{
   
    
   public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Sorular> Sorularim { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Doktor> Doktorlar { get; set; }
        public DbSet<Hemsire> Hemsireler { get; set; }
        public DbSet<Oda> Odalar { get; set; }
        public DbSet<Koridor> Koridorlar { get; set; }
        public DbSet<Hastane> Hastaneler { get; set; }
        public DbSet<Nesne> Nesneler { get; set; }
        private readonly string _veritabaniYolu;

        public DatabaseContext(string veritabaniYolu)
        {
            _veritabaniYolu = veritabaniYolu;

          //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_veritabaniYolu}");
        }
    }
}
