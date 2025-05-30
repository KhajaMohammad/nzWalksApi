﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using nzWalksApi.Data;

#nullable disable

namespace nzWalksApi.Migrations
{
    [DbContext(typeof(NzWalksDbContext))]
    partial class NzWalksDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("nzWalksApi.Models.Domain.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b431dc54-e0ea-474d-9e7b-3f51b8de0f59"),
                            Name = "Easy"
                        },
                        new
                        {
                            Id = new Guid("f556bf05-f365-4ac8-91dc-0534245f87e7"),
                            Name = "Medium"
                        },
                        new
                        {
                            Id = new Guid("a2901401-f5d8-489f-87dc-21eb35874b3f"),
                            Name = "Hard"
                        });
                });

            modelBuilder.Entity("nzWalksApi.Models.Domain.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FileSizeInBytes")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("image");
                });

            modelBuilder.Entity("nzWalksApi.Models.Domain.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                            Code = "AKL",
                            Name = "Auckland",
                            RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                        },
                        new
                        {
                            Id = new Guid("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                            Code = "NTL",
                            Name = "Northland"
                        },
                        new
                        {
                            Id = new Guid("14ceba71-4b51-4777-9b17-46602cf66153"),
                            Code = "BOP",
                            Name = "Bay Of Plenty"
                        },
                        new
                        {
                            Id = new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                            Code = "WGN",
                            Name = "Wellington",
                            RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                        },
                        new
                        {
                            Id = new Guid("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                            Code = "NSN",
                            Name = "Nelson",
                            RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                        },
                        new
                        {
                            Id = new Guid("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                            Code = "STL",
                            Name = "Southland"
                        });
                });

            modelBuilder.Entity("nzWalksApi.Models.Domain.Walk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("LengthInKm")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalkImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("walks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("327aa9f7-26f7-4ddb-8047-97464374bb63"),
                            Description = "This scenic walk takes you around the top of Mount Victoria, offering stunning views of Wellington and its harbor.",
                            DifficultyId = new Guid("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"),
                            LengthInKm = 3.5,
                            Name = "Mount Victoria Loop",
                            RegionId = new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                            WalkImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                        },
                        new
                        {
                            Id = new Guid("1cc5f2bc-ff4b-47c0-a475-1add56c6497b"),
                            Description = "This walk takes you along the wild and rugged coastline of Makara Beach, with breathtaking views of the Tasman Sea.",
                            DifficultyId = new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                            LengthInKm = 8.1999999999999993,
                            Name = "Makara Beach Walkway",
                            RegionId = new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                            WalkImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                        },
                        new
                        {
                            Id = new Guid("09601132-f92d-457c-b47e-da90e117b33c"),
                            Description = "Explore the beautiful Botanic Garden of Wellington on this leisurely walk, with a wide variety of plants and flowers to admire.",
                            DifficultyId = new Guid("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"),
                            LengthInKm = 2.0,
                            Name = "Botanic Garden Walk",
                            RegionId = new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                            WalkImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                        },
                        new
                        {
                            Id = new Guid("30d654c7-89ac-4704-8333-5065b740150b"),
                            Description = "This walk takes you to the summit of Mount Eden, the highest natural point in Auckland, with panoramic views of the city.",
                            DifficultyId = new Guid("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"),
                            LengthInKm = 2.0,
                            Name = "Mount Eden Summit Walk",
                            RegionId = new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                            WalkImageUrl = "https://images.pexels.com/photos/5342974/pexels-photo-5342974.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                        },
                        new
                        {
                            Id = new Guid("f7578324-f025-4c86-83a9-37a7f3d8fe81"),
                            Description = "Explore the beautiful Cornwall Park on this leisurely walk, with a wide variety of trees, gardens, and animals to admire.",
                            DifficultyId = new Guid("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"),
                            LengthInKm = 3.0,
                            Name = "Cornwall Park Walk",
                            RegionId = new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                            WalkImageUrl = "https://images.pexels.com/photos/5342974/pexels-photo-5342974.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                        },
                        new
                        {
                            Id = new Guid("bdf28703-6d0e-4822-ad8b-e2923f4e95a2"),
                            Description = "This coastal walk takes you along the beautiful beaches of Takapuna and Milford, with stunning views of Rangitoto Island.",
                            DifficultyId = new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                            LengthInKm = 5.0,
                            Name = "Takapuna to Milford Coastal Walk",
                            RegionId = new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                            WalkImageUrl = "https://images.pexels.com/photos/5342974/pexels-photo-5342974.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                        },
                        new
                        {
                            Id = new Guid("43132402-3d5e-467a-8cde-351c5c7c5dde"),
                            Description = "This walk takes you to the geographical centre of New Zealand, with stunning views of Nelson and its surroundings.",
                            DifficultyId = new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                            LengthInKm = 1.0,
                            Name = "Centre of New Zealand Walkway",
                            RegionId = new Guid("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                            WalkImageUrl = "https://images.pexels.com/photos/808466/pexels-photo-808466.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                        },
                        new
                        {
                            Id = new Guid("1ea0b064-2d44-4324-91ee-6dd86c91b713"),
                            Description = "Explore the picturesque Maitai Valley on this easy walk, with a tranquil river and native bush to enjoy.",
                            DifficultyId = new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                            LengthInKm = 5.0,
                            Name = "Maitai Valley Walk",
                            RegionId = new Guid("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                            WalkImageUrl = "https://images.pexels.com/photos/808466/pexels-photo-808466.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                        },
                        new
                        {
                            Id = new Guid("04ab77f0-e145-4fbf-b641-989df24e5573"),
                            Description = "This coastal walk takes you along the unique Boulder Bank, a long narrow bar of rocks that extends into Tasman Bay.",
                            DifficultyId = new Guid("f808ddcd-b5e5-4d80-b732-1ca523e48434"),
                            LengthInKm = 8.0,
                            Name = "Boulder Bank Walkway",
                            RegionId = new Guid("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                            WalkImageUrl = "https://images.pexels.com/photos/808466/pexels-photo-808466.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                        },
                        new
                        {
                            Id = new Guid("b5aa2791-3616-4db6-ab33-c54d03d17f62"),
                            Description = "This walk takes you to the summit of Mount Maunganui, with stunning views of the ocean and surrounding landscape.",
                            DifficultyId = new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                            LengthInKm = 3.0,
                            Name = "Mount Maunganui Summit Walk",
                            RegionId = new Guid("14ceba71-4b51-4777-9b17-46602cf66153"),
                            WalkImageUrl = "https://images.pexels.com/photos/808466/pexels-photo-808466.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                        },
                        new
                        {
                            Id = new Guid("2d9d6604-bef9-4b0a-805d-630240a29595"),
                            Description = "Enjoy panoramic views of Tauranga and Mount Maunganui on this walk through the Papamoa Hills, with a mix of bush and open farmland.",
                            DifficultyId = new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                            LengthInKm = 5.0,
                            Name = "The Papamoa Hills Regional Park Walk",
                            RegionId = new Guid("14ceba71-4b51-4777-9b17-46602cf66153"),
                            WalkImageUrl = "https://images.pexels.com/photos/808466/pexels-photo-808466.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                        },
                        new
                        {
                            Id = new Guid("135a6e58-969f-47e1-8278-d7fbf2b3bd69"),
                            Description = "Explore the lush and peaceful White Pine Bush on this easy walk, with a variety of native flora and fauna to discover.",
                            DifficultyId = new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                            LengthInKm = 2.0,
                            Name = "The White Pine Bush Track",
                            RegionId = new Guid("14ceba71-4b51-4777-9b17-46602cf66153"),
                            WalkImageUrl = "https://images.pexels.com/photos/808466/pexels-photo-808466.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                        });
                });

            modelBuilder.Entity("nzWalksApi.Models.Domain.Walk", b =>
                {
                    b.HasOne("nzWalksApi.Models.Domain.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("nzWalksApi.Models.Domain.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}
