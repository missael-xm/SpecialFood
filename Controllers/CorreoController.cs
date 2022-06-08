using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpecialFood.Models;
using System.Net.Mail;

namespace SpecialFood.Controllers
{
    public class CorreoController : Controller
    {
         private readonly ILogger<HomeController> _logger;

        public CorreoController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Contacto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contacto(String Correo, String Subject, String Body, String CorreoUsu, String Nombre){
           
           try{
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress("comercioelectronicousmp@gmail.com"); 
                correo.To.Add(Correo);
                correo.Subject = Subject;
                correo.Body = Body;
                
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                string sCuentaCorreo = "comercioelectronicousmp@gmail.com";
                string sPasswordCorreo = "Comercio12!";
                smtp.Credentials = new System.Net.NetworkCredential(sCuentaCorreo, sPasswordCorreo);

                smtp.Send(correo);
                ViewBag.Mensaje = "Mensaje enviado correctamente";
           }
           catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
            
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
