using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static WebApiPerson.Dtos.EcommerceDtos;
using WebApiPerson.Services.MQ;
using WebApiPerson.Services.Intefaces;

namespace WebApiPerson.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomersService _service;

        public CustomersController(ICustomersService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            return Ok(await _service.ListAsync());
        }

        [HttpGet]
        [Route("id/{id?}")]
        public async Task<IActionResult> GetCustomers(string id)
        {
            var response = await _service.GetAsync(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] CustomerDto request)
        {
            var response = await _service.CreateAsync(request);

            if (response.Success)
            {
                request.Id = response.Result;
                //var message = JsonConvert.SerializeObject(request);
                //_rabbitMQService.PublishToQueue("categoriesQueue", message);
            }

            return CreatedAtAction("GetCustomers", new
            {
                id = response.Result
            }, response);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutCustomers(string id, [FromBody] CustomerDto request)
        {
            return Ok(await _service.UpdateAsync(id, request));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCustomers(string id)
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
