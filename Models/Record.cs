using System;

namespace Models
{
    public class Record : IComparable<Record>
    {
        public int ID { get; set; }
        public Int32 InsuranceNumber { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Symptomes { get; set; }
        public string Diagnosis { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime Modified { get; set; }

        public override string ToString()
        {
            return "Name: " + Name + " Address: " + Address;
        }

        public override bool Equals(object obj)
        {
            if (obj is Record)
            {
                return this.Equals((Record)obj);
            }
            else
            {
                return base.Equals(obj);
            }
        }
        public bool Equals(Record other)
        {
            if (this.Modified.Equals(other.Modified) &&
                this.InsuranceNumber.Equals(other.InsuranceNumber) &&
                this.Name.Equals(other.Name) &&
                this.Address.Equals(other.Address) &&
                this.Symptomes.Equals(other.Symptomes)
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int CompareTo(Record other)
        {
            foreach (var property in GetType().GetProperties())
            {
                var objValue = property.GetValue(this);
                var otherValue = property.GetValue(other);
                if (!objValue.Equals(otherValue))
                {
                    break;
                }

            }
            return Symptomes.CompareTo(other.Symptomes);
        }
    }
}
