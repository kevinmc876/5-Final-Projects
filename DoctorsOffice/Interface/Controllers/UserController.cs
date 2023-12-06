using Microsoft.AspNetCore.Mvc;

namespace Interface.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IActionResult Post()
        {
            // Check the username and password in the database (replace this with your authentication logic)
            bool isValid = CheckCredentials(Username, Password);

            if (isValid)
            {
                // If credentials are valid, add username to session and redirect to home index
                HttpContext.Session.SetString("username", Username);
                return RedirectToPage("/Index"); // Redirect to the home index
            }

            // If credentials are not valid, redirect back to the login page
            return RedirectToPage("/Login");
        }

        private bool CheckCredentials(string username, string password)
        {
            // Check credentials in your database here
            // Replace this with your actual database check logic
            // Return true if credentials are valid, false otherwise
            // Example:
            if (username == "exampleUser" && password == "examplePassword")
            {
                return true;
            }
            return false;
        }
    }








}

