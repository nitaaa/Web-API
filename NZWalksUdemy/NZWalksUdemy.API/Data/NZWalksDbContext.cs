using Microsoft.EntityFrameworkCore;
using NZWalksUdemy.API.Models.Domain;

namespace NZWalksUdemy.API.Data
{
    public class NZWalksDbContext: DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Difficulties - Easy, Medium, Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("d4741157-d06a-4346-bbfb-c205ec460041"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("1b2ee604-6129-4d84-bb02-cc3aaffb85f8"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("79afb848-95c0-4270-a4ca-4e3e9b67d53a"),
                    Name = "Hard"
                },
            };
            // Seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("8768d5da-d098-4949-a37b-86ba93d6d7dd"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region()
                {
                    Id = Guid.Parse("117a1e26-f604-4334-a257-8640a9d826b4"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("b15929e0-10bf-43a6-be10-62d7998796d2"),
                    Name = "Bay of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region()
                {
                    Id = Guid.Parse("c68d8631-6304-4019-9a50-7ffc9b30b27d"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region()
                {
                    Id = Guid.Parse("c7167312-0cbe-48a7-a747-eabf8a975325"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);

            // TODO: ADD MIGRATION
            // Add-Migration "Seeding Data for Difficulties and Regions"
            // Update-Database

        }
    }

}
