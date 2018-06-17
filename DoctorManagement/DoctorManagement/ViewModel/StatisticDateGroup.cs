using System;
using System.ComponentModel.DataAnnotations;

namespace DoctorManagement.ViewModel
{
    public class StatisticDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
        public int PatientCount { get; set; }
    }
}