using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        Record record;
        int recordIndex;
        public ModifyWindow(Record record)
        {
            InputHandler handler = new InputHandler();
            InitializeComponent();
            this.record = record;
            TextName.Text = this.record.Name;
            TextAddress.Text = this.record.Address;
            TextSymptoms.Text = this.record.Symptomes;
            TextDiagnose.Text = this.record.Diagnosis;
            string insuranceNumber;
            if(!handler.InsuranceNumToString(this.record.InsuranceNumber,out insuranceNumber))
            {
                MessageBox.Show("Hibás TAJ-szám!\nVegye fel a kapcsolatot a rendszergazdával!");
            }
            TextInsurance.Text = insuranceNumber;
        }

        public ModifyWindow(Record record, int recordIndex) : this(record)
        {
            this.recordIndex = recordIndex;
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

            var json = JsonConvert.SerializeObject(record);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                var result = httpClient.PutAsync("http://localhost:8080/doctor/",stringContent).Result;
            }
            this.Close();
        }

        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            using (var httpClient = new HttpClient())
            {
                var result = httpClient.DeleteAsync("http://localhost:8080/doctor/"+record.ID);
            }
        }
    }
}
