using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Net.Mail;
using WebApplication6.Models;

namespace WebApplication6.Controllers{
    public class SendMailerController : Controller {
        // GET: SendMailer
        public ActionResult Index() {
            return View();
        }
        /// <summary>
        /// Send Mail with Gmail
        /// </summary>
        /// <param name="objModelMail">MailModel Object, keeps all properties</param>
        /// <param name="fileUploader">Selected file data, example-filename,content,content type(file type- .txt,.png etc.),length etc.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(MailModel objModelMail, HttpPostedFileBase fileUploader) {
             if (ModelState.IsValid) {
             
                using (MailMessage mail = new MailMessage("YourEMailID(From)", "YourEMailID(To)")){
                    mail.Subject = "Exam Attachments" + User.Identity.Name;
                    mail.Body = "Below is the Attached File";
                    if (fileUploader != null) {
                        string fileName = Path.GetFileName(fileUploader.FileName);
                        mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                    }
                    mail.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential("YourEMailID(From)", "Your Email Password");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;
                    smtp.Send(mail);
                    ViewBag.Message = "Sent";
                    return View("proposal_defense", objModelMail);
                }
            }
            else
            {
               return View();
            }
        }

    }
}