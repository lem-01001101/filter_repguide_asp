using Unicel_init2.Repositories;
using Microsoft.AspNetCore.Mvc;
using Unicel_init2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Unicel_init2.Models.Domain;
using Microsoft.AspNetCore.Authorization;

namespace Unicel_init2.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminFiltersController : Controller
    {
        private readonly IOEMRepository oemRepository;
        private readonly IFiltersRepository filtersRepository;

        public AdminFiltersController(IOEMRepository oemRepository, IFiltersRepository filtersRepository)
        {
            this.oemRepository = oemRepository;
            this.filtersRepository = filtersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // get filters from repository
            var oem = await oemRepository.GetAllAsync();

            var model = new AddFilterRequest
            {
                OEM = oem.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddFilterRequest addFilterRequest)
        {
            var filter = new Filters
            {
                Name = addFilterRequest.Name,
                TopEndCap = addFilterRequest.TopEndCap,
                BottomEndCap = addFilterRequest.BottomEndCap,
                PleatCount = addFilterRequest.PleatCount,
                Media = addFilterRequest.Media,
                Description = addFilterRequest.Description,
                OD = addFilterRequest.OD,
                Length = addFilterRequest.Length
            };

            // map tags from selected filters

            var selectedOEM = new List<OEM>();
            foreach (var selectedFilterId in addFilterRequest.SelectedOEM)
            {
                var selectedOEMIdAsGuid = Guid.Parse(selectedFilterId);
                var existingOEM = await oemRepository.GetAsync(selectedOEMIdAsGuid);

                if(existingOEM != null)
                {
                    selectedOEM.Add(existingOEM);
                }
            }

            // mapping filters back to domain model
            filter.OEM = selectedOEM;

            await filtersRepository.AddAsync(filter);

            return RedirectToAction("Add");
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            // call repo
            var filters = await filtersRepository.GetAllAsync();

            return View(filters);
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            // retrieve result from repo
            var filter = await filtersRepository.GetAsync(id);
            var oemDomainModel = await filtersRepository.GetAllAsync();

            // map domain model into view model
            if(filter != null)
            {
                var model = new EditFilterRequest
                {
                    Id = filter.Id,
                    Name = filter.Name,
                    Description = filter.Description,
                    TopEndCap = filter.TopEndCap,
                    BottomEndCap = filter.BottomEndCap,
                    PleatCount = filter.PleatCount,
                    Media = filter.Media,
                    OD = filter.OD,
                    Length = filter.Length,
                    OEM = oemDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedOEM = filter.OEM.Select(x => x.Id.ToString()).ToArray()
                };
                return View(model);
            }
            
            // pass data to view
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditFilterRequest editFilterRequest)
        {
            // map view model back to domain model
            var filtersDomainModel = new Filters
            {
                Id = editFilterRequest.Id,
                Name = editFilterRequest.Name,
                Description = editFilterRequest.Description,
                TopEndCap = editFilterRequest.TopEndCap,
                BottomEndCap = editFilterRequest.BottomEndCap,
                PleatCount = editFilterRequest.PleatCount,
                Media = editFilterRequest.Media,
                OD = editFilterRequest.OD,
                Length = editFilterRequest.Length
            };

            // map oems to domain model
            var selectedOEMs = new List<OEM>();
            foreach (var selectedOEM in editFilterRequest.SelectedOEM)
            {
                if(Guid.TryParse(selectedOEM, out var oem))
                {
                    var foundOEM = await oemRepository.GetAsync(oem);

                    if(foundOEM != null)
                    {
                        selectedOEMs.Add(foundOEM);
                    }
                }
            }

            filtersDomainModel.OEM = selectedOEMs;

            // submit to repository to update
            var updatedFilter = await filtersRepository.UpdateAsync(filtersDomainModel);

            if(updatedFilter != null)
            {
                return RedirectToAction("Edit");
            }

            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditFilterRequest editFilterRequest)
        {
            // talk to repo 
            var deletedFilter = await filtersRepository.DeleteAsync(editFilterRequest.Id);
            
            if(deletedFilter != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editFilterRequest.Id });
        }
    }
}
