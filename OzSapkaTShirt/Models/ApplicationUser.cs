using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace OzSapkaTShirt.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [Column(TypeName = "nchar(50)")]
    [DisplayName("İsim")]
    [Required(ErrorMessage = "Bu alan zorunludur.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "En fazla 50, en az 2 karakter")]
    public string Name { get; set; } = default!;

    [Column(TypeName = "nchar(50)")]
    [DisplayName("Soyad")]
    [Required(ErrorMessage = "Bu alan zorunludur.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "En fazla 50, en az 2 karakter")]
    public string SurName { get; set; } = default!;

    [DisplayName("Kurumsal hesap")]
    public bool Corporate { get; set; }

    [Column(TypeName = "nchar(200)")]
    [DisplayName("Adres")]
    [Required(ErrorMessage = "Bu alan zorunludur.")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "En fazla 200, en az 3 karakter")]
    public string Address { get; set; } = default!;

    [DisplayName("Cinsiyet")]
    public byte Gender { get; set; }

    [ForeignKey("Gender")]
    public Gender? GenderType { get; set; }

    [Column(TypeName = "date")]
    [DisplayName("Doğum tarihi")]
    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }

    [Column(TypeName = "nchar(256)")]
    [DisplayName("Kullanıcı adı")]
    [Required(ErrorMessage = "Bu alan zorunludur.")]
    [StringLength(256, MinimumLength = 2, ErrorMessage = "En fazla 256, en az 2 karakter")]
    public override string UserName { get => base.UserName; set => base.UserName = value; }

    [Column(TypeName = "char(256)")]
    [DisplayName("E-posta")]
    [Required(ErrorMessage = "Bu alan zorunludur.")]
    [StringLength(256, MinimumLength = 5, ErrorMessage = "En fazla 256, en az 5 karakter")]
    [EmailAddress(ErrorMessage = "Geçersiz format")]
    public override string Email { get => base.Email; set => base.Email = value; }

    [Column(TypeName = "char(10)")]
    [DisplayName("Telefon")]
    [Required(ErrorMessage = "Bu alan zorunludur.")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "10 karakter")]
    public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

    [DisplayName("Şehir")]
    public byte CityCode { get; set; }

    [ForeignKey("CityCode")]
    public City? City { get; set; }

    [NotMapped]
    [DisplayName("Parola")]
    [Required(ErrorMessage = "Bu alan zorunludur.")]
    [StringLength(128, MinimumLength = 8, ErrorMessage = "En fazla 128, en az 8 karakter")]
    [DataType(DataType.Password)]
    public string PassWord { get; set; } = default!;

    [NotMapped]
    [DisplayName("Parola (tekrar)")]
    [Required(ErrorMessage = "Bu alan zorunludur.")]
    [StringLength(128, MinimumLength = 8, ErrorMessage = "En fazla 128, en az 8 karakter")]
    [DataType(DataType.Password)]
    [Compare("PassWord", ErrorMessage = "Parola eşleşme başarısız")]
    public string ConfirmPassWord { get; set; } = default!;

    public ApplicationUser Trim()
    {
        this.Name = this.Name.Trim();
        this.SurName = this.SurName.Trim();
        this.Address = this.Address.Trim();
        this.UserName = this.UserName.Trim();
        this.Email = this.Email.Trim();
        this.PhoneNumber = this.PhoneNumber.Trim();
        return this;
    }
}

