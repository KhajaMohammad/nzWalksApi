using nzWalksApi.Models.Domain;

namespace nzWalksApi.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
