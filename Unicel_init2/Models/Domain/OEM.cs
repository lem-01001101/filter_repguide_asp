namespace Unicel_init2.Models.Domain
{
    public class OEM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Filters> Filters { get; set; }
    }
}
