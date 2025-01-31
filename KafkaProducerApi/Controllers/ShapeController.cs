using KafkaPocCommon.DTOs;
using KafkaProducerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace KafkaProducerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShapeController : ControllerBase
    {
        private readonly ShapeKafkaProducerService _shapeProducerService;
        public ShapeController(ShapeKafkaProducerService shapeProducerService)
        {
            _shapeProducerService = shapeProducerService;
        }

        [HttpPost(Name = "SendShape")]
        public async Task<IActionResult> SendShape(ShapeDto input, CancellationToken cancellationToken)
        {
            await _shapeProducerService.SendMessage(input, cancellationToken);
            return Ok();
        }
    }
}
