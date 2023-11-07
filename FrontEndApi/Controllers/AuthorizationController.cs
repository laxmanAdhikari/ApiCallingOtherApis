using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderProcessing.Services;
using System.Threading.Tasks;

namespace OrderProcessing.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private ILogger _logger;
        private readonly IAuthService _authService;

        public AuthorizationController(ILogger<AuthorizationController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpGet]
        [Route("api/v1/authtoken")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> AuthToken()
        {
            var token = await  _authService.GetToken();

            if (token !=null)
            {
                _logger.LogInformation("Sucessfuly retrieved auth token");
                return Ok(token);
            }
            _logger.LogInformation("Could not retrieve authorization tokenn");
            return Unauthorized("Could not retrieve authorization token");
        }

    }
}
