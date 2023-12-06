using AmberEnrollmentInterface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Text;

namespace AmberEnrollmentInterface.Controllers
{
    public class EnrollmentController : Controller
    {
        const string BASEURL = "https://localhost:7142/api/Enrollment/";

        private readonly IHttpClientFactory _httpClientFactory;
        public EnrollmentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        #region Index Page
        public async Task<IActionResult> Index()
        {
            var studList = new List<Students>();
            var httpClient = _httpClientFactory.CreateClient("StudentAPI");

            HttpResponseMessage response = httpClient.GetAsync($"{BASEURL}").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                studList = JsonConvert.DeserializeObject<List<Students>>(data);
            }

            return View(studList);
        }
        #endregion

        #region Deatils Page
        [HttpGet]
        public IActionResult Details(int id)
        {
            Students students = new Students();

            var httpClient = _httpClientFactory.CreateClient("StudentAPI");
            HttpResponseMessage response = httpClient.GetAsync($"{BASEURL}{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<Students>(data)!;
            }

            return View(students);
        }
        #endregion


        #region Edit Page
        [HttpGet]
        public IActionResult Edit(int id)
        {
            List<Parishes> parishesList = new List<Parishes>();
            List<Programs> programsList = new List<Programs>();
            List<Shirts> shirtsList = new List<Shirts>();

            Students students = new Students();

            var httpClient = _httpClientFactory.CreateClient("StudentAPI");

            //Student Response Message
            HttpResponseMessage reponse = httpClient.GetAsync($"{BASEURL}{id}").Result;
            if (reponse.IsSuccessStatusCode)
            {
                var data = reponse.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<Students>(data)!;
            }

            //Parishes Response Message
            HttpResponseMessage pResponse = httpClient.GetAsync($"{BASEURL}Parish/").Result;
            if (pResponse.IsSuccessStatusCode)
            {
                var parhData = pResponse.Content.ReadAsStringAsync().Result;
                parishesList = JsonConvert.DeserializeObject<List<Parishes>>(parhData)!;
            }

            //Programs Response Message
            HttpResponseMessage pgResponse = httpClient.GetAsync($"{BASEURL}Programs/").Result;
            if (pgResponse.IsSuccessStatusCode)
            {
                var progData = pgResponse.Content.ReadAsStringAsync().Result;
                programsList = JsonConvert.DeserializeObject<List<Programs>>(progData)!;
            }

            //Shirts Response Message
            HttpResponseMessage sResponse = httpClient.GetAsync($"{BASEURL}Shirt/").Result;
            if (sResponse.IsSuccessStatusCode)
            {
                var sizeData = sResponse.Content.ReadAsStringAsync().Result;
                shirtsList = JsonConvert.DeserializeObject<List<Shirts>>(sizeData)!;
            }

            var ViewModel = new StudentsVM
            {
                Id = students.Id,
                Name = students.Name,
                Email = students.Email,
                Telephone = students.Telephone,
                ParishsId = students.ParishId,
                ProgramsId = students.ProgramsId,
                ShirtsId = students.ShirtId,
                SelectedParishId = students.ParishId,
                SelectedProgramsId = students.ProgramsId,
                SelectedShirtId = students.ShirtId,


                ParishList = parishesList.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()

                }).ToList(),

                ProgramsList = programsList.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()

                }).ToList(),

                ShirtsList = shirtsList.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()

                }).ToList()

            };

            return View(ViewModel);
        }
        [HttpPost]
        public IActionResult Edit(int id, StudentsVM studentsVM)
        {
            var httpClient = _httpClientFactory.CreateClient("StudentAPI");

            var student = new Students
            {
                Id = id,
                Name = studentsVM.Name,
                Email = studentsVM.Email,
                Telephone = studentsVM.Telephone,
                ParishId = studentsVM.SelectedParishId,
                ProgramsId = studentsVM.SelectedProgramsId,
                ShirtId = studentsVM.SelectedShirtId
            };

            var json = JsonConvert.SerializeObject(student);
            var content = new StringContent(json, Encoding.UTF8, "application/Json");

            HttpResponseMessage response = httpClient.PutAsync($"{BASEURL}{id}", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to Update Student");
                return View(studentsVM);
            }

        }
        #endregion

        #region Create View
        [HttpGet]
        public IActionResult Create()
        {
            //Create List to store parameters
            List<Parishes> parishesList = new List<Parishes>();
            List<Programs> programsList = new List<Programs>();
            List<Shirts> shirtsList = new List<Shirts>();
            
            //ClientFactory to connect to API
            var httpClient = _httpClientFactory.CreateClient("StudentAPI");
            
            //HTTP Response List to grab information from the database
            //Parishes Response Message
            HttpResponseMessage pResponse = httpClient.GetAsync($"{BASEURL}Parish/").Result;
            if (pResponse.IsSuccessStatusCode)
            {
                var parhData = pResponse.Content.ReadAsStringAsync().Result;
                parishesList = JsonConvert.DeserializeObject<List<Parishes>>(parhData)!;
            }

            //Programs Response Message
            HttpResponseMessage pgResponse = httpClient.GetAsync($"{BASEURL}Programs/").Result;
            if (pgResponse.IsSuccessStatusCode)
            {
                var progData = pgResponse.Content.ReadAsStringAsync().Result;
                programsList = JsonConvert.DeserializeObject<List<Programs>>(progData)!;
            }

            //Shirts Response Message
            HttpResponseMessage sResponse = httpClient.GetAsync($"{BASEURL}Shirt/").Result;
            if (sResponse.IsSuccessStatusCode)
            {
                var sizeData = sResponse.Content.ReadAsStringAsync().Result;
                shirtsList = JsonConvert.DeserializeObject<List<Shirts>>(sizeData)!;
            }

            //Create ViewModels - to store Response data in a dropdown List
            var ViewModel = new StudentsVM
            {

                ParishList = parishesList.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()

                }).ToList(),

                ProgramsList = programsList.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()

                }).ToList(),

                ShirtsList = shirtsList.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()

                }).ToList()

            };

            //Return the View
            return View(ViewModel);
        }
        [HttpPost]
        public IActionResult Create(StudentsVM studentsVM)
        {
            //ClientFactory to connect to the database
            var httpClient = _httpClientFactory.CreateClient("StudentAPI");

            //Create a Model to store data gataher from the viewModel into a simple Model
            var student = new Students
            {
                Name = studentsVM.Name,
                Email = studentsVM.Email,
                Telephone = studentsVM.Telephone,
                ParishId = studentsVM.SelectedParishId,
                ProgramsId = studentsVM.SelectedProgramsId,
                ShirtId = studentsVM.SelectedShirtId,
            };

            //Json Convert to serialize data back into a readable format
            var json = JsonConvert.SerializeObject(student);
            var content = new StringContent(json, Encoding.UTF8, "application/Json");

            //HTTP Message to store data and post to the Database
            HttpResponseMessage response = httpClient.PostAsync($"{BASEURL}", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to Create Student Data");
                return View(studentsVM);
            }

            
        }

        #endregion


        #region Delete View
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("StudentAPI");
            
            HttpResponseMessage response = httpClient.DeleteAsync($"{BASEURL}{id}").Result;
            if (response.IsSuccessStatusCode)
            {
               return RedirectToAction("Delete");
            }

            return RedirectToAction("Index");
        }
        
        #endregion

    }
}
