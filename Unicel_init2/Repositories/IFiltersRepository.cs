using Unicel_init2.Models.Domain;

namespace Unicel_init2.Repositories
{
    public interface IFiltersRepository
    {
        Task<IEnumerable<Filters>> GetAllAsync();
        Task<Filters?> GetAsync(Guid Id);
        Task<Filters> AddAsync(Filters filters);
        Task<Filters?> UpdateAsync(Filters filters);
        Task<Filters?> DeleteAsync(Guid Id);

    }
}
