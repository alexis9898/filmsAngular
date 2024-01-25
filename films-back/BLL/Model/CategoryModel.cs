using BLL.Model;
using BLL.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Model
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string PatchFilmCategory { get; set; }// add/remove/change/Exists

        public List<FilmModel> FilmsModel{ get; set; }
      
    }
}
