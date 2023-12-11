using Interface.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PlantStoreApi.Models;
using System.Numerics;
using System.Text;
using Adopter = Interface.Models.Adopter;
using AdoptionRequest = Interface.Models.AdoptionRequest;
using Plant = Interface.Models.Plant;

namespace Interface.Controllers
{

    public class AdoptionForm : Controller
    {
        const string BASEURL = "https://localhost:7277/api/";
        private readonly IHttpClientFactory _httpClientFactory;

        public AdoptionForm(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        #region List Page
        // Adoption Process/Page Action
        public IActionResult Index()
        {
            var AdopterList = new List<AdoptionRequest>();
            var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");

            HttpResponseMessage response = httpClient.GetAsync($"{BASEURL}Plants/Request").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                AdopterList = JsonConvert.DeserializeObject<List<AdoptionRequest>>(data);
            }

            return View(AdopterList);
        }

        #endregion


        #region Deatils 
        [HttpGet]
        public IActionResult Details(int id)
        {
            var Adopt = new Models.AdoptionRequest();
            var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");

            HttpResponseMessage response = httpClient.GetAsync($"{BASEURL}Plants/Request/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                Adopt = JsonConvert.DeserializeObject<Models.AdoptionRequest>(data);
            }

            return View(Adopt);
        }
        #endregion

        #region AdoptionRequest
        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                var Plantlist = new List<Plant>();
                var Adopterlist = new List<Adopter>();

                var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");

                //Reuest a List of Plants
                HttpResponseMessage plantResponse = httpClient.GetAsync($"{BASEURL}Plants").Result;
                if (plantResponse.IsSuccessStatusCode)
                {
                    var plantData = plantResponse.Content.ReadAsStringAsync().Result;
                    Plantlist = JsonConvert.DeserializeObject<List<Plant>>(plantData);
                }
                //Request a List of Adopters
                HttpResponseMessage adopterResponse = httpClient.GetAsync($"{BASEURL}Plants/Adopter").Result;
                if (adopterResponse.IsSuccessStatusCode)
                {
                    var adopterData = adopterResponse.Content.ReadAsStringAsync().Result;
                    Adopterlist = JsonConvert.DeserializeObject<List<Adopter>>(adopterData);
                }

                var ViewModel = new AdoptionRequestVM
                {
                    Plantlist = Plantlist.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.ID.ToString()
                    }).ToList(),

                    Adopterlist = Adopterlist.Select(x => new SelectListItem { 
                    
                        Text = x.FirstName + " " + x.LastName,
                        Value = x.ID.ToString()

                    }).ToList()

                 };

                return View(ViewModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Create(AdoptionRequestVM model)
        {
            var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");

            var adoptionRequest = new AdoptionRequest
            {
                ID = model.ID,
                PlantID = model.SelectedPlantID,
                AdopterID = model.SelectedAdopterID,
                RequestDate = model.RequestDate,
                Status = model.Status,
                Message = model.Message,
                ApprovalStatus = model.ApprovalStatus,
            };

            var json = JsonConvert.SerializeObject(adoptionRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/Json");

            HttpResponseMessage response = httpClient.PostAsync($"{BASEURL}Plants/Request", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to Create Request");
                return View(model);
            }
        }

        #endregion




        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");

                var response = await httpClient.GetAsync($"{BASEURL}Plants/Request/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var adoptionRequestVM = JsonConvert.DeserializeObject<AdoptionRequest>(data);

                    response = await httpClient.GetAsync($"{BASEURL}Plants");
                    if (response.IsSuccessStatusCode)
                    {
                        var data2 = await response.Content.ReadAsStringAsync();
                        var plantList = JsonConvert.DeserializeObject<List<Plant>>(data2);

                        response = await httpClient.GetAsync($"{BASEURL}Adopter");
                        if (response.IsSuccessStatusCode)
                        {
                            var data3 = await response.Content.ReadAsStringAsync();
                            var adopterList = JsonConvert.DeserializeObject<List<Adopter>>(data3);

                            // Construct the AdoptionRequestVM here using the obtained data and return the view
                            var viewModel = new AdoptionRequestVM
                            {
                                // Construct your ViewModel here with the obtained data
                                Plantlist = plantList.Select(x => new SelectListItem
                                {
                                    Text = x.Name,
                                    Value = x.ID.ToString()
                                }).ToList(),

                                Adopterlist = adopterList.Select(x => new SelectListItem
                                {
                                    Text = x.FirstName,
                                    Value = x.ID.ToString()
                                }).ToList(),
                            };

                            return View(viewModel);
                        }
                    }
                }

                // Handle cases where API requests fail or data isn't retrieved properly
                return View("Error");
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                return View("Error", ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdoptionRequestVM adoptionRequestVM)
        {
            try
            {
                if (adoptionRequestVM == null)
                {
                    return BadRequest();
                }

                var httpClient = _httpClientFactory.CreateClient("PlantStoreApi");

                // Map ViewModel to the Appointment object
                var adoptionRequest = new AdoptionRequest
                {
                    // Map properties from appointmentVM to appointment object
                    // ...

                };

                var jsonContent = new StringContent(JsonConvert.SerializeObject(adoptionRequest), Encoding.UTF8, "application/json");
                
                var response = await httpClient.PutAsync($"{BASEURL}Plants/{adoptionRequest.ID}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); // Successfully updated appointment
                }
                else
                {
                    // Handle unsuccessful API requests
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                // Log and handle exceptions appropriately
                return View("Error", ex.Message);
            }
        }

    }
}
