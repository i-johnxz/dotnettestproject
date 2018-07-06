using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsCore.Models
{
    public class PostTag
    {
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public string TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
