using System;
namespace DentistrySystem
{
    public class AppointmentStatus
    {
        public string Name { get; private set; }

        public static AppointmentStatus Confirmed = new AppointmentStatus("Confirmed");
        public static AppointmentStatus Cancelled = new AppointmentStatus("Cancelled");
        public static AppointmentStatus Completed = new AppointmentStatus("Completed");

        private AppointmentStatus(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

}

