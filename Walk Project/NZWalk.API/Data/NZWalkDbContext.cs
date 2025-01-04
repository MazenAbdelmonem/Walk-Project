using Microsoft.EntityFrameworkCore;
using NZWalk.API.Models.Domin;

namespace NZWalk.API.Data
{
    public class NZWalkDbContext : DbContext
    {
        public NZWalkDbContext(DbContextOptions<NZWalkDbContext> dbContextOptions) : base(dbContextOptions)
        {
                
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seed Data for Difficulties
            // Easy, Medium, Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("38ef6ec7-7980-4246-8870-03c20a55ca51"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("69b761a0-cfbf-433b-9714-5ec3c51f96b0"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("2bafd952-673e-4f83-93a3-f9d5ee64b1b7"),
                    Name = "Hard"
                }
            };

            // seed Difficulties for Database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            // seed Data for Region

            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("73ec7f3d-d25d-4e1b-94dd-ac98766b410b"),
                    Name = "Auckland",
                    code = "AKL",
                    RegioImageUrl = "https://media.istockphoto.com/id/1137079196/photo/auckland-panorama-at-sunrise.jpg?s=1024x1024&w=is&k=20&c=ebyZHJ0BbOh_w4gwzmkgAcfzM_6dvXoZ2D3U1ex_Gpc="

                },
                new Region()
                {
                    Id = Guid.Parse("641e51e6-68c0-4dec-bb40-e85a8b1a9ce8"),
                    Name = "Bayof Plenty",
                    code = "BOP",
                    RegioImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("685dd3c8-c37f-407b-9989-92709bfdd23f"),
                    Name = "Wellington",
                    code = "WGN",
                    RegioImageUrl = "https://media.istockphoto.com/id/2066058412/photo/wellington-new-zealands-capital-city.jpg?s=1024x1024&w=is&k=20&c=FasHsT_tNKoUTHwiBsOM1AMjw_u9UCH4Kba1MHmBu0M="

                },
                new Region()
                {
                    Id = Guid.Parse("68312b7f-21e4-4871-934f-667afb63894d"),
                    Name = "Nelson",
                    code = "NSN",
                    RegioImageUrl = "https://media.istockphoto.com/id/1209995566/photo/panorama-of-nelson-city-reflected-in-the-maitai-river-new-zealand.jpg?s=1024x1024&w=is&k=20&c=ynC5H3sKnrq4Tc4swHlao07JPv13wPEmdwuGG6d7IQE="

                },
                new Region()
                {
                    Id = Guid.Parse("19bfb094-a62d-4a63-a7fe-4d5edff31645"),
                    Name = "Southland",
                    code = "STL",
                    RegioImageUrl = null

                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
