using DatabaseAccessLayer;
using JobsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobsPortal.Controllers
{
    public class UserController : Controller
    {
        private JobsPortalDbEntities db = new JobsPortalDbEntities();

        // GET: User
        public ActionResult NewUser()
        {
            return View(new UserMV());
        }

        //Post: User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewUser(UserMV userMV)
        {
            if (ModelState.IsValid)
            {
                var checkUser = db.UserTables.Where(u => u.EmailAddress == userMV.EmailAddress).FirstOrDefault();
                if (checkUser != null)
                {
                    ModelState.AddModelError("EmailAddress", "Email is already registered");
                    return View(userMV);
                }

                checkUser = db.UserTables.Where(u => u.UserName == userMV.UserName).FirstOrDefault();
                if (checkUser != null)
                {
                    ModelState.AddModelError("UserName", "User Name is already registered");
                    return View(userMV);
                }

                using (var transact = db.Database.BeginTransaction())
                {
                    try
                    {
                        var user = new UserTable();
                        user.UserName = userMV.UserName;
                        user.Password = userMV.Password;
                        user.ContactNo = userMV.ContactNo;
                        user.EmailAddress = userMV.EmailAddress;
                        user.UserTypeID = userMV.AreYouProvider == true ? 2 : 3;
                        db.UserTables.Add(user);
                        db.SaveChanges();

                        if (userMV.AreYouProvider == true)
                        {
                            var company = new CompanyTable();
                            company.UserID = user.UserID;
                            if (string.IsNullOrEmpty(userMV.Company.EmailAddress))
                            {
                                transact.Rollback();
                                ModelState.AddModelError("Company.EmailAddress", "*Required");
                                return View(userMV);
                            }
                            if (string.IsNullOrEmpty(userMV.Company.CompanyName))
                            {
                                transact.Rollback();
                                ModelState.AddModelError("Company.CompanyName", "*Required");
                                return View(userMV);
                            }
                            if (string.IsNullOrEmpty(userMV.Company.PhoneNo))
                            {
                                transact.Rollback();
                                ModelState.AddModelError("Company.PhoneNo", "*Required");
                                return View(userMV);
                            }
                            if (string.IsNullOrEmpty(userMV.Company.Description))
                            {
                                transact.Rollback();
                                ModelState.AddModelError("Company.Description", "*Required");
                                return View(userMV);
                            }
                            company.EmailAddress = userMV.Company.EmailAddress;
                            company.CompanyName = userMV.Company.CompanyName;
                            company.ContactNo = userMV.ContactNo;
                            company.PhoneNo = userMV.Company.PhoneNo;
                            company.Logo = "~/Content/assests/img/logo/logo.png";
                            company.Description = userMV.Company.Description;
                            db.CompanyTables.Add(company);
                            db.SaveChanges();
                        }
                        transact.Commit();
                        return RedirectToAction("Login");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "Please provide correct details!");
                        Console.WriteLine(ex.ToString());
                        transact.Rollback();
                    }                    
                }
            }
            return View(userMV);
        }

        public ActionResult Login()
        {
            return View(new UserLoginMV());
        }

        //Login: User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginMV userLoginMV)
        {
            if (ModelState.IsValid)
            {
                var user = db.UserTables.Where(u => u.UserName == userLoginMV.UserName && u.Password == userLoginMV.Password).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "User Name or Password are incorrect");
                    return View(userLoginMV);
                }
                Session["UserID"] = user.UserID;
                Session["UserName"] = user.UserName;
                Session["UserTypeID"] = user.UserTypeID;
                if(user.UserTypeID == 2)
                {
                    Session["CompanyID"] = user.CompanyTables.FirstOrDefault().CompanyID;
                }
                if (user.UserTypeID == 3)
                {
                    Session["EmployeeID"] = user.EmployeesTables.FirstOrDefault().EmployeeID;
                }
                return RedirectToAction("Index", "Home");
            }
            return View(userLoginMV);
        }

        public ActionResult Logout()
        {
            Session["UserID"] = string.Empty;
            Session["UserName"] = string.Empty;
            Session["CompanyID"] = string.Empty;
            Session["EmployeeID"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            return RedirectToAction("Index", "Home");
        }

        //Get all Users for Admin
        public ActionResult AllUsers()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            var users = db.UserTables.ToList();
            return View(users);
        }

        //Forgot Password
        public ActionResult Forgot()
        {
            return View(new ForgotPasswordMV());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forgot(ForgotPasswordMV forgotPasswordMV)
        {
            if (forgotPasswordMV == null)
                return View();

            var user = db.UserTables.Where(u => u.EmailAddress == forgotPasswordMV.Email).FirstOrDefault();
            if (user != null)
            {
                string userandpassword = "User Name: " + user.UserName + "\n" + "Password: " + user.Password;
                string body = userandpassword;

                bool IsSendEmail = JobsPortal.Helper.Email.Emailsend(user.EmailAddress, "Account Details", body, true);
                if (IsSendEmail)
                {
                    ModelState.AddModelError(string.Empty, "Username and Password is sent!");
                }
                else
                {
                    ModelState.AddModelError("Email", "Your Email is Registered! Current email sending is not working properly, please try again after some time ");
                }
            }
            else
            {
                ModelState.AddModelError("Email", "Email is not registered");
            }
            return View(forgotPasswordMV);
        }

    }
}