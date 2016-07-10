using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IceBlog.Models
{
    public class User:IdentityUser<long>
    {
        public string Name { get; set; }
        public string BlogTagTitle { get; set; }
        public string BlogTitle { get; set; }
    }
}
