using Microsoft.AspNetCore.Mvc;
using SBP_Mongo.Models;
using SBP_Mongo.Services;

namespace SBP_Mongo.Controllers
{
    public class LokacijaController : Controller
    {
        private readonly LokacijaService _lokacijaService;

        public LokacijaController(LokacijaService lokacijaService)
        {
            _lokacijaService = lokacijaService;
        }
        // GET: LokacijaController
        public async Task<IActionResult> Index()
        {
            var lokacije = await _lokacijaService.GetAsync();
            return View(lokacije);
        }

        // GET: LokacijaController/Details/5

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _lokacijaService.GetAsync(id) == null)
            {
                return NotFound();
            }

            var lokacija = await _lokacijaService.GetAsync(id);
            if (lokacija == null)
            {
                return NotFound();
            }

            return View(lokacija);
        }

        // GET: LokacijaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LokacijaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Naziv")] Lokacija lokacija)
        {
            if (ModelState.IsValid)
            {
                await _lokacijaService.CreateAsync(lokacija);
                return RedirectToAction(nameof(Index));
            }
            return View(lokacija);
        }

        // GET: LokacijaController/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _lokacijaService.GetAsync(id) == null)
            {
                return NotFound();
            }

            var lokacija = await _lokacijaService.GetAsync(id);
            if (lokacija == null)
            {
                return NotFound();
            }

            return View(lokacija);
        }

        // POST: LokacijaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Naziv")] Lokacija lokacija)
        {
            if (id != lokacija.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _lokacijaService.UpdateAsync(lokacija.Id, lokacija);

                return RedirectToAction(nameof(Index));
            }
            return View(lokacija);
        }

        // GET: LokacijaController/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokacija = await _lokacijaService.GetAsync(id);
            if (lokacija == null)
            {
                return NotFound();
            }

            return View(lokacija);
        }

        // POST: Lokacija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var lokacija = await _lokacijaService.GetAsync(id);
            if (lokacija != null)
            {
                await _lokacijaService.RemoveAsync(id);
            }


            return RedirectToAction(nameof(Index));
        }
    }
}
