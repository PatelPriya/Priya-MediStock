using DAL.Domains;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MediStockWeb.Areas.Admin.Models
{
    public partial class MedicineModel
    {
        //public MedicineModel()
        //{
        //    Pictures = new List<Picture>();
        //}

        public int MedicineId { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public Guid ProductGUID { get; set; }
        public decimal Price { get; set; }
        public int Manufacturer { get; set; }
        public string Description { get; set; }
        //public string Image { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Stock Stock { get; set; }
       
        //public string PictureStr { get; set; }
        
        public IFormFile Picture { get; set; }
        
        public List<CategoryModel> AllCategories { get; set; }
        public string categoryName { get; set; }
    }
}
