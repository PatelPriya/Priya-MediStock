using BAL.Services;
using DAL.Data;
using DAL.Domains;
using MediStockWeb.Areas.Admin.Controllers.Base;
using MediStockWeb.Areas.Admin.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using X.PagedList;

namespace MediStockWeb.Areas.Admin.Controllers
{
    public class MedicineController : BaseController
    {
        #region Fields
        private readonly IWebHostEnvironment _env;
        private readonly IMedicineService _medicineService;
        private readonly ICategoryService _categoryService;
        private readonly MediStockContext _context;

        #endregion

        #region Ctor

        public MedicineController(IMedicineService medicineService,
        IWebHostEnvironment environment, ICategoryService categoryService,
                                     MediStockContext context
            )
        {
            _env = environment;
            _medicineService = medicineService;
            _medicineService = medicineService;
            _categoryService = categoryService;
            _context = context;
        }

        #endregion

        #region Methods

        public IActionResult List()
        {

            var medicine = _context.Medicines.ToList();
            return View(medicine);
            //get all medicine data.
            //var medicineData = _medicineService.GetAllMedicines().ToList();
            //var medicineList = new List<MedicineModel>();
            //foreach (var item in medicineData)
            //{
            //    var model = new MedicineModel();
            //    model.MedicineId = item.Id;
            //    model.Name = item.Name;
            //    model.SKU = item.SKU;
            //    //model.Picture = item.Picture;
            //    model.ProductGUID = item.ProductGUID;
            //    model.Price = item.Price;
            //    model.Manufacturer = item.Manufacturer;
            //    model.ExpiryDate = item.ExpiryDate;
            //    model.IsActive = item.IsActive;
            //    model.IsDeleted = false;
            //    medicineList.Add(model);
            //}
            //return View(medicineList);
        }

        //public IActionResult List(MedicineModel searchModel)
        //{
        //    return View(searchModel);
        //}

        public IActionResult Create()
        {
            var model = new MedicineModel();
            var categories = _categoryService.GetAllCategories()/*.ToList()*/;
            SelectList list = new SelectList(categories, "Id", "Name");
            ViewBag.categories = list;
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(MedicineModel model)
        {
            Medicine obj = new Medicine();

            //Image code
            string uniqueFileName = null;
            if (model.Picture != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.Picture.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            // Model Prepration
            Medicine objMedicineModel = new Medicine
            {
                Name = model.Name,
                SKU = model.SKU,
                ProductGUID = model.ProductGUID,
                Price = model.Price,
                Manufacturer = model.Manufacturer,
                ManufacturingDate = model.ManufacturingDate,
                Description = model.Description,
                ExpiryDate = model.ExpiryDate,
                IsActive = model.IsActive,
                IsDeleted = false,
                Stock = model.Stock,
                PictureStr = uniqueFileName,
            };

            var medicines = _medicineService.InsertMedicine(objMedicineModel);
            obj = medicines;
            if (obj == null)
            {
                return RedirectToAction("Create", "AdminUser", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("List", new { id = objMedicineModel.Id });
            }

        }


        public IActionResult Edit(int id)
        {
            var medicineModel = _medicineService.GetMedicineById(id);

            var categories = _categoryService.GetAllCategories()/*.ToList()*/;
            SelectList list = new SelectList(categories, "Id", "Name");
            ViewBag.categories = list;

            MedicineModel model = new MedicineModel
            {
                MedicineId = medicineModel.Id,
                Name = medicineModel.Name,
                SKU = medicineModel.SKU,
                ProductGUID = medicineModel.ProductGUID,
                Price = medicineModel.Price,
                Manufacturer = medicineModel.Manufacturer,
                ManufacturingDate = medicineModel.ManufacturingDate,
                Description = medicineModel.Description,
                ExpiryDate = medicineModel.ExpiryDate,
                IsActive = medicineModel.IsActive,
                IsDeleted = false
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(MedicineModel model)
        {
            string uniqueFileName = null;
            if (model.Picture != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.Picture.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            Medicine medicineModel = new Medicine
            {
                Id = model.MedicineId,
                Name = model.Name,
                SKU = model.SKU,
                ProductGUID = model.ProductGUID,
                Price = model.Price,
                Manufacturer = model.Manufacturer,
                ManufacturingDate = model.ManufacturingDate,
                Description = model.Description,
                PictureStr = uniqueFileName,
                ExpiryDate = model.ExpiryDate,
                IsActive = model.IsActive,
                IsDeleted = false
            };
            var medicineModels = _medicineService.UpdateMedicine(medicineModel);
            if (medicineModels == null)
            {
                // Failed
                ViewBag.AddCategoryStatus = "Failed";
                return RedirectToAction("Edit");
            }
            else
            {
                // Success
                ViewBag.AddUserStatus = "Success";
                return RedirectToAction("List");
            }
        }

        public IActionResult Delete(Medicine medicineEntity)
        {
            //_adminMedicineService.DeleteMedicine(id);
            //return RedirectToAction("List");

            var medicineData = _medicineService.DeleteMedicine(medicineEntity);

            if (medicineData == null)
            {
                //Failed
                ViewBag.AddUserStatus = "Failed";
                return RedirectToAction("Create");
            }
            else
            {
                // Success
                ViewBag.AddUserStatus = "Success";
                return RedirectToAction("List");
            }
        }

        public ActionResult SearchMedicine(string searchString)
        {
            // Get particular user data
            var medicineByName = _medicineService.GetMedicineByName(searchString);
            var medicineNameList = new List<MedicineModel>();
            foreach (var item in medicineByName)
            {
                var lst = new MedicineModel();
                lst.MedicineId = item.Id;
                lst.Name = item.Name;
                lst.SKU = item.SKU;
                lst.ProductGUID = item.ProductGUID;
                lst.Manufacturer = item.Manufacturer;
                lst.ManufacturingDate = item.ManufacturingDate;
                lst.ExpiryDate = item.ExpiryDate;
                medicineNameList.Add(lst);
            }

            if (medicineNameList == null)
            {
                return View("List", medicineNameList);
            }
            else
            {
                return View("List", medicineNameList);
            }
        }
        #endregion
    }
}