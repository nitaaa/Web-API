using NZWalksUdemy.API.Models.Domain;

namespace NZWalksUdemy.API.Repositories
{
    public class InMemoryRegionRepository : IRegionRepository
    {
        public async Task<List<Region>> GetAllAsync()
        {
            return new List<Region>{
                new Region() { Id = Guid.NewGuid(), Name = "Auckland", Code = "AKL", RegionImageUrl="https://unsplash.com/photos/overlooking-island-L-mvjXO1WAM"},
                new Region() { Id = Guid.NewGuid(), Name = "Wellington", Code = "WLG", RegionImageUrl = "https://unsplash.com/photos/a-path-through-a-forest-with-lots-of-trees-n0WEwn6gtqc" }
            };
        }
    }
}
