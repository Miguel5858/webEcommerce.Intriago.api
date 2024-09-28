using static WebApiPerson.Dtos.EcommerceDtos;

namespace WebApiPerson.Services.Intefaces
{
    public interface IProductService
    {
        Task<BaseResponse<ProductDto>> GetAsync(string id);

        Task<ProductDtoCollectionResponse> ListAsync();

        Task<BaseResponse<string>> CreateAsync(ProductDto request);

        Task<BaseResponse<string>> UpdateAsync(string id, ProductDto request);

        Task<BaseResponse<string>> DeleteAsync(string id);

    }
}
