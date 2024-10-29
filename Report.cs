//Stratagy pattern
using System;
namespace DentistrySystem
{

    public class Report
    {
        private IReportStrategy reportStrategy;

        public Report(IReportStrategy reportStrategy)
        {
            this.reportStrategy = reportStrategy; 
        }

        public void GenerateReport(List<Appointment> appointments)
        {
            reportStrategy.GenerateReport(appointments);
        }
    }

    public interface IReportStrategy
    {
        void GenerateReport(List<Appointment> appointments);
    }

    public class PdfReportStrategy : IReportStrategy
    {
        public void GenerateReport(List<Appointment> appointments)
        {
            Console.WriteLine("PDF звіт згенеровано з " + appointments.Count + " записами.");
        }
    }

    public class ExcelReportStrategy : IReportStrategy
    {
        public void GenerateReport(List<Appointment> appointments)
        {
            Console.WriteLine("Excel звіт згенеровано з " + appointments.Count + " записами.");
        }
    }


}

