using DAL.Data;
using DAL.Domains;
using DAL.Mappings;
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

        public Medicine InsertMedicine(Medicine medicineEntity,string categoryName)
        {
            context.Medicines.Add(medicineEntity);
            context.SaveChanges();

            var lastAddedMedicine = context.Medicines.ToList().LastOrDefault();
            CategoryMedicine categoryMedicine = new CategoryMedicine
            {
                MedicineId = lastAddedMedicine.Id,
                CategoryId = context.Categories.Where(x => x.Name == categoryName).FirstOrDefault().Id
            };
            context.CategoryMedicine.Add(categoryMedicine);
            context.SaveChanges();

            return lastAddedMedicine;

        }

        public Medicine DeleteMedicine(int medicineID)
        {
            var medicineData = context.Medicines.FirstOrDefault(a => a.Id == medicineID);
            medicineData.IsDeleted = true;
            medicineData.IsActive = false;
            context.SaveChanges();

            // Conifirmation of data is deleted or not
            var checkDeletedOrNot = context.Medicines.FirstOrDefault(s => s.Id == medicineID);
            if (checkDeletedOrNot.IsDeleted != true)
                return null;
            else
                return checkDeletedOrNot;
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
            var medicineData = context.Medicines.SingleOrDefault(c => c.Id == medicineEntity.Id);
            medicineData.Id = medicineEntity.Id;
            medicineData.Name = medicineEntity.Name;
            medicineData.SKU = medicineEntity.SKU;
            medicineData.ProductGUID = medicineEntity.ProductGUID;
            medicineData.Price = medicineEntity.Price;
            medicineData.Manufacturer = medicineEntity.Manufacturer;
            medicineData.ManufacturingDate = medicineEntity.ManufacturingDate;
            medicineData.Description = medicineEntity.Description;
            //if (medicineEntity.Image != null)
            //{
            //    medicineData.Image = medicineEntity.Image;
            //}
            medicineData.ExpiryDate = medicineEntity.ExpiryDate;
            medicineData.IsActive = medicineEntity.IsActive;
            medicineData.IsDeleted = medicineEntity.IsDeleted;

            context.SaveChanges();

            // Verify weather user inserted(actually updated) or not.
            var presentMedicine = context.Medicines.SingleOrDefault(c => c.Id == medicineEntity.Id);
            if (presentMedicine != null)
                return presentMedicine;
            else
                return null;
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
