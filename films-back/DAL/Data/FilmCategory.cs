﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class FilmCategory
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
    
    }

}
