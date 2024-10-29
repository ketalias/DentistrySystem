using System;
using System.Collections.Generic;

namespace DentistrySystem
{
    public class AppointmentChanger
    {
        public Appointment BookAppointment(Patient patient, Doctor doctor, DateTime date)
        {
            var appointment = Appointment.CreateAppointment(patient, doctor, date);
            return appointment;
        }

        public void CancelAppointment(Appointment appointment)
        {
            appointment.ChangeAppointmentStatus(AppointmentStatus.Cancelled);
        }

        public void CompleteAppointment(Appointment appointment)
        {
            appointment.ChangeAppointmentStatus(AppointmentStatus.Completed);
        }
    }
}
