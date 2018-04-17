using Core.Dominio;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GESTADv2.Controllers
{
    public class AreaController : Controller
    {
        Area Uppd;
        UnitOfWork unitOfWork;
        BDContext _context;
        // GET: Area
        public ActionResult Area(List<Area> obj, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Area> area = new List<Area>();
            if (obj == null)
            {
                foreach (var nusuario in unitOfWork.Area.GetAll())
                {
                    area.Add(nusuario);
                }
            }
            else
            {
                area = TempData["area"] as List<Area>;
            }
            int tamanoPagina = 5;
            int numeroPagina = (pagina ?? 1);

            return View(area.ToPagedList(numeroPagina, tamanoPagina));
        }

        [HttpPost]
        public ActionResult BuscarArea(string nombre)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    _context = new BDContext();


                    var busqueda = from nombreUsu in _context.Area select nombreUsu;

                    if (nombre != null || !nombre.Equals(""))
                    {

                        busqueda = from nombreUsu in _context.Area where nombreUsu.nombreArea.Contains(nombre) select nombreUsu;
                    }
                    List<Area> usu = new List<Area>();

                    foreach (var item in busqueda)
                    {
                        usu.Add(item);
                    }

                    TempData["area"] = usu;

                    return RedirectToAction("Area", new { obj = usu });

                }
                else
                {
                    return RedirectToAction("Area");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Un error ha ocurrido '{0}'", ex);
                return View("Area");
            }
        }

        [HttpGet]
        public ActionResult CrearArea(Area obj)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            
            return View(obj);
        }

        [HttpPost]
        public ActionResult ActionCrearArea(Area obj)
        {

            if (ModelState.IsValid)
            {
                _context = new BDContext();
                unitOfWork = new UnitOfWork(_context);
                unitOfWork.Area.Add(obj);
                unitOfWork.Complete();
                return RedirectToAction("Area");
            }
            else
            {
                return RedirectToAction("CrearArea", obj);
            }
        }

        [HttpGet]
        public ActionResult ActualizarArea(int idArea)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            
            Uppd = unitOfWork.Area.Get(idArea);
            return View(Uppd);
        }

        [HttpPost]
        public ActionResult ActionActualizarArea(Area obj)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _context = new BDContext();
                    unitOfWork = new UnitOfWork(_context);
                    Uppd = unitOfWork.Area.Get(obj.idArea);
                    unitOfWork.Area.Update(Uppd);
                    Uppd.nombreArea = obj.nombreArea;
                    Uppd.descripcionArea = obj.descripcionArea;

                    _context.Configuration.ValidateOnSaveEnabled = false;

                    unitOfWork.Complete();

                    return RedirectToAction("Area");
                }
                else
                {
                    return View("ActualizarArea", obj);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Un error ha ocurrido '{0}'", ex);
                return View("ActualizarArea");
            }

        }
    }


}