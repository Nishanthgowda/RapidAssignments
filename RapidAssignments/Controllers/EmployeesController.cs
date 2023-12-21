using RapidAssignments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RapidAssignments.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
       
        public async Task<ActionResult> Index()
        {            
            var finalData = await EmployeeData.GetFinalData();
            return View(finalData);
        }
    }
}