using Microsoft.EntityFrameworkCore; // Required for Entity Framework operations.
using nzWalksApi.Data; // The DbContext class used to interact with the database.
using nzWalksApi.Models.Domain; // Domain models (e.g., Walk) that represent the entities in the database.

namespace nzWalksApi.Repositories
{
    public class SqlWalkRepository : IWalkRepository
    {
        private readonly NzWalksDbContext dbContext; // The DbContext instance used to interact with the database.

        // Constructor to inject the DbContext dependency.
        public SqlWalkRepository(NzWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Create a new walk and save it to the database.
        public async Task<Walk> CreateAsync(Walk walk)
        {
            // Asynchronously adding the new walk entity to the database.
            await dbContext.walks.AddAsync(walk);

            // Persist the changes to the database.
            await dbContext.SaveChangesAsync();

            // Return the saved walk entity with the assigned database ID.
            return walk;
        }

        // Fetch all walks with optional filters, sorting, and pagination.
        public async Task<List<Walk>> GetAllAsync(
            string? filterOn,
            string? filterQuery,
            string? sortBy,
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 1000
        )
        {
            // Starting query for fetching walks, including related data like Difficulty and Region.
            var walks = dbContext.walks.Include("Difficulty").Include("Region").AsQueryable();

            // Filtering: Check if the user has requested to filter by a specific field.
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    // Filtering walks by name.
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Sorting: Sorting the results based on the 'sortBy' field and order (ascending or descending).
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    // Sort by Name either ascending or descending.
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    // Sort by Length (distance) either ascending or descending.
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination: Skip results based on page number and page size.
            var skipResults = (pageNumber - 1) * pageSize;

            // Return the requested subset of walks based on pagination.
            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        // Fetch a single walk by its ID.
        public async Task<Walk?> GetWalkByIdAsync(Guid Id)
        {
            // Fetch a walk and its related data (Difficulty, Region) by ID.
            var allWalk = await dbContext.walks.Include("Difficulty").Include("Region")
                .FirstOrDefaultAsync(x => x.Id == Id);

            // Return the walk if found, otherwise return null.
            return allWalk;
        }

        // Update an existing walk with new data.
        public async Task<Walk?> UpdateWalk(Walk walk, Guid Id)
        {
            // Fetch the existing walk by ID to update.
            var existingWalk = await dbContext.walks.Include("Difficulty").Include("Region")
                .FirstOrDefaultAsync(x => x.Id == Id);

            // If no walk is found, return null to indicate it could not be updated.
            if (existingWalk == null)
            {
                return null;
            }

            // Update the existing walk's properties with the new data.
            existingWalk.Description = walk.Description;
            existingWalk.Name = walk.Name;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            // Save the changes to the database.
            await dbContext.SaveChangesAsync();

            // Return the updated walk entity.
            return existingWalk;
        }

        // Delete a walk by its ID.
        public async Task<Walk?> DeleteWalkByIdAsync(Guid Id)
        {
            // Fetch the walk to delete by ID.
            var deleteDomainModel = await dbContext.walks.FirstOrDefaultAsync(x => x.Id == Id);

            // If no walk is found to delete, return null.
            if (deleteDomainModel == null)
            {
                return null;
            }

            // Remove the walk from the database.
            dbContext.walks.Remove(deleteDomainModel);

            // Persist the changes to the database.
            await dbContext.SaveChangesAsync();

            // Return the deleted walk entity as confirmation.
            return deleteDomainModel;
        }
    }
}
