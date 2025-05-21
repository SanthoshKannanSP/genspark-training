using assignment_2.Models;
using assignment_2.Repositories;
using assignment_2.Services;

AppointmentRepository appointmentRepository = new AppointmentRepository();
AppointmentService appointmentService = new AppointmentService(appointmentRepository);

string userChoice = "";
do
{
    Console.WriteLine("Appointment Portal (leave blank to exit)");
    Console.WriteLine("1. Add a new appointment");
    Console.WriteLine("2. Appointment Search");
    userChoice = Console.ReadLine().Trim();
    ExecuteUserChoice(userChoice);
} while (userChoice.Any());

void ExecuteUserChoice(string userChoice)
{
    switch (userChoice)
    {
        case "":
            Console.WriteLine("Exiting....");
            break;
        case "1":
            AddAppointment();
            break;

        case "2":
            SearchAppointments();
            break;
        default:
            Console.WriteLine("Enter a valid choice!");
            break;
    }
}

void AddAppointment() 
{
    var newAppointment = new Appointment();
    newAppointment.GetAppointmentDetailsFromUser();
    var appointmentId = appointmentService.AddAppointment(newAppointment);
    if (appointmentId == -1)
        Console.WriteLine("Error: Appointment could not be created!");
    else
        Console.WriteLine("Appointment created successfully");
    Console.WriteLine();
}

void SearchAppointments()
{
    var searchModel = new AppointmentSearchModel();
    searchModel.GetSearchParamsFromUser();
    var appointments = appointmentService.SearchAppointments(searchModel);
    if (appointments == null)
        Console.WriteLine("No matching appointments could be found");
    else
    {
        foreach (var appointment in appointments)
        {
            Console.WriteLine("\n"+appointment);
        }
    }
    Console.WriteLine();
}