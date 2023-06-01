using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OzSapkaTShirt.Models
{
    public class Order
    {
        public long Id { get; set; }

        [DisplayName("Müşteri")]
        public string UserId { get; set; } = default!;

        [ForeignKey("UserId")]
        [DisplayName("Müşteri")]
        public ApplicationUser? User { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Tarih")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [DisplayName("Toplam")]
        public float TotalPrice { get; set; }

        [DisplayName("Durum")]
        public byte Status { get; set; }

        public List<OrderProduct>? OrderProducts { get; set; }
    }
}
