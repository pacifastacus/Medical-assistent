using Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Doctor
{
    /// <summary>
    /// Interaction logic for ModifyWindow.xaml
    /// </summary>
    public partial class ModifyWindow : Window
    {
        Record record;
        public ModifyWindow(Record record)
        {
            InputHandler handler = new InputHandler();
            InitializeComponent();
            this.record = record;
            TextName.Text = this.record.Name;
            TextAddress.Text = this.record.Address;
            TextSymptoms.Text = this.record.Symptomes;
            TextDateOfBirth.Text = this.record.DateOfBirth.ToString("yyyy. MM. dd.");
            TextDiagnose.Text = this.record.Diagnosis;
            string insuranceNumber;
            if (!handler.InsuranceNumIntToStr(this.record.InsuranceNumber, out insuranceNumber))
            {
                MessageBox.Show("Hibás TAJ-szám!\nVegye fel a kapcsolatot a rendszergazdával!");
            }
            TextInsurance.Text = insuranceNumber;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ModifyRecord(object sender, RoutedEventArgs e)
        {
            //If fields are unchanged unnecessary to make HTTP PUT request
            if (TextSymptoms.Text.Equals(record.Symptomes) &&
                TextDiagnose.Text.Equals(record.Diagnosis))
            {
                this.Close();
                return;
            }

            record.Symptomes = TextSymptoms.Text;
            record.Diagnosis = TextDiagnose.Text;
            record.Modified = DateTime.Now;

            var json = JsonConvert.SerializeObject(record);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                var result = httpClient.PutAsync("http://localhost:8080/doctor/", stringContent).Result;
                if (!result.IsSuccessStatusCode)
                {
                    MessageBox.Show("A bejegyzés módosítása sikertelen!\nHibakód:" + result.ReasonPhrase, "Hiba!");
                }
            }
            this.Close();
        }

        private void DeleteRecord(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Biztos, hogy törölni akarja a bejegyzést?",
                "Törlés megerősítése",
                MessageBoxButton.YesNo);
            if (res == MessageBoxResult.No)
            {
                return;
            }

            using (var httpClient = new HttpClient())
            {
                var result = httpClient.DeleteAsync("http://localhost:8080/doctor/" + record.ID).Result;

            }
            this.Close();
        }

        private void SelectAll(object sender, KeyboardFocusChangedEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;

            if (textBox != null && e.KeyboardDevice.IsKeyDown(Key.Tab))
                textBox.SelectAll();
        }
    }
}
