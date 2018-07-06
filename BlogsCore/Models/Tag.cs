using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogsCore.Models
{
    public class Tag
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual List<PostTag> PostTags { get; set; }
    }
}
