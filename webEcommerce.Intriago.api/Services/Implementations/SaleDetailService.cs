using Azure.Core;
using WebApiPerson.Context;
using WebApiPerson.Dtos;
using WebApiPerson.Services.Intefaces;
using static WebApiPerson.Dtos.EcommerceDtos;
using static WebApiPerson.Models.EntitiesModels;

namespace WebApiPerson.Services.Implementations
{
    public class SaleDetailService : CrudGenericService<SaleDetail>, ISaleDetailService
    {

        public SaleDetailService(AppDbContext context) : base(context)
        {

        }

        public async Task<BaseResponse<string>> CreateAsync(SaleDetailDto request)
        {
            var response = new BaseResponse<string>();

            SaleDetail saleDetailEntity = new();
            saleDetailEntity.SaleId = request.SaleId;
            saleDetailEntity.ItemNumber = request.ItemNumber;
            saleDetailEntity.ProductId = request.ProductId;
            saleDetailEntity.UnitPrice = request.UnitPrice;
            saleDetailEntity.Quantity = request.Quantity;
            saleDetailEntity.Total = request.Total;

            response.Result = await Insert(saleDetailEntity);
            response.Success = true;
            return response;
        }

        public async Task<BaseResponse<string>> DeleteAsync(string id)
        {
            var response = new BaseResponse<string>();
            try
            {
                await DeleteEntity(id);
                response.Result = id;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<SaleDetailDto>> GetAsync(string id)
        {
            var response = new BaseResponse<SaleDetailDto>();
            try
            {
                var saleDetailEntity = await Select(id);
                if (saleDetailEntity == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Registro no encontrado";
                    return response;
                }

                response.Result = new SaleDetailDto
                {
                    Id = id,
                    SaleId = saleDetailEntity.SaleId,
                    ItemNumber = saleDetailEntity.ItemNumber,
                    ProductId = saleDetailEntity.ProductId,
                    UnitPrice = saleDetailEntity.UnitPrice,
                    Quantity = saleDetailEntity.Quantity,
                    Total = saleDetailEntity.Total
                };

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<SaleDetailDtoCollectionResponse> ListAsync()
        {
            var response = new SaleDetailDtoCollectionResponse();
            try
            {
                var result = await SelectAll();

                response.Collection = result.Select(p => new SaleDetailDto
                {
                    Id = p.Id,
                    SaleId = p.SaleId,
                    ItemNumber = p.ItemNumber,
                    ProductId = p.ProductId,
                    UnitPrice = p.UnitPrice,
                    Quantity = p.Quantity,
                    Total = p.Total
                }).ToList();

                response.TotalPages = result.Count;

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<string>> UpdateAsync(string id, SaleDetailDto request)
        {
            var response = new BaseResponse<string>();
            try
            {
                SaleDetail saleDetailEntity = new();
                saleDetailEntity.Id = id;
                saleDetailEntity.SaleId = request.SaleId;
                saleDetailEntity.ItemNumber = request.ItemNumber;
                saleDetailEntity.ProductId = request.ProductId;
                saleDetailEntity.UnitPrice = request.UnitPrice;
                saleDetailEntity.Quantity = request.Quantity;
                saleDetailEntity.Total = request.Total;

                await UpdateEntity(saleDetailEntity);

                response.Result = id;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
    }
}
