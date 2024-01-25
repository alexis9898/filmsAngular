using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }

        public int UserId { get; set; }
        public UserDetail User { get; set; }

    }
}
