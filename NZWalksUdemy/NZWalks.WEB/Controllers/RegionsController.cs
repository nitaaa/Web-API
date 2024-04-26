using Microsoft.AspNetCore.Mvc;
using NZWalks.WEB.Models;
using NZWalksUdemy.WEB.Models.DTO;
using System.Text;
using System.Text.Json;

namespace NZWalks.WEB.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDTO> response = new List<RegionDTO>();
            try
            {
                // Get all Regions from web API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7243/api/regions"); //should be defined in appsettings
                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>()); 

               // ViewBag.Response = stringResponseBody;

            }
            catch (Exception ex)
            {
                // Log error
            }
            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel addRegionViewModel)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpRequest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7243/api/regions"),
                    Content = new StringContent(JsonSerializer.Serialize(addRegionViewModel), Encoding.UTF8, "application/json")
                };

                var httpResponseMessage = await client.SendAsync(httpRequest);
                httpResponseMessage.EnsureSuccessStatusCode();
                var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

                if (response != null)
                    return RedirectToAction("Index");

            }
            catch (Exception)
            {
                // Log Error
            };

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<RegionDTO>($"https://localhost:7243/api/regions/{id.ToString()}");
            if (response != null)
                return View(response);

            return View(null);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(RegionDTO editRegionViewModel)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpRequest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"https://localhost:7243/api/regions/{editRegionViewModel.Id}"),
                    Content = new StringContent(JsonSerializer.Serialize(editRegionViewModel), Encoding.UTF8, "application/json")
                };

                var httpResponseMessage = await client.SendAsync(httpRequest);
                httpResponseMessage.EnsureSuccessStatusCode();
                var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

                if (response != null)
                    return RedirectToAction("Edit", "Regions");

            }
            catch (Exception)
            {
                // Log Error
            };

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDTO regionDTO)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponse = await client.DeleteAsync($"https://localhost:7243/api/regions/{regionDTO.Id}");
                httpResponse.EnsureSuccessStatusCode();

                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                // Log Error
            };

            return View("Edit");
        }
    }
}
