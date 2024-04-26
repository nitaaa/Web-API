using Microsoft.EntityFrameworkCore;
using NZWalksUdemy.API.Data;
using NZWalksUdemy.API.Models.Domain;

namespace NZWalksUdemy.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid WalkId)
        {
            var exWalk = await dbContext.Walks.FirstOrDefaultAsync(w => w.Id == WalkId);
            if (exWalk == null) return null;

            dbContext.Walks.Remove(exWalk);
            await dbContext.SaveChangesAsync();
            return exWalk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortby = null, bool isAsc = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walkList = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            // Filter
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery) )
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walkList = walkList.Where(x => x.Name.Contains(filterQuery));
                }
            }
            // Sort
            if (!string.IsNullOrWhiteSpace(sortby))
            {
                if (sortby.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walkList = isAsc ? walkList.OrderBy(x => x.Name) : walkList.OrderByDescending(x => x.Name);
                }
                else if (sortby.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walkList = isAsc ? walkList.OrderBy(x => x.LengthInKm) : walkList.OrderByDescending(x => x.LengthInKm);
                }
            }
            // Pagination
            var skipped = (pageNumber - 1) * pageSize;


            return await walkList.Skip(skipped).Take(pageSize).ToListAsync();
            //return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            //return await dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).ToListAsync();
        }

        public async Task<Walk?> GetByIDAsync(Guid WalkId)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(w => w.Id == WalkId);
        }

        public async Task<Walk?> UpdateAsync(Guid WalkId, Walk walk)
        {
            var exWalk = await dbContext.Walks.FirstOrDefaultAsync(w => w.Id == WalkId);
            if (exWalk == null) return null;

            exWalk.Name = walk.Name;
            exWalk.Description = walk.Description;
            exWalk.LengthInKm = walk.LengthInKm;
            exWalk.WalkImageUrl = walk.WalkImageUrl;
            exWalk.DifficultyId = walk.DifficultyId;
            exWalk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();
            return exWalk;
        }
    }
}
