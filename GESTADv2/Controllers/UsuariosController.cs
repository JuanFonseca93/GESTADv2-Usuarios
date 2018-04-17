using System.Net.Mail;
using System.Net;
using Core.Dominio;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GESTADv2.Controllers
{
    public class UsuariosController : Controller
    {
        Usuario Uppd;
        UnitOfWork unitOfWork;
        BDContext _context;
        // GET: Usuarios
        public ActionResult Usuarios(List<Usuario> obj, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Usuario> usuario = new List<Usuario>();
            var USU = from us in _context.Usuarios where us.Estatus == 1 select us;
            if (obj == null)
            {
                foreach (var nusuario in USU)
                {
                    usuario.Add(nusuario);
                }
            }
            else
            {
                usuario = TempData["usuario"] as List<Usuario>;
            }
            int tamanoPagina = 15;
            int numeroPagina = (pagina ?? 1);
            
            return View(usuario.ToPagedList(numeroPagina, tamanoPagina));

        }

        public ActionResult UsuariosA(List<Usuario> obj, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Usuario> usuario = new List<Usuario>();
            var USU = from us in _context.Usuarios where us.Estatus ==2 select us;
            if (obj == null)
            {
                foreach (var nusuario in USU)
                {
                    usuario.Add(nusuario);
                }
            }
            else
            {
                usuario = TempData["usuario"] as List<Usuario>;
            }
            int tamanoPagina = 15;
            int numeroPagina = (pagina ?? 1);

            return View(usuario.ToPagedList(numeroPagina, tamanoPagina));

        }

        public ActionResult UsuariosR(List<Usuario> obj, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Usuario> usuario = new List<Usuario>();
            var USU = from us in _context.Usuarios where us.Estatus == 3 select us;
            if (obj == null)
            {
                foreach (var nusuario in USU)
                {
                    usuario.Add(nusuario);
                }
            }
            else
            {
                usuario = TempData["usuario"] as List<Usuario>;
            }
            int tamanoPagina = 15;
            int numeroPagina = (pagina ?? 1);

            return View(usuario.ToPagedList(numeroPagina, tamanoPagina));

        }

        [HttpPost]
        public ActionResult BuscarUsuario(string nombre)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context = new BDContext();


                    var busqueda = from nombreUsu in _context.Usuarios select nombreUsu;

                    if (nombre != null || !nombre.Equals(""))
                    {

                        busqueda = from nombreUsu in _context.Usuarios where nombreUsu.nombreUsuario.Contains(nombre) select nombreUsu;
                    }
                    List<Usuario> usu = new List<Usuario>();

                    foreach (var item in busqueda)
                    {
                        usu.Add(item);
                    }

                    TempData["usuario"] = usu;

                    return RedirectToAction("Usuario", new { obj = usu });

                }
                else
                {
                    return RedirectToAction("Usuario");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Un error ha ocurrido '{0}'", ex);
                return View("Usuario");
            }
        }

        [HttpGet]
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

            if (ModelState.IsValid)
            {
                _context = new BDContext();
                unitOfWork = new UnitOfWork(_context);
                unitOfWork.Usuarios.Add(obj);
                unitOfWork.Complete();
                
                return RedirectToAction("Usuarios");
            }
            else
            {
                return RedirectToAction("CrearUsuario",obj);
            }
        }

        [HttpGet]
        public ActionResult ActualizarUsuario(int idUsuario)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Area> Areas = new List<Area>();
            foreach (var nusuario in unitOfWork.Area.GetAll())
            {
                Areas.Add(nusuario);
            }
            ViewBag.Areas = Areas;
            Uppd = unitOfWork.Usuarios.Get(idUsuario);
            return View(Uppd);
        }

        [HttpPost]
        public ActionResult ActionActualizarUsuario(Usuario obj)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _context = new BDContext();
                    unitOfWork = new UnitOfWork(_context);
                    Uppd = unitOfWork.Usuarios.Get(obj.idUsuario);
                    unitOfWork.Usuarios.Update(Uppd);
                    Uppd.nombreUsuario = obj.nombreUsuario;
                    Uppd.curpUsuario = obj.curpUsuario;
                    Uppd.correoUsuario = obj.correoUsuario;
                    Uppd.passwordUsuario = obj.passwordUsuario;
                    Uppd.fechaNacimientoUsuario = obj.fechaNacimientoUsuario;
                    Uppd.generoUsuario = obj.generoUsuario;
                    Uppd.institucionUsuario = obj.institucionUsuario;
                    Uppd.telefonoUsuario = obj.telefonoUsuario;
                    Uppd.idArea = obj.idArea;

                    _context.Configuration.ValidateOnSaveEnabled = false;

                    unitOfWork.Complete();

                    return RedirectToAction("Usuarios");
                }
                else
                {
                    return View("ActualizarUsuario", obj);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Un error ha ocurrido '{0}'", ex);
                return View("ActualizarUsuario");
            }

        }
        
        public ActionResult AceptarUsuario(Usuario obj)
        {
                    _context = new BDContext();
                    unitOfWork = new UnitOfWork(_context);
                    Uppd = unitOfWork.Usuarios.Get(obj.idUsuario);
                    unitOfWork.Usuarios.Update(Uppd);
                    
                    Uppd.Estatus = 2;

                    _context.Configuration.ValidateOnSaveEnabled = false;

                    unitOfWork.Complete();

                    var fromAddress = new MailAddress("gestadutsoe@gmail.com", "GESTAD");
                    var toAddress = new MailAddress(Uppd.correoUsuario, Uppd.nombreUsuario);
                    const string fromPassword = "Gestad00";
                    const string subject = "Estatus de Usuario";

                    string mensaje = "Felicidades, Apartir de ahora usted puede iniciar secion en el sistema GESTAD con el correo " + obj.correoUsuario + " y la contraseña seleccionada, Cualquier duda favor de responder a este correo.";


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

            return RedirectToAction("UsuariosA");
                

        }
        
        public ActionResult RechazarUsuario(Usuario obj)
        {

           
                    _context = new BDContext();
                    unitOfWork = new UnitOfWork(_context);
                    Uppd = unitOfWork.Usuarios.Get(obj.idUsuario);
                    unitOfWork.Usuarios.Update(Uppd);

                    Uppd.Estatus = 3;

                    _context.Configuration.ValidateOnSaveEnabled = false;

                    unitOfWork.Complete();

                    var fromAddress = new MailAddress("gestadutsoe@gmail.com", "From Name");
                    var toAddress = new MailAddress(Uppd.correoUsuario, "To Name");
                    const string fromPassword = "Gestad00";
                    const string subject = "Estatus de Usuario";

                    string mensaje = "Lamentablemente fue rechazada su solicitud de usuario en el sistema Gestad, Cualquier duda favor de responder a este correo.";


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

            return RedirectToAction("UsuariosR");

        }
    }
}