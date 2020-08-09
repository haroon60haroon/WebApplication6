using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Web.Http.Results;
using System.Web.Razor;

using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net.Http;
using System.Web.Http.Description;
using WebApplication6.Models;



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
            var name = User.Identity.Name;
            MailMessage mn = new MailMessage("Your Email Id (From)", model.To);
            mn.Subject = "Project Approval";
            mn.Body = "A new Project has been Made by "+name;

            mn.IsBodyHtml = false;
           
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential("Your Email Id (From)", "Your Password");
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = nc;
            smtpClient.Send(mn);

            return null;
        }
    }
}