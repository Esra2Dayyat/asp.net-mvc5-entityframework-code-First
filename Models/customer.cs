using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace amazon.Models
{
    public class Customer
    {
        
        [Key]
        [Column(Order = 1)]
        public int Customerid { get; set; }
        [Required]
        [DisplayName("Your Name")]
        public string Namee { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("Re-enter Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

              public int Phone { get; set; }

        public int  AdressId { get; set; }
        [ForeignKey("AdressId")]
        public virtual Adress  Adresses { get; set; }
  
        public virtual ICollection<Order> Orderss { get; set; }

    }

    public class Adress
    {
        [Key]
        
        [DisplayName("Your Adress")]
        public int AdressId { get; set; }
        public string City { get; set; }


    }
    public class Imgeupload
    {
        [Key]

        public int ImgId { get; set; }

        public string ImgPath { get; set; }
        public string Select_Order { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }



    }


    public class Order
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Your Nmae ")]

        public int Customerid { get; set; }
        [DisplayName("Your Adress")]
        public int? AdressId { get; set; }  //ForeignKey

        [DataType(DataType.Date)]

        public DateTime OrderDate { get; set; }

        public int ImgId { get; set; }

        public decimal Totalorder { get; set; }



        [ForeignKey("Customerid")]
        public virtual Customer Customeres { get; set; }
        [ForeignKey("AdressId")]

        public virtual Adress shipToAdress { get; set; }
        [ForeignKey("ImgId")]
        public virtual Imgeupload GetOrder { get; set; }

    }

}