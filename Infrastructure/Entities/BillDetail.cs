using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
	//TNT91
	[Table("BillDetails")]
    public class BillDetail: BaseEntity
    {
		[Column(TypeName = "nvarchar(1000)")]
		public string ProductName { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [ForeignKey("Bill-BillDetail")]
        public Guid BillId { get; set; }
        public Bill Bill { get; set; }

    }
}
