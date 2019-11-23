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

namespace Doctor
{
    /// <summary>
    /// Interaction logic for ModifyWindow.xaml
    /// </summary>
    public partial class ModifyWindow : Window
    {
        MainWindow caller;
        Person record;
        public ModifyWindow(MainWindow caller, Person record)
        {
            InitializeComponent();
            this.record = record;
            this.caller = caller;
            TextName.Text = this.record.Name;
            TextInsurance.Text = InsuranceNumToString(this.record.InsuranceNumber);
            TextAddress.Text = this.record.Address;
            TextSymptoms.Text = this.record.Symptoms;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ModifyRecord(object sender, RoutedEventArgs e)
        {
            record.Symptoms = TextSymptoms.Text;
            record.Diagnosys = TextDiagnose.Text;
            record.Modified = DateTime.Now;
            this.Close();
        }
        private void DeleteRecord(object sender, RoutedEventArgs e)
        {

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
