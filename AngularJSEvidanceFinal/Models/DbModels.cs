using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AngularJSEvidanceFinal.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public int EmployeeAge { get; set; }
        public DateTime JoinDate { get; set; }
        public string Gender { get; set; }
        public string PicturePath { get; set; }
        public virtual Department Department { get; set; }

    }

    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public virtual IList<Employee> Employees { get; set; }
    }


    public class EmployeeDbContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }

}