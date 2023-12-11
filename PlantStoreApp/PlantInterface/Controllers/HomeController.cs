using Microsoft.AspNetCore.Mvc;
using PlantInterface.Models;
using System.Diagnostics;

namespace PlantInterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Home Page Action
        public IActionResult Index()
        {
            // Logic to fetch featured plants or other relevant data for the home page
            return View();
        }

        // Plants Catalog/Listing Page Action
        public IActionResult PlantCatalog()
        {
            // Logic to fetch a list of available plants for adoption
            return View();
        }

        // Plant Details Page Action
        public IActionResult PlantDetails(int id)
        {
            // Logic to fetch details of a specific plant by its ID
            return View();
        }

        // Adoption Process/Page Action
        public IActionResult AdoptionProcess()
        {
            // Logic to explain the adoption process
            return View();
        }

        // Adoption Application/Page Action
        public IActionResult AdoptionApplication()
        {
            // Logic to handle the adoption application form
            return View();
        }

        // FAQs Page Action
        public IActionResult FAQ()
        {
            // Logic to provide frequently asked questions and answers
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