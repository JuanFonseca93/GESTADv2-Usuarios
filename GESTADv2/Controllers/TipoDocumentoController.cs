using Core.Dominio;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GESTADv2.Controllers
{
    public class TipoDocumentoController : Controller
    {
        TipoDocumento Uppd;
        UnitOfWork unitOfWork;
        BDContext _context;
        // GET: TipoDocumento
        public ActionResult TipoDocumento(List<TipoDocumento> obj, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<TipoDocumento> area = new List<TipoDocumento>();
            if (obj == null)
            {
                foreach (var nusuario in unitOfWork.TipoDocumento.GetAll())
                {
                    area.Add(nusuario);
                }
            }
            else
            {
                area = TempData["TipDoc"] as List<TipoDocumento>;
            }
            int tamanoPagina = 5;
            int numeroPagina = (pagina ?? 1);

            return View(area.ToPagedList(numeroPagina, tamanoPagina));
        }

        [HttpPost]
        public ActionResult BuscarTipos(string nombre)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _context = new BDContext();


                    var busqueda = from nombreUsu in _context.TipoDocumento select nombreUsu;

                    if (nombre != null || !nombre.Equals(""))
                    {

                        busqueda = from nombreUsu in _context.TipoDocumento where nombreUsu.nombreTipo.Contains(nombre) select nombreUsu;
                    }
                    List<TipoDocumento> usu = new List<TipoDocumento>();

                    foreach (var item in busqueda)
                    {
                        usu.Add(item);
                    }

                    TempData["TipoDoc"] = usu;

                    return RedirectToAction("TipoDocumento", new { obj = usu });

                }
                else
                {
                    return RedirectToAction("TipoDocumento");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Un error ha ocurrido '{0}'", ex);
                return View("TipoDocumento");
            }
        }

        public ActionResult CrearTipoDocumento(TipoDocumento obj)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Nivel1> Areas = new List<Nivel1>();
            foreach (var nusuario in unitOfWork.Nivel1.GetAll())
            {
                Areas.Add(nusuario);
            }
            ViewBag.Areas = Areas;
            return View(obj);
        }

        [HttpPost]
        public ActionResult ActionCrearTipo(TipoDocumento obj)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            if (ModelState.IsValid)
            {
                
                unitOfWork.TipoDocumento.Add(obj);
                unitOfWork.Complete();
                return RedirectToAction("TipoDocumento");
            }
            else
            {
                return View("CrearTipoDocumento", obj);
            }
        }

        [HttpGet]
        public ActionResult ActualizarTipo(int idArea)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Nivel1> Areas = new List<Nivel1>();
            foreach (var nusuario in unitOfWork.Nivel1.GetAll())
            {
                Areas.Add(nusuario);
            }
            ViewBag.Areas = Areas;
            Uppd = unitOfWork.TipoDocumento.Get(idArea);
            return View(Uppd);
        }

        [HttpPost]
        public ActionResult ActionActualizarTipo(TipoDocumento obj)
        {

            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);


            if (ModelState.IsValid)
                {
                    _context = new BDContext();
                    unitOfWork = new UnitOfWork(_context);
                    Uppd = unitOfWork.TipoDocumento.Get(obj.idTipo);
                    unitOfWork.TipoDocumento.Update(Uppd);
                    Uppd.nombreTipo = obj.nombreTipo;
                Uppd.detallesTipo = obj.detallesTipo;
                Uppd.ubicacion = obj.ubicacion;

                _context.Configuration.ValidateOnSaveEnabled = false;

                    unitOfWork.Complete();

                    return RedirectToAction("TipoDocumento");
                }
                else
                {
                    return View("ActualizarTipo", obj);
                }

        }

        public ActionResult AsignarRutasN1(int idArea, List<Nivel1> obj5, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            var usuario = unitOfWork.TipoDocumento.Get(idArea);
            ViewBag.Nombre = usuario.nombreTipo;
            ViewBag.id = usuario.idTipo;
            ViewBag.Permisos = unitOfWork.Nivel1.GetAll();
            var usu = from p in _context.Nivel1 where p.TipoDoc.Any(u => u.idTipo == idArea) select p;


            List<Nivel1> permisosUsuario = new List<Nivel1>();
            foreach (var perusu in usu)
            {
                permisosUsuario.Add(perusu);
            }

            ViewBag.PermisosUsu = permisosUsuario;
            unitOfWork = new UnitOfWork(_context);
            List<Nivel1> permiso = new List<Nivel1>();
            if (obj5 == null)
            {
                foreach (var nusuario in unitOfWork.Nivel1.GetAll())
                {
                    permiso.Add(nusuario);
                }
            }
            else
            {
                permiso = TempData["permiso"] as List<Nivel1>;
            }
            int tamanoPagina = 5;
            int numeroPagina = (pagina ?? 1);
            return View(permiso.ToPagedList(numeroPagina, tamanoPagina));
        }

        public ActionResult AsignarRutasN2(int idArea, List<Nivel2> obj5, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            var usuario = unitOfWork.TipoDocumento.Get(idArea);
            ViewBag.Nombre = usuario.nombreTipo;
            ViewBag.id = usuario.idTipo;
            ViewBag.Permisos = unitOfWork.Nivel2.GetAll();
            var usu = from p in _context.Nivel2 where p.TipoDoc.Any(u => u.idTipo == idArea) select p;


            List<Nivel2> permisosUsuario = new List<Nivel2>();
            foreach (var perusu in usu)
            {
                permisosUsuario.Add(perusu);
            }

            ViewBag.PermisosUsu = permisosUsuario;
            unitOfWork = new UnitOfWork(_context);
            List<Nivel2> permiso = new List<Nivel2>();
            if (obj5 == null)
            {
                foreach (var nusuario in unitOfWork.Nivel2.GetAll())
                {
                    permiso.Add(nusuario);
                }
            }
            else
            {
                permiso = TempData["permiso"] as List<Nivel2>;
            }
            int tamanoPagina = 5;
            int numeroPagina = (pagina ?? 1);
            return View(permiso.ToPagedList(numeroPagina, tamanoPagina));
        }

        public ActionResult AsignarRutasN3(int idArea, List<Nivel3> obj5, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            var usuario = unitOfWork.TipoDocumento.Get(idArea);
            ViewBag.Nombre = usuario.nombreTipo;
            ViewBag.id = usuario.idTipo;
            ViewBag.Permisos = unitOfWork.Nivel3.GetAll();
            var usu = from p in _context.Nivel3 where p.TipoDoc.Any(u => u.idTipo == idArea) select p;


            List<Nivel3> permisosUsuario = new List<Nivel3>();
            foreach (var perusu in usu)
            {
                permisosUsuario.Add(perusu);
            }

            ViewBag.PermisosUsu = permisosUsuario;
            unitOfWork = new UnitOfWork(_context);
            List<Nivel3> permiso = new List<Nivel3>();
            if (obj5 == null)
            {
                foreach (var nusuario in unitOfWork.Nivel3.GetAll())
                {
                    permiso.Add(nusuario);
                }
            }
            else
            {
                permiso = TempData["permiso"] as List<Nivel3>;
            }
            int tamanoPagina = 5;
            int numeroPagina = (pagina ?? 1);
            return View(permiso.ToPagedList(numeroPagina, tamanoPagina));
        }

        public ActionResult AsignarRutasN4(int idArea, List<Nivel4> obj5, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            var usuario = unitOfWork.TipoDocumento.Get(idArea);
            ViewBag.Nombre = usuario.nombreTipo;
            ViewBag.id = usuario.idTipo;
            ViewBag.Permisos = unitOfWork.Nivel1.GetAll();
            var usu = from p in _context.Nivel1 where p.TipoDoc.Any(u => u.idTipo == idArea) select p;


            List<Nivel1> permisosUsuario = new List<Nivel1>();
            foreach (var perusu in usu)
            {
                permisosUsuario.Add(perusu);
            }

            ViewBag.PermisosUsu = permisosUsuario;
            unitOfWork = new UnitOfWork(_context);
            List<Nivel1> permiso = new List<Nivel1>();
            if (obj5 == null)
            {
                foreach (var nusuario in unitOfWork.Nivel1.GetAll())
                {
                    permiso.Add(nusuario);
                }
            }
            else
            {
                permiso = TempData["permiso"] as List<Nivel1>;
            }
            int tamanoPagina = 5;
            int numeroPagina = (pagina ?? 1);
            return View(permiso.ToPagedList(numeroPagina, tamanoPagina));
        }

        public ActionResult AsignarRutasN5(int idArea, List<Nivel1> obj5, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            var usuario = unitOfWork.TipoDocumento.Get(idArea);
            ViewBag.Nombre = usuario.nombreTipo;
            ViewBag.id = usuario.idTipo;
            ViewBag.Permisos = unitOfWork.Nivel1.GetAll();
            var usu = from p in _context.Nivel1 where p.TipoDoc.Any(u => u.idTipo == idArea) select p;


            List<Nivel1> permisosUsuario = new List<Nivel1>();
            foreach (var perusu in usu)
            {
                permisosUsuario.Add(perusu);
            }

            ViewBag.PermisosUsu = permisosUsuario;
            unitOfWork = new UnitOfWork(_context);
            List<Nivel1> permiso = new List<Nivel1>();
            if (obj5 == null)
            {
                foreach (var nusuario in unitOfWork.Nivel1.GetAll())
                {
                    permiso.Add(nusuario);
                }
            }
            else
            {
                permiso = TempData["permiso"] as List<Nivel1>;
            }
            int tamanoPagina = 5;
            int numeroPagina = (pagina ?? 1);
            return View(permiso.ToPagedList(numeroPagina, tamanoPagina));
        }

        public ActionResult ActionAsignarN1(int idUsuario, int idPermiso)
        {
                    _context = new BDContext();
                    unitOfWork = new UnitOfWork(_context);
                    Uppd = unitOfWork.TipoDocumento.Get(idUsuario);
                    Nivel1 permiso = unitOfWork.Nivel1.Get(idPermiso);
                    Uppd.N1.Add(permiso);

                    unitOfWork.Complete();

                    return RedirectToAction("AsignarRutasN1", new { idArea = idUsuario, perm = true, AQ = 1 });
        }

        public ActionResult ActionAsignarN2(int idUsuario, int idPermiso)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            Uppd = unitOfWork.TipoDocumento.Get(idUsuario);
            Nivel2 permiso = unitOfWork.Nivel2.Get(idPermiso);
            Uppd.N2.Add(permiso);

            unitOfWork.Complete();

            return RedirectToAction("AsignarRutasN2", new { idArea = idUsuario, perm = true, AQ = 1 });
        }

        public ActionResult ActionAsignarN3(int idUsuario, int idPermiso)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            Uppd = unitOfWork.TipoDocumento.Get(idUsuario);
            Nivel3 permiso = unitOfWork.Nivel3.Get(idPermiso);
            Uppd.N3.Add(permiso);

            unitOfWork.Complete();

            return RedirectToAction("AsignarRutasN3", new { idArea = idUsuario, perm = true, AQ = 1 });
        }

        public ActionResult ActionAsignarN4(int idUsuario, int idPermiso)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            Uppd = unitOfWork.TipoDocumento.Get(idUsuario);
            Nivel4 permiso = unitOfWork.Nivel4.Get(idPermiso);
            Uppd.N4.Add(permiso);

            unitOfWork.Complete();

            return RedirectToAction("AsignarRutasN4", new { idArea = idUsuario, perm = true, AQ = 1 });
        }

        public ActionResult ActionAsignarN5(int idUsuario, int idPermiso)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            Uppd = unitOfWork.TipoDocumento.Get(idUsuario);
            Nivel5 permiso = unitOfWork.Nivel5.Get(idPermiso);
            Uppd.N5.Add(permiso);

            unitOfWork.Complete();

            return RedirectToAction("AsignarRutasN5", new { idArea = idUsuario, perm = true, AQ = 1 });
        }

        public ActionResult ActionQuitarN1(int idUsuario, int idPermiso)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            Uppd = unitOfWork.TipoDocumento.Get(idUsuario);
            Nivel1 permiso = unitOfWork.Nivel1.Get(idPermiso);
            _context.Entry(Uppd).Collection("N1").Load();
            Uppd.N1.Remove(permiso);

            unitOfWork.Complete();

            return RedirectToAction("AsignarRutasN1", new { idArea = idUsuario, perm = true, AQ = 1 });
        }

        public ActionResult ActionQuitarN2(int idUsuario, int idPermiso)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            Uppd = unitOfWork.TipoDocumento.Get(idUsuario);
            Nivel2 permiso = unitOfWork.Nivel2.Get(idPermiso);
            _context.Entry(Uppd).Collection("N2").Load();
            Uppd.N2.Remove(permiso);

            unitOfWork.Complete();

            return RedirectToAction("AsignarRutasN2", new { idArea = idUsuario, perm = true, AQ = 1 });
        }

        public ActionResult ActionQuitarN3(int idUsuario, int idPermiso)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            Uppd = unitOfWork.TipoDocumento.Get(idUsuario);
            Nivel3 permiso = unitOfWork.Nivel3.Get(idPermiso);
            _context.Entry(Uppd).Collection("N3").Load();
            Uppd.N3.Remove(permiso);

            unitOfWork.Complete();

            return RedirectToAction("AsignarRutasN3", new { idArea = idUsuario, perm = true, AQ = 1 });
        }

        public ActionResult ActionQuitarN4(int idUsuario, int idPermiso)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            Uppd = unitOfWork.TipoDocumento.Get(idUsuario);
            Nivel4 permiso = unitOfWork.Nivel4.Get(idPermiso);
            _context.Entry(Uppd).Collection("N4").Load();
            Uppd.N4.Remove(permiso);

            unitOfWork.Complete();

            return RedirectToAction("AsignarRutasN4", new { idArea = idUsuario, perm = true, AQ = 1 });
        }

        public ActionResult ActionQuitarN5(int idUsuario, int idPermiso)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            Uppd = unitOfWork.TipoDocumento.Get(idUsuario);
            Nivel5 permiso = unitOfWork.Nivel5.Get(idPermiso);
            _context.Entry(Uppd).Collection("N5").Load();
            Uppd.N5.Remove(permiso);

            unitOfWork.Complete();

            return RedirectToAction("AsignarRutasN5", new { idArea = idUsuario, perm = true, AQ = 1 });
        }

    }
}
