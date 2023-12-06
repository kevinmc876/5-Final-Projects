using Interface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json;
using System.Text;

namespace Interface.Controllers
{
    public class Appointment : Controller
    {
        // client factory methods

        const string BASEURL = "https://localhost:7260/api/Record/";

        private readonly IHttpClientFactory _httpClientFactory;
        public Appointment(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }




        #region Index Page
        public async Task<IActionResult> Index()
        {
            var AptList = new List<Models.Appointment>();
            var httpClient = _httpClientFactory.CreateClient("DoctorOffice");

            HttpResponseMessage response = httpClient.GetAsync($"{BASEURL}Appointment").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                AptList = JsonConvert.DeserializeObject<List<Models.Appointment>>(data);
            }

            return View(AptList);
        }
        #endregion


        #region Deatils Page
        [HttpGet]
        public IActionResult Details(int id)
        {
            var Apt = new Models.Appointment();
            var httpClient = _httpClientFactory.CreateClient("DoctorOffice");
            HttpResponseMessage response = httpClient.GetAsync($"{BASEURL}Appointment/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                Apt = JsonConvert.DeserializeObject<Models.Appointment>(data);
                return View(Apt);


            }
            return View();
        }
        #endregion

        #region Delete View
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var Apt = new Models.Appointment();
            var httpClient = _httpClientFactory.CreateClient("DoctorOffice");
            HttpResponseMessage response = httpClient.DeleteAsync($"{BASEURL}Appointment/{id}").Result;

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
            // Create lists to store parameters
            var patientList = new List<Patient>();
            var doctorList = new List<Doctor>();
            var visitMethodList = new List<VisitMethod>();

            try
            {
                // Use the HttpClientFactory to connect to the API
                var httpClient = _httpClientFactory.CreateClient("DoctorOffice");

                HttpResponseMessage response = httpClient.GetAsync($"{BASEURL}Patient").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    patientList = JsonConvert.DeserializeObject<List<Patient>>(data);

                    response = httpClient.GetAsync($"{BASEURL}Doctor").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var data2 = response.Content.ReadAsStringAsync().Result;
                        doctorList = JsonConvert.DeserializeObject<List<Doctor>>(data2);

                        response = httpClient.GetAsync($"{BASEURL}VisitMethod").Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var data3 = response.Content.ReadAsStringAsync().Result;
                            visitMethodList = JsonConvert.DeserializeObject<List<VisitMethod>>(data3);
                        }
                        return View(new AppointmentVM
                        {
                            PatientList = patientList.Select(x => new SelectListItem
                            {
                                Text = x.FullName,
                                Value = x.Id.ToString()
                            }).ToList(),

                            Doctorlist = doctorList.Select(x => new SelectListItem
                            {
                                Text = x.FullName,
                                Value = x.Id.ToString()
                            }).ToList(),

                            VisitMethodList = visitMethodList.Select(x => new SelectListItem
                            {
                                Text = x.MethodName,
                                Value = x.Id.ToString()
                            }).ToList()
                        });

                    }
                }

                // Handle the case where the API request is not successful
                // You can return an error view or handle it based on your application's requirements.
                return View("Error");
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., network issues, JSON deserialization errors)
                // Log the error, return an error view, or handle it as appropriate for your application.
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppointmentVM appointmentVM)
        {
            if (appointmentVM == null)
            {
                return BadRequest();
            }

            try
            {
                // Use the HttpClientFactory to create an HttpClient instance
                var httpClient = _httpClientFactory.CreateClient("DoctorOffice");

                // Map the view model to the Appointment object
                var AppointmentVM = new AppointmentVM
                {
                    Id = appointmentVM.Id,
                    PatientId = appointmentVM.PatientId,
                    DoctorId = appointmentVM.DoctorId,
                    VisitMethodId = appointmentVM.VisitMethodId,
                    AppointmentDateTime = appointmentVM.AppointmentDateTime,
                    Created = appointmentVM.Created,
                    Description = appointmentVM.Description,

                    // Map other properties as needed
                };

                // Serialize the appointment data to JSON
                var jsonContent = new StringContent(JsonConvert.SerializeObject(appointmentVM), Encoding.UTF8, "application/json");

                // Send a POST request to create the appointment
                HttpResponseMessage response = await httpClient.PostAsync($"{BASEURL}Appointment", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    // Successfully created the appointment
                    return RedirectToAction("Index"); // Redirect to the appointment list page
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
            var patientList = new List<Patient>();
            var doctorList = new List<Doctor>();
            var visitMethodList = new List<VisitMethod>();
            AppointmentVM appointmentVM;

            try
            {
                // Use the HttpClientFactory to connect to the API
                var httpClient = _httpClientFactory.CreateClient("DoctorOffice");

                HttpResponseMessage response = httpClient.GetAsync($"{BASEURL}Appointment/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    appointmentVM = JsonConvert.DeserializeObject<AppointmentVM>(data)!;

                    response = httpClient.GetAsync($"{BASEURL}Patient").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var data2 = response.Content.ReadAsStringAsync().Result;
                        patientList = JsonConvert.DeserializeObject<List<Patient>>(data2);

                        response = httpClient.GetAsync($"{BASEURL}Doctor").Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var data3 = response.Content.ReadAsStringAsync().Result;
                            doctorList = JsonConvert.DeserializeObject<List<Doctor>>(data3);

                            response = httpClient.GetAsync($"{BASEURL}VisitMethod").Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var data4 = response.Content.ReadAsStringAsync().Result;
                                visitMethodList = JsonConvert.DeserializeObject<List<VisitMethod>>(data4);
                            }
                        }
                    }

                    return View(new AppointmentVM
                    {
                        Id = appointmentVM.Id,
                        PatientId = appointmentVM.PatientId,
                        DoctorId = appointmentVM.DoctorId,
                        VisitMethodId = appointmentVM.VisitMethodId,
                        AppointmentDateTime = appointmentVM.AppointmentDateTime,
                        Created = appointmentVM.Created,
                        Description = appointmentVM.Description,


                        PatientList = patientList.Select(x => new SelectListItem
                        {
                            Text = x.FullName,
                            Value = x.Id.ToString(),
                            Selected = x.Id == appointmentVM.PatientId
                        }).ToList(),
                        Doctorlist = doctorList.Select(x => new SelectListItem
                        {
                            Text = x.FullName,
                            Value = x.Id.ToString(),
                            Selected = x.Id == appointmentVM.DoctorId
                        }).ToList(),
                        VisitMethodList = visitMethodList.Select(x => new SelectListItem
                        {
                            Text = x.MethodName,
                            Value = x.Id.ToString(),
                            Selected = x.Id == appointmentVM.VisitMethodId
                        }).ToList()
                    });
                }

                // Handle the case where the API request is not successful
                // You can return an error view or handle it based on your application's requirements.
                return View("Error");
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., network issues, JSON deserialization errors)
                // Log the error, return an error view, or handle it as appropriate for your application.
                return View(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AppointmentVM appointmentVM)
        {
            if (appointmentVM == null)
            {
                return BadRequest();
            }

            try
            {
                // Use the HttpClientFactory to create an HttpClient instance
                var httpClient = _httpClientFactory.CreateClient("DoctorOffice");

                // Map the view model to the Appointment object
                var AppointmentVM = new AppointmentVM
                {
                    Id = appointmentVM.Id,
                    PatientId = appointmentVM.PatientId,
                    DoctorId = appointmentVM.DoctorId,
                    VisitMethodId = appointmentVM.VisitMethodId,
                    AppointmentDateTime = appointmentVM.AppointmentDateTime,
                    Created = appointmentVM.Created,
                    Description = appointmentVM.Description,

                    // Map other properties as needed
                };
                // Serialize the appointment data to JSON
                var jsonContent = new StringContent(JsonConvert.SerializeObject(appointmentVM), Encoding.UTF8, "application/json");

                // Send a PUT request to update the appointment
                HttpResponseMessage response = await httpClient.PutAsync($"{BASEURL}Appointment/{appointmentVM.Id}", jsonContent);




                if (response.IsSuccessStatusCode)
                {
                    // Successfully updated the appointment
                    return RedirectToAction("Index"); // Redirect to the appointment list page
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

            #endregion
        }


    }

}
