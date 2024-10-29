using System;
namespace DentistrySystem
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<MedicalCard> MedicalRecords { get; set; } = new List<MedicalCard>();

        public void CancelAppointment(Appointment appointment)
        {
            appointment.Status = AppointmentStatus.Cancelled;
            Console.WriteLine($"Appointment for {Name} on {appointment.Date} was cancelled.");
        }
    }
}

