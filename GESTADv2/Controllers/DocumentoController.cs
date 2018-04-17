using Core.Dominio;
using GESTADv2.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace GESTADv2.Controllers
{
    public class DocumentoController : Controller
    {
        Documento Uppd;
        UnitOfWork unitOfWork;
        BDContext _context;
        // GET: Documento
        public ActionResult Documento(List<Documento> obj, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Documento> area = new List<Documento>();
            if (obj == null)
            {
                foreach (var nusuario in _context.Documento.Where(d => d.estadoDocumento == 1))
                {
                    area.Add(nusuario);
                }
            }
            else
            {
                area = TempData["area"] as List<Documento>;
            }
            int tamanoPagina = 15;
            int numeroPagina = (pagina ?? 1);

            return View(area.ToPagedList(numeroPagina, tamanoPagina));
        }

        public ActionResult DocumentoA(List<Documento> obj, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Documento> area = new List<Documento>();
            if (obj == null)
            {
                foreach (var nusuario in _context.Documento.Where(d => d.estadoDocumento == 2))
                {
                    area.Add(nusuario);
                }
            }
            else
            {
                area = TempData["area"] as List<Documento>;
            }
            int tamanoPagina = 15;
            int numeroPagina = (pagina ?? 1);

            return View(area.ToPagedList(numeroPagina, tamanoPagina));
        }

        public ActionResult DocumentoR(List<Documento> obj, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Documento> area = new List<Documento>();
            if (obj == null)
            {
                foreach (var nusuario in _context.Documento.Where(d => d.estadoDocumento == 3))
                {
                    area.Add(nusuario);
                }
            }
            else
            {
                area = TempData["area"] as List<Documento>;
            }
            int tamanoPagina = 15;
            int numeroPagina = (pagina ?? 1);

            return View(area.ToPagedList(numeroPagina, tamanoPagina));
        }

        public ActionResult CrearArea(DocumentoArchivo obj)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<TipoDocumento> area = new List<TipoDocumento>();
            foreach (var nusuario in _context.TipoDocumento)
            {
                area.Add(nusuario);
            }

            ViewBag.Areas = area;
            return View(obj);
        }

        public ActionResult AceptarDocumento(Documento obj)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            Uppd = unitOfWork.Docs.Get(obj.idDocumento);
            unitOfWork.Docs.Update(Uppd);

            

            Uppd.estadoDocumento = 2;

            _context.Configuration.ValidateOnSaveEnabled = false;

            unitOfWork.Complete();

            Usuario Uppd2 = unitOfWork.Usuarios.Get(obj.idUsuario);
            var fromAddress = new MailAddress("gestadutsoe@gmail.com", "GESTAD");
            var toAddress = new MailAddress(Uppd2.correoUsuario, Uppd2.nombreUsuario);
            const string fromPassword = "Gestad00";
            const string subject = "Estatus del Documento";

            string mensaje = "Felicidades, Apartir de ahora Puede agregar Colaboraciones al Documento " + obj.nombreDocumento + ", Cualquier duda favor de responder a este correo.";


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

            return RedirectToAction("DocumentoA");


        }

        public ActionResult RechazarDocumento(Documento obj)
        {


            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            Uppd = unitOfWork.Docs.Get(obj.idUsuario);
            unitOfWork.Docs.Update(Uppd);

            Uppd.estadoDocumento = 3;

            _context.Configuration.ValidateOnSaveEnabled = false;

            unitOfWork.Complete();

            Usuario Uppd2 = unitOfWork.Usuarios.Get(obj.idUsuario);
            var fromAddress = new MailAddress("gestadutsoe@gmail.com", "GESTAD");
            var toAddress = new MailAddress(Uppd2.correoUsuario, Uppd2.nombreUsuario);
            const string fromPassword = "Gestad00";
            const string subject = "Estatus del Documento";

            string mensaje = "Lamentablemente el documento " + obj.nombreDocumento + " fue rechazado, Cualquier duda favor de responder a este correo.";


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

            return RedirectToAction("DocumentoR");

        }

        public ActionResult ActionCrearArea(DocumentoArchivo obj)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            obj.estadoDocumento = 1;
            obj.fechaSubidaDocumento = DateTime.Today.ToString("d");
            obj.fechaModificiacionDocumento = DateTime.Today.ToString("d");

            var tp = from ntp in _context.TipoDocumento.Include("N1").Include("N2").Include("N3").Include("N4").Include("N5") where ntp.idTipo == obj.idTipo select ntp;

            List<Nivel1> n1 = tp.FirstOrDefault().N1.ToList();
            List<Nivel2> n2 = tp.FirstOrDefault().N2.ToList();
            List<Nivel3> n3 = tp.FirstOrDefault().N3.ToList();
            List<Nivel4> n4 = tp.FirstOrDefault().N4.ToList();
            List<Nivel5> n5 = tp.FirstOrDefault().N5.ToList();

            if(n1.Count > 0)
            {
                foreach (var lv1 in n1)
                {
                    string path = Path.Combine("C:/Gestad", lv1.nombreN);
                    obj.rutaDocumento = path;
                }
            }

            if (n2.Count > 0)
            {
                foreach (var lv1 in n2)
                {
                    
                    var nl1 = unitOfWork.Nivel1.Get(Int32.Parse(lv1.nivel1));
                    string path = Path.Combine("C:/Gestad", nl1.nombreN, lv1.nombreN);
                    obj.rutaDocumento = path;
                }
            }

            if (n1.Count > 0)
            {
                foreach (var lv1 in n3)
                {
                    
                    var nl2 = unitOfWork.Nivel2.Get(Int32.Parse(lv1.nivel2));
                    var nl1 = unitOfWork.Nivel1.Get(Int32.Parse(nl2.nivel1));
                    string path = Path.Combine("C:/Gestad", nl1.nombreN, nl2.nombreN, lv1.nombreN);
                    obj.rutaDocumento = path;
                }
            }

            if (n1.Count > 0)
            {
                foreach (var lv1 in n4)
                {
                    
                    var nl3 = unitOfWork.Nivel3.Get(Int32.Parse(lv1.nivel3));
                    var nl2 = unitOfWork.Nivel2.Get(Int32.Parse(nl3.nivel2));
                    var nl1 = unitOfWork.Nivel1.Get(Int32.Parse(nl2.nivel1));
                    string path = Path.Combine("C:/Gestad", nl1.nombreN, nl2.nombreN, nl3.nombreN, lv1.nombreN);
                    obj.rutaDocumento = path;
                }
            }

            if (n1.Count > 0)
            {
                foreach (var lv1 in n5)
                {
                    var nl4 = unitOfWork.Nivel4.Get(Int32.Parse(lv1.nivel4));
                    var nl3 = unitOfWork.Nivel3.Get(Int32.Parse(nl4.nivel3));
                    var nl2 = unitOfWork.Nivel2.Get(Int32.Parse(nl3.nivel2));
                    var nl1 = unitOfWork.Nivel1.Get(Int32.Parse(nl2.nivel1));
                    string path = Path.Combine("C:/Gestad", nl1.nombreN, nl2.nombreN, nl3.nombreN, nl4.nombreN, lv1.nombreN);
                    obj.rutaDocumento = path;
                }
            }

            if (n1.Count > 0)
            {
                foreach (var lv1 in n1)
                {
                    string path = Path.Combine(Server.MapPath("~/Gestad"), lv1.nombreN);
                    obj.rutaDocumento = path;
                }
            }

            var id = HttpContext.Request.Cookies["id"].Value;
            

            if (ModelState.IsValid)
            {
                _context = new BDContext();
                unitOfWork = new UnitOfWork(_context);

                Documento dc = new Documento();
                dc.descripccionDocumento = obj.descripccionDocumento;
                dc.nombreDocumento = obj.nombreDocumento;
                dc.estadoDocumento = obj.estadoDocumento;
                dc.fechaModificiacionDocumento = obj.fechaModificiacionDocumento;
                dc.fechaSubidaDocumento = obj.fechaSubidaDocumento;
                dc.rutaDocumento = obj.rutaDocumento;
                dc.vigencia = obj.vigencia;
                dc.idTipo = obj.idTipo;
                dc.idUsuario = Int32.Parse(id);

                unitOfWork.Docs.Add(dc);
                

                var ttp = from ntp in _context.TipoDocumento.Include("N1").Include("N2").Include("N3").Include("N4").Include("N5") where ntp.idTipo == obj.idTipo select ntp;

                List<Nivel1> nn1 = ttp.FirstOrDefault().N1.ToList();
                List<Nivel2> nn2 = ttp.FirstOrDefault().N2.ToList();
                List<Nivel3> nn3 = ttp.FirstOrDefault().N3.ToList();
                List<Nivel4> nn4 = ttp.FirstOrDefault().N4.ToList();
                List<Nivel5> nn5 = ttp.FirstOrDefault().N5.ToList();

                Usuario UP = unitOfWork.Usuarios.Get(Int32.Parse(id));
                HttpPostedFileBase file = obj.DocFile;
                var name = file.FileName;
                var name2 = name.Split('\\');
                var name3 = name2[name2.Length - 1];

                var uname = UP.nombreUsuario;
                var uname2 = uname.Split(' ');
                var uname3 = "";

                if (uname2.Length > 2)
                {

                   uname3 = uname2[0].Substring(0, 1) + uname2[2];
                }
                else
                {

                    uname3 = uname2[0].Substring(0, 1) + uname2[1];
                }

                var filename = uname3 + name3.ToUpper(); ;

                if (nn1.Count > 0)
                {
                    foreach (var lv1 in nn1)
                    {
                        string path = Path.Combine("C:/Gestad", lv1.nombreN, filename);
                        dc.rutaDocumento = path;
                        file.SaveAs(path);
                    }
                }

                if (nn2.Count > 0)
                {
                    foreach (var lv1 in nn2)
                    {

                        var nl1 = unitOfWork.Nivel1.Get(Int32.Parse(lv1.nivel1));
                        string path = Path.Combine("C:/Gestad", nl1.nombreN, lv1.nombreN, filename);
                        dc.rutaDocumento = path;
                        file.SaveAs(path);
                    }
                }

                if (nn3.Count > 0)
                {
                    foreach (var lv1 in nn3)
                    {

                        var nl2 = unitOfWork.Nivel2.Get(Int32.Parse(lv1.nivel2));
                        var nl1 = unitOfWork.Nivel1.Get(Int32.Parse(nl2.nivel1));
                        string path = Path.Combine("C:/Gestad", nl1.nombreN, nl2.nombreN, lv1.nombreN, filename);
                        dc.rutaDocumento = path;
                        file.SaveAs(path);
                    }
                }

                if (nn4.Count > 0)
                {
                    foreach (var lv1 in nn4)
                    {

                        var nl3 = unitOfWork.Nivel3.Get(Int32.Parse(lv1.nivel3));
                        var nl2 = unitOfWork.Nivel2.Get(Int32.Parse(nl3.nivel2));
                        var nl1 = unitOfWork.Nivel1.Get(Int32.Parse(nl2.nivel1));
                        string path = Path.Combine("C:/Gestad", nl1.nombreN, nl2.nombreN, nl3.nombreN, lv1.nombreN, filename);
                        dc.rutaDocumento = path;
                        file.SaveAs(path);
                    }
                }

                if (nn5.Count > 0)
                {
                    foreach (var lv1 in nn5)
                    {
                        var nl4 = unitOfWork.Nivel4.Get(Int32.Parse(lv1.nivel4));
                        var nl3 = unitOfWork.Nivel3.Get(Int32.Parse(nl4.nivel3));
                        var nl2 = unitOfWork.Nivel2.Get(Int32.Parse(nl3.nivel2));
                        var nl1 = unitOfWork.Nivel1.Get(Int32.Parse(nl2.nivel1));
                        string path = Path.Combine("C:/Gestad", nl1.nombreN, nl2.nombreN, nl3.nombreN, nl4.nombreN, lv1.nombreN, filename);
                        dc.rutaDocumento = path;
                        file.SaveAs(path);
                    }
                }

                if (nn1.Count > 0)
                {
                    foreach (var lv1 in nn1)
                    {
                        string path = Path.Combine("C:/Gestad", lv1.nombreN, filename);
                        dc.rutaDocumento = path;
                        file.SaveAs(path);
                    }
                }



                //unitOfWork.Complete();

                Usuario Uppd2 = unitOfWork.Usuarios.Get(Int32.Parse(id));
                var fromAddress = new MailAddress("gestadutsoe@gmail.com", "GESTAD");
                var toAddress = new MailAddress("marroyoal@utsoe.edu.mx", "Administrador");
                const string fromPassword = "Gestad00";
                const string subject = "Nuevo Usuario";

                string mensaje = "La persona " + Uppd2.nombreUsuario + " Subio un nuevo documento \""+ obj.nombreDocumento+"\", porfavor acepte o rechace esta solicitud";


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

                return RedirectToAction("Documento");
            }
            else
            {
                return RedirectToAction("CrearArea", obj);
            }
        }

        public ActionResult DocUser(List<Documento> obj, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Documento> area = new List<Documento>();
            if (obj == null)
            {
                var id = HttpContext.Request.Cookies["id"].Value;
                int id2 = Int32.Parse(id);
                foreach (var nusuario in _context.Documento.Where(d => d.estadoDocumento == 2).Where(d => d.idUsuario == id2))
                {
                    area.Add(nusuario);
                }
            }
            else
            {
                area = TempData["area"] as List<Documento>;
            }
            int tamanoPagina = 15;
            int numeroPagina = (pagina ?? 1);

            return View(area.ToPagedList(numeroPagina, tamanoPagina));
        }

        public ActionResult Colaboracion(Documento obj)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            ViewBag.idD = obj.idDocumento;
            List<Usuario> us = new List<Usuario>();
            foreach (var u in _context.Usuarios.Where(d => d.Estatus == 2))
            {
                us.Add(u);
            }
            ViewBag.us = us as List<Usuario>;
            List<TipoColaboracion> tp = new List<TipoColaboracion>();
            foreach (var u in unitOfWork.TipoColaboracion.GetAll())
            {
                tp.Add(u);
            }
            ViewBag.tp = tp as List<TipoColaboracion>;
            return View();
        }

        [HttpPost]
        public ActionResult ActionColaboracion(Colaboracion obj)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);

            var id = HttpContext.Request.Cookies["id"].Value;
            int id2 = Int32.Parse(id);

            obj.propietarioColaboracion = id2;
            obj.tipoColaboracionInterno = false;

            unitOfWork.Colaboracion.Add(obj);

            unitOfWork.Complete();

            return RedirectToAction("DocUser");
        }

        public ActionResult DocumentoCol(List<Documento> obj, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Colaboracion> ids = new List<Core.Dominio.Colaboracion>();

            var id = HttpContext.Request.Cookies["id"].Value;
            int id2 = Int32.Parse(id);
            List<Documento> area = new List<Documento>();

            foreach (var col in _context.Colaboracion.Where(c=> c.idUsuario == id2))
            {
                if (obj == null)
                {
                    _context = new BDContext();
                    unitOfWork = new UnitOfWork(_context);
                    foreach (var nusuario in _context.Documento.Where(d => d.estadoDocumento == 2).Where(e => e.idDocumento == col.idDocumento))
                    {
                        area.Add(nusuario);
                    }
                    _context = new BDContext();
                    unitOfWork = new UnitOfWork(_context);
                }
                else
                {
                    area = TempData["area"] as List<Documento>;
                }
            }

            
            
           
            int tamanoPagina = 15;
            int numeroPagina = (pagina ?? 1);

            return View(area.ToPagedList(numeroPagina, tamanoPagina));
        }

        public ActionResult EliColaboracion(int idDocumento)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);

            var id = HttpContext.Request.Cookies["id"].Value;
            int id2 = Int32.Parse(id);
            var col = from a in _context.Colaboracion.Where(c => c.idDocumento == idDocumento).Where(c => c.idUsuario == id2) select a;
        
            unitOfWork.Colaboracion.Remove(col.SingleOrDefault());

            unitOfWork.Complete();

            return RedirectToAction("DocumentoCol");
        }
    }
}