using Microsoft.AspNetCore.Mvc;
using SBP_Mongo.Models;
using SBP_Mongo.Services;

namespace SBP_Mongo.Controllers
{
    public class MarkaController : Controller
    {
        private readonly MarkaService _markaService;

        public MarkaController(MarkaService markaService)
        {
            _markaService = markaService;
        }
        // GET: MarkaController
        public async Task<IActionResult> Index()
        {
            var marke = await _markaService.GetAsync();
            return View(marke);
        }

        // GET: MarkaController/Details/5

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _markaService.GetAsync(id) == null)
            {
                return NotFound();
            }

            var marka = await _markaService.GetAsync(id);
            if (marka == null)
            {
                return NotFound();
            }

            return View(marka);
        }

        // GET: MarkaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MarkaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Naziv")] Marka marka)
        {
            if (ModelState.IsValid)
            {
                await _markaService.CreateAsync(marka);
                return RedirectToAction(nameof(Index));
            }
            return View(marka);
        }

        // GET: MarkaController/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _markaService.GetAsync(id) == null)
            {
                return NotFound();
            }

            var marka = await _markaService.GetAsync(id);
            if (marka == null)
            {
                return NotFound();
            }

            return View(marka);
        }

        // POST: MarkaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Naziv")] Marka marka)
        {
            if (id != marka.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _markaService.UpdateAsync(marka.Id, marka);

                return RedirectToAction(nameof(Index));
            }
            return View(marka);
        }

        // GET: MarkaController/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marka = await _markaService.GetAsync(id);
            if (marka == null)
            {
                return NotFound();
            }

            return View(marka);
        }

        // POST: Marka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var marka = await _markaService.GetAsync(id);
            if (marka != null)
            {
                await _markaService.RemoveAsync(id);
            }


            return RedirectToAction(nameof(Index));
        }
    }
}
