namespace nzWalksApi.Data
{
    using Microsoft.EntityFrameworkCore;
    using nzWalksApi.Models.Domain;

    /// <summary>
    /// Defines the <seecref="NzWalksDbContext" />
    /// </summary>
    public class NzWalksDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NzWalksDbContext"/> class.
        /// </summary>
        /// <param name="dbContextOptions">The dbContextOptions<see //cref="DbContextOptions"/></param>
        public NzWalksDbContext(DbContextOptions<NzWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

       
        public DbSet<Difficulty> difficulties { get; set; }

        public DbSet<Region> regions { get; set; }

        
        public DbSet<Walk> walks { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Seed data for difficulty
            // Easy , Model , Hard

            var difficulties = new List<Difficulty>() {


                new Difficulty() {

            Id=Guid.Parse("b431dc54-e0ea-474d-9e7b-3f51b8de0f59"),
            Name="Easy"

            },

            new Difficulty()
            {
                Id = Guid.Parse("f556bf05-f365-4ac8-91dc-0534245f87e7"),
                Name="Medium"
            },
            new Difficulty()
            {
                Id = Guid.Parse("a2901401-f5d8-489f-87dc-21eb35874b3f"),
                Name="Hard"
            }

            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
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
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                },
            };


            modelBuilder.Entity<Region>().HasData(regions);

        }
    }
}
