using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceBlog.Models
{
    public class File
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime CreateTime { get; set; }
        public string Path { get; set; }

    }
}
