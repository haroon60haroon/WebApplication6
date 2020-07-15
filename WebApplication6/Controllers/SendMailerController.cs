using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Net.Mail;
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
        public ActionResult Index(WebApplication6.Models.MailModel objModelMail, HttpPostedFileBase fileUploader) {
             if (ModelState.IsValid) {
               string from = "haroonqureshi60@gmail.com";
                using (MailMessage mail = new MailMessage(from, objModelMail.To)){
                    mail.Subject = objModelMail.Subject;
                    mail.Body = objModelMail.Body;
                    if (fileUploader != null) {
                        string fileName = Path.GetFileName(fileUploader.FileName);
                        mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                    }
                    mail.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(from, "5348Inam128$");
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