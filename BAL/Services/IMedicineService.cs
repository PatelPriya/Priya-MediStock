using DAL.Domains;
using System.Collections.Generic;

namespace BAL.Services
{
    public interface IMedicineService
    {
        public IEnumerable<Medicine> GetAllMedicines();
        public IEnumerable<Medicine> GetMedicineByName(string MedicineName);
        public Medicine GetMedicineById(int MedicineID);
        public Medicine InsertMedicine(Medicine MedicineEntity,string categoryName);
        public Medicine UpdateMedicine(Medicine MedicineEntity);
        public Medicine DeleteMedicine(int MedicineID);
    }
}
