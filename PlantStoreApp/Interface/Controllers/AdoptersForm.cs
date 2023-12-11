using Interface.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Interface.Controllers
{
    public class AdoptersForm : Controller
    {

        const string BASEURL = "https://localhost:7277/api/";
        private readonly IHttpClientFactory _httpClientFactory;

        public AdoptersForm(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        // Adoption Application/Page Action



        #region List Page
        public IActionResult Index()
        {
            var AdopterList = new List<Adopter>();
            var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");

            HttpResponseMessage response = httpClient.GetAsync($"{BASEURL}Plants/Adopter").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                AdopterList = JsonConvert.DeserializeObject<List<Adopter>>(data);
            }

            return View(AdopterList);
        }
        #endregion





        #region Deatils 
        [HttpGet]
        public IActionResult Details(int id)
        {
            var Adopt = new Models.Adopter();
            var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");
            HttpResponseMessage response = httpClient.GetAsync($"{BASEURL}Plants/Adopter/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                Adopt = JsonConvert.DeserializeObject<Models.Adopter>(data);
                return View(Adopt);


            }
            return View();
        }
        #endregion



        #region Adoption Application (CREATE)

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Adopter());
        }
        [HttpPost]
        public IActionResult Create(Adopter adopter)
        {
            try
            {
                // Use the HttpClientFactory to create an HttpClient instance
                var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");

                // Serialize the doctor data to JSON
                var json = JsonConvert.SerializeObject(adopter);
                var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

                // Send a POST request to create the doctor
                HttpResponseMessage response = httpClient.PostAsync($"{BASEURL}Plants/Adopter", jsonContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    // Successfully created the doctor
                    return RedirectToAction("Index"); // Redirect to the doctor list page or a success page
                }
                else
                {
                    // Handle the case where the API request is not successful
                    // You can return an error view or handle it based on your application's requirements.
                    ModelState.AddModelError(string.Empty, "Unable to create a Applicant");
                    return View(adopter);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, or return an error view as appropriate for your application.
                return View("Error");
            }
        }
        #endregion




        #region Edit Page

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Use the HttpClientFactory to create an HttpClient instance
            var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");

            // Get the existing doctor data from the API
            HttpResponseMessage response = await httpClient.GetAsync($"{BASEURL}Plants/Adopter/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the doctor data from JSON
                var Adopt = JsonConvert.DeserializeObject<Adopter>(await response.Content.ReadAsStringAsync());

                // Return the doctor object to the view
                return View(Adopt);
            }
            else
            {
                // Handle the case where the API request is not successful
                // You can return an error view or handle it based on your application's requirements.
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Adopter adopter)
        {
            if (ModelState.IsValid)
            {
                // Use the HttpClientFactory to create an HttpClient instance
                var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");

                // Serialize the updated doctor object to JSON
                var jsonDoctor = JsonConvert.SerializeObject(adopter);
                var content = new StringContent(jsonDoctor, Encoding.UTF8, "application/json");

                // Send the updated doctor data to the API
                HttpResponseMessage response = await httpClient.PutAsync($"{BASEURL}Plants/Adopter/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Redirect to a success page or the updated doctor's details page
                    return RedirectToAction("Details", new { id = id });
                }
                else
                {
                    // Handle the case where the API request is not successful
                    // You can return an error view or handle it based on your application's requirements.
                    return View("Error");
                }
            }
            else
            {
                // Model validation failed, return to the edit view with validation errors
                return View(adopter);
            }
        }
        #endregion


        #region Delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            // Use the HttpClientFactory to create an HttpClient instance
            var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");

            // Get the existing doctor data from the API
            HttpResponseMessage response = await httpClient.GetAsync($"{BASEURL}Plants/Adopter/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the doctor data from JSON
                var adopt = JsonConvert.DeserializeObject<Adopter>(await response.Content.ReadAsStringAsync());

                // Return the doctor object to the delete confirmation view
                return View(adopt);
            }
            else
            {
                // Handle the case where the API request is not successful
                // You can return an error view or handle it based on your application's requirements.
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Deleteconfirm(int id)
        {
            try
            {
                // Use the HttpClientFactory to create an HttpClient instance
                var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");

                // Send a DELETE request to delete the doctor
                HttpResponseMessage response = await httpClient.DeleteAsync($"{BASEURL}Plants/Adopter/{id}");

                if (response.IsSuccessStatusCode)
                {
                    // Successfully deleted the doctor
                    return RedirectToAction("Index"); // Redirect to the doctor list page or a success page
                }
                else
                {
                    // Handle the case where the API request is not successful
                    // You can return an error view or handle it based on your application's requirements.
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, or return an error view as appropriate for your application.
                return View("Error");
            }
        }

        #endregion
    }
}
