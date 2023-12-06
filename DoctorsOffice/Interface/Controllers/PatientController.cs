using Interface.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Interface.Controllers
{
    public class PatientController : Controller
    {



        const string BASEURL = "https://localhost:7260/api/Record/";

        private readonly IHttpClientFactory _httpClientFactory;
        public PatientController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }



        #region Index Page
        public async Task<IActionResult> Index()
        {
            var PatientList = new List<Patient>();
            var httpClient = _httpClientFactory.CreateClient("DoctorOffice");

            HttpResponseMessage response = httpClient.GetAsync($"{BASEURL}Patient").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                PatientList = JsonConvert.DeserializeObject<List<Patient>>(data);
            }

            return View(PatientList);
        }
        #endregion

        #region Deatils Page
        [HttpGet]
        public IActionResult Details(int id)
        {
            var Pn = new Models.Patient();
            var httpClient = _httpClientFactory.CreateClient("DoctorOffice");
            HttpResponseMessage response = httpClient.GetAsync($"{BASEURL}Patient/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                Pn = JsonConvert.DeserializeObject<Models.Patient>(data);
                return View(Pn);


            }
            return View();
        }
        #endregion


        #region Delete View
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var Patient = new Models.Patient();
            var httpClient = _httpClientFactory.CreateClient("DoctorOffice");
            HttpResponseMessage response = httpClient.DeleteAsync($"{BASEURL}Patient/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Delete");


            }

            return RedirectToAction("Index");


        }

        #endregion


        #region Create View

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Patient());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Patient patient)
        {
            if (patient == null)
            {
                return BadRequest();
            }

            try
            {
                // Use the HttpClientFactory to create an HttpClient instance
                var httpClient = _httpClientFactory.CreateClient("DoctorOffice");

                // Serialize the patient data to JSON
                var jsonContent = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");

                // Send a POST request to create the patient
                HttpResponseMessage response = await httpClient.PostAsync($"{BASEURL}Patient", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    // Successfully created the patient
                    return RedirectToAction("Index"); // Redirect to the patient list page or a success page
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

        #region Edit View

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var patient = new Patient();
            try
            {
                // Use the HttpClientFactory to connect to the API
                var httpClient = _httpClientFactory.CreateClient("DoctorOffice");

                HttpResponseMessage response = httpClient.GetAsync($"{BASEURL}Patient/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    patient = JsonConvert.DeserializeObject<Patient>(data);

                    return View(patient);
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
                // Handle exceptions (e.g., network issues, JSON deserialization errors)
                // Log the error, return an error view, or handle it as appropriate for your application.
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Patient patient)
        {
            if (patient == null)
            {
                return BadRequest();
            }

            try
            {
                // Use the HttpClientFactory to create an HttpClient instance
                var httpClient = _httpClientFactory.CreateClient("DoctorOffice");

                // Serialize the patient data to JSON
                var jsonContent = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");

                // Send a PUT request to update the patient
                HttpResponseMessage response = await httpClient.PutAsync($"{BASEURL}Patient/{patient.Id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    // Successfully updated the patient
                    return RedirectToAction("Index"); // Redirect to the patient list page or another appropriate action
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
