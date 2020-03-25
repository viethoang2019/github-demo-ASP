using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ASPwithADO.Models;
using System.Data;
using System.Data.SqlClient;

namespace ASPwithADO.Controllers
{
    public class StudentsController : Controller
    {
        ModelDAL db = new ModelDAL();

        public IActionResult Index()
        {
            return View(db.GetStudents().ToList());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreateStudent(Student student)
        {
            try
            {
                if(ModelState.IsValid)
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

        public IActionResult Delete(int id)
        {
           
             try
            {
                var res = db.FindId(id);
                if(res!=null)
                {

                    db.Delete(id);
                   
                }

               
              
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("Index", "Students");
        }


        

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student student = db.FindId(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(int id, Student student)
        {
            try
            {
                var stud = db.FindId(id);
                if (stud != null)
                {
                    if (ModelState.IsValid)
                    {
                        db.UpdateStudent(student);
                        ViewBag.Msg = "Congratulation!";
                    }
                }

            }
            catch (Exception)
            {

                ViewBag.Msg = "Failed!";
            }

            return View();

        }






        //[HttpGet]
        //Version có nullable int ? id
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Student student = db.FindId(id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(student);
        //}


        //Cách khác 
        //[HttpPost]
        //public IActionResult Edit(int id, Student student)
        //{

        //    if (id != student.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        db.UpdateStudent(student);
        //        ViewBag.Msg = "Congratulation!";
        //        //return RedirectToAction("Index");
        //    }

        //    return View();

        //}







    }
}