using Core.Dominio;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GESTADv2.Controllers
{
    public class Nivel1Controller : Controller
    {
        Nivel1 Uppd;
        UnitOfWork unitOfWork;
        BDContext _context;
        // GET: AreaDocumento
        public ActionResult AreaDoc(List<Nivel1> obj, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Nivel1> area = new List<Nivel1>();
            if (obj == null)
            {
                foreach (var nusuario in unitOfWork.Nivel1.GetAll())
                {
                    area.Add(nusuario);
                }
            }
            else
            {
                area = TempData["area"] as List<Nivel1>;
            }
            int tamanoPagina = 5;
            int numeroPagina = (pagina ?? 1);

            return View(area.ToPagedList(numeroPagina, tamanoPagina));
        }

        [HttpPost]
        public ActionResult BuscarAreaD(string nombre)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _context = new BDContext();


                    var busqueda = from nombreUsu in _context.Nivel1 select nombreUsu;

                    if (nombre != null || !nombre.Equals(""))
                    {

                        busqueda = from nombreUsu in _context.Nivel1 where nombreUsu.nombreN.Contains(nombre) select nombreUsu;
                    }
                    List<Nivel1> usu = new List<Nivel1>();

                    foreach (var item in busqueda)
                    {
                        usu.Add(item);
                    }

                    TempData["area"] = usu;

                    return RedirectToAction("AreaDoc", new { obj = usu });

                }
                else
                {
                    return RedirectToAction("AreaDoc");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Un error ha ocurrido '{0}'", ex);
                return View("AreaDoc");
            }
        }

        [HttpGet]
        public ActionResult CrearAreaD(Nivel1 obj)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);

            return View(obj);
        }

        [HttpPost]
        public ActionResult ActionCrearAreaD(Nivel1 obj)
        {

            if (ModelState.IsValid)
            {
                _context = new BDContext();
                unitOfWork = new UnitOfWork(_context);
                unitOfWork.Nivel1.Add(obj);
                unitOfWork.Complete();
                string path = Path.Combine(Server.MapPath("~/Gestad"),obj.nombreN);
                Directory.CreateDirectory(path);
                return RedirectToAction("AreaDoc");
            }
            else
            {
                return RedirectToAction("CrearAreaD", obj);
            }
        }

        [HttpGet]
        public ActionResult ActualizarAreaD(int idArea)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);

            Uppd = unitOfWork.Nivel1.Get(idArea);
            return View(Uppd);
        }

        [HttpPost]
        public ActionResult ActionActualizarAreaD(Nivel1 obj)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _context = new BDContext();
                    unitOfWork = new UnitOfWork(_context);
                    Uppd = unitOfWork.Nivel1.Get(obj.idN);
                    unitOfWork.Nivel1.Update(Uppd);
                    Uppd.nombreN = obj.nombreN;
                    Uppd.descripcionN = obj.descripcionN;

                    _context.Configuration.ValidateOnSaveEnabled = false;

                    unitOfWork.Complete();

                    return RedirectToAction("AreaDoc");
                }
                else
                {
                    return View("ActualizarAreaD", obj);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Un error ha ocurrido '{0}'", ex);
                return View("ActualizarAreaD");
            }

        }
    }
}