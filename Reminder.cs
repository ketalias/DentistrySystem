//Singleton
//Observer
using System;
namespace DentistrySystem
{
    public class Reminder : IObserver
    {
        public static Reminder instance;

        public Reminder() { }

        public static Reminder Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Reminder();
                }
                return instance;
            }
        }

        public void Update(Appointment appointment)
        {
            switch (appointment.Status)
            {
                case var status when status == AppointmentStatus.Confirmed:
                    Console.WriteLine($"Нагадування: Прийом для {appointment.Patient.Name} заплановано на {appointment.Date}.");
                    break;
                case var status when status == AppointmentStatus.Cancelled:
                    Console.WriteLine($"Нагадування: Прийом для {appointment.Patient.Name} скасовано.");
                    break;
                case var status when status == AppointmentStatus.Completed:
                    Console.WriteLine($"Нагадування: Прийом для {appointment.Patient.Name} завершено.");
                    break;
                default:
                    Console.WriteLine("Статус прийому невідомий.");
                    break;
            }
        }

        public void SendReminder(Patient patient, Appointment appointment)
        {
            if (appointment != null && patient != null)
            {
                Console.WriteLine($"Нагадування: Шановний(а) {patient.Name}, у вас заплановано прийом з лікарем Dr. {appointment.Doctor.Name} на {appointment.Date}. Будь ласка, приходьте за 10 хвилин до початку.");
            }
            else
            {
                Console.WriteLine("Помилка: Неправильні дані пацієнта або прийому.");
            }
        }


    }
}

