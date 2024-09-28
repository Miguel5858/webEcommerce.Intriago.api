using Microsoft.AspNetCore.Mvc;
using WebApiPerson.Services.Intefaces;
using static WebApiPerson.Dtos.EcommerceDtos;

namespace WebApiPerson.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SalesDetailsController : Controller
    {
        private readonly ISaleDetailService _service;

        public SalesDetailsController(ISaleDetailService service)
        {
            _service = service;
        }



        [HttpGet]
        public async Task<IActionResult> GetSalesDetail()
        {
            return Ok(await _service.ListAsync());
        }

        [HttpGet]
        [Route("id/{id?}")]
        public async Task<IActionResult> GetSalesDetail(string id)
        {
            var response = await _service.GetAsync(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> PostSalesDetail([FromBody] SaleDetailDto request)
        {
            var response = await _service.CreateAsync(request);

            if (response.Success)
            {
                request.Id = response.Result;
            }

            return CreatedAtAction("GetSalesDetail", new
            {
                id = response.Result
            }, response);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutSales(string id, [FromBody] SaleDetailDto request)
        {
            return Ok(await _service.UpdateAsync(id, request));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSales(string id)
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
