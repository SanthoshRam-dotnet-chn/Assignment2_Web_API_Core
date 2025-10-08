using Assignment_2_10_08_2025.Models;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_2_10_08_2025.Repo
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employees = new List<Employee>();
        public Employee? GetById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }
        public List<Employee> GetAll()
        {
            return _employees;
        }
        public List<Employee> GetByDepartment(string department)
        {
            return _employees.Where(e => e.Department == department).ToList();
        }
        public void Add(Employee employee)
        {
            _employees.Add(employee);
        }
        public void Update(Employee employee)
        {
            var existingEmployee = GetById(employee.Id);
            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.Department = employee.Department;
                existingEmployee.MobileNo = employee.MobileNo;
                existingEmployee.Email = employee.Email;
            }
        }
        public void Delete(int id)
        {
            var employee = GetById(id);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
        }
        public void UpdateEmail(int id, string email)
        {
            var employee = GetById(id);
            if (employee != null)
            {
                employee.Email = email;
            }
        }
        public bool Exists(int id)
        {
            return _employees.Any(e => e.Id == id);
        }
    }
}
