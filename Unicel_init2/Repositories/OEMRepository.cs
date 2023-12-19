using Microsoft.EntityFrameworkCore;
using Unicel_init2.Data;
using Unicel_init2.Models.Domain;

namespace Unicel_init2.Repositories
{
    public class OEMRepository : IOEMRepository
    {
        private readonly UnicelDbContext unicelDbContext;

        public OEMRepository(UnicelDbContext unicelDbContext)
        {
            this.unicelDbContext = unicelDbContext;
        }
        public async Task<OEM> AddAsync(OEM oem)
        {
            await unicelDbContext.OEM.AddAsync(oem);
            await unicelDbContext.SaveChangesAsync();
            return oem;
        }

        public async Task<OEM?> DeleteAsync(Guid id)
        {
            var existingOEM = await unicelDbContext.OEM.FindAsync(id);

            if (existingOEM != null)
            {
                unicelDbContext.OEM.Remove(existingOEM);
                await unicelDbContext.SaveChangesAsync();

                // show success notif
                return existingOEM;
            }

            return null;
        }

        public async Task<IEnumerable<OEM>> GetAllAsync()
        {
            return await unicelDbContext.OEM.ToListAsync();
        }

        public Task<OEM?> GetAsync(Guid id)
        {
            return unicelDbContext.OEM.FirstOrDefaultAsync(x  => x.Id == id);
        }

        public async Task<OEM?> UpdateAsync(OEM oem)
        {
            var existingOEM = await unicelDbContext.OEM.FindAsync(oem.Id);

            if (existingOEM != null)
            {
                existingOEM.Name = oem.Name;

                await unicelDbContext.SaveChangesAsync();

                return existingOEM;
            }

            return null;
        }
    }
}
