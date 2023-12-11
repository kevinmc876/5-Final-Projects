using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PlantInterface.Models;

namespace PlantInterface.Controllers
{


    //[Authorize(Roles ="Admin, Manager")]
    public class PlantController : Controller
    {


        private string BASEURL = "https://localhost:7277/api/";

        private readonly IHttpClientFactory _httpClientFactory;
        public PlantController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet]


        #region Plant Index Page
        public async Task<IActionResult> Index()
        {
            try
            {
                var PlantList = new List<Plant>();
                var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");

                HttpResponseMessage response = await httpClient.GetAsync($"{BASEURL}Plant");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    PlantList = JsonConvert.DeserializeObject<List<Plant>>(data);
                }
                else
                {
                    // Handle unsuccessful API request here
                    // For instance, log the error or handle the response code accordingly
                    // You might want to return an error view or display an error message to the user
                    // Example: return RedirectToAction("Error");
                }

                return View(PlantList);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // Example: return RedirectToAction("Error");
                throw; // Only rethrow the exception if it should be propagated upwards
            }
        }
        #endregion


        #region Deatils Plant Page
        [HttpGet]
        public IActionResult Details(int id)
        {
            var Pln = new Models.Plant();
            var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");
            HttpResponseMessage response = httpClient.GetAsync($"{BASEURL}Plant/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                Pln = JsonConvert.DeserializeObject<Models.Plant>(data);
                return View(Pln);


            }
            return View();
        }

        #endregion


    }
}