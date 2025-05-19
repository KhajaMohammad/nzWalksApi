using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace nzWalksApi.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {

        public NZWalksAuthDbContext (DbContextOptions<NZWalksAuthDbContext> options) : base (options)
        {


            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var  readerRoleId = "660e950b-158f-4321-9a87-a700a51f15f5";
            var writerRoleId = "d87569a2-1439-428b-b5b4-ceb2bea4e783";
            var roles = new List<IdentityRole> {


                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()

                },

                 new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()

                }



            };

            builder.Entity<IdentityRole>().HasData(roles);

        }


    }
}
