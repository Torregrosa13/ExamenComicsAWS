using ExamenComicsAWS.Models;
using ExamenComicsAWS.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExamenComicsAWS.Controllers
{
    public class ComicsController : Controller
    {

        private RepositoryComics repo;

        public ComicsController(RepositoryComics repo) {
            this.repo = repo;
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
        public async Task<IActionResult> Create(Comic comic) {
            await this.repo.PostComic(comic.IdComic, comic.Nombre, comic.Imagen);
            return RedirectToAction("Index");
        }
    }
}
