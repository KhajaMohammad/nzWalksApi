using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using nzWalksApi.Data;
using nzWalksApi.Models.Domain;

namespace nzWalksApi.Repositories
{
    public class SqlRegionRepository : IRegionRepository
    {
        private readonly NzWalksDbContext nzWalksDbContext; // DbContext for interacting with the database

        // Constructor initializes the repository with the DbContext
        public SqlRegionRepository(NzWalksDbContext nzWalksDbContext)
        {
            this.nzWalksDbContext = nzWalksDbContext;
        }

        // Asynchronously creates a new region in the database
        public async Task<Region> createRegionAsync(Region region)
        {
            // Add the region entity to the DbContext's regions collection (but it hasn't been saved yet)
            await nzWalksDbContext.regions.AddAsync(region);

            // Commit the changes to the database
            await nzWalksDbContext.SaveChangesAsync();

            // Return the newly created region
            return region;
        }

        // Asynchronously retrieves all regions from the database
        public async Task<List<Region>> GetAllAsync()
        {
            // Use LINQ to fetch all records from the regions table and convert them to a list
            return await nzWalksDbContext.regions.ToListAsync();
        }

        // Asynchronously retrieves a region by its ID from the database
        public async Task<Region?> GetByIdAsync(Guid Id)
        {
            // Use LINQ to search for the first region that matches the provided Id
            return await nzWalksDbContext.regions.FirstOrDefaultAsync(x => x.Id == Id);
        }

        // Asynchronously updates an existing region's information in the database
        public async Task<Region?> updateAsync(Guid Id, Region region)
        {
            // Fetch the existing region to be updated
            var existingRegion = await nzWalksDbContext.regions.FirstOrDefaultAsync(x =>
                x.Id == Id
            );

            // If the region is found, update its properties
            if (existingRegion != null)
            {
                existingRegion.RegionImageUrl = region.RegionImageUrl; // Update region's image URL
                existingRegion.Code = region.Code; // Update region's code
                existingRegion.Name = region.Name; // Update region's name

                // Commit the changes to the database
                await nzWalksDbContext.SaveChangesAsync();

                // Return the updated region
                return existingRegion;
            }

            // If the region is not found, return null
            return null;
        }

        // Asynchronously deletes a region from the database
        public async Task<Region?> Delete(Guid Id)
        {
            // Retrieve the region to be deleted
            var existingRegion = await nzWalksDbContext.regions.FirstOrDefaultAsync(x =>
                x.Id == Id
            );

            // If the region does not exist, return null
            if (existingRegion == null)
            {
                return null;
            }

            // Remove the region from the DbContext's regions collection
            nzWalksDbContext.regions.Remove(existingRegion);

            // Commit the changes to the database
            await nzWalksDbContext.SaveChangesAsync();

            // Return the deleted region
            return existingRegion;
        }
    }
}
