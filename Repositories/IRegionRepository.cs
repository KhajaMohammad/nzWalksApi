using nzWalksApi.Models.Domain;

namespace nzWalksApi.Repositories
{
    public interface IRegionRepository
    {
        public Task<List<Region>> GetAllAsync();

        public Task<Region?> GetByIdAsync(Guid Id);

        public Task<Region> createRegionAsync(Region region);

        public Task<Region?> updateAsync(Guid Id, Region region);

        public Task<Region?> Delete(Guid Id);
    }
}
