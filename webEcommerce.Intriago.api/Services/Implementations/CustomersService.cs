using Azure.Core;
using WebApiPerson.Context;
using WebApiPerson.Dtos;
using WebApiPerson.Services.Intefaces;
using static WebApiPerson.Dtos.EcommerceDtos;
using static WebApiPerson.Models.EntitiesModels;

namespace WebApiPerson.Services.Implementations
{
    public class CustomersService : CrudGenericService<Customer>, ICustomersService
    {

        public CustomersService(AppDbContext context) : base(context)
        {

        }

        public async Task<EcommerceDtos.BaseResponse<string>> CreateAsync(EcommerceDtos.CustomerDto request)
        {
            var response = new BaseResponse<string>();
            Customer customerEntity = new();
            customerEntity.Name = request.Name;
            customerEntity.LastName = request.LastName;
            customerEntity.BirthDate = request.BirthDate;
            customerEntity.Email = request.Email;
            customerEntity.Dni = request.Dni;
            customerEntity.Status = true;

            response.Result = await Insert(customerEntity);
            response.Success = true;
            return response;
        }

        public async Task<EcommerceDtos.BaseResponse<string>> DeleteAsync(string id)
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

        public async Task<EcommerceDtos.BaseResponse<EcommerceDtos.CustomerDto>> GetAsync(string id)
        {
            var response = new BaseResponse<CustomerDto>();
            try
            {
                var customers = await Select(id);
                if (customers == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Registro no encontrado";
                    return response;
                }

                response.Result = new CustomerDto
                {
                    Id = customers.Id,
                    Name = customers.Name,
                    LastName = customers.LastName,
                    BirthDate = customers.BirthDate,
                    Email = customers.Email,
                    Dni = customers.Dni
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

        public async Task<EcommerceDtos.CustomersDtoCollectionResponse> ListAsync()
        {
            var response = new CustomersDtoCollectionResponse();
            try
            {
                var result = await SelectAll();

                response.Collection = result.Select(p => new CustomerDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    LastName = p.LastName,
                    BirthDate = p.BirthDate,
                    Email = p.Email,
                    Dni = p.Dni
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

        public async Task<EcommerceDtos.BaseResponse<string>> UpdateAsync(string id, EcommerceDtos.CustomerDto request)
        {
            var response = new BaseResponse<string>();
            try
            {
                Customer customerEntity = new();
                customerEntity.Name = request.Name;
                customerEntity.LastName = request.LastName;
                customerEntity.BirthDate = request.BirthDate;
                customerEntity.Email = request.Email;
                customerEntity.Dni = request.Dni;
                customerEntity.Status = true;
                customerEntity.Id = id;

                await UpdateEntity(customerEntity);

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
