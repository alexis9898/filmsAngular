using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class Imdb
    {
        public int Id { get; set; }
        public int Rate {  get; set; }
        public int FilmId {  get; set; }
        public Film Film { get; set; }
        public int UserId{ get; set; }
    }
}
