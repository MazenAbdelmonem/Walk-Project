using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Models.Domin;

namespace NZWalk.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreatWalkAsync(Walk walk);
        Task<List<Walk>> GelAlleWalkAsync(string? filterOn= null, string? filterQuery= null, string? sortBy= null, bool isAscending= true, int pageNumber = 1, int bageSize =1000);
        Task<Walk?> GetWalkByIdAsync(Guid id);
        Task<Walk?> UpdateWalkAsync(Guid id, Walk walk);
        Task<Walk?> DeleteWalkAsync(Guid id);
    }
}
