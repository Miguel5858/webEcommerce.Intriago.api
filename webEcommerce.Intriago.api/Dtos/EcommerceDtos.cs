using static WebApiPerson.Models.EntitiesModels;
using System.ComponentModel.DataAnnotations;

namespace WebApiPerson.Dtos
{
    public class EcommerceDtos
    {
        public class BaseResponse<TResult>
        {
            public bool Success { get; set; }
            public string? ErrorMessage { get; set; }
            public TResult Result { get; set; }
        }

        public class BaseCollectionResponse<TDtoClass> where TDtoClass : class
        {
            public bool Success { get; set; }
            public string? ErrorMessage { get; set; }
            public int TotalPages { get; set; }
            public ICollection<TDtoClass>? Collection { get; set; }
        }



        public class CategoryDtoCollectionResponse : BaseCollectionResponse<CategoryDto>
        {

        }

        public class CustomersDtoCollectionResponse : BaseCollectionResponse<CustomerDto>
        {

        }

        public class ProductDtoCollectionResponse : BaseCollectionResponse<ProductDto>
        {

        }

        public class SaleDtoCollectionResponse : BaseCollectionResponse<SaleDto>
        {

        }


        public class SaleDetailDtoCollectionResponse : BaseCollectionResponse<SaleDetailDto>
        {

        }

        public class CategoryDto
        {
            public string? Id { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
        }

        public class CustomerDto
        {
            public string? Id { get; set; }

            public string? Name { get; set; }

            public string? LastName { get; set; }

            public string? Email { get; set; }

            public DateTime BirthDate { get; set; }

            public string? Dni { get; set; }

        }

        public class ProductDto
        {
            public string? Id { get; set; }

            public string? Name { get; set; }

            public string? Description { get; set; }

            public string? CategoryId { get; set; }

            public decimal UnitPrice { get; set; }

            public string? ProductUrl { get; set; }

            public bool Active { get; set; }
        }

        public class SaleDto
        {
            public string? Id { get; set; }

            public string? CustomerId { get; set; }

            public DateTime SaleDate { get; set; }

            public string? InvoiceNumber { get; set; }

            public string? PaymentMethod { get; set; }

            public decimal TotalSale { get; set; }

            public bool Status { get; set; }
        }

        public class SaleDetailDto
        {
            public string? Id { get; set; }

            public string? SaleId { get; set; }

            public int ItemNumber { get; set; }

            public string? ProductId { get; set; }

            public decimal UnitPrice { get; set; }

            public decimal Quantity { get; set; }

            public decimal Total { get; set; }
        }
    }
}
