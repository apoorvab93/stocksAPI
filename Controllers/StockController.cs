using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace StockWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        public readonly ILogger logger;
        public StockController(ILogger<StockController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public async Task<string> GetStockPrice()
        {
            using var client = new HttpClient();
            this.logger.LogInformation("Going to query api robinhood for top 5 stocks");
            HttpResponseMessage result = await client.GetAsync("https://api.robinhood.com/instruments/?top=5");
            this.logger.LogInformation("Got results from robinhood");
            string str = await result.Content.ReadAsStringAsync();
            return str;

        }

        [HttpGet("{companyName}")]
        public async Task<string> GetStockPrice(string companyName)
        {
            using var client = new HttpClient();
            HttpResponseMessage result = await client.GetAsync("https://api.robinhood.com/instruments/?symbol="+companyName);
            string str = await result.Content.ReadAsStringAsync();
            return str;
        }
    }
}