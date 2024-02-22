using Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Products
{
	public class ProductFilterModel
	{
        public string KeyWord { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
        public decimal? FromPrice { get; set; }
        public decimal? ToPrice { get; set; }
        public SortEnum SortBy { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 9;
    }
}
