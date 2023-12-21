using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RapidAssignments.Models
{
    // Created this model to class to tansfer the data between view and controller
    public class EmployeeNameHour
    {
        public string EmployeeName { get; set; }
        public int TotalHours { get; set; }
    }
}