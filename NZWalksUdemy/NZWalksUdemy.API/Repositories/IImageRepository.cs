using NZWalksUdemy.API.Models.Domain;

namespace NZWalksUdemy.API.Repositories
{
    public interface IImageRepository
    {
        public Task<Image> Upload(Image image);
    }
}
