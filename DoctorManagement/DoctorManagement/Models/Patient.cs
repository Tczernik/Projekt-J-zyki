//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoctorManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Patient
    {
        public int ID { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string SSN { get; set; }
        public System.DateTime Date { get; set; }
        public string BloodGroup { get; set; }
        public Nullable<int> ID_Doctor { get; set; }
        public Nullable<int> ID_Blood { get; set; }
        public Nullable<int> ID_Hospital { get; set; }
    
        public virtual Doctor Doctor { get; set; }
    }
}