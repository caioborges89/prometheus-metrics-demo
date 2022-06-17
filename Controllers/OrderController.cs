using Microsoft.AspNetCore.Mvc;
using Prometheus.Metrics.Demo.Model;
using Prometheus.Metrics.Demo.repository;

namespace Prometheus.Metrics.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        //TODO: Criar métricas para cada endpoint

        // GET api/todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderModel>>> Get()
        {
            return new ObjectResult(await _orderRepository.GetAllAsync());
        }
        // GET api/todos/1
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderModel>> Get(int id)
        {
            var todo = await _orderRepository.GetOrderByIdAsync(id);
            if (todo == null)
                return new NotFoundResult();
            
            return new ObjectResult(todo);
        }
        // POST api/todos
        [HttpPost]
        public async Task<ActionResult<OrderModel>> Post([FromBody] OrderModel order)
        {
            order.Id = await _orderRepository.GetNextIdAsync();
            await _orderRepository.CreateAsync(order);
            return new OkObjectResult(order);
        }
        // PUT api/todos/1
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderModel>> Put(int id, [FromBody] OrderModel order)
        {
            var orderFromDb = await _orderRepository.GetOrderByIdAsync(id);
            if (orderFromDb == null)
                return new NotFoundResult();

            order.Id = orderFromDb.Id;
            order.InternalId = orderFromDb.InternalId;
            await _orderRepository.UpdateAsync(order);
            return new OkObjectResult(order);
        }
        // DELETE api/todos/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _orderRepository.GetOrderByIdAsync(id);
            if (post == null)
                return new NotFoundResult();

            await _orderRepository.DeleteAsync(id);
            return new OkResult();
        }
    }
}