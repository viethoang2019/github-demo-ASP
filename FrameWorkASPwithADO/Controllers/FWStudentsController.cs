using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FrameWorkASPwithADO.Models;
namespace FrameWorkASPwithADO.Controllers
{
    public class FWStudentsController : Controller
    {
        FWModelDAL db = new FWModelDAL();

        // GET: FWStudents
        public ActionResult FWIndex()
        {
            return View(db.GetStudents().ToList());
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreateStudent(FWStudent student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.PostStudent(student);
                    ViewBag.Msg = "Congratulation!";
                }

            }
            catch (Exception)
            {

                ViewBag.Msg = "Fail";
            }
            return View();
        }

    }
}