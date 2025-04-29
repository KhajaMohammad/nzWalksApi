using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using nzWalksApi.Data;
using nzWalksApi.Models.Domain;
namespace nzWalksApi.Repositories

{
    public class SqlRegionRepository : IRegionRepository

    {
        private readonly NzWalksDbContext nzWalksDbContext;

        public SqlRegionRepository(NzWalksDbContext nzWalksDbContext)
        {
            this.nzWalksDbContext = nzWalksDbContext;
        }

        public async Task<Region> createRegionAsync(Region region)
        {
           await nzWalksDbContext.regions.AddAsync(region);
           await nzWalksDbContext.SaveChangesAsync();

            return region;
        }

        public async  Task<List<Region>> GetAllAsync()
        {
           return  await nzWalksDbContext.regions.ToListAsync();   

        }

        public async Task<Region?> GetByIdAsync(Guid Id)
        {
            return await nzWalksDbContext.regions.FirstOrDefaultAsync(x => x.Id == Id);


        }

        public async  Task<Region?> updateAsync(Guid Id, Region region)
        {
            var existingRegion = await nzWalksDbContext.regions.FirstOrDefaultAsync(x=>x.Id== Id);

            if (existingRegion != null)
            {
                existingRegion.RegionImageUrl= region.RegionImageUrl;
                
                existingRegion.Code=region.Code;
                existingRegion.Name=region.Name;

                await nzWalksDbContext.SaveChangesAsync();

                return existingRegion;

            }
            return null;

        }


        public async Task<Region?> Delete(Guid Id)
        {

            var existingRegion = await nzWalksDbContext.regions.FirstOrDefaultAsync(x=>x.Id== Id);
            if (existingRegion == null)
            {
                return null;
            }
            nzWalksDbContext.regions.Remove(existingRegion);
            await nzWalksDbContext.SaveChangesAsync();
            return existingRegion;

        }
    }
}
