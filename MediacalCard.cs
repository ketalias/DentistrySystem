using System;
namespace DentistrySystem
{
    public class MedicalCard
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

