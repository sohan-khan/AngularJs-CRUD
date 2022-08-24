using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngularJSEvidanceFinal.Models;
namespace AngularJSEvidanceFinal.Controllers
{
    public class HomeController : Controller
    {
        EmployeeDbContext db = new EmployeeDbContext();
        public ActionResult Index()
        {
            return View();
        }

        //Back End Code Start


        // For Get Employees
        public JsonResult GetAllEmployees()
        {
            var employeeList = new List<EmployeeVM>();
            using (EmployeeDbContext db = new EmployeeDbContext())
            {
                employeeList = (from e in db.Employees
                                join d in db.Departments
                                on e.DepartmentId equals d.DepartmentId
                                where e.DepartmentId == d.DepartmentId
                                select new EmployeeVM()
                                {
                                    EmployeeId = e.EmployeeId,
                                    EmployeeName = e.EmployeeName,
                                    EmployeeAge = e.EmployeeAge,
                                    Gender = e.Gender,
                                    JoinDate = e.JoinDate,
                                    DepartmentId = e.DepartmentId,
                                    DepartmentName = d.DepartmentName

                                }).ToList();
            }
            return Json(employeeList, JsonRequestBehavior.AllowGet);
        }


        // For Get Departments

        public JsonResult GetDepartments()
        {
            var departmentList = new List<DepartmentVM>();
            using (EmployeeDbContext db = new EmployeeDbContext())
            {
                departmentList = (from x in db.Departments
                                  select new DepartmentVM
                                  {
                                      DepartmentId = x.DepartmentId,
                                      DepartmentName = x.DepartmentName
                                  }).ToList();
            }

            return Json(departmentList, JsonRequestBehavior.AllowGet);
             
        }


        // Add or Update Employee

        public string SaveEmp(Employee employee)
        {
            string message = "";
            if (employee !=null)
            {
                using (EmployeeDbContext db = new EmployeeDbContext())
                {
                    var oldEmployee = db.Employees.Where(x => x.EmployeeId == employee.EmployeeId).FirstOrDefault();

                    if (oldEmployee == null)
                    {
                        db.Employees.Add(employee);
                        message = "Data Save Successfully!!";
                    }
                    else
                    {
                        oldEmployee.DepartmentId = employee.DepartmentId;
                        oldEmployee.EmployeeName = employee.EmployeeName;
                        oldEmployee.EmployeeAge = employee.EmployeeAge;
                        oldEmployee.Gender = employee.Gender;
                        oldEmployee.JoinDate = employee.JoinDate;
                        oldEmployee.PicturePath = employee.PicturePath;
                        message = "Data Updated Successfully";
                    }

                    db.SaveChanges();
                }
            }
            else
            {
                message = "Data Not Valid!!";
            }
            return message;
        }

        // Delete
        public string DeleteEmp(int id)
        {
            string massage = "";
            using (EmployeeDbContext db = new EmployeeDbContext())
            {
                var oldEmployee = db.Employees.Find(id);
                if (oldEmployee != null)
                {
                    db.Employees.Remove(oldEmployee); 

                    db.SaveChanges();
                    massage = "Employee Deleted Succesfully!!";
                }
                else
                {
                    massage = "Data Not Found";
                }
            }
            return massage;
        }












        //Back End Code End











        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}