using AutoMapper;
using BLL.Model;
using DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace BLL.Mapper
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            CreateMap<Film, FilmModel>()
                .ForMember(f=>f.ImagesModel, opt=>opt.MapFrom(src=>src.Images))
                .ForMember(x => x.CategoriesModel, opt => opt.MapFrom(src => src.Categories))
                .ForMember(x => x.CommentsModel, opt => opt.MapFrom(src => src.Comments))
                .ForMember(x => x.ImdbsModel, opt => opt.MapFrom(src => src.Imdbs))
                .ReverseMap();

            CreateMap<Image, ImageModel>()
                .ReverseMap();

            CreateMap<Comment, CommentModel>()
              .ForMember(x => x.UserModel, opt => opt.MapFrom(src => src.User))
              .ReverseMap();

            CreateMap<Category, CategoryModel>()
                //.ForMember(c=>c.FilmsModel,f=> f.MapFrom(c => c.Films))
                .ReverseMap();

            CreateMap<UserModel,UserDetail>()
                .ReverseMap();

            CreateMap<ImdbModel, Imdb>()
               .ReverseMap();
        }
    }
}
