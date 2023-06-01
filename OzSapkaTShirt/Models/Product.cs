using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OzSapkaTShirt.Models
{
    public class Product
    {
        public long Id { get; set; }

        [Column(TypeName = "nchar(30)")]
        [DisplayName("İsim")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "En fazla 30, en az 2 karakter")]
        public string Name { get; set; } = default!;

        [Column(TypeName = "nchar(200)")]
        [DisplayName("Açıklama")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "En fazla 200, en az 10 karakter")]
        public string Description { get; set; } = default!;

        [Column(TypeName = "char(3)")]
        [DisplayName("Beden")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(3, ErrorMessage = "En fazla 3 karakter")]
        public string Size { get; set; } = default!;

        [Column(TypeName = "nchar(10)")]
        [DisplayName("Model")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(10, ErrorMessage = "En fazla 10 karakter")]
        public string Model { get; set; } = default!;

        [DisplayName("Fiyat")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [Range(10, 500, ErrorMessage = "₺10 - ₺500 arası")]
        public float Price { get; set; }

        [Column(TypeName = "nchar(20)")]
        [DisplayName("Kumaş")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "En fazla 20, en az 3 karakter")]
        public string Fabric { get; set; } = default!;

        [Column(TypeName = "nchar(20)")]
        [DisplayName("Renk")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "En fazla 20, en az 2 karakter")]
        public string Color { get; set; } = default!;

        [NotMapped]
        [DisplayName("Resim")]
        public IFormFile? Image { get; set; }

        [Column(TypeName = "image")]
        [DisplayName("Resim")]
        public byte[]? DBImage { get; set; }

        [Column(TypeName = "image")]
        [DisplayName("Resim")]
        public byte[]? ThumbNail { get; set; }
    }
}
