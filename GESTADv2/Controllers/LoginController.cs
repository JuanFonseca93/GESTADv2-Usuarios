using Core.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace GESTADv2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        UnitOfWork unitOfWork;
        BDContext _context;
        public ActionResult Index(string Mensaje)
        {
            ViewBag.msj = Mensaje;
            return View();
        }

        [HttpPost]
        public ActionResult login(string correo, string pass)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);

            var usu = from us in _context.Usuarios where us.correoUsuario == correo where us.passwordUsuario==pass where us.Estatus==2 select us;
            var idUser = usu.SingleOrDefault();

            if (idUser != null)
            {
                Response.Cookies["id"].Value = idUser.idUsuario.ToString();
                return RedirectToAction("Index", "Home", new { num = 1 });
            }
            else
            {
                return RedirectToAction("Index","Login", new { Mensaje = "Usuario o contraseña incorrectos" });
            }

            
        }

        public ActionResult CrearUsuario(Usuario obj)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Area> Areas = new List<Area>();
            foreach (var nusuario in unitOfWork.Area.GetAll())
            {
                Areas.Add(nusuario);
            }
            ViewBag.Areas = Areas;
            return View(obj);
        }

        [HttpPost]
        public ActionResult ActionCrearUsuario(Usuario obj)
        {
            obj.Estatus = 1;
            var ob = 1;
            foreach (var nusuario in unitOfWork.Usuarios.GetAll())
            {
                if (nusuario.correoUsuario.Equals(obj.correoUsuario) || nusuario.curpUsuario.Equals(obj.curpUsuario))
                {
                    ob++;
                }
            }

            if (ModelState.IsValid && ob == 1)
            {
                _context = new BDContext();
                unitOfWork = new UnitOfWork(_context);
                unitOfWork.Usuarios.Add(obj);

                

                unitOfWork.Complete();

                var fromAddress = new MailAddress("gestadutsoe@gmail.com", "GESTAD");
                var toAddress = new MailAddress("exsala01@gmail.com", "Administrador");
                const string fromPassword = "Gestad00";
                const string subject = "Nuevo Usuario";

                string mensaje = "La persona " + obj.nombreUsuario + " Creo una nueva cuenta, porfavor acepte o rechace esta solicitud";


                string body = mensaje;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

                return RedirectToAction("Index", new { Mensaje = "Usuario creado, Porfavor espere a que un administrador lo acepte" });
            }
            else
            {
                return RedirectToAction("Index", new { Mensaje = "Ya Existe un usuario con ese correo o curp"});
            }
        }

    }
}