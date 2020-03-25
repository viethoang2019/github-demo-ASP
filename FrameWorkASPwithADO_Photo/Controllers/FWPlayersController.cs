using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using FrameWorkASPwithADO_Photo.Models;

namespace FrameWorkASPwithADO_Photo.Controllers
{
    public class FWPlayersController : Controller
    {
        FWModelDAO db = new FWModelDAO();

        // GET: FWPlayers
        public ActionResult Index()
        {
            return View(db.GetPlayers().ToList());
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FWPlayer player, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                        file.SaveAs(_path);
                    }

                     
                    
                        db.PostPlayer(player);

                        ViewBag.Msg = "Add player successfully";
                    return View();
                    

                }


            }
            catch (Exception)
            {

                ViewBag.Msg = "Failed";
            }
            return View();

        }


    }
}