using System.ComponentModel.DataAnnotations;

namespace OrderProcessingApi.Model
{
    public class OrderRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Customer ID must be a positive integer.")]
        public int CustomerId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Product ID must be a positive integer.")]
        public int ProductId { get; set; }
    }
}
