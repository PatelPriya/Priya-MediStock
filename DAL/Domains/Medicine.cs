using DAL.Domains.Base;
using DAL.Mappings;
using System;
using System.Collections.Generic;

namespace DAL.Domains
{
    public partial class Medicine : BaseEntity
    {
        public string Name { get; set; }
        public string SKU { get; set; }
        public Guid ProductGUID { get; set; }
        public decimal Price { get; set; }
        public int Manufacturer { get; set; }
        public string Description { get; set; }        
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Stock Stock { get; set; }

        public string PictureStr { get; set; }

        public ICollection<CategoryMedicine> _categoryMedicine;

        public virtual ICollection<CategoryMedicine> CategoryMedicine
        {
            get => _categoryMedicine ?? (_categoryMedicine = new List<CategoryMedicine>());
            set => _categoryMedicine = value;
        }
        public ICollection<Picture> _pictures;

        public virtual ICollection<Picture> Pictures
        {
            get => _pictures ?? (_pictures = new List<Picture>());
            set => _pictures = value;
        }
    }
}
