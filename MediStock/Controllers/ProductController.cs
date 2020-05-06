using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediStockWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly MediStockContext _context;

        public ProductController(MediStockContext context)
        {
            _context = context;
        }
        public IActionResult Medicine(int Id)
        {
            ViewBag.CategoryList = new SelectList(GetCategoriesList(),"Id","Name");

            List<Medicine> selectList = _context.Medicines.ToList();
            ViewBag.MList = new SelectList(selectList, "Id", "Name");
            return View();
        }

        public List<Category> GetCategoriesList()
        {
            List<Category> categories = _context.Categories.ToList();
            return categories;
        }

        public ActionResult GetMedicineList(int Id)
        {
            List<Medicine> selectList = _context.Medicines.Where(x => x.Id == Id).ToList();
            ViewBag.MList = new SelectList(selectList, "Id", "Name");

            return PartialView("DisplayMedicine");
        }
    }
}