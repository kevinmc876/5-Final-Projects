using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Interface.Views.Home
{
    public class ContactModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var smtpClient = new SmtpClient("your_smtp_server", 7276)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("heavycoder13@gmail.com", "Friday3#"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("your_email_address"),
                Subject = "Contact Form Submission",
                Body = $"Name: {Name}\nEmail: {Email}\n\nMessage:\n{Message}"
            };

            mailMessage.To.Add("company@example.com"); // Replace with your company's email address

            await smtpClient.SendMailAsync(mailMessage);

            return RedirectToPage("/ThankYou"); // Redirect to a thank you page
        }
    }
}
