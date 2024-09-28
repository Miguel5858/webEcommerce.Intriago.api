using System;
using WebApiPerson.Context;
using WebApiPerson.Dtos;
using WebApiPerson.Services.Intefaces;
using static WebApiPerson.Dtos.EcommerceDtos;
using static WebApiPerson.Models.EntitiesModels;

namespace WebApiPerson.Services.Implementations
{
    public class ProductService : CrudGenericService<Product>, IProductService
    {

        public ProductService(AppDbContext context) : base(context)
        {

        }

        public async Task<BaseResponse<string>> CreateAsync(EcommerceDtos.ProductDto request)
        {
            var response = new BaseResponse<string>();

            Product productEntity = new();
            productEntity.Name = request.Name;
            productEntity.Description = request.Description;
            productEntity.CategoryId = request.CategoryId;
            productEntity.UnitPrice = request.UnitPrice;
            productEntity.ProductUrl = request.ProductUrl;
            productEntity.Active = true;
            response.Result = await Insert(productEntity);
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

        public async Task<BaseResponse<ProductDto>> GetAsync(string id)
        {
            var response = new BaseResponse<ProductDto>();
            try
            {
                var product = await Select(id);
                if (product == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Registro no encontrado";
                    return response;
                }

                response.Result = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    UnitPrice = product.UnitPrice,
                    ProductUrl = product.ProductUrl,
                    Active = product.Active,
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

        public async Task<ProductDtoCollectionResponse> ListAsync()
        {
            var response = new ProductDtoCollectionResponse();
            try
            {
                var result = await SelectAll();

                response.Collection = result.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    UnitPrice = p.UnitPrice,
                    ProductUrl = p.ProductUrl,
                    Active = p.Active,
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

        public async Task<BaseResponse<string>> UpdateAsync(string id, ProductDto request)
        {
            var response = new BaseResponse<string>();
            try
            {
                Product product = new()
                {
                    Id = id,
                    Name = request.Name,
                    Description = request.Description,
                    CategoryId = request.CategoryId,
                    UnitPrice = request.UnitPrice,
                    ProductUrl = request.ProductUrl,
                    Active = request.Active,
                };

                await UpdateEntity(product);

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
