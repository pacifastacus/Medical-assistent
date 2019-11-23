using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DummyDB
    {
        public ObservableCollection<Record> Persons { get; }

        public DummyDB()
        {
            Persons = new ObservableCollection<Record>
            {
                new Record
                {
                    Name = "John Doe",
                    Address = "1234 BigCity, Unnamed road 13.",
                    InsuranceNumber = 123456789,
                    Symptomes = "Derék fájás",
                    //RecordingDate = DateTime.Parse("2019-06-08, 08:00"),
                    Modified = DateTime.Parse("2019-06-08, 08:00")
                },
                new Record
                {
                     Name = "Big Lebowsky",
                    Address = "4822 Los Angeles, Chasing street 5.",
                    InsuranceNumber = 999999999,
                    Symptomes = "Mindenhol fáj",
                    //RecordingDate = DateTime.Parse("2019-10-08, 08:00"),
                    Modified = DateTime.Parse("2019-10-08, 08:00")
                },
                new Record
                {
                    Name = "Biggus Dickus",
                    Address = "0000 Rome, foobar út 1.",
                    InsuranceNumber = 777666555,
                    Symptomes = "Pöszeség",
                    //RecordingDate = DateTime.Parse("2019-06-08, 08:00"),
                    Modified = DateTime.Parse("2019-06-08, 08:00")
                }
            };
        }
    }
}
