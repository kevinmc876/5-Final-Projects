using Interface.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Text;

namespace Interface.Controllers
{
    public class PlantsController : Controller
    {
        const string BASEURL = "https://localhost:7277/api/";
        private readonly IHttpClientFactory _httpClientFactory;

        public PlantsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            var Plantlist = new List<Plant>();
            var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");

            HttpResponseMessage response = httpClient.GetAsync($"{BASEURL}Plants").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                Plantlist = JsonConvert.DeserializeObject<List<Plant>>(data);

            }

            return View(Plantlist);
        }


        // ----------------------------------------------------------------------------------------------------------------------------------------------

        // Plant Details Page Action
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var plant = new Plant();
                var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");
                HttpResponseMessage response = await httpClient.GetAsync($"{BASEURL}Plants/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    plant = JsonConvert.DeserializeObject<Plant>(data);
                    // Do something with the retrieved plant data if needed
                    return View(plant);
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return NotFound(); // Return a 404 Not Found status
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home"); // Redirect to an error page
                    }
                }
            }
            catch (HttpRequestException)
            {
                // Log the exception or handle it accordingly
                return RedirectToAction("Error", "Home"); // Redirect to an error page
            }

        }


        // ----------------------------------------------------------------------------------------------------------------------------------------------

        // Create method
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Plant plant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");

                    var json = JsonConvert.SerializeObject(plant);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync($"{BASEURL}Plants/", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // If creation is successful, redirect to the Details action of the newly created plant
                        var responseData = await response.Content.ReadAsStringAsync();
                        var createdPlant = JsonConvert.DeserializeObject<Plant>(responseData);
                        return RedirectToAction("Details", new { id = createdPlant.ID });
                    }
                    else
                    {
                        if (response.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ModelState.AddModelError(string.Empty, "Invalid data provided."); // Or add appropriate error message
                        }
                        else
                        {
                            return RedirectToAction("Error", "Home"); // Redirect to an error page
                        }
                    }
                }
            }
            catch (HttpRequestException)
            {
                // Log the exception or handle it accordingly
                return RedirectToAction("Error", "Home"); // Redirect to an error page
            }

            // If ModelState is invalid or any other issues, return to the Create view with the provided data
            return View(plant);
        }

        //--------------------------------------------------------------------------------------------


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");
                HttpResponseMessage response = await httpClient.GetAsync($"{BASEURL}Plants/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var plant = JsonConvert.DeserializeObject<Plant>(data);

                    return View(plant);
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return NotFound(); // Return a 404 Not Found status
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home"); // Redirect to an error page
                    }
                }
            }
            catch (HttpRequestException)
            {
                // Log the exception or handle it accordingly
                return RedirectToAction("Error", "Home"); // Redirect to an error page
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Use AntiForgeryToken for enhanced security
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");
                HttpResponseMessage response = await httpClient.DeleteAsync($"{BASEURL}Plants/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); // Redirect to the list of plants or any other appropriate action
                }
                else
                {
                    return RedirectToAction("Error", "Home"); // Redirect to an error page
                }
            }
            catch (HttpRequestException)
            {
                // Log the exception or handle it accordingly
                return RedirectToAction("Error", "Home"); // Redirect to an error page
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");
                HttpResponseMessage response = await httpClient.GetAsync($"{BASEURL}Plants/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var plant = JsonConvert.DeserializeObject<Plant>(responseData);
                    return View(plant);
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return RedirectToAction("NotFound", "Error"); // Redirect to a not found error page
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home"); // Redirect to an error page
                    }
                }
            }
            catch (HttpRequestException)
            {
                // Log the exception or handle it accordingly
                return RedirectToAction("Error", "Home"); // Redirect to an error page
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Plant plant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");

                    var json = JsonConvert.SerializeObject(plant);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PutAsync($"{BASEURL}Plants/{id}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Details", new { id = id });
                    }
                    else
                    {
                        if (response.StatusCode == HttpStatusCode.NotFound)
                        {
                            return RedirectToAction("NotFound", "Error"); // Redirect to a not found error page
                        }
                        else if (response.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ModelState.AddModelError(string.Empty, "Invalid data provided."); // Or add appropriate error message
                        }
                        else
                        {
                            return RedirectToAction("Error", "Home"); // Redirect to an error page
                        }
                    }
                }
            }
            catch (HttpRequestException)
            {
                // Log the exception or handle it accordingly
                return RedirectToAction("Error", "Home"); // Redirect to an error page
            }

            // If ModelState is invalid or any other issues, return to the Edit view with the provided data
            return View(plant);
        }






    }
}

