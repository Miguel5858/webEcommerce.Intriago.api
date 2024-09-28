using Microsoft.AspNetCore.Mvc;
using WebApiPerson.Services.Intefaces;
using static WebApiPerson.Dtos.EcommerceDtos;

namespace WebApiPerson.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _service.ListAsync());
        }

        [HttpGet]
        [Route("id/{id?}")]
        public async Task<IActionResult> GetProducts(string id)
        {
            var response = await _service.GetAsync(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> PostProducts([FromBody] ProductDto request)
        {
            var response = await _service.CreateAsync(request);

            if (response.Success)
            {
                request.Id = response.Result;
                //var message = JsonConvert.SerializeObject(request);
            }

            return CreatedAtAction("GetProducts", new
            {
                id = response.Result
            }, response);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutProducts(string id, [FromBody] ProductDto request)
        {
            return Ok(await _service.UpdateAsync(id, request));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var response = await _service.DeleteAsync(id);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
