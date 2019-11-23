using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        Record record;
        public ModifyWindow(MainWindow caller, Record record)
        {
            InputHandler handler = new InputHandler();
            InitializeComponent();
            this.record = record;
            this.caller = caller;
            TextName.Text = this.record.Name;
            string insuranceNumber;
            if(!handler.InsuranceNumToString(this.record.InsuranceNumber,out insuranceNumber))
            {
                MessageBox.Show("Hibás TAJ-szám!\nVegye fel a kapcsolatot a rendszergazdával!");
            }
            TextAddress.Text = this.record.Address;
            TextSymptoms.Text = this.record.Symptomes;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ModifyRecord(object sender, RoutedEventArgs e)
        {
            record.Symptomes = TextSymptoms.Text;
            record.Diagnosis = TextDiagnose.Text;
            record.Modified = DateTime.Now;
            this.Close();
        }
        private void DeleteRecord(object sender, RoutedEventArgs e)
        {

        }
    }
}
