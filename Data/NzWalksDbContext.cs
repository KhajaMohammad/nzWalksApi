

using Microsoft.EntityFrameworkCore;
using nzWalksApi.Models.Domain;
namespace nzWalksApi.Data

{
    public class NzWalksDbContext : DbContext
    {
        public  NzWalksDbContext(DbContextOptions dbContextOptions) :base(dbContextOptions) 
        {


        }

         public DbSet<Difficulty> difficulties { get; set; }
        public DbSet<Region>  regions { get; set; }
        public DbSet<Walk> walks { get; set; }  

    }
}
