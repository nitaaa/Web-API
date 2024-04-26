using Microsoft.EntityFrameworkCore;
using NZWalksUdemy.API.Data;
using NZWalksUdemy.API.Models.Domain;
using NZWalksUdemy.API.Models.DTO;

namespace NZWalksUdemy.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid Id)
        {
            var exRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if (exRegion == null) return null;

            dbContext.Regions.Remove(exRegion);
            await dbContext.SaveChangesAsync();

            return exRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();            
        }

        public async Task<Region?> GetByIDAsync(Guid Id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == Id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var exRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (exRegion == null) return null;

            exRegion.Code = region.Code;
            exRegion.Name = region.Name;
            exRegion.RegionImageUrl = region.RegionImageUrl;
            await dbContext.SaveChangesAsync();

            return exRegion;
        }
    }
}
