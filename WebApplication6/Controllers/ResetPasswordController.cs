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
    public class ResetPasswordController : Controller
    {
        // GET: ResetPassword
        public ActionResult Index()
        {
            return View();

        }
        //
        [HttpPost]
        public ActionResult Index(WebApplication6.Models.ResetPassword model)
        {
            MailMessage mn = new MailMessage("haroonqureshi60@gmail.com", model.To);
            mn.Subject = "Your New Password ";
            mn.Body = "Your New Password is:  '" +generatorpassword() + "'  and Kindly Login from email and password provided" ;
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
        public string generatorpassword() {
            string password;
            password = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
            password += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            password += "1,2,3,4,5,6,7,8,9,0,!,@,#,$,%,&,?";
            char[] sep = { ',' };
            string[] arr = password.Split(sep);
            string passwordString = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i <6; i++){
                temp = arr[rand.Next(0, arr.Length)];
                passwordString += temp;
            }
            password = passwordString;

            return password;
        }
    }
}