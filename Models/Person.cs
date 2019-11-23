using System;

namespace Models
{
    public class Person : IComparable<Person>
    {
        public Int32 InsuranceNumber { get; internal set; }
        public string Name { get; internal set; }
        public string Address { get; internal set; }
        public string Symptoms { get; set; }
        public string Diagnosys { get; set; }
        public DateTime RecordingDate { get; internal set; }
        public DateTime Modified { get; set; }
        
        public override string ToString()
        {
            return "Name: " + Name + " Address: " + Address;
        }

        public override bool Equals(object obj)
        {
            if (obj is Person)
            {
                return this.Equals((Person)obj);
            }
            else
            {
                return base.Equals(obj);
            }
        }
        public bool Equals(Person other)
        {
            if (this.RecordingDate.Equals(other.RecordingDate) &&
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

        public int CompareTo(Person other)
        {
            bool isEquals = true;
            foreach (var property in GetType().GetProperties())
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
