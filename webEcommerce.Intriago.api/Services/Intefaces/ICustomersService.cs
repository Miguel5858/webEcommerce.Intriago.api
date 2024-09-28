using static WebApiPerson.Dtos.EcommerceDtos;

namespace WebApiPerson.Services.Intefaces
{
    public interface ICustomersService
    {
        Task<BaseResponse<CustomerDto>> GetAsync(string id);

        Task<CustomersDtoCollectionResponse> ListAsync();

        Task<BaseResponse<string>> CreateAsync(CustomerDto request);

        Task<BaseResponse<string>> UpdateAsync(string id, CustomerDto request);

        Task<BaseResponse<string>> DeleteAsync(string id);
    }
}
