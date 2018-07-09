using System.Collections.Generic;

namespace BlogsCore.Models
{
    public class Blog
    {
        public int BlogId { get; set; }

        public string Name { get; set; }


        public string Url { get; set; }

        public BlogType BlogType { get; set; }

        public byte[] Timestamp { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }

    public enum BlogType
    {
        Basic = 1,
        Rss = 2
    }

}