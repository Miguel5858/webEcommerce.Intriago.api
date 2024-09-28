using static WebApiPerson.Dtos.EcommerceDtos;

namespace WebApiPerson.Services.Intefaces
{
    public interface ICategoryService
    {
        Task<BaseResponse<CategoryDto>> GetAsync(string id);

        Task<CategoryDtoCollectionResponse> ListAsync();

        Task<BaseResponse<string>> CreateAsync(CategoryDto request);

        Task<BaseResponse<string>> UpdateAsync(string id, CategoryDto request);

        Task<BaseResponse<string>> DeleteAsync(string id);
    }
}
