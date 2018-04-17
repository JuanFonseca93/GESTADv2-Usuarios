using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Syncfusion.JavaScript;

namespace GESTADexplorer.Controllers
{
    public class ExplorerController : Controller
    {
        // GET: Explorador
        public ActionResult Explorador(FileExplorerParams args)
        {
            FileExplorerOperations operation = new FileExplorerOperations();
            switch (args.ActionType)
            {
                case "Read":
                    return Json(operation.Read(args.Path, args.ExtensionsAllow));
                case "CreateFolder":
                    return Json(operation.CreateFolder(args.Path, args.Name));
                case "Paste":
                    return Json(operation.Paste(args.LocationFrom, args.LocationTo, args.Names, args.Action, args.CommonFiles));
                case "Remove":
                    return Json(operation.Remove(args.Names, args.Path));
                case "Rename":
                    return Json(operation.Rename(args.Path, args.Name, args.NewName, args.CommonFiles));
                case "GetDetails":
                    return Json(operation.GetDetails(args.Path, args.Names));
                case "Download":
                    operation.Download(args.Path, args.Names);
                    break;
                case "Upload":
                    RedirectToAction("Account/Register");
                    operation.Upload(args.FileUpload, args.Path);
                    
                    
                    break;
                case "Search":
                    return Json(operation.Search(args.Path, args.ExtensionsAllow, args.SearchString, args.CaseSensitive), JsonRequestBehavior.AllowGet);

            }
            //return Json("", JsonRequestBehavior.AllowGet);
            ViewBag.MyExplorer = operation;
            return View(operation);

        }

    }
}