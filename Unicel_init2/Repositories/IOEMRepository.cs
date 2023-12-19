using Unicel_init2.Models.Domain;

namespace Unicel_init2.Repositories
{
    public interface IOEMRepository
    {
        Task<IEnumerable<OEM>> GetAllAsync();

        Task<OEM?> GetAsync(Guid id);

        Task<OEM> AddAsync(OEM oem);

        Task<OEM?> UpdateAsync(OEM oem);

        Task<OEM?> DeleteAsync(Guid id);
    }
}
