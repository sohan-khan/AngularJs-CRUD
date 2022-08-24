using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngularJSEvidanceFinal.Models;
namespace AngularJSEvidanceFinal.Controllers
{
    public class AboutController : Controller
    {
        EmployeeDbContext db = new EmployeeDbContext();
        public ActionResult Index()
        {
            return View();
        }

        // GET Employees

        public JsonResult GetEmployees()
        {
            var employeeList = new List<EmployeeVM>();
            using(EmployeeDbContext DB = new EmployeeDbContext())
            {
                employeeList = (from e in db.Employees
                                join d in db.Departments
                                on e.DepartmentId equals d.DepartmentId
                                where e.DepartmentId == d.DepartmentId
                                select new EmployeeVM()
                                {
                                    EmployeeId = e.EmployeeId,
                                    EmployeeName = e.EmployeeName,
                                    DepartmentId = e.DepartmentId,
                                    DepartmentName = d.DepartmentName,
                                    Gender = e.Gender,
                                    EmployeeAge = e.EmployeeAge,
                                    JoinDate = e.JoinDate
                                }).ToList();
            }
            return Json(employeeList, JsonRequestBehavior.AllowGet);
        }


        // Get Department

        public JsonResult GetDepartments()
        {
            var departmentList = new List<DepartmentVM>();
            using (EmployeeDbContext db = new EmployeeDbContext())
            {
                departmentList = (from d in db.Departments
                                  select new DepartmentVM()
                                  {
                                      DepartmentId = d.DepartmentId,
                                      DepartmentName = d.DepartmentName
                                  }).ToList();
            }
            return Json(departmentList, JsonRequestBehavior.AllowGet);
        }


       

        // For Add or Update

        public string SaveEmp(Employee employee)
        {
            string message = "";
            if (employee != null)
            {
                using(EmployeeDbContext db = new EmployeeDbContext())
                {
                    var oldEmployee = db.Employees.Where(x => x.EmployeeId == employee.EmployeeId).FirstOrDefault();
                    if (oldEmployee == null)
                    {
                        db.Employees.Add(employee);
                        message = "Data Saved Successfully!!";
                    }
                    else
                    {
                        oldEmployee.EmployeeName = employee.EmployeeName;
                        oldEmployee.DepartmentId = employee.DepartmentId;
                        oldEmployee.Gender = employee.Gender;
                        oldEmployee.EmployeeAge = employee.EmployeeAge;
                        oldEmployee.JoinDate = employee.JoinDate;
                        oldEmployee.PicturePath = employee.PicturePath;
                        message = "Data Updated Successfully!!";
                    }
                    db.SaveChanges();
                }
            }
            else
            {
                message = "Not Valid Data";
            }
            return message;
        }

        // For Delete Employee

        public string DeleteEmp( int id)
        {
            string message = "";
            using(EmployeeDbContext db = new EmployeeDbContext())
            {
                var oldEmployee = db.Employees.Find(id);
                if (oldEmployee !=null)
                {
                    db.Employees.Remove(oldEmployee);
                    db.SaveChanges();
                    message = "Data Deleted Successfully!!";
                }
                else
                {
                    message = "Data Not found";
                }
            }
            return message;
        }



    }
}