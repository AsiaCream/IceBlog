using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IceBlog.Models
{
    public class Post
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Priority { get; set; }
        public DateTime CreateTime { get; set; }

        [ForeignKey("Category")]
        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
