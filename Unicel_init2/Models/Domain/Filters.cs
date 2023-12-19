namespace Unicel_init2.Models.Domain
{
    public class Filters
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string TopEndCap { get; set; }
        public string BottomEndCap { get; set; }
        public string PleatCount { get; set; }
        public string Media { get; set; }
        public string OD { get; set; }
        public string Length { get; set; }

        public ICollection<OEM> OEM { get; set; }
    }
}
