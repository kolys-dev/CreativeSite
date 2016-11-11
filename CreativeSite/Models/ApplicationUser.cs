using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativeSite.Models
{
    public class ApplicationUser
    {
        public virtual ICollection<Creative> Creatives
        {
            get; set;
        }
    }
}