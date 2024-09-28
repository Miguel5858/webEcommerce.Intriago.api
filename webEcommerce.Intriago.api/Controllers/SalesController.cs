using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApiPerson.Services.Intefaces;
using WebApiPerson.Services.MQ;
using static WebApiPerson.Dtos.EcommerceDtos;

namespace WebApiPerson.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : Controller
    {
        private readonly ISalesService _service;
        private readonly RabbitMQService _rabbitMQService;

        public SalesController(ISalesService service, RabbitMQService rabbitMQService)
        {
            _service = service;
            _rabbitMQService = rabbitMQService;
        }


        [HttpGet]
        public async Task<IActionResult> GetSales()
        {
            return Ok(await _service.ListAsync());
        }

        [HttpGet]
        [Route("id/{id?}")]
        public async Task<IActionResult> GetSales(string id)
        {
            var response = await _service.GetAsync(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> PostSales([FromBody] SaleDto request)
        {
            var response = await _service.CreateAsync(request);

            if (response.Success)
            {
                request.Id = response.Result;

                var message = JsonConvert.SerializeObject(request);
                _rabbitMQService.PublishToQueue("saleQueue", message);
            }

            return CreatedAtAction("GetSales", new
            {
                id = response.Result
            }, response);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutSales(string id, [FromBody] SaleDto request)
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
