
using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Models.Domin;

namespace NZWalk.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalkDbContext dbContext;

        public SQLWalkRepository(NZWalkDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreatWalkAsync(Walk walk)
        {

            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }
        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            var walk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            { 
                return null;
            }
            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GelAlleWalkAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var SkipResult = (pageNumber - 1) * pageSize;

            return await walks.Skip(SkipResult).Take(pageSize).ToListAsync();
            //return await dbContext.walks.Include("Difficulty").Include("Region").ToListAsync();




        }

        public async Task<Walk> GetWalkByIdAsync(Guid id)
        {
            var walk = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            
            return walk;
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            var existingwalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingwalk == null)
            {
                return null;
            }
            existingwalk.DifficultyId = walk.DifficultyId;
            existingwalk.Description = walk.Description;
            existingwalk.RegionId = walk.RegionId;
            existingwalk.Name = walk.Name;
            existingwalk.LengthInKm = walk.LengthInKm; 
            existingwalk.WalkImageUrl = walk.WalkImageUrl;
            await dbContext.SaveChangesAsync();
            return existingwalk;
        }
    }
}
