using System.Collections.Generic;

namespace ActionVideo.Models
{
    public class PageResult<T>
    {
        public IList<T> Items { get; set; }
        public int Total { get; set; }
    }
}
