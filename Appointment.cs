//Observer
using System;
namespace DentistrySystem
{
    public interface IObserver
    {
        void Update(Appointment appointment);
    }

    public class Appointment
    {
        private List<IObserver> observers = new List<IObserver>();

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime Date { get; set; }
        public AppointmentStatus Status { get; set; }
        public List<Procedure> Procedures { get; set; } = new List<Procedure>();
        public decimal TotalCost { get; set; }

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update(this);
            }
        }

        public void ChangeAppointmentStatus(AppointmentStatus status)
        {
            this.Status = status;
            NotifyObservers();
        }

        public static Appointment CreateAppointment(Patient patient, Doctor doctor, DateTime date)
        {
            return new Appointment
            {
                Patient = patient,
                Doctor = doctor,
                Date = date,
                Status = AppointmentStatus.Confirmed
            };
        }

        public void AddProcedures(List<Procedure> procedures)
        {
            Procedures.AddRange(procedures);
            CalculateTotalCost();
        }

        public decimal CalculateTotalCost()
        {
            decimal total = 0;
            foreach (var procedure in Procedures)
            {
                total += procedure.Price;
            }
            TotalCost = total;
            return TotalCost;
        }
    }
}


