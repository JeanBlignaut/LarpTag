namespace LarpTag.Controllers
{
    using System.Net.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public class TagReadController : ControllerBase
    {
        private readonly ILogger<TagReadController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public TagReadController(ILogger<TagReadController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;

            // _logger.Log(LogLevel.Information, "Controller constructor...");
        }

        public static int playerhealth = 100;

        [HttpGet]
        public async Task OnGet(string mac, string tag)
        {
            _logger.Log(LogLevel.Information, $"got Get: mac: {mac}, tag: {tag}", this.Request.Query);

            playerhealth = playerhealth -1;

            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"http://192.168.0.100/get?health={playerhealth}");
        // {
        //     Headers =
        //     {
        //         { HeaderNames.Accept, "application/vnd.github.v3+json" },
        //         { HeaderNames.UserAgent, "HttpRequestsSample" }
        //     }
        // };

        var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            _logger.Log(LogLevel.Information, content);
        }
        else
        {
            _logger.Log(
                LogLevel.Error,
                $"httpclient request failed ({httpResponseMessage.StatusCode}): {httpResponseMessage.ReasonPhrase}",
                this.Request.Query
                );
        }

            //return Ok();
        }
    }
}