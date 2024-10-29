using System;
using System.Collections.Generic;

namespace DentistrySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var appointmentService = new AppointmentChanger();
            var reportService = new Report(null);
            var reminderService = new Reminder();

            var patients = new List<Patient>();
            var doctors = new List<Doctor>
            {
                new Doctor { Name = "Dr. Ксенія" },
                new Doctor { Name = "Dr. Джонсон" },
                new Doctor { Name = "Dr. Ахмед" }
            };

            var appointments = new List<Appointment>();
            Patient selectedPatient = null;

            while (true)
            {
                Console.WriteLine("\nОберіть вашу роль:");
                Console.WriteLine("1. Пацієнт");
                Console.WriteLine("2. Лікар");
                Console.WriteLine("0. Вихід");

                var roleChoice = Console.ReadLine();

                if (roleChoice == "0") break;

                switch (roleChoice)
                {
                    case "1":
                        while (true)
                        {
                            Console.WriteLine("\nМеню пацієнта:");
                            Console.WriteLine("1. Створити нового пацієнта");
                            Console.WriteLine("2. Вибрати пацієнта");
                            Console.WriteLine("3. Записатися на прийом");
                            Console.WriteLine("4. Переглянути медичну книжку пацієнта");
                            Console.WriteLine("5. Додати медичну картку");
                            Console.WriteLine("6. Скасувати запис");
                            Console.WriteLine("F. Повернутися до вибору ролі");

                            var patientChoice = Console.ReadLine();

                            if (patientChoice.ToUpper() == "F") break;

                            if (patientChoice == "1")
                            {
                                Console.WriteLine("Введіть ім'я пацієнта: ");
                                var patientName = Console.ReadLine();
                                var newPatient = new Patient { Name = patientName };
                                patients.Add(newPatient);
                                Console.WriteLine($"Пацієнта {newPatient.Name} успішно створено.");
                            }
                            else if (patientChoice == "2")
                            {
                                Console.WriteLine("Оберіть пацієнта зі списку:");
                                for (int i = 0; i < patients.Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {patients[i].Name}");
                                }
                                Console.WriteLine("Введіть номер пацієнта: ");
                                int patientIndex = int.Parse(Console.ReadLine()) - 1;

                                if (patientIndex < 0 || patientIndex >= patients.Count)
                                {
                                    Console.WriteLine("Неправильний вибір пацієнта.");
                                    continue; 
                                }

                                selectedPatient = patients[patientIndex];
                                Console.WriteLine($"Вибрано пацієнта: {selectedPatient.Name}");
                            }
                            else if (patientChoice == "3" && selectedPatient != null)
                            {
                                Console.WriteLine("Оберіть лікаря зі списку:");
                                for (int i = 0; i < doctors.Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {doctors[i].Name}");
                                }
                                Console.WriteLine("Введіть номер лікаря: ");
                                int doctorIndex = int.Parse(Console.ReadLine()) - 1;

                                if (doctorIndex < 0 || doctorIndex >= doctors.Count)
                                {
                                    Console.WriteLine("Неправильний вибір лікаря.");
                                    continue; 
                                }

                                var doctor = doctors[doctorIndex];

                                Console.WriteLine("Введіть дату прийому (рррр-мм-дд): ");
                                DateTime date = DateTime.Parse(Console.ReadLine());

                                var appointment = appointmentService.BookAppointment(selectedPatient, doctor, date);
                                appointments.Add(appointment);
                                appointment.AddObserver(reminderService);

                                Console.WriteLine($"Запис створено для {selectedPatient.Name} з {doctor.Name} на {date}.");
                                appointment.NotifyObservers();
                            }
                            else if (patientChoice == "4" && selectedPatient != null)
                            {
                                Console.WriteLine($"Медична книжка пацієнта {selectedPatient.Name}:");
                                foreach (var record in selectedPatient.MedicalRecords)
                                {
                                    Console.WriteLine($"ID: {record.Id}, Діагноз: {record.Diagnosis}, Лікування: {record.Treatment}, Дата: {record.DateCreated}");
                                }
                            }
                            else if (patientChoice == "5" && selectedPatient != null)
                            {
                                var medicalCard = new MedicalCard
                                {
                                    Id = selectedPatient.MedicalRecords.Count + 1,
                                    Patient = selectedPatient,
                                    DateCreated = DateTime.Now
                                };

                                Console.WriteLine("Введіть діагноз: ");
                                medicalCard.Diagnosis = Console.ReadLine();
                                Console.WriteLine("Введіть лікування: ");
                                medicalCard.Treatment = Console.ReadLine();

                                selectedPatient.MedicalRecords.Add(medicalCard);
                                Console.WriteLine($"Медична картка для {selectedPatient.Name} успішно додана.");
                            }
                            else if (patientChoice == "6")
                            {
                                Console.WriteLine("Введіть індекс запису для скасування: ");
                                int index = int.Parse(Console.ReadLine());

                                if (index >= 0 && index < appointments.Count)
                                {
                                    var appointment = appointments[index];
                                    appointmentService.CancelAppointment(appointment);
                                }
                                else
                                {
                                    Console.WriteLine("Неправильний індекс запису.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Спочатку виберіть пацієнта для запису на прийом.");
                            }
                        }
                        break;

                    case "2":
                        while (true)
                        {
                            Console.WriteLine("\nМеню лікаря:");
                            Console.WriteLine("1. Завершити прийом");
                            Console.WriteLine("2. Згенерувати звіт");
                            Console.WriteLine("3. Переглянути всі записи");
                            Console.WriteLine("F. Повернутися до вибору ролі");

                            var doctorChoice = Console.ReadLine();

                            if (doctorChoice.ToUpper() == "F") break;

                            if (doctorChoice == "1")
                            {
                                Console.WriteLine("Введіть індекс запису для завершення: ");
                                int index = int.Parse(Console.ReadLine());

                                if (index >= 0 && index < appointments.Count)
                                {
                                    var appointment = appointments[index];
                                    appointmentService.CompleteAppointment(appointment);
                                }
                                else
                                {
                                    Console.WriteLine("Неправильний індекс запису.");
                                }
                            }
                            else if (doctorChoice == "2")
                            {
                                Console.WriteLine("Оберіть формат звіту:");
                                Console.WriteLine("1. PDF");
                                Console.WriteLine("2. XML");
                                var formatChoice = Console.ReadLine();

                                if (formatChoice == "1")
                                {
                                    reportService = new Report(new PdfReportStrategy());
                                }
                                else if (formatChoice == "2")
                                {
                                    reportService = new Report(new ExcelReportStrategy()); 
                                }
                                else
                                {
                                    Console.WriteLine("Неправильний вибір формату.");
                                    continue; 
                                }

                                reportService.GenerateReport(appointments);
                            }
                            else if (doctorChoice == "3")
                            {
                                Console.WriteLine("Список всіх записів:");
                                for (int i = 0; i < appointments.Count; i++)
                                {
                                    var appointment = appointments[i];
                                    Console.WriteLine($"{i}. {appointment.Patient.Name} з {appointment.Doctor.Name} на {appointment.Date}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Неправильний вибір. Спробуйте ще раз.");
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Неправильний вибір. Спробуйте ще раз.");
                        break;
                }
            }
        }
    }
}
