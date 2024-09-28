using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApiPerson.Models
{
    public class EntitiesModels
    {
        public class EntityBase
        {
            //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            [StringLength(36)]
            public string Id { get; set; }
            public bool Status { get; set; }

            protected EntityBase()
            {
                Id = Guid.NewGuid().ToString();
                Status = true;
            }
        }

        public class Category : EntityBase
        {
            [Required]
            [StringLength(100)]
            public string? Name { get; set; }

            [StringLength(200)]
            public string? Description { get; set; }
        }

        public class Customer : EntityBase
        {

            [Required]
            [StringLength(200)]
            public string? Name { get; set; }

            [Required]
            [StringLength(600)]
            public string? LastName { get; set; }

            [Required]
            [StringLength(600)]
            public string? Email { get; set; }

            public DateTime BirthDate { get; set; }

            [Required]
            [StringLength(15)]
            public string? Dni { get; set; }

        }


        public class Product : EntityBase
        {
            [Required]
            [StringLength(100)]
            public string? Name { get; set; }

            [Required]
            [StringLength(100)]
            public string? Description { get; set; }

            [Required]
            public string? CategoryId { get; set; }

            public Category? Category { get; set; }

            public decimal UnitPrice { get; set; }

            [StringLength(1000)]
            public string? ProductUrl { get; set; }

            public bool Active { get; set; }
        }

        public class Sale : EntityBase
        {
            public string? CustomerId { get; set; }

            public Customer Customer { get; set; }

            public DateTime SaleDate { get; set; }

            public string? InvoiceNumber { get; set; }

            public string? PaymentMethod { get; set; }

            public decimal TotalSale { get; set; }
        }

        public class SaleDetail : EntityBase
        {
            public string? SaleId { get; set; }

            public Sale Sale { get; set; }

            public int ItemNumber { get; set; }

            public string? ProductId { get; set; }

            public Product Product { get; set; }

            public decimal UnitPrice { get; set; }

            public decimal Quantity { get; set; }

            public decimal Total { get; set; }
        }

        [Keyless]
        public class Usuario
        {
            [Required]
            [StringLength(30)]
            public string? Correo { get; set; }


            [Required]
            [StringLength(30)]
            public string? Clave { get; set; }

        }

    }
}
