using Microsoft.AspNetCore.Mvc;
using SBP_Mongo.Models;
using SBP_Mongo.Services;

namespace SBP_Mongo.Controllers
{
    public class VrstaController : Controller
    {
        private readonly VrstaService _vrstaService;

        public VrstaController(VrstaService vrstaService)
        {
            _vrstaService = vrstaService;
        }
        // GET: VrstaController
        public async Task<IActionResult> Index()
        {
            var vrste = await _vrstaService.GetAsync();
            return View(vrste);
        }

        // GET: VrstaController/Details/5

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _vrstaService.GetAsync(id) == null)
            {
                return NotFound();
            }

            var vrsta = await _vrstaService.GetAsync(id);
            if (vrsta == null)
            {
                return NotFound();
            }

            return View(vrsta);
        }

        // GET: VrstaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VrstaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Naziv")] Vrsta vrsta)
        {
            if (ModelState.IsValid)
            {
                await _vrstaService.CreateAsync(vrsta);
                return RedirectToAction(nameof(Index));
            }
            return View(vrsta);
        }

        // GET: VrstaController/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _vrstaService.GetAsync(id) == null)
            {
                return NotFound();
            }

            var vrsta = await _vrstaService.GetAsync(id);
            if (vrsta == null)
            {
                return NotFound();
            }

            return View(vrsta);
        }

        // POST: VrstaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Naziv")] Vrsta vrsta)
        {
            if (id != vrsta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _vrstaService.UpdateAsync(vrsta.Id, vrsta);

                return RedirectToAction(nameof(Index));
            }
            return View(vrsta);
        }

        // GET: VrstaController/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrsta = await _vrstaService.GetAsync(id);
            if (vrsta == null)
            {
                return NotFound();
            }

            return View(vrsta);
        }

        // POST: Vrsta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var vrsta = await _vrstaService.GetAsync(id);
            if (vrsta != null)
            {
                await _vrstaService.RemoveAsync(id);
            }


            return RedirectToAction(nameof(Index));
        }
    }
}
