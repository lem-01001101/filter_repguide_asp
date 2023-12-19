using Microsoft.EntityFrameworkCore;
using Unicel_init2.Data;
using Unicel_init2.Models.Domain;

namespace Unicel_init2.Repositories
{
    public class FilterRepository : IFiltersRepository
    {
        private readonly UnicelDbContext unicelDbContext;
        public FilterRepository(UnicelDbContext unicelDbContext)
        {
            this.unicelDbContext = unicelDbContext;
        }

        public async Task<Filters> AddAsync(Filters filters)
        {
            await unicelDbContext.AddAsync(filters);
            await unicelDbContext.SaveChangesAsync();
            return filters;
        }

        public async Task<Filters?> DeleteAsync(Guid Id)
        {
            var existingFilter = await unicelDbContext.Filters.FindAsync(Id);

            if(existingFilter != null)
            {
                unicelDbContext.Filters.Remove(existingFilter);
                await unicelDbContext.SaveChangesAsync();
                return existingFilter;
            }

            return null;
        }

        public async Task<IEnumerable<Filters>> GetAllAsync()
        {
            return await unicelDbContext.Filters.Include(x => x.OEM).ToListAsync();
        }

        public async Task<Filters?> GetAsync(Guid Id)
        {
            return await unicelDbContext.Filters.Include(x => x.OEM).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Filters?> UpdateAsync(Filters filters)
        {
            var existingFilter = await unicelDbContext.Filters.Include(x => x.OEM).FirstOrDefaultAsync(x => x.Id == filters.Id);

            if (existingFilter != null)
            {
                existingFilter.Id = filters.Id;
                existingFilter.OEM = filters.OEM;
                existingFilter.Name = filters.Name;
                existingFilter.Description = filters.Description;
                existingFilter.TopEndCap = filters.TopEndCap;
                existingFilter.BottomEndCap = filters.BottomEndCap;
                existingFilter.PleatCount = filters.PleatCount;
                existingFilter.Media = filters.Media;
                existingFilter.OD = filters.OD;
                existingFilter.Length = filters.Length;

                await unicelDbContext.SaveChangesAsync();
                return existingFilter;
            }

            return null;
        }
    }
}
