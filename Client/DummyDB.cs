using Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class DummyDB
    {
        public ObservableCollection<Patient> Patients { get; }

        public DummyDB()
        {
            Patients = new ObservableCollection<Patient>
            {
                new Patient
                {
                    Name = "John Doe",
                    Address = "1234 BigCity, Unnamed road 13.",
                    InsuranceNumber = 123456789,
                    Symptoms = "Derék fájás",
                    RecordingDate = DateTime.Parse("2019-06-08, 08:00")
                },
                new Patient
                {
                     Name = "Big Lebowsky",
                    Address = "4822 Los Angeles, Chasing street 5.",
                    InsuranceNumber = 999999999,
                    Symptoms = "Mindenhol fáj",
                    RecordingDate = DateTime.Parse("2019-10-08, 08:00")
                },
                new Patient
                {
                    Name = "Biggus Dickus",
                    Address = "0000 Rome, foobar út 1.",
                    InsuranceNumber = 777666555,
                    Symptoms = "Pöszeség",
                    RecordingDate = DateTime.Parse("2019-06-08, 08:00")
                }
            };
        }
    }
}
