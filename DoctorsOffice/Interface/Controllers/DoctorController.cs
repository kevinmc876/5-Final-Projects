using Interface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;

namespace Interface.Controllers
{
    public class DoctorController : Controller
    {




        const string BASEURL = "https://localhost:7260/api/Record/";

        private readonly IHttpClientFactory _httpClientFactory;
        public DoctorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }



        #region Index Page
        public IActionResult Index()
        {
            var DoctorList = new List<Doctor>();
            var httpClient = _httpClientFactory.CreateClient("DoctorOffice");

            HttpResponseMessage response = httpClient.GetAsync($"{BASEURL}Doctor").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                DoctorList = JsonConvert.DeserializeObject<List<Doctor>>(data);
            }

            return View(DoctorList);
        }
        #endregion

        #region Deatils Page
        [HttpGet]
        public IActionResult Details(int id)
        {
            var Doc = new Models.Doctor();
            var httpClient = _httpClientFactory.CreateClient("DoctorOffice");
            HttpResponseMessage response = httpClient.GetAsync($"{BASEURL}Doctor/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                Doc = JsonConvert.DeserializeObject<Models.Doctor>(data);
                return View(Doc);


            }
            return View();
        }
        #endregion

        #region Create page

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Doctor());
        }

        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            try
            {
                // Use the HttpClientFactory to create an HttpClient instance
                var httpClient = _httpClientFactory.CreateClient("DoctorOffice");

                // Serialize the doctor data to JSON
                var json = JsonConvert.SerializeObject(doctor);
                var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

                // Send a POST request to create the doctor
                HttpResponseMessage response = httpClient.PostAsync($"{BASEURL}Doctor", jsonContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    // Successfully created the doctor
                    return RedirectToAction("Index"); // Redirect to the doctor list page or a success page
                }
                else
                {
                    // Handle the case where the API request is not successful
                    // You can return an error view or handle it based on your application's requirements.
                    ModelState.AddModelError(string.Empty, "Unable to create a Doctor");
                    return View(doctor);
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
            var httpClient = _httpClientFactory.CreateClient("DoctorOffice");

            // Get the existing doctor data from the API
            HttpResponseMessage response = await httpClient.GetAsync($"{BASEURL}Doctor/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the doctor data from JSON
                var doctor = JsonConvert.DeserializeObject<Doctor>(await response.Content.ReadAsStringAsync());

                // Return the doctor object to the view
                return View(doctor);
            }
            else
            {
                // Handle the case where the API request is not successful
                // You can return an error view or handle it based on your application's requirements.
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                // Use the HttpClientFactory to create an HttpClient instance
                var httpClient = _httpClientFactory.CreateClient("DoctorOffice");

                // Serialize the updated doctor object to JSON
                var jsonDoctor = JsonConvert.SerializeObject(doctor);
                var content = new StringContent(jsonDoctor, Encoding.UTF8, "application/json");

                // Send the updated doctor data to the API
                HttpResponseMessage response = await httpClient.PutAsync($"{BASEURL}Doctor/{id}", content);

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
                return View(doctor);
            }
        }
        #endregion


        #region Delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            // Use the HttpClientFactory to create an HttpClient instance
            var httpClient = _httpClientFactory.CreateClient("DoctorOffice");

            // Get the existing doctor data from the API
            HttpResponseMessage response = await httpClient.GetAsync($"{BASEURL}Doctor/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the doctor data from JSON
                var doctor = JsonConvert.DeserializeObject<Doctor>(await response.Content.ReadAsStringAsync());

                // Return the doctor object to the delete confirmation view
                return View(doctor);
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
                var httpClient = _httpClientFactory.CreateClient("DoctorOffice");

                // Send a DELETE request to delete the doctor
                HttpResponseMessage response = await httpClient.DeleteAsync($"{BASEURL}Doctor/{id}");

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

