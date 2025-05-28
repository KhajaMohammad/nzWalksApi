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
        public NzWalksDbContext(DbContextOptions<NzWalksDbContext> dbContextOptions)
            : base(dbContextOptions) { }

        public DbSet<Difficulty> difficulties { get; set; }

        public DbSet<Region> regions { get; set; }

        public DbSet<Walk> walks { get; set; }

        public DbSet<Image> image { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for difficulty
            // Easy , Model , Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("b431dc54-e0ea-474d-9e7b-3f51b8de0f59"),
                    Name = "Easy",
                },
                new Difficulty()
                {
                    Id = Guid.Parse("f556bf05-f365-4ac8-91dc-0534245f87e7"),
                    Name = "Medium",
                },
                new Difficulty()
                {
                    Id = Guid.Parse("a2901401-f5d8-489f-87dc-21eb35874b3f"),
                    Name = "Hard",
                },
            };


           
            var walks = new List<Walk>
            {

                new Walk {
     Id = Guid.Parse("327aa9f7-26f7-4ddb-8047-97464374bb63"),
    Name = "Mount Victoria Loop",
    Description = "This scenic walk takes you around the top of Mount Victoria, offering stunning views of Wellington and its harbor.",
    LengthInKm = 3.5,
    WalkImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    DifficultyId= Guid.Parse("54466F17-02AF-48E7-8ED3-5A4A8BFACF6F"),
    RegionId= Guid.Parse("CFA06ED2-BF65-4B65-93ED-C9D286DDB0DE")
},
new Walk {
     Id = Guid.Parse("1cc5f2bc-ff4b-47c0-a475-1add56c6497b"),
    Name = "Makara Beach Walkway",
    Description = "This walk takes you along the wild and rugged coastline of Makara Beach, with breathtaking views of the Tasman Sea.",
    LengthInKm = 8.2,
    WalkImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    DifficultyId= Guid.Parse("EA294873-7A8C-4C0F-BFA7-A2EB492CBF8C"),
    RegionId= Guid.Parse("CFA06ED2-BF65-4B65-93ED-C9D286DDB0DE")
},
new Walk {
     Id = Guid.Parse("09601132-f92d-457c-b47e-da90e117b33c"),
    Name = "Botanic Garden Walk",
    Description = "Explore the beautiful Botanic Garden of Wellington on this leisurely walk, with a wide variety of plants and flowers to admire.",
    LengthInKm = 2,
    WalkImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    DifficultyId= Guid.Parse("54466F17-02AF-48E7-8ED3-5A4A8BFACF6F"),
    RegionId= Guid.Parse("CFA06ED2-BF65-4B65-93ED-C9D286DDB0DE")
},
new Walk {
     Id = Guid.Parse("30d654c7-89ac-4704-8333-5065b740150b"),
    Name = "Mount Eden Summit Walk",
    Description = "This walk takes you to the summit of Mount Eden, the highest natural point in Auckland, with panoramic views of the city.",
    LengthInKm = 2,
    WalkImageUrl = "https://images.pexels.com/photos/5342974/pexels-photo-5342974.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    DifficultyId= Guid.Parse("54466F17-02AF-48E7-8ED3-5A4A8BFACF6F"),
    RegionId= Guid.Parse("F7248FC3-2585-4EFB-8D1D-1C555F4087F6")
},
new Walk {
     Id = Guid.Parse("f7578324-f025-4c86-83a9-37a7f3d8fe81"),
    Name = "Cornwall Park Walk",
    Description = "Explore the beautiful Cornwall Park on this leisurely walk, with a wide variety of trees, gardens, and animals to admire.",
    LengthInKm = 3,
    WalkImageUrl = "https://images.pexels.com/photos/5342974/pexels-photo-5342974.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    DifficultyId= Guid.Parse("54466F17-02AF-48E7-8ED3-5A4A8BFACF6F"),
    RegionId= Guid.Parse("F7248FC3-2585-4EFB-8D1D-1C555F4087F6")
},
new Walk {
     Id = Guid.Parse("bdf28703-6d0e-4822-ad8b-e2923f4e95a2"),
    Name = "Takapuna to Milford Coastal Walk",
    Description = "This coastal walk takes you along the beautiful beaches of Takapuna and Milford, with stunning views of Rangitoto Island.",
    LengthInKm = 5,
    WalkImageUrl = "https://images.pexels.com/photos/5342974/pexels-photo-5342974.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    DifficultyId= Guid.Parse("EA294873-7A8C-4C0F-BFA7-A2EB492CBF8C"),
    RegionId= Guid.Parse("F7248FC3-2585-4EFB-8D1D-1C555F4087F6")
},
new Walk {
     Id = Guid.Parse("43132402-3d5e-467a-8cde-351c5c7c5dde"),
    Name = "Centre of New Zealand Walkway",
    Description = "This walk takes you to the geographical centre of New Zealand, with stunning views of Nelson and its surroundings.",
    LengthInKm = 1.0,
    WalkImageUrl = "https://images.pexels.com/photos/808466/pexels-photo-808466.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    DifficultyId= Guid.Parse("EA294873-7A8C-4C0F-BFA7-A2EB492CBF8C"),
    RegionId= Guid.Parse("906CB139-415A-4BBB-A174-1A1FAF9FB1F6")
},
new Walk {
     Id = Guid.Parse("1ea0b064-2d44-4324-91ee-6dd86c91b713"),
    Name = "Maitai Valley Walk",
    Description = "Explore the picturesque Maitai Valley on this easy walk, with a tranquil river and native bush to enjoy.",
    LengthInKm = 5.0,
    WalkImageUrl = "https://images.pexels.com/photos/808466/pexels-photo-808466.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    DifficultyId= Guid.Parse("EA294873-7A8C-4C0F-BFA7-A2EB492CBF8C"),
    RegionId= Guid.Parse("906CB139-415A-4BBB-A174-1A1FAF9FB1F6")
},
new Walk {
     Id = Guid.Parse("04ab77f0-e145-4fbf-b641-989df24e5573"),
    Name = "Boulder Bank Walkway",
    Description = "This coastal walk takes you along the unique Boulder Bank, a long narrow bar of rocks that extends into Tasman Bay.",
    LengthInKm = 8.0,
    WalkImageUrl = "https://images.pexels.com/photos/808466/pexels-photo-808466.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    DifficultyId= Guid.Parse("F808DDCD-B5E5-4D80-B732-1CA523E48434"),
    RegionId= Guid.Parse("906CB139-415A-4BBB-A174-1A1FAF9FB1F6")
},
new Walk {
     Id = Guid.Parse("b5aa2791-3616-4db6-ab33-c54d03d17f62"),
    Name = "Mount Maunganui Summit Walk",
    Description = "This walk takes you to the summit of Mount Maunganui, with stunning views of the ocean and surrounding landscape.",
    LengthInKm = 3.0,
    WalkImageUrl = "https://images.pexels.com/photos/808466/pexels-photo-808466.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    DifficultyId= Guid.Parse("EA294873-7A8C-4C0F-BFA7-A2EB492CBF8C"),
    RegionId= Guid.Parse("14CEBA71-4B51-4777-9B17-46602CF66153")
},
new Walk {
     Id = Guid.Parse("2d9d6604-bef9-4b0a-805d-630240a29595"),
    Name = "The Papamoa Hills Regional Park Walk",
    Description = "Enjoy panoramic views of Tauranga and Mount Maunganui on this walk through the Papamoa Hills, with a mix of bush and open farmland.",
    LengthInKm = 5.0,
    WalkImageUrl = "https://images.pexels.com/photos/808466/pexels-photo-808466.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    DifficultyId= Guid.Parse("EA294873-7A8C-4C0F-BFA7-A2EB492CBF8C"),
    RegionId= Guid.Parse("14CEBA71-4B51-4777-9B17-46602CF66153")
},
new Walk {
     Id = Guid.Parse("135a6e58-969f-47e1-8278-d7fbf2b3bd69"),
    Name = "The White Pine Bush Track",
    Description = "Explore the lush and peaceful White Pine Bush on this easy walk, with a variety of native flora and fauna to discover.",
    LengthInKm = 2.0,
    WalkImageUrl = "https://images.pexels.com/photos/808466/pexels-photo-808466.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    DifficultyId= Guid.Parse("EA294873-7A8C-4C0F-BFA7-A2EB492CBF8C"),
    RegionId= Guid.Parse("14CEBA71-4B51-4777-9B17-46602CF66153")
}





            };
            modelBuilder.Entity<Walk>().HasData(walks);


            modelBuilder.Entity<Difficulty>().HasData(difficulties);
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl =
                        "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                },
                new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null,
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null,
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl =
                        "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl =
                        "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null,
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
