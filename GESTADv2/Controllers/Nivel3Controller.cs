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
    public class Nivel3Controller : Controller
    {
        Nivel3 Uppd;
        UnitOfWork unitOfWork;
        BDContext _context;
        // GET: AreaDocumento
        public ActionResult AreaDoc(List<Nivel3> obj, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Nivel3> area = new List<Nivel3>();
            if (obj == null)
            {
                foreach (var nusuario in unitOfWork.Nivel3.GetAll())
                {
                    area.Add(nusuario);
                }
            }
            else
            {
                area = TempData["area"] as List<Nivel3>;
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


                    var busqueda = from nombreUsu in _context.Nivel3 select nombreUsu;

                    if (nombre != null || !nombre.Equals(""))
                    {

                        busqueda = from nombreUsu in _context.Nivel3 where nombreUsu.nombreN.Contains(nombre) select nombreUsu;
                    }
                    List<Nivel3> usu = new List<Nivel3>();

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
        public ActionResult CrearAreaD(Nivel3 obj)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Nivel2> Areas = new List<Nivel2>();
            foreach (var nusuario in unitOfWork.Nivel2.GetAll())
            {
                Areas.Add(nusuario);
            }
            ViewBag.Areas = Areas;
            return View(obj);
        }

        [HttpPost]
        public ActionResult ActionCrearAreaD(Nivel3 obj)
        {

            if (ModelState.IsValid)
            {
                _context = new BDContext();
                unitOfWork = new UnitOfWork(_context);
                unitOfWork.Nivel3.Add(obj);
                unitOfWork.Complete();
                var n2 = unitOfWork.Nivel2.Get(Int32.Parse(obj.nivel2));
                var n1 = unitOfWork.Nivel1.Get(Int32.Parse(n2.nivel1));
                string path = Path.Combine(Server.MapPath("~/Gestad"), n1.nombreN, n2.nombreN, obj.nombreN);
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
            List<Nivel2> Areas = new List<Nivel2>();
            foreach (var nusuario in unitOfWork.Nivel2.GetAll())
            {
                Areas.Add(nusuario);
            }
            ViewBag.Areas = Areas;
            Uppd = unitOfWork.Nivel3.Get(idArea);
            return View(Uppd);
        }

        [HttpPost]
        public ActionResult ActionActualizarAreaD(Nivel3 obj)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _context = new BDContext();
                    unitOfWork = new UnitOfWork(_context);
                    Uppd = unitOfWork.Nivel3.Get(obj.idN);
                    unitOfWork.Nivel3.Update(Uppd);
                    Uppd.nombreN = obj.nombreN;
                    Uppd.descripcionN = obj.descripcionN;

                    _context.Configuration.ValidateOnSaveEnabled = false;

                    unitOfWork.Complete();

                    var n2 = unitOfWork.Nivel2.Get(Int32.Parse(obj.nivel2));
                    var n1 = unitOfWork.Nivel1.Get(Int32.Parse(n2.nivel1));
                    string path = Path.Combine(Server.MapPath("~/Gestad"), n1.nombreN, n2.nombreN, obj.nombreN);
                    Directory.CreateDirectory(path);

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