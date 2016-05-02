using System.Collections.Generic;

namespace EmployeeService.Models
{
    public class edata
    {
        public int Id
        {
            get; set;
        }
        public string lName
        {
            get; set;
        }
        public string fName
        {
            get; set;
        }
        public string Title
        {
            get; set;
        }
        public string Address
        {
            get; set;
        }
        public string City
        {
            get; set;
        }
        public string Region
        {
            get; set;
        }
        public string PostalCode
        {
            get; set;
        }
        public string Country
        {
            get; set;
        }
        public string ext
        {
            get; set;
        }
        public string Salary
        {
            get; set;
        }
        public string dept
        {
            get; set;
        }
        public string Super
        {
            get; set;
        }
        public string Tenure
        {
            get; set;
        }
        public virtual ICollection<edata> Employees
        {
            get; set;
        }
    }
}