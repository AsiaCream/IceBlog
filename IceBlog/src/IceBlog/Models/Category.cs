using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceBlog.Models
{
    public class Category
    {
        public long Id { get; set; }
        public int Priority { get; set; }
        public string Title { get; set; }
    }
}
