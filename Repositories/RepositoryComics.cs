using ExamenComicsAWS.Data;
using ExamenComicsAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenComicsAWS.Repositories
{
    public class RepositoryComics
    {
        private ComicsContext context;

        public RepositoryComics(ComicsContext context) 
        {
            this.context = context;
        }

        public async Task<List<Comic>> GetComics() 
        {
            return await context.Comics.ToListAsync();
        }   

        public async Task<Comic> PostComic(int idComic, string nombre, string imagen) {
            Comic comic = new Comic
            {
                IdComic = await GetMaxId(),
                Nombre = nombre,
                Imagen = imagen
            };
            await this.context.Comics.AddAsync(comic);
            await this.context.SaveChangesAsync();
            return comic;
        }

        public async Task<int> GetMaxId() 
        {
            return await this.context.Comics.MaxAsync(x => x.IdComic) + 1;
        }
    }
}
