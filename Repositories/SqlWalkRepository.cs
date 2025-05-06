namespace nzWalksApi.Repositories
{
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using nzWalksApi.Data;
    using nzWalksApi.Models.Domain;

    public class SqlWalkRepository : IWalkRepository
    {
        private readonly NzWalksDbContext dbContext;

        public SqlWalkRepository(NzWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.walks.AddAsync(walk);

            await dbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn  , string? filterQuery ,string? sortBy , bool isAscending = true,int pageNumber =1, int pageSize=1000 )
        {
            //var allWalks = await dbContext
            //    .walks.Include("Difficulty")
            //    .Include("Region")
            //    .ToListAsync();


            var walks =  dbContext.walks.Include("Difficulty")
                .Include("Region").AsQueryable();


            //Filtrering

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery)==false) {

                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase)) {


                    walks =  walks.Where(x => x.Name.Contains(filterQuery) );

                    

            }
            }

            //Sorting

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }

                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase)){

                    walks = isAscending ? walks.OrderBy(x=>x.LengthInKm) : walks.OrderByDescending(x=>x.LengthInKm);

                }

            }

            //Pagination


            var skipResults = (pageNumber - 1) * pageSize;






            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();

            
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid Id)
        {
            var allWalk = await dbContext
                .walks.Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == Id);
            if (allWalk == null)
            {
                return null;
            }

            return allWalk;
        }

        public async Task<Walk?> UpdateWalk(Walk walk, Guid Id)
        {
            var existingWalk = await dbContext
                .walks.Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == Id);
            if (existingWalk == null)
            {
                return null;
            }
            ;

            {
                existingWalk.Description = walk.Description;

                existingWalk.Name = walk.Name;
                existingWalk.LengthInKm = walk.LengthInKm;
                existingWalk.DifficultyId = walk.DifficultyId;
                existingWalk.RegionId = walk.RegionId;
            }
            await dbContext.SaveChangesAsync();

            return existingWalk;
        }



        public   async Task<Walk?> DeleteWalkByIdAsync(Guid Id)
        {
            var deleteDomainModel = await  dbContext.walks.FirstOrDefaultAsync(x=>x.Id == Id);
            if(deleteDomainModel == null)
            {
                return null;
            }

            dbContext.walks.Remove(deleteDomainModel);
            await dbContext.SaveChangesAsync();

            return deleteDomainModel;


        }
    }
}
