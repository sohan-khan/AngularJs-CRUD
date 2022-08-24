using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AngularJSEvidanceFinal.Models
{
    public class EmployeeVM
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int? DepartmentId { get; set; }
        public int EmployeeAge { get; set; }
        public DateTime JoinDate { get; set; }
        public string Gender { get; set; }
        public string PicturePath { get; set; }
        public string DepartmentName { get; set; }
        public virtual DepartmentVM Department { get; set; }
    }
}