using BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IImdbService
    {
        Task<ImdbModel> GetImdbAsync(int filmId, int userId);
        Task<ImdbModel> AddImdbAsync(ImdbModel imdbModel);
        Task<ImdbModel> ChangeImdbAsync(ImdbModel imdbModel);
        Task<ImdbModel> RemoveImdbAsync(ImdbModel imdbModel);

    }
}
