using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Models.Domin;
using NZWalk.API.Models.DTO;
using System.Security.Cryptography;

namespace NZWalk.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalkDbContext _dbcontext;

        public SQLRegionRepository(NZWalkDbContext dbContext) 
        { 
            _dbcontext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbcontext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
           return await _dbcontext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }


        public async Task<Region> CreatAsync(Region region)
        {
            await _dbcontext.Regions.AddAsync(region);
            await _dbcontext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null) 
            {
                return null;
            }
            existingRegion.Name = region.Name;
            existingRegion.code = region.code;
            existingRegion.RegioImageUrl = region.RegioImageUrl;
            await _dbcontext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await _dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            _dbcontext.Regions.Remove(existingRegion);
            await _dbcontext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
