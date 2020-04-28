using BAL.Services;
using DAL.Domains;
using MediStockWeb.Areas.Admin.Controllers.Base;
using MediStockWeb.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using X.PagedList;

namespace MediStockWeb.Areas.Admin.Controllers
{
    public class AdminUserController : BaseController
    {
        #region Fields

        private readonly IUserService _adminService;

        #endregion

        #region Ctor
        public AdminUserController(IUserService userService)
        {
            _adminService = userService;
        }
        #endregion

        #region Method

        #region CRUD

        public IActionResult List(int? page)
        {
            
                // get all users all data.
                var userData = _adminService.GetAllUsers();
                var userList = new List<UserModel>();
                foreach (var item in userData)
                {
                    var lst = new UserModel();
                    lst.UserID = item.Id;
                    lst.FirstName = item.FirstName;
                    lst.LastName = item.LastName;
                    lst.Address = item.Address;
                    lst.State = item.State;
                    lst.Email = item.Email;
                    lst.Phone = item.Phone;
                    lst.IsActive = item.IsActive;
                    lst.IsDeleted = item.IsDeleted;
                    lst.CreatedOn = item.CreatedOn;
                    lst.UpdatedOn = item.UpdatedOn;
                    userList.Add(lst);
                }

            var pageNumber = page ?? 1;
            var pageSize = 6;//Show 10 rows every time
            return View(userList.ToPagedList(pageNumber, pageSize));


        }

        public IActionResult Create()
        {
            var model = new UserModel();
            return View(model
                );
        }

        [HttpPost]
        public IActionResult Create(UserModel model , bool continueEditing)
        { // Adding new user
            Customer obj = new Customer();
            // Model Prepration
            Customer objUserModel = new Customer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    City = model.City,
                    State = model.State,
                    Address = model.Address,
                    Zipcode = model.Zipcode,
                    Phone = model.Phone,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                };

            objUserModel.Password = new Password()
            {
                PasswordString = model.Passwordstr
            };

            var userData = _adminService.AddUser(objUserModel);
                obj = userData;

            if (obj == null)
            {
                return RedirectToAction("AddUser", "AdminUser", new { area = "Admin" });
            }
            else if(!continueEditing)
            {
                return RedirectToAction("List");
                //return RedirectToAction("ManageUsers", "Admin", new { area = "Admin" });
            }
            else
            {
                return RedirectToAction("Edit", new { id = objUserModel.Id });
            }
        }

        public IActionResult Edit(int id)
        {
            var customerModel = _adminService.GetUserById(id);
            UserModel model = new UserModel
            {
                UserID = customerModel.Id,
                FirstName = customerModel.FirstName,
                LastName = customerModel.LastName,
                Email = customerModel.Email,
                Password = customerModel.Password,
                State = customerModel.State,
                Address = customerModel.Address,
                Zipcode = customerModel.Zipcode,
                Phone = customerModel.Phone,
                IsActive = customerModel.IsActive,
                IsDeleted = customerModel.IsDeleted,
                CreatedOn = customerModel.CreatedOn,
                UpdatedOn = customerModel.UpdatedOn
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Customer model)
        {
            Customer admincustomerModel = new Customer
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                State = model.State,
                Address = model.Address,
                Zipcode = model.Zipcode,
                Phone = model.Phone,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                CreatedOn = model.CreatedOn,
                UpdatedOn = model.UpdatedOn
            };

            var customerModel =  _adminService.UpdateUser(admincustomerModel);

            if (customerModel == null)
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

        public IActionResult Delete(Customer customerEntity)
        {
           
            var customerData = _adminService.DeleteUser(customerEntity);
            if (customerData == null)
            {
                // Failed
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


        public ActionResult SearchUser(string searchString , int? page)
        {

            // Get particular user data
            var userByName = _adminService.GetUsersByName(searchString);
            var userNameList = new List<UserModel>();
            foreach (var item in userByName)
            {
                var lst = new UserModel();
                lst.UserID= item.Id;
                lst.FirstName = item.FirstName;
                lst.LastName = item.LastName;
                lst.Address = item.Address;
                lst.State = item.State;
                lst.Zipcode = item.Zipcode;
                lst.Email = item.Email;
                lst.Phone = item.Phone;
                lst.IsActive = item.IsActive;
                lst.IsDeleted = item.IsDeleted;
                lst.CreatedOn = item.CreatedOn;
                lst.UpdatedOn = item.UpdatedOn;
                userNameList.Add(lst);
            }

            var pageNumber = page ?? 1;
            var pageSize = 6;//Show 10 rows every time


            if (userNameList == null)
            {
                return View(userNameList.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View(userNameList.ToPagedList(pageNumber, pageSize));
            }
        }

        #endregion

        #region Profile

        public IActionResult Profile(int id)
        {
            var model = new UserProfileModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Profile(UserProfileModel model)
        {
            return View(model);
        }

        #endregion

        #endregion
    }
}