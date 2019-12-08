using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Patient
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int? InsuranceNumber { get; set; }
        public DateTime? dateOfBirth { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Patient patient &&
                   FirstName == patient.FirstName &&
                   LastName == patient.LastName &&
                   Address == patient.Address &&
                   InsuranceNumber == patient.InsuranceNumber &&
                   dateOfBirth == patient.dateOfBirth;
        }
    }
}
