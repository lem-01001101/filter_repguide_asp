using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unicel_init2.Data;
using Unicel_init2.Models.Domain;
using Unicel_init2.Models.ViewModels;
using Unicel_init2.Repositories;

namespace Unicel_init2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminOEMController : Controller
    {
        private readonly IOEMRepository oemRepository;

        public AdminOEMController(IOEMRepository oemRepository)
        {
            this.oemRepository = oemRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddOEMRequest addOEMRequest)
        {
            // mapping addoemreq to oem dom model
            var oem = new OEM
            {
                Name = addOEMRequest.Name
            };

            await oemRepository.AddAsync(oem);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List() 
        {
            // use dbcontext to readm oem's
            var oem = await oemRepository.GetAllAsync();

            return View(oem);
        }

        [HttpGet]   
        public async Task<IActionResult> Edit(Guid id)
        {
            var oem = await oemRepository.GetAsync(id);
            if (oem != null)
            {
                var editOEMRequest = new EditOEMRequest
                {
                    Id = oem.Id,
                    Name = oem.Name
                };

                return View(editOEMRequest);
            }
            
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditOEMRequest editOEMRequest)
        {
            var oem = new OEM
            {
                Id = editOEMRequest.Id,
                Name = editOEMRequest.Name
            };

            var updatedOEM = await oemRepository.UpdateAsync(oem);

            if(updatedOEM != null)
            {
                //show success notif
            }
            else
            {
                // show error notif
            }

            // show error notif
            return RedirectToAction("Edit", new {  id = editOEMRequest.Id });
        }

        public async Task<IActionResult> Delete(EditOEMRequest editOEMRequest)
        {
            var deletedOEM = await oemRepository.DeleteAsync(editOEMRequest.Id);

            if(deletedOEM != null)
            {
                // show succes notif
                return RedirectToAction("List");
            }

               // show error notif
            return RedirectToAction("Edit", new { id = editOEMRequest.Id });
        }
    }
}
