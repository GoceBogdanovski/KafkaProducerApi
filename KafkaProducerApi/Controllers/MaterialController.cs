using KafkaPocCommon.DTOs;
using KafkaProducerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace KafkaProducerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly MaterialKafkaProducerService _materialProducerService;
        public MaterialController(MaterialKafkaProducerService materialProducerService)
        {
            _materialProducerService = materialProducerService;
        }

        [HttpPost(Name = "SendMaterial")]
        public async Task<IActionResult> SendMaterial(MaterialDto input, CancellationToken cancellationToken)
        {
            await _materialProducerService.SendMessage(input, cancellationToken);
            return Ok();
        }
    }
}