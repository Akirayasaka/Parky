﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkyWeb.Models;
using ParkyWeb.Repository.IRepository;

namespace ParkyWeb.Controllers
{
    [Authorize]
    public class NationalParksController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public NationalParksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            NationalPark obj = new();
            if(id == null)
            {
                return View(obj);
            }
            obj = await _unitOfWork.NationalPark.GetAsync(SD.NationalParkAPIPath, id.GetValueOrDefault(), HttpContext.Session.GetString("JWToken"));
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(NationalPark obj)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if(files.Count > 0)
                {
                    byte[] photo = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            photo = ms1.ToArray();
                        }
                    }
                    obj.Picture = photo;
                }
                else
                {
                    if(obj.Id != 0)
                    {
                        var objFromDb = await _unitOfWork.NationalPark.GetAsync(SD.NationalParkAPIPath, obj.Id, HttpContext.Session.GetString("JWToken"));
                        obj.Picture = objFromDb.Picture;
                    }
                    else
                    {
                        return View(obj);
                    }
                }
                if (obj.Id == 0)
                {
                    await _unitOfWork.NationalPark.CreateAsync(SD.NationalParkAPIPath, obj, HttpContext.Session.GetString("JWToken"));
                }
                else
                {
                    await _unitOfWork.NationalPark.UpdateAsync(SD.NationalParkAPIPath + obj.Id, obj, HttpContext.Session.GetString("JWToken"));
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }

        public async Task<IActionResult> GetAllNationalPark()
        {
            return Json(new { data = await _unitOfWork.NationalPark.GetAllAsync(SD.NationalParkAPIPath, HttpContext.Session.GetString("JWToken")) });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _unitOfWork.NationalPark.DeleteAsync(SD.NationalParkAPIPath, id, HttpContext.Session.GetString("JWToken"));            
            return status ? Json(new { success = true, message = "Delete Successful" }) : Json(new { success = true, message = "Delete Failed" });
        }
    }
}
