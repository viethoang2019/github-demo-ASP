using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ASPwithADO_Photo.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.IO;
using X.PagedList;

namespace ASPwithADO_Photo.Controllers
{
    public class PlayersController : Controller
    {
        ModelDAO db = new ModelDAO();
        public IActionResult Index(int? page)
        {
            int maxsize = 3;
            var pageNum = page ?? 1;
           
            var model = db.GetPlayers().OrderByDescending(p => p.Name).ToList().ToPagedList(pageNum, maxsize);
            //var model = db.GetPlayers().ToList().ToPagedList(pageNum, maxsize);

            // Search player by name
            //if (string.IsNullOrWhiteSpace(name))
            //{

            //    ViewBag.page = model;
            //}
            //else
            //{
            //    ViewBag.page= db.SearchByName(name);
            //    model = model.Where(p => p.Name.ToUpper().Contains(pname) || p.Name.ToLower().Contains(pname)).ToPagedList(pageNum, maxsize);

            //    ViewBag.page = res;
            //}

            ViewBag.page = model;
            return View();

        }

        public IActionResult Search(string name)

        {

            var model = db.GetPlayers().ToList();
            if (string.IsNullOrWhiteSpace(name))
            {
                return View(model);
            }
            //var model = db.GetPlayers().ToList();

            //return View();

            else
            {
                model = model.Where(p => p.Name.Contains(name)).ToList();
                return View(model);
            }



        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Player player,IFormFile file)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if (file.Length > 0)
                    {
                        var filePath = Path.Combine("wwwroot/images", file.FileName);
                        var stream = new FileStream(filePath, FileMode.Create);
                        file.CopyToAsync(stream);

                        player.Photo = "images/" + file.FileName;

                        db.PostPlayer(player);

                        ViewBag.Msg = "Add player successfully";
                    }

                }


            }
            catch (Exception)
            {

                ViewBag.Msg = "Failed";
            }
            return View();
            
        }


        public IActionResult Delete(int id)
        {

            try
            {
                var res = db.FindId(id);
                if (res != null)
                {

                    db.Delete(id);

                }



            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("Index", "Players");
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            Player player = db.FindId(id);
            if (player == null)
            {
                return NotFound();
            }
            return View(player);
        }

        public IActionResult Edit(int id, Player player,IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file.Length > 0)
                    {
                        var filePath = Path.Combine("wwwroot/images", file.FileName);
                        var stream = new FileStream(filePath, FileMode.Create);
                        file.CopyToAsync(stream);

                        player.Photo = "images/" + file.FileName;

                        db.EditPlayer(player);

                        ViewBag.Msg = "Edit player successfully";
                    }

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