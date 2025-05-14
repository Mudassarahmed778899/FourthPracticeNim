using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FourthPracticeNim.Models
{
    public class PagedProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public List<int> AvailablePageSizes { get; set; } = Enumerable.Range(1, 10).ToList();
    }
}