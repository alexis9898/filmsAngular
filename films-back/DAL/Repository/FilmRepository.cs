using DAL.Data;
using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.Repository
{
    public class FilmRepository: IFilmRepository
    {
        private readonly AppDataContext _context;

        public FilmRepository(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<Film>> GetFilmsAsync()
        {
           // await this.GetFilmAvgAsync();
            var films = await _context.Films
                .Include(f=>f.Images)
                .Include(x=>x.Categories)
                .Include(f=>f.Comments)
                .ToListAsync();
            return films;
        }

        public async Task<Film> GetFilmAsync(int filmId)
        {
            var films = await _context.Films
                .Where(x=>x.Id==filmId)
                .Include(f=>f.Images)
                .Include(f=>f.Categories)
                .Include(f=>f.Comments)
                    .ThenInclude(c=>c.User)
                .Include(f=>f.Imdbs)
                .FirstOrDefaultAsync();
            return films;
        }


        public async Task<dynamic> GetFilmAvgAsync()
        {
            //await _context.Films.GroupJoin<Film, Imdb, int, dynamic>(_context.Imdb, x => x.Id, x => x.FilmId, (x, y) => new { id = x.Id, name = x.Name, averageImdb = y.Average(z => z.Rate) }).ToListAsync();
            var temp = await _context.Films.Include(f => f.Imdbs).GroupBy(x => new { id = x.Id, name = x.Name }, x => x.Imdbs).Select(x => new { id = x.Key.id, name = x.Key.name, IMDB = x.Select(z => z.Average(y => y.Rate)) })
                .ToListAsync();
            return temp;
        }

        public async Task<List<Film>> GetFilmByNameAsync(string filmName)
        {
            var films = await _context.Films
                .Where(x => x.Name.ToLower().Contains(filmName.ToLower()))
                .Include(f => f.Images)
                //.Include(f=>f.Categories).Take(3)
                .ToListAsync();
            return films;
        }

        //add
        public async Task<Film> AddFilmAsync(Film film)
        {
            _context.Films.Add(film);
            await _context.SaveChangesAsync();
            return film;
        }

        // PUT/PATCH 
        public async Task UpdateFilmAsync(Film film)
        {
            _context.Films.Update(film); 
            await _context.SaveChangesAsync();
            return;
        }

        //delete
        public async Task DeleteFilmAsync(Film film)
        {
            _context.Remove(film);
            await _context.SaveChangesAsync();
        }


        public async Task<List<Film>> GetFilmsByCategoryId(int categoryId)
        {
            return await _context.Films
                .Where(f => f.FilmCategories.Any(fc => fc.CategoryId == categoryId)).Include(x => x.Images).Include(x=>x.Categories)
                .ToListAsync();
            return null;
        }

        //get filmCtegory
        public async Task<FilmCategory> GetFilmCategoryAsync(int filmId, int categoryId)
        {
            return await _context.FilmCategory.Where(
                    x=>x.FilmId==filmId &&
                    x.CategoryId==categoryId 
                ).FirstOrDefaultAsync();
        }

        //post FilmCategory
        public async Task<FilmCategory> ConectFilmToCategory(FilmCategory filmCategory)
        {
            _context.FilmCategory.Add(filmCategory);
            await _context.SaveChangesAsync();
            return filmCategory;
        }
        //updat FilmCategory
        public async Task EditFilmCategory(FilmCategory filmCategory)
        {
            _context.FilmCategory.Update(filmCategory);
            await _context.SaveChangesAsync();
            return;
        }
        //delete FilmCategory
        public async Task RemoveFilmCategory(FilmCategory filmCategory)
        {
            _context.FilmCategory.Remove(filmCategory);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<double> GetAverageRatingForAllFilmsAsync()
        {
            var averageRating = await _context.Imdb.AverageAsync(x=>x.Rate);

            return averageRating;
        }

    }
}
