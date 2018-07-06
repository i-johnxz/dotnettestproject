using System.Collections.Generic;

namespace BlogsCore.Models
{
    public class Post
    {
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int BlogId { get; set; }

        public virtual Blog Blog { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }  
    }
}