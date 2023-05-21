using Microsoft.AspNetCore.Mvc;
using SBP_Mongo.Models;
using SBP_Mongo.Services;

namespace SBP_Mongo.Controllers
{
    public class VoziloController : Controller
    {
        private readonly VoziloService _voziloService;
        private readonly ModelService modelService;
        private readonly LokacijaService lokacijaService;
        private readonly VrstaService vrstaService;

        public VoziloController(VoziloService voziloService, ModelService modelService, LokacijaService lokacijaService, VrstaService vrstaService)
        {
            _voziloService = voziloService;
            this.modelService = modelService;
            this.lokacijaService = lokacijaService;
            this.vrstaService = vrstaService;
        }
        // GET: VoziloController
        public async Task<IActionResult> Index()
        {
            var vozila = await _voziloService.GetAsync();
            return View(vozila);
        }

        // GET: VoziloController/Details/5

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _voziloService.GetAsync(id) == null)
            {
                return NotFound();
            }

            var vozilo = await _voziloService.GetAsync(id);
            if (vozilo == null)
            {
                return NotFound();
            }

            return View(vozilo);
        }

        // GET: VoziloController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VoziloController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModelVozila,BrojSasije,RegistracijskaOznaka,GodinaProizvodnje,VrstaVozila,IdLokacije,Gorivo")] Vozilo vozilo)
        {
            if (ModelState.IsValid)
            {
                await _voziloService.CreateAsync(vozilo);
                return RedirectToAction(nameof(Index));
            }
            return View(vozilo);
        }

        // GET: VoziloController/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _voziloService.GetAsync(id) == null)
            {
                return NotFound();
            }

            var vozilo = await _voziloService.GetAsync(id);
            if (vozilo == null)
            {
                return NotFound();
            }

            return View(vozilo);
        }

        // POST: VoziloController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ModelVozila,BrojSasije,RegistracijskaOznaka,GodinaProizvodnje,VrstaVozila,IdLokacije,Gorivo")] Vozilo vozilo)
        {
            if (id != vozilo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _voziloService.UpdateAsync(vozilo.Id, vozilo);

                return RedirectToAction(nameof(Index));
            }
            return View(vozilo);
        }

        // GET: VoziloController/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vozilo = await _voziloService.GetAsync(id);
            if (vozilo == null)
            {
                return NotFound();
            }

            return View(vozilo);
        }

        // POST: Vozilo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var vozilo = await _voziloService.GetAsync(id);
            if (vozilo != null)
            {
                await _voziloService.RemoveAsync(id);
            }


            return RedirectToAction(nameof(Index));
        }
    }
}
