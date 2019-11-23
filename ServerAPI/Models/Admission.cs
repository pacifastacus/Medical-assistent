using System;
using System.Collections.Generic;
using System.Text;

namespace ServerAPI.Models
{
    public class Admission
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public DateTime TimeOfAdmission { get; set; }
        public string Symptomes { get; set; }
        public string Diagnosis{get;set;}

    }
}
