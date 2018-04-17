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
    public class Nivel5Controller : Controller
    {
        Nivel5 Uppd;
        UnitOfWork unitOfWork;
        BDContext _context;
        // GET: AreaDocumento
        public ActionResult AreaDoc(List<Nivel2> obj, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Nivel5> area = new List<Nivel5>();
            if (obj == null)
            {
                foreach (var nusuario in unitOfWork.Nivel5.GetAll())
                {
                    area.Add(nusuario);
                }
            }
            else
            {
                area = TempData["area"] as List<Nivel5>;
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


                    var busqueda = from nombreUsu in _context.Nivel5 select nombreUsu;

                    if (nombre != null || !nombre.Equals(""))
                    {

                        busqueda = from nombreUsu in _context.Nivel5 where nombreUsu.nombreN.Contains(nombre) select nombreUsu;
                    }
                    List<Nivel5> usu = new List<Nivel5>();

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
        public ActionResult CrearAreaD(Nivel5 obj)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Nivel4> Areas = new List<Nivel4>();
            foreach (var nusuario in unitOfWork.Nivel4.GetAll())
            {
                Areas.Add(nusuario);
            }
            ViewBag.Areas = Areas;
            return View(obj);
        }

        [HttpPost]
        public ActionResult ActionCrearAreaD(Nivel5 obj)
        {

            if (ModelState.IsValid)
            {
                _context = new BDContext();
                unitOfWork = new UnitOfWork(_context);
                unitOfWork.Nivel5.Add(obj);
                unitOfWork.Complete();
                var n4 = unitOfWork.Nivel4.Get(Int32.Parse(obj.nivel4));
                var n3 = unitOfWork.Nivel3.Get(Int32.Parse(n4.nivel3));
                var n2 = unitOfWork.Nivel2.Get(Int32.Parse(n3.nivel2));
                var n1 = unitOfWork.Nivel1.Get(Int32.Parse(n2.nivel1));
                string path = Path.Combine(Server.MapPath("~/Gestad"), n1.nombreN, n2.nombreN, n3.nombreN, n4.nombreN, obj.nombreN);
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
            List<Nivel4> Areas = new List<Nivel4>();
            foreach (var nusuario in unitOfWork.Nivel4.GetAll())
            {
                Areas.Add(nusuario);
            }
            ViewBag.Areas = Areas;
            Uppd = unitOfWork.Nivel5.Get(idArea);
            return View(Uppd);
        }

        [HttpPost]
        public ActionResult ActionActualizarAreaD(Nivel5 obj)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _context = new BDContext();
                    unitOfWork = new UnitOfWork(_context);
                    Uppd = unitOfWork.Nivel5.Get(obj.idN);
                    unitOfWork.Nivel5.Update(Uppd);
                    Uppd.nombreN = obj.nombreN;
                    Uppd.descripcionN = obj.descripcionN;

                    _context.Configuration.ValidateOnSaveEnabled = false;

                    unitOfWork.Complete();

                    var n4 = unitOfWork.Nivel4.Get(Int32.Parse(obj.nivel4));
                    var n3 = unitOfWork.Nivel3.Get(Int32.Parse(n4.nivel3));
                    var n2 = unitOfWork.Nivel2.Get(Int32.Parse(n3.nivel2));
                    var n1 = unitOfWork.Nivel1.Get(Int32.Parse(n2.nivel1));
                    string path = Path.Combine(Server.MapPath("~/Gestad"), n1.nombreN, n2.nombreN, n3.nombreN, n4.nombreN, obj.nombreN);
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