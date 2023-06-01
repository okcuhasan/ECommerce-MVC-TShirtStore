using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OzSapkaTShirt.Models
{
    public class OrderProduct
    {
        [DisplayName("Sipariş")]
        public long OrderId { get; set; }

        [DisplayName("Sipariş")]
        [ForeignKey("OrderId")]
        [JsonIgnore]
        public Order? Order { get; set; }

        [DisplayName("Ürün")]
        public long ProductId { get; set; }

        [DisplayName("Ürün")]
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        [DisplayName("Adet")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        public short Quantity { get; set; }

        [DisplayName("Fiyat")]
        public float Price { get; set; }

        [DisplayName("Toplam")]
        public float Total { get; set; }
    }
}
