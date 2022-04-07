using LiveMotion.Core.Entities;
using System.Data.Entity;

namespace LiveMotion.Core.DbConfiguration
{
    public class LiveMotionContext : DbContext
    {
        public LiveMotionContext() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LiveMotionContext, LiveMotionContextConfiguration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Slide>().ToTable("Slide");
            modelBuilder.Entity<Presentation>().ToTable("Presentation");
        }

        public DbSet<Slide> Slide { get; set; }

        public DbSet<Presentation> Presentation { get; set; }
    }
}
