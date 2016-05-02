using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EmployeeService.Models;

namespace EmployeeService.Controllers
{
    public class EmployeesController : ApiController
    {
        private EmployeeServiceContext db = new EmployeeServiceContext();

        // GET: api/Employees
        public IQueryable<EmployeeDTO> GetEmployees()
        {
            var employees = from e in db.Employee
                            select new EmployeeDTO()
                            {
                                Id = e.Id,
                                lName = e.lName,
                                fName = e.fName,
                                Title = e.Title,
                                Address = e.Address,
                                City = e.City,
                                Region = e.Region,
                                PostalCode = e.PostalCode,
                                Country = e.Country,
                                Ext = e.ext,
                                Salary = e.Salary,
                                Dept = e.dept,
                                Super = e.Super,
                                Tenure = e.Tenure
                            };

            return employees;
        }

        // GET: api/Employees/5
        [ResponseType(typeof(EmployeeDetailDTO))]
        public async Task<IHttpActionResult> GetEmployee(int id)
        {
            var employee = await db.Employee.Include(e => e.Employees).Select(e =>
            new EmployeeDetailDTO()
            {
                EmID = e.Id,
                lName = e.lName,
                fName = e.fName,
                Title = e.Title,
                Address = e.Address,
                City = e.City,
                Region = e.Region,
                PostalCode = e.PostalCode,
                Country = e.Country,
                Ext = e.ext,
                Salary = e.Salary,
                Dept = e.dept,
                Super = e.Super,
                Tenure = e.Tenure
            }).SingleOrDefaultAsync(e => e.EmID == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmployee(int id, EmployeeUpdateDTO employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedUser = db.Employee.SingleOrDefault(x => x.Id == id);

            updatedUser.fName = employee.fName;
            updatedUser.lName = employee.lName;
            updatedUser.Title = employee.Title;
            updatedUser.Address = employee.Address;
            updatedUser.City = employee.City;
            updatedUser.Region = employee.Region;
            updatedUser.PostalCode = employee.PostalCode;
            updatedUser.Country = employee.Country;
            updatedUser.ext = employee.Ext;
            updatedUser.Salary = employee.Salary;
            updatedUser.dept = employee.Dept;
            updatedUser.Super = employee.Super;
            updatedUser.Tenure = employee.Tenure;

            /***
            var existingEmployee = await db.Employee.Select(e =>
           new EmployeeDetailDTO()
           {
               EmID = e.Id,
               lName = e.lName,
               fName = e.fName,
               Title = e.Title,
               Address = e.Address,
               City = e.City,
               Region = e.Region,
               PostalCode = e.PostalCode,
               Country = e.Country,
               Ext = e.ext,
               Salary = e.Salary,
               Dept = e.dept,
               Super = e.Super,
               Tenure = e.Tenure
           }).SingleOrDefaultAsync(e => e.EmID == id);
           

            employee.Id = existingEmployee.EmID;

            if (employee.lName == null)
                employee.lName = existingEmployee.lName;
            if (employee.fName == null)
                employee.fName = existingEmployee.fName;
            if (employee.Title == null)
                employee.Title = existingEmployee.Title;
            if (employee.Address == null)
                employee.Address = existingEmployee.Address;
            if (employee.City == null)
                employee.City = existingEmployee.City;
            if (employee.Region == null)
                employee.Region = existingEmployee.Region;
            if (employee.PostalCode == null)
                employee.PostalCode = existingEmployee.PostalCode;
            if (employee.Country == null)
                employee.Country = existingEmployee.Country;
            if (employee.Ext == null)
                employee.Ext = existingEmployee.Ext;
            if (employee.Salary == null)
                employee.Salary = existingEmployee.Salary;
            if (employee.Dept == null)
                employee.Dept = existingEmployee.Dept;
            if (employee.Super == null)
                employee.Super = existingEmployee.Super;
            if (employee.Tenure == null)
                employee.Tenure = existingEmployee.Tenure;

            var upEmp = new EmployeeUpdateDTO()
            {
                Id = id,
                lName = employee.lName,
                fName = employee.fName,
                Title = employee.Title,
                Address = employee.Address,
                City = employee.City,
                Region = employee.Region,
                PostalCode = employee.PostalCode,
                Country = employee.Country,
                Ext = employee.Ext,
                Salary = employee.Salary,
                Dept = employee.Dept,
                Super = employee.Super,
                Tenure = employee.Tenure
            };
            ***/

            db.Entry(updatedUser).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees
        [ResponseType(typeof(edata))]
        public async Task<IHttpActionResult> PostEmployee(edata employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employee.Add(employee);
            await db.SaveChangesAsync();

            var dto = new EmployeeAddDTO()
            {
                lName = employee.lName,
                fName = employee.fName,
                Title = employee.Title,
                Address = employee.Address,
                City = employee.City,
                Region = employee.Region,
                PostalCode = employee.PostalCode,
                Country = employee.Country,
                Ext = employee.ext,
                Salary = employee.Salary,
                Dept = employee.dept,
                Super = employee.Super,
                Tenure = employee.Tenure
            };

            return CreatedAtRoute("DefaultApi", new { id = employee.Id }, dto);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(edata))]
        public async Task<IHttpActionResult> DeleteEmployee(int id)
        {
            edata employee = await db.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employee.Remove(employee);
            await db.SaveChangesAsync();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employee.Count(e => e.Id == id) > 0;
        }
    }
}