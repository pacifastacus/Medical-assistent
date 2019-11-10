using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Patient : IComparable<Patient>
    {
        public Int32 InsuranceNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Symptoms { get; set; }
        public DateTime RecordingDate { get; set; }

        public override string ToString()
        {
            return "Name: " + Name + " Address: " + Address;
        }

        public override bool Equals(object obj)
        {
            if (obj is Patient)
            {
                return this.Equals((Patient)obj);
            }
            else
            {
                return base.Equals(obj);
            }
        }
        public bool Equals(Patient other)
        {
            if(this.RecordingDate.Equals(other.RecordingDate) &&
                this.InsuranceNumber.Equals(other.InsuranceNumber) &&
                this.Name.Equals(other.Name) &&
                this.Address.Equals(other.Address) &&
                this.Symptoms.Equals(other.Symptoms)
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int CompareTo(Patient other)
        {
            bool isEquals = true;
            foreach(var property in GetType().GetProperties())
            {
                var objValue = property.GetValue(this);
                var otherValue = property.GetValue(other);
                if (!objValue.Equals(otherValue))
                {
                    isEquals = false;
                    break;
                }
                    
            }
            return Symptoms.CompareTo(other.Symptoms);
        }
    }
}
