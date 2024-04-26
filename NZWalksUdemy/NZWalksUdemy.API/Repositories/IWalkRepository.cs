using Microsoft.AspNetCore.Mvc;
using NZWalksUdemy.API.Models.Domain;

namespace NZWalksUdemy.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortby = null, bool isAsc = true, int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> GetByIDAsync(Guid WalkId);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk?> UpdateAsync(Guid WalkId, Walk walk);
        Task<Walk?> DeleteAsync(Guid WalkId);
    }
}
