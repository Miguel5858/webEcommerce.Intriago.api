using Azure.Core;
using WebApiPerson.Context;
using WebApiPerson.Dtos;
using WebApiPerson.Services.Intefaces;
using static WebApiPerson.Dtos.EcommerceDtos;
using static WebApiPerson.Models.EntitiesModels;

namespace WebApiPerson.Services.Implementations
{
    public class SalesService : CrudGenericService<Sale>, ISalesService
    {

        public SalesService(AppDbContext context) : base(context)
        {

        }


        public async Task<BaseResponse<string>> CreateAsync(SaleDto request)
        {
            var response = new BaseResponse<string>();

            Sale saleEntity = new();
            saleEntity.CustomerId = request.CustomerId;
            saleEntity.SaleDate = request.SaleDate;
            saleEntity.InvoiceNumber = request.InvoiceNumber;
            saleEntity.PaymentMethod = request.PaymentMethod;
            saleEntity.TotalSale = request.TotalSale;

            response.Result = await Insert(saleEntity);
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

        public async Task<BaseResponse<SaleDto>> GetAsync(string id)
        {
            var response = new BaseResponse<SaleDto>();
            try
            {
                var sale = await Select(id);
                if (sale == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Registro no encontrado";
                    return response;
                }

                response.Result = new SaleDto
                {
                    Id = id,
                    CustomerId = sale.CustomerId,
                    SaleDate = sale.SaleDate,
                    InvoiceNumber = sale.InvoiceNumber,
                    PaymentMethod = sale.PaymentMethod,
                    TotalSale = sale.TotalSale
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

        public async Task<SaleDtoCollectionResponse> ListAsync()
        {
            var response = new SaleDtoCollectionResponse();
            try
            {
                var result = await SelectAll();

                response.Collection = result.Select(p => new SaleDto
                {
                    Id = p.Id,
                    CustomerId = p.CustomerId,
                    SaleDate = p.SaleDate,
                    InvoiceNumber = p.InvoiceNumber,
                    PaymentMethod = p.PaymentMethod,
                    TotalSale = p.TotalSale
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

        public async Task<BaseResponse<string>> UpdateAsync(string id, SaleDto request)
        {
            var response = new BaseResponse<string>();
            try
            {
                Sale saleEntity = new();
                saleEntity.Id = id;
                saleEntity.CustomerId = request.CustomerId;
                saleEntity.SaleDate = request.SaleDate;
                saleEntity.InvoiceNumber = request.InvoiceNumber;
                saleEntity.PaymentMethod = request.PaymentMethod;
                saleEntity.TotalSale = request.TotalSale;

                await UpdateEntity(saleEntity);

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
