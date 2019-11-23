using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ModifyWindow : Window
    {
        MainWindow caller;
        Person record;
        bool newEntry;
        public ModifyWindow(MainWindow caller)
        {
            InitializeComponent();
            this.caller = caller;
            newEntry = true;
        }

        public ModifyWindow(MainWindow caller, Person record)
        {
            InitializeComponent();
            this.record = record;
            this.caller = caller;
            TextName.Text = this.record.Name;
            TextInsurance.Text = InsuranceNumToString(this.record.InsuranceNumber);
            TextAddress.Text = this.record.Address;
            TextSymptoms.Text = this.record.Symptoms;
            newEntry = false;
        }
        private void CancelRecord(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddRecord(object sender, RoutedEventArgs e)
        {
            string name = TextName.Text;
            int insNum = InsuranceNumToInt(TextInsurance.Text);
            string addr = TextAddress.Text;
            string symptoms = TextSymptoms.Text;
            if (newEntry)
            {
                caller.Db.Persons.Add(new Person()
                {
                    Name = name,
                    InsuranceNumber = insNum,
                    Address = addr,
                    Symptoms = symptoms,
                    RecordingDate = DateTime.Now
                });
            }
            else
            {
                record.Name = name;
                record.InsuranceNumber = insNum;
                record.Address = addr;
                record.Symptoms = symptoms;
                record.RecordingDate = DateTime.Now;
            }
            this.Close();
        }

        private int InsuranceNumToInt(string insNum)
        {
            string[] members = insNum.Split('-');
            return int.Parse(string.Concat(members));
        }

        private string InsuranceNumToString(int insNum)
        {
            string str = insNum.ToString();
            string[] members = new string[3];
            for (int i = 0; i < 3; i++)
            {
                members[i] = str.Substring(i * 3, 3);
            }
            return members[0] + "-" + members[1] + "-" + members[2];
        }
    }
}
