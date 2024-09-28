using WebApiPerson.Context;
using WebApiPerson.Dtos;
using WebApiPerson.Services.Intefaces;
using static WebApiPerson.Dtos.EcommerceDtos;
using static WebApiPerson.Models.EntitiesModels;

namespace WebApiPerson.Services.Implementations
{
    public class CategoryService : CrudGenericService<Category>, ICategoryService
    {
        public CategoryService(AppDbContext context) : base(context)
        {

        }

        public async Task<BaseResponse<string>> CreateAsync(CategoryDto request)
        {
            var response = new BaseResponse<string>();

            Category categoryEntity = new();
            categoryEntity.Name = request.Name;
            categoryEntity.Description = request.Description;
            response.Result = await Insert(categoryEntity);
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

        public async Task<BaseResponse<CategoryDto>> GetAsync(string id)
        {
            var response = new BaseResponse<CategoryDto>();
            try
            {
                var category = await Select(id);
                if (category == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Registro no encontrado";
                    return response;
                }

                response.Result = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
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

        public async Task<CategoryDtoCollectionResponse> ListAsync()
        {
            var response = new CategoryDtoCollectionResponse();
            try
            {
                var result = await SelectAll();

                response.Collection = result.Select(p => new CategoryDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description
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


        public async Task<BaseResponse<string>> UpdateAsync(string id, CategoryDto request)
        {
            var response = new BaseResponse<string>();
            try
            {
                Category category = new();
                category.Id = id;
                category.Name = request.Name;
                category.Description = request.Description;

                await UpdateEntity(category);

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
