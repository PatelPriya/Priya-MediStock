using DAL.Data;
using DAL.Domains;
using DAL.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BAL.Services
{
    public class MedicineService : IMedicineService
    {
        #region Fields

        //MediStockContext db = new MediStockContext();
        private readonly MediStockContext context;

        #endregion

        #region Ctor
        public MedicineService(MediStockContext context)
        {
            this.context = context;
        }

        #endregion

        #region Methods

        public Medicine InsertMedicine(Medicine medicineEntity)
        {
            context.Medicines.Add(medicineEntity);
            context.SaveChanges();

            // Get the last added medicine to store the medicine and category mapping data
            //var lastAddedMedicine = context.Medicines.ToList().LastOrDefault();
            //CategoryMedicine categoryMedicine = new CategoryMedicine
            //{
            //    MedicineId = lastAddedMedicine.Id,
            //    CategoryId = context.Categories.Where(x => x.Name == categoryName).FirstOrDefault().Id
            //};
            //context.CategoryMedicine.Add(categoryMedicine);
            //context.SaveChanges();

            return medicineEntity;

        }

        public Medicine DeleteMedicine(Medicine medicineEntity)
        {
            context.Entry(medicineEntity).State = EntityState.Deleted;
            context.SaveChanges();

            return medicineEntity;
        }

        public IEnumerable<Medicine> GetAllMedicines()
        {
            IList<Medicine> medicineModel = new List<Medicine>();
            var query = context.Medicines.ToList();
            var medicineData = query.ToList();
            foreach (var item in medicineData)
            {
                medicineModel.Add(new Medicine()
                {
                    Id = item.Id,
                    Name = item.Name,
                    SKU = item.SKU,
                    PictureStr = item.PictureStr,
                    ProductGUID = item.ProductGUID,
                    Price = item.Price,
                    Manufacturer = item.Manufacturer,
                    ExpiryDate = item.ExpiryDate,
                    IsActive = item.IsActive,
                    IsDeleted = false
                });

            }
            return medicineData;
        }

        public Medicine UpdateMedicine(Medicine medicineEntity)
        {
            context.Entry(medicineEntity).State = EntityState.Modified;
            context.SaveChanges();

            return medicineEntity;
           
        }

        public Medicine GetMedicineById(int medicineId)
        {
            var query = from c in context.Medicines where c.Id == medicineId select c;
            var medicine = query.FirstOrDefault();
            var model = new Medicine()
            {
                Id = medicine.Id,
                Name = medicine.Name,
                SKU = medicine.SKU,
                ProductGUID = medicine.ProductGUID,
                Price = medicine.Price,
                Manufacturer = medicine.Manufacturer,
                ManufacturingDate = medicine.ManufacturingDate,
                Description = medicine.Description,
                ExpiryDate = medicine.ExpiryDate,
                IsActive = medicine.IsActive,
                IsDeleted = false
            };
            return model;
        }

        public IEnumerable<Medicine> GetMedicineByName(string medicineName)
        {
            List<Medicine> medicines = context.Medicines.Where(t => t.Name.Contains(medicineName)).ToList();
            return medicines;
        }
        #endregion
    }
}
