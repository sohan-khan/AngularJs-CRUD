using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSEvidanceFinal.Models
{
    public class DepartmentVM
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public virtual IList<Employee> Employees { get; set; }
    }
}