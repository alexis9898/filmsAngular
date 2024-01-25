using DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IImdbRepository
    {
        Task<Imdb> GetImdbAsync(int filmId, int userId);
        Task<Imdb> AddImdbAsync(Imdb imdb);
        Task UpdateImdbAsync(Imdb imdb);
        Task DeleteImdbAsync(Imdb imdb);

    }
}
