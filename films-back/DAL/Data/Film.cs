using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class Film
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }

        [NotMapped]
        public int AvgImdb { get; set; }
        //public string Author { get; set; }
        //public int Like { get; set; }
        //public double Imdb { get; set; }
        //public double Price { get; set; }


        public List<FilmCategory> FilmCategories{ get; set; } 
        public virtual List<Category> Categories{ get; set; }
        public List<Image> Images { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Imdb> Imdbs { get; set; }
    }
}
