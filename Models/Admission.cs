using System;

namespace Models
{
    public class Admission
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public DateTime TimeOfAdmission { get; set; }
        public string Symptomes { get; set; }
        public string Diagnosis { get; set; }

    }
}
