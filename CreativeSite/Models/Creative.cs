﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativeSite.Models
{
    public class Creative
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set;}
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}