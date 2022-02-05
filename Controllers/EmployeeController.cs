using Microsoft.AspNetCore.Mvc;
using employee.Models;

namespace employee.Controllers
{
    public class EmployeeController : Controller
    {
        HRDatabaseContext dbContext = new HRDatabaseContext();   
        public IActionResult Index()
        {
            var employees = GetEmployees();
           
            return employees;
        }

        private IActionResult GetEmployees()
        {
            var employees = (from employee in dbContext.Employees
                             join department in dbContext.Department on employee.DepartmentId equals department.DepartmentId
                             select new Employee
                             {
                                 EmployeeId = employee.EmployeeId,
                                 EmployeeName = employee.EmployeeName,
                                 EmployeeNumber = employee.EmployeeNumber,
                                 DDB = employee.DDB,
                                 HiraingDate = employee.HiraingDate,
                                 GrossSalary = employee.GrossSalary,
                                 NetSalary = employee.NetSalary,
                                 DepartmentName = department.DepartmentName


                             }).ToList();
            //List<Employee> employees = dbContext.Employees.ToList();
            return View(employees);
        }

        public IActionResult Create()
        {
            ViewBag.Department = this.dbContext.Department.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee model)
        {
            ModelState.Remove("EmployeeId");
            ModelState.Remove("DepartmentName");
            ModelState.Remove("Department");
            if (ModelState.IsValid)
            {
                dbContext.Employees.Add(model);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Department = this.dbContext.Department.ToList();
            return View();
        }
        public IActionResult Edit(int ID)
        {
            Employee data = this.dbContext.Employees.Where(e=> e.EmployeeId == ID).FirstOrDefault(); 
            ViewBag.Department = this.dbContext.Department.ToList();
            return View("Create",data);
        }
        [HttpPost]
        public IActionResult Edit(Employee model)
        {

            ModelState.Remove("EmployeeId");
            ModelState.Remove("DepartmentName");
            ModelState.Remove("Department");
            if (ModelState.IsValid)
            {
                dbContext.Employees.Update(model);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Department = this.dbContext.Department.ToList();
            return View("Create", model);
        }
        [HttpGet]
        public IActionResult Delete(int ID)
        {

            Employee data = this.dbContext.Employees.Where(e => e.EmployeeId == ID).FirstOrDefault();
            if(data != null)
            {
                dbContext.Employees.Remove(data);
                dbContext.SaveChanges();

            }
            return RedirectToAction("Index");
        }
    }
}
