using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
	//TNT91
	[Table("ProductImages")]
    public class ProductImage: BaseEntity
    {
        [Column(TypeName = "ntext")]
        public string ImageLink { get; set; }

        [Column(TypeName = "ntext")]
        public string? Alt { get; set; }

        [ForeignKey("Image-Product")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
