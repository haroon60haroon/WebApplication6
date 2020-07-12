using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Web.Http.Results;
using System.Web.Razor;

namespace WebApplication6.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(WebApplication6.Models.ResetPassword model)
        {
            MailMessage mn = new MailMessage("haroonqureshi60@gmail.com", model.To);
            mn.Subject = "Project Approval";
            mn.Body = "A new Project has been Made.";
            mn.IsBodyHtml = false;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential("haroonqureshi60@gmail.com", "5348Inam128$");
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = nc;
            smtpClient.Send(mn);

            return null;
        }
    }
}