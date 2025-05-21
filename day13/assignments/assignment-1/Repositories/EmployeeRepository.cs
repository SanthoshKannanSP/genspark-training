using assignment_1.Exceptions;
using assignment_1.Models;

namespace assignment_1.Repositories
{
    public class EmployeeRepository : AbstractRepository<int, Employee>
    {
        public EmployeeRepository() : base()
        {
        }
        public override ICollection<Employee> GetAll()
        {
            if (_items.Count == 0)
            {
                throw new CollectionEmptyException("No employees found");
            }
            return _items;
        }

        public override Employee GetById(int id)
        {
            var employee = _items.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                throw new KeyNotFoundException("Employee not found");
            }
            return employee;
        }

        protected override int GenerateID()
        {
            if (_items.Count == 0)
            {
                return 101;
            }
            else
            {
                return _items.Max(e => e.Id) + 1;
            }
        }
    }

}
