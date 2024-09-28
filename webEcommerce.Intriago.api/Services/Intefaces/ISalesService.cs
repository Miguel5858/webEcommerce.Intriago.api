using static WebApiPerson.Dtos.EcommerceDtos;

namespace WebApiPerson.Services.Intefaces
{
    public interface ISalesService
    {
        Task<BaseResponse<SaleDto>> GetAsync(string id);

        Task<SaleDtoCollectionResponse> ListAsync();

        Task<BaseResponse<string>> CreateAsync(SaleDto request);

        Task<BaseResponse<string>> UpdateAsync(string id, SaleDto request);

        Task<BaseResponse<string>> DeleteAsync(string id);
    }
}
