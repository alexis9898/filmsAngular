using AutoMapper;
using BLL.Interface;
using BLL.Model;
using DAL.Data;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    
    public class ImdbService:IImdbService
    {
        private readonly IMapper _mapper;
        private readonly IImdbRepository _imdbRepository;

        public ImdbService(IImdbRepository imdbRepository, IMapper mapper)
        {

            _imdbRepository = imdbRepository;
            _mapper = mapper;
        }
        
        public async Task<ImdbModel> GetImdbAsync(int filmId, int userId)
        {
            var imdb = await _imdbRepository.GetImdbAsync(filmId, userId);
            if (imdb == null)
                return null;
            var imdbModel=_mapper.Map<ImdbModel>(imdb);
            return imdbModel;
        }
        public async Task<ImdbModel> AddImdbAsync(ImdbModel imdbModel)
        {
            var imdb = await _imdbRepository.GetImdbAsync(imdbModel.FilmId, imdbModel.UserId); //check if already exist
            if (imdb != null) //if exist cant add more
                return null;
            imdb = _mapper.Map<Imdb>(imdbModel);
            imdb=await _imdbRepository.AddImdbAsync(imdb);
            imdbModel=_mapper.Map<ImdbModel>(imdb);
            return imdbModel;
        }
        public async Task<ImdbModel> ChangeImdbAsync(ImdbModel imdbModel)
        {
            var imdb = await _imdbRepository.GetImdbAsync(imdbModel.FilmId,imdbModel.UserId);
            if (imdb == null)
                return null;
            imdb.Rate = imdbModel.Rate;
            await _imdbRepository.UpdateImdbAsync(imdb);
            return imdbModel;
        }
        public async Task<ImdbModel> RemoveImdbAsync(ImdbModel imdbModel)
        {
            var imdb = await _imdbRepository.GetImdbAsync(imdbModel.FilmId, imdbModel.UserId);
            if (imdb == null)
                return null;
            await _imdbRepository.UpdateImdbAsync(imdb);
            return imdbModel; 
        }
    }
}
