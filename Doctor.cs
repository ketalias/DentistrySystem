using System;
namespace DentistrySystem
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }

        public MedicalCard CreateMedicalCard(Patient patient, string diagnosis, string treatment)
        {
            var card = new MedicalCard
            {
                Patient = patient,
                Diagnosis = diagnosis,
                Treatment = treatment,
                DateCreated = DateTime.Now
            };
            patient.MedicalRecords.Add(card);
            return card;
        }

        public void RecordProcedure(Appointment appointment, List<Procedure> procedures)
        {
            appointment.Procedures.AddRange(procedures);
            appointment.TotalCost = appointment.CalculateTotalCost();
            Console.WriteLine($"Procedures for appointment on {appointment.Date} have been recorded.");
        }
    }

}

