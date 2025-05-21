using assignment_2.Exceptions;
using assignment_2.Models;

namespace assignment_2.Repositories
{
    public class AppointmentRepository : AbstractRepository<int, Appointment>
    {
        public AppointmentRepository() : base() { }

        public override ICollection<Appointment> GetAll()
        {
            if (_items.Count == 0)
            {
                throw new CollectionEmptyException("No Appointments found");
            }
            return _items;
        }

        public override Appointment GetById(int id)
        {
            var employee = _items.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                throw new KeyNotFoundException("Appointment not found");
            }
            return employee;
        }
    }
}
