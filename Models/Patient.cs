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
        public int InsuranceNumber { get; set; }
        public DateTime dateOfBirth { get; set; }

    }
}
