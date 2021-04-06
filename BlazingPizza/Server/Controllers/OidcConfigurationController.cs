using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza.Server.Controllers
{
    public class OidcConfigurationController : Controller
    {
        private readonly ILogger<OidcConfigurationController> Logger;
        private readonly IClientRequestParametersProvider ClientRequestParametersProvider;

        public OidcConfigurationController(ILogger<OidcConfigurationController> logger, 
            IClientRequestParametersProvider clientRequestParametersProvider)
        {
            Logger = logger;
            ClientRequestParametersProvider = clientRequestParametersProvider;
        }

        /// <summary>
        /// Send all the configuration parameter to the client to get dynamic
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet("_configuration/{clientId}")]
        public IActionResult GetClientRequestParameters([FromRoute] string clientId)
        {
            IDictionary<string, string> Parameters = ClientRequestParametersProvider.GetClientParameters(HttpContext, clientId);
            return Ok(Parameters);
        }
    }
}
