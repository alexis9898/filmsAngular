using DAL.Data;
using DAL.Interface;
using DAL.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ImdbRepository:IImdbRepository
    {
        private readonly AppDataContext _context;

        public ImdbRepository(AppDataContext context)
        {
            _context = context;
        }
        public async Task<Imdb> GetImdbAsync(int filmId, int userId)
        {
            return await _context.Imdb.Where(x=>
                x.UserId==userId &&
                x.FilmId==filmId
               ).FirstOrDefaultAsync();
        }
        public async Task<Imdb> AddImdbAsync(Imdb imdb)
        {
            _context.Imdb.Add(imdb);
            await _context.SaveChangesAsync();
            return imdb;
        }
        public async Task UpdateImdbAsync(Imdb imdb)
        {
            _context.Imdb.Update(imdb);
            await _context.SaveChangesAsync();
            return;
        }
        public async Task DeleteImdbAsync(Imdb imdb)
        {
            _context.Imdb.Remove(imdb);
            await _context.SaveChangesAsync();
            return;
        }
        public async Task<double> GetAverageRatingForAllFilmsAsync()
        {
            var averageRating = await _context.Imdb
                .Select(imdb => imdb.Rate)
                .DefaultIfEmpty(0) // Handle the case when there are no IMDb ratings
                .AverageAsync();

            return averageRating;
        }

    }
}
