using Assignment_2_10_08_2025.Models;
using System.Collections.Generic;

namespace Assignment_2_10_08_2025.Repo
{
    public interface IEmployeeRepository
    {
        Employee? GetById(int id);
        List<Employee> GetAll();
        List<Employee> GetByDepartment(string department);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(int id);
        void UpdateEmail(int id, string email);
        bool Exists(int id);
    }
}
