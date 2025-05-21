using assignment_1.Models;

namespace assignment_1.Interfaces
{
    public interface IEmployeeService
    {
        int AddEmployee(Employee employee);
        List<Employee>? SearchEmployee(SearchModel searchModel);

        List<Employee>? GetAllEmployees();
    }
}