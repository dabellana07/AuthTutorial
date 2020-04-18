using AuthTutorial.Web.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuthTutorial.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var tokenClient = new HttpClient();
            var tokenResponse = await tokenClient.RequestTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:44316/connect/token",
                GrantType = "client_credentials",
                ClientId = "WebApp",
                ClientSecret = "MySecret",
                Scope = "DemoApi"
            });

            object model;
            if (tokenResponse.IsError)
            {
                model = "Error...could not get access token for API";
            }
            else
            {
                var apiClient = new HttpClient();
                apiClient.SetBearerToken(tokenResponse.AccessToken);
                var response = await apiClient.GetAsync("https://localhost:44378/api/text/welcome");
                if (response.IsSuccessStatusCode)
                {
                    model = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    model = "Error could not retrieve text";
                }
            }
            return View(model);
        }


        public IActionResult Spa()
        {
            return View();
        }

        [Authorize]
        [Route("/userinformation")]
        public IActionResult UserInformation()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
