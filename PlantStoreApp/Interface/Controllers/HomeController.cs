using Interface.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using System.Numerics;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Interface.Controllers
{
    public class HomeController : Controller
    {



        const String BASEURL = "https://localhost:7277/api/";

        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }






        // ----------------------------------------------------------------------------------------------------------------------------------------------
        // Home Page Action
        public IActionResult Index()
        {
            // Logic to fetch featured plants or other relevant data for the home page
            return View();
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------



        // Adoption Process/Page Action
        public IActionResult AdoptionProcess()
        {
            // Logic to explain the adoption process
            return View();
        } 
        // ----------------------------------------------------------------------------------------------------------------------------------------------

        public IActionResult FAQ()
        {
            // Logic to provide frequently asked questions and answers

        // FAQs Page Action
            return View();
        }

        // About Us/Page Action
        public IActionResult AboutUs()
        {
            // Logic to provide information about the organization or individuals
            return View();
        }

        // Contact Us/Page Action
        public IActionResult ContactUs()
        {
            // Logic to provide contact details
            return View();
        }

        // Testimonials/Success Stories Page Action
        public IActionResult Testimonials()
        {
            // Logic to fetch and display testimonials or success stories
            return View();
        }



        // Terms and Conditions/Privacy Policy Page Action
        public IActionResult TermsAndConditions()
        {
            // Logic to display terms and conditions or privacy policy
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}