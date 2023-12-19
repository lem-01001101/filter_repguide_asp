using Microsoft.AspNetCore.Mvc.Rendering;

namespace Unicel_init2.Models.ViewModels
{
    public class EditFilterRequest
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

        // display filters
        public IEnumerable<SelectListItem> OEM { get; set; }
        // collect tag
        public string[] SelectedOEM { get; set; } = Array.Empty<string>();
    }
}
