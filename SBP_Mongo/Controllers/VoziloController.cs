using Microsoft.AspNetCore.Mvc;
using SBP_Mongo.Models;
using SBP_Mongo.Services;
using SBP_Mongo.ViewModel;

namespace SBP_Mongo.Controllers
{
    public class VoziloController : Controller
    {
        private readonly VoziloService _voziloService;
        private readonly ModelService _modelService;
        private readonly LokacijaService lokacijaService;
        private readonly VrstaService vrstaService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VoziloController(VoziloService voziloService, ModelService modelService, LokacijaService lokacijaService, VrstaService vrstaService, IWebHostEnvironment webHostEnvironment)
        {
            _voziloService = voziloService;
            _modelService = modelService;
            this.lokacijaService = lokacijaService;
            this.vrstaService = vrstaService;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<ActionResult> Create()
        {
            ViewBag.Model = await _modelService.GetAsync();
            ViewBag.Lokacija = await lokacijaService.GetAsync();
            ViewBag.Vrsta = await vrstaService.GetAsync();
            return View();
        }

        // POST: VoziloController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VoziloViewModel model)
        {

            // Handle file upload
            if (model.PictureFile != null && model.PictureFile.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.PictureFile.FileName);
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.PictureFile.CopyToAsync(stream);
                }

                // Set the picture URL property
                model.PictureUrl = "/uploads/" + fileName;
            }

            // Create a new Vozilo object and map the properties from the view model
            var vozilo = new Vozilo
            {
                ModelVozila = model.ModelVozila,
                BrojSasije = model.BrojSasije,
                RegistracijskaOznaka = model.RegistracijskaOznaka,
                GodinaProizvodnje = model.GodinaProizvodnje,
                VrstaVozila = model.VrstaVozila,
                IdLokacije = model.IdLokacije,
                Gorivo = model.Gorivo,
                PictureUrl = model.PictureUrl
            };

            await _voziloService.CreateAsync(vozilo);

            return RedirectToAction(nameof(Index));

        }



        // GET: VoziloController/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || await _voziloService.GetAsync(id) == null)
            {
                return NotFound();
            }

            var vozilo = await _voziloService.GetAsync(id);
            if (vozilo == null)
            {
                return NotFound();
            }

            ViewBag.Model = await _modelService.GetAsync();
            ViewBag.Lokacija = await lokacijaService.GetAsync();
            ViewBag.Vrsta = await vrstaService.GetAsync();

            var viewModel = new VoziloViewModel
            {
                Id = vozilo.Id,
                ModelVozila = vozilo.ModelVozila,
                BrojSasije = vozilo.BrojSasije,
                RegistracijskaOznaka = vozilo.RegistracijskaOznaka,
                GodinaProizvodnje = vozilo.GodinaProizvodnje,
                VrstaVozila = vozilo.VrstaVozila,
                IdLokacije = vozilo.IdLokacije,
                Gorivo = vozilo.Gorivo
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, VoziloViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }


            // Retrieve the existing Vozilo object from the database
            var existingVozilo = await _voziloService.GetAsync(id);
            if (existingVozilo == null)
            {
                return NotFound();
            }

            // Update the properties with the new values
            existingVozilo.ModelVozila = viewModel.ModelVozila;
            existingVozilo.BrojSasije = viewModel.BrojSasije;
            existingVozilo.RegistracijskaOznaka = viewModel.RegistracijskaOznaka;
            existingVozilo.GodinaProizvodnje = viewModel.GodinaProizvodnje;
            existingVozilo.VrstaVozila = viewModel.VrstaVozila;
            existingVozilo.IdLokacije = viewModel.IdLokacije;
            existingVozilo.Gorivo = viewModel.Gorivo;

            // Handle file upload
            if (viewModel.PictureFile != null && viewModel.PictureFile.Length > 0)
            {
                // Generate a unique filename or use a naming convention
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(viewModel.PictureFile.FileName);
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName); // Specify the directory to save the file

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await viewModel.PictureFile.CopyToAsync(stream);
                }

                // Set the new picture URL property
                existingVozilo.PictureUrl = "/uploads/" + fileName;
            }

            // Save the updated Vozilo object to the database
            await _voziloService.UpdateAsync(existingVozilo.Id, existingVozilo);

            return RedirectToAction(nameof(Index));



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
