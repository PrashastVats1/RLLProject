using DatabaseAccessLayer;
using JobsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using System.Net;

namespace JobsPortal.Controllers
{
    public class UserController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UserController));
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
            bool hasValidationErrors = false;
            if (ModelState.IsValid)
            {
                var checkUser = db.UserTables.Where(u => u.EmailAddress == userMV.EmailAddress).FirstOrDefault();
                if (checkUser != null)
                {
                    ModelState.AddModelError("EmailAddress", "Email is already registered");
                    hasValidationErrors = true;
                }

                checkUser = db.UserTables.Where(u => u.UserName == userMV.UserName).FirstOrDefault();
                if (checkUser != null)
                {
                    ModelState.AddModelError("UserName", "User Name is already registered");
                    hasValidationErrors = true;
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
                                ModelState.AddModelError("Company.EmailAddress", "*Required");
                                hasValidationErrors = true;
                            }
                            if (string.IsNullOrEmpty(userMV.Company.CompanyName))
                            {
                                ModelState.AddModelError("Company.CompanyName", "*Required");
                                hasValidationErrors = true;
                            }
                            if (string.IsNullOrEmpty(userMV.Company.PhoneNo))
                            {
                                ModelState.AddModelError("Company.PhoneNo", "*Required");
                                hasValidationErrors = true;
                            }
                            if (string.IsNullOrEmpty(userMV.Company.Description))
                            {
                                ModelState.AddModelError("Company.Description", "*Required");
                                hasValidationErrors = true;
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
                        else if(userMV.AreYouProvider != true)
                        {
                            var employee = new EmployeesTable();
                            employee.UserId = user.UserID;
                            if (string.IsNullOrEmpty(userMV.Employee.EmailAddress))
                            {
                                ModelState.AddModelError("Employee.EmailAddress", "*Required");
                                hasValidationErrors = true;
                            }
                            if (string.IsNullOrEmpty(userMV.Employee.EmployeeName))
                            {
                                ModelState.AddModelError("Employee.EmployeeName", "*Required");
                                hasValidationErrors = true;
                            }
                            if (string.IsNullOrEmpty(userMV.Employee.Gender))
                            {
                                ModelState.AddModelError("Employee.Gender", "*Required");
                                hasValidationErrors = true;
                            }
                            employee.EmailAddress = userMV.Employee.EmailAddress;
                            employee.EmployeeName = userMV.Employee.EmployeeName;
                            employee.Gender = userMV.Employee.Gender;
                            employee.Photo = "~/Content/assests/img/adapt_icon/3.png";
                            db.EmployeesTables.Add(employee);
                            db.SaveChanges();
                        }
                        else
                        {
                            transact.Rollback();
                        }
                        transact.Commit();
                        log.Info($"New user with username {user.UserName} created successfully.");
                        return RedirectToAction("Login");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "Please provide correct details!");
                        log.Error("Error while creating new user.", ex);
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
                    log.Warn($"Login failed for username {userLoginMV.UserName}. Incorrect username or password.");
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
                    var employeeTableEntry = user.EmployeesTables.FirstOrDefault();
                    if (employeeTableEntry != null)
                    {
                        Session["EmployeeID"] = employeeTableEntry.EmployeeID;
                    }
                }
                log.Info($"User with username {user.UserName} logged in successfully.");
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
            log.Info($"User logged out.");
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
                    log.Info($"Password recovery email sent to {forgotPasswordMV.Email}.");
                }
                else
                {
                    ModelState.AddModelError("Email", "Your Email is Registered! Current email sending is not working properly, please try again after some time ");
                    log.Warn($"Failed to send password recovery email to {forgotPasswordMV.Email}.");
                }
            }
            else
            {
                ModelState.AddModelError("Email", "Email is not registered");
                log.Warn($"Email is not registered. Email typed: {forgotPasswordMV.Email}.");
            }
            return View(forgotPasswordMV);
        }

        public ActionResult DeleteUser(int? id)
        {
            // Check if the user is logged in and has admin rights. 
            // For this example, I assume an admin has a UserTypeID different from 2 and 3. 
            // Adjust this as per your application logic.
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])) ||
                (Convert.ToInt32(Session["UserTypeID"]) == 2) ||
                (Convert.ToInt32(Session["UserTypeID"]) == 3))
            {
                return RedirectToAction("Login", "User");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Fetch the user
            var user = db.UserTables.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            using (var transact = db.Database.BeginTransaction())
            {
                try
                {
                    // If user is a provider, delete the associated company
                    if (user.UserTypeID == 2 && user.CompanyTables.Any())
                    {
                        var company = user.CompanyTables.FirstOrDefault();
                        db.CompanyTables.Remove(company);
                    }
                    // If user is an employee, delete the associated employee record
                    else if (user.UserTypeID == 3 && user.EmployeesTables.Any())
                    {
                        var employee = user.EmployeesTables.FirstOrDefault();
                        db.EmployeesTables.Remove(employee);
                    }

                    // Delete the user
                    db.UserTables.Remove(user);
                    db.SaveChanges();

                    transact.Commit();

                    log.Info($"DeleteUser - Successfully deleted user with ID: {id}");
                }
                catch (Exception ex)
                {
                    log.Error($"Error deleting user with ID: {id}.", ex);
                    transact.Rollback();
                }
            }

            return RedirectToAction("AllUsers");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}