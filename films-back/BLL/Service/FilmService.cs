using BLL.Interface;
using DAL.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Model;
using DAL.Data;
using Microsoft.AspNetCore.JsonPatch;
using BLL.Enum;
using DAL.Interfaces;

namespace BLL.Service
{
    public class FilmService: IFilmService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public FilmService(
            IFilmRepository filmRepository,
            IMapper mapper,
            ICategoryRepository categoryRepository 
            )
        {
            _filmRepository = filmRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<FilmModel>> GetFilmsAsync()
        {
            var films = await _filmRepository.GetFilmsAsync();
           // for (int i = 0; i < films.Count; i++)
            //{
              //  var categories=new List<CategoryModel>();
            //}
            var filmsModel= _mapper.Map<List<FilmModel>>(films);
            return filmsModel;
        }

        public async Task<List<FilmModel>> GetFilmByNameAsync(string value)
        {
            var films = await _filmRepository.GetFilmByNameAsync(value);
            var filmsModel = _mapper.Map<List<FilmModel>>(films);
            return filmsModel;
        }

        public async Task<FilmModel> GetFilmAsync(int filmId)
        {
            var film = await _filmRepository.GetFilmAsync(filmId);
            var filmModel = _mapper.Map<FilmModel>(film);
            if (film.Imdbs.Count > 0)
            {
                filmModel.Imdb = film.Imdbs.Average(x => x.Rate);
                filmModel.Imdb=Math.Round(filmModel.Imdb,1);
                
            }

            //filmModel.AvetageImdb=filmModel.;
            return filmModel;
        }

        //add
        public async Task<FilmModel> AddFilmAsync(FilmModel film)
        {

            var categoriesModel = film.CategoriesModel;
            film.CategoriesModel= null;

            var NewFilm = _mapper.Map<Film>(film);
            NewFilm = await _filmRepository.AddFilmAsync(NewFilm);
            film= _mapper.Map<FilmModel>(NewFilm);

            film.CategoriesModel = await HandleFilmCategory(categoriesModel, film.Id); ;
            return film;
        }

        // PUT
        public async Task<FilmModel> UpdateFilmAsync(int filmId, FilmModel filmModel)
        {

            var film = await _filmRepository.GetFilmAsync(filmId);
            if (film == null)
                return null;


            film.Discription = filmModel.Discription;   
            film.Name= filmModel.Name;
            //film.Like= filmModel.Like;  
            //film.Imdb = filmModel.Imdb;

            await _filmRepository.UpdateFilmAsync(film);
            filmModel.Id= filmId;
            filmModel.CategoriesModel= await HandleFilmCategory( filmModel.CategoriesModel,filmId);

            return filmModel;
        }

        //PATCH
        public async Task<FilmModel> UpdatePatchFilmAsync(int filmId, JsonPatchDocument filmPatch)
        {
            var film = await _filmRepository.GetFilmAsync(filmId);
            if (film == null)
                return null;

            filmPatch.ApplyTo(film);
            await _filmRepository.UpdateFilmAsync(film);
            return _mapper.Map<FilmModel>(film);
        }


        //delete
        public async Task<bool> DeleteFilmAsync(int filmId)
        {
            var film = await _filmRepository.GetFilmAsync(filmId);
            if (film == null)
                return false;

            await _filmRepository.DeleteFilmAsync(film);
            return true;
        }
        //get filmd by ID
        public async Task<List<FilmModel>> GetFilmsByCregoryIdAsync(int id)
        {
            var films = await _filmRepository.GetFilmsByCategoryId(id);
            if (films == null)
                return null;

            var filmsModel = _mapper.Map<List<FilmModel>>(films);
            return filmsModel;
        }


        public async Task AddFilmCategoryAsync(int filmId,CategoryModel category)
        {
            var categoryId = category.Id;
            var filmCategory = new FilmCategory()
            {
                CategoryId = categoryId,
                FilmId = filmId
            };
            var res = await _filmRepository.ConectFilmToCategory(filmCategory);
        }
     
        public async Task RemoveFilmCategoryAsync(int filmId, CategoryModel category)
        {
            var filmCategory = await _filmRepository.GetFilmCategoryAsync(filmId, category.Id);
            if( filmCategory == null )
                return;
            await _filmRepository.RemoveFilmCategory(filmCategory);
        }

        public async Task<List<CategoryModel>> HandleFilmCategory(List<CategoryModel> categories   ,int filmId)
        {
            List<CategoryModel> SendCategories = new List<CategoryModel>();
            for(int i = 0;i < categories.Count; i++)
            {
                var category = await _categoryRepository.GetOneCategory(categories[i].Id);
                if ( category == null ) continue; //if category even exist

                switch (categories[i].PatchFilmCategory)
                {
                    case Patch.Add:
                        await AddFilmCategoryAsync(filmId, categories[i]);
                        break;
                    case Patch.Remove:
                        await RemoveFilmCategoryAsync (filmId, categories[i]);
                        break;  
                    default:
                        break;
                }
                SendCategories.Add(categories[i]);
            }
            return SendCategories;
        }
    }
}
