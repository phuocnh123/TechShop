using Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace TechShop.Models
{
    public class CustomerInfoModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
