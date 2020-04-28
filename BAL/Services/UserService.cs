using DAL.Data;
using DAL.Domains;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BAL.Services
{
    public class UserService : IUserService
    {
        private MediStockContext context;

        public UserService(MediStockContext context)
        {
            this.context = context;
        }

        Customer customer = new Customer();
        public Customer AddUser(Customer customerEntity)
        {
            if (context.Customers.Any(x => x.Email == customerEntity.Email))
            {
                return null;
            }
            else
            {
                context.Customers.Add(customerEntity);
                context.SaveChanges();
                Customer result = context.Customers.Where(s => s.Email == customerEntity.Email).FirstOrDefault();
                return result;
            }
        }

        public Customer GetUserById(int id)
        {
            customer = context.Customers.Where(a => a.Id == id).FirstOrDefault();
            return customer;
            throw new NotImplementedException();
        }

        public Customer UpdateUser(Customer userEntity)
        {
            Customer dataToEdit = context.Customers.SingleOrDefault(c => c.Id == userEntity.Id);
            dataToEdit.FirstName = userEntity.FirstName;
            dataToEdit.LastName = userEntity.LastName;
            dataToEdit.Email = userEntity.Email;
            dataToEdit.Phone = userEntity.Phone;
            dataToEdit.City = userEntity.City;
            dataToEdit.State = userEntity.State;
            dataToEdit.Address = userEntity.Address;
            dataToEdit.Zipcode = userEntity.Zipcode;
            dataToEdit.IsActive = userEntity.IsActive;
            dataToEdit.IsDeleted = userEntity.IsDeleted;
            dataToEdit.CreatedOn = DateTime.Now;
            dataToEdit.UpdatedOn = DateTime.Now;
            context.SaveChanges();

            // Verify weather user inserted(actually updated) or not.
            Customer presentUser = context.Customers.SingleOrDefault(c => c.Email == userEntity.Email);
            if (presentUser != null)
            {
                return presentUser;
            }
            else
            {
                return null;
            }
            throw new NotImplementedException();
        }

        public Customer DeleteUser(Customer customerEntity)
        {

            context.Entry(customerEntity).State = EntityState.Deleted;
            context.SaveChanges();

            return customerEntity;
        }

        public virtual IList<SelectListItem> PrepareTitleList()
        {
            var data = context.Customers.Select(x => new SelectListItem()
            {
                Text = x.FirstName,
                Value = x.FirstName
            }).ToList();

            // Add default value. Set selected = true so it will load all employee list.
            data.Add(new SelectListItem() { Text = "Select One", Value = "", Selected = true });
            return data;
        }

        public IEnumerable<Customer> GetUsersByName(string searchString)
        {
            List<Customer> list = context.Customers.Where(t => t.FirstName.Contains(searchString) || t.Email.Contains(searchString) || t.Address.Contains(searchString) || t.Phone.Contains(searchString)).ToList();
            return list;
        }

        public IEnumerable<Customer> GetAllUsers()
        {
            IList<Customer> customerModel = new List<Customer>();
            var query = context.Customers.ToList();
            //var query = from Customer in context.Customers select Customer;
            var customerData = query.ToList();
            foreach (var item in customerData)
            {
                customerModel.Add(new Customer()
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Address = item.Address,
                    State = item.State,
                    Email = item.Email,
                    Phone = item.Phone,
                    IsActive = item.IsActive,
                    IsDeleted = item.IsDeleted,
                    CreatedOn = item.CreatedOn,
                    UpdatedOn = item.UpdatedOn

                });

            }
            return customerModel;
        }

        #region User Authentication

        // Sign In to authenticate the user
        public Customer SignIn(Customer userEntity)
        {
            var userData = context.Customers.Where(a => a.Email == userEntity.Email).FirstOrDefault();

            var passwordData = context.Passwords.Where(a => a.PasswordString == userEntity.Password.PasswordString).FirstOrDefault();

            if ( userData != null && passwordData != null)
            {
                if (userData.Id == passwordData.CustomerId)
                {
                    return userData;
                }
            }
            
            return null;
        }

        public Customer EmailExists(Customer userEntity)
        {
            var userData = context.Customers.Where(a => a.Email == userEntity.Email).FirstOrDefault();
            return userData;
        }
        public bool UpdateUserPassword(Customer userEntity)
        {
            var userData = context.Customers.SingleOrDefault(a => a.Email == userEntity.Email);
            userData.Password = userEntity.Password;
            context.SaveChanges();

            // Check user password updated or not
            Customer userPasswordUpdatedOrNot = context.Customers.SingleOrDefault(s => s.Email == userEntity.Email && s.Password == userEntity.Password);
            if (userPasswordUpdatedOrNot != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}