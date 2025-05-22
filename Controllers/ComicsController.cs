using ExamenComicsAWS.Models;
using ExamenComicsAWS.Repositories;
using ExamenComicsAWS.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamenComicsAWS.Controllers
{
    public class ComicsController : Controller
    {

        private RepositoryComics repo;
        private ServiceStorageS3 service;

        public ComicsController(RepositoryComics repo, ServiceStorageS3 service) {
            this.repo = repo;
            this.service = service;
        }

        public async Task<IActionResult> Index() {
            List<Comic> comics = await this.repo.GetComics();
            return View(comics);
        }

        public async Task<IActionResult> Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Comic comic, IFormFile file) {
            using (Stream stream = file.OpenReadStream()) {
                await this.service.UploadFileAsync
                (file.FileName, stream);
            }
            comic.Imagen = file.FileName;
            await this.repo.PostComic(comic.IdComic, comic.Nombre, comic.Imagen);
            return RedirectToAction("Index");
        }
    }
}
