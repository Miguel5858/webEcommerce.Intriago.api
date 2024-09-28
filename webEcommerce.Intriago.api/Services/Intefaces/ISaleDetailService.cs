using static WebApiPerson.Dtos.EcommerceDtos;

namespace WebApiPerson.Services.Intefaces
{
    public interface ISaleDetailService
    {
        Task<BaseResponse<SaleDetailDto>> GetAsync(string id);

        Task<SaleDetailDtoCollectionResponse> ListAsync();

        Task<BaseResponse<string>> CreateAsync(SaleDetailDto request);

        Task<BaseResponse<string>> UpdateAsync(string id, SaleDetailDto request);

        Task<BaseResponse<string>> DeleteAsync(string id);
    }
}
