using System.Collections.Generic;

namespace BlogsCore.Models
{
    public class Blog
    {
        public int BlogId { get; set; }

        public string Url { get; set; }

        public byte[] Timestamp { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}