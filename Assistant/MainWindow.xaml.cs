using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Assistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Patient patient;
        public MainWindow()
        {
            InitializeComponent();
            RefreshList();
        }
        private void RegisterPatient_Click(object sender, RoutedEventArgs e)
        {
            int? insuranceNumber;
            if (!UserInputControl.InsuranceNumToInt(TextInsuranceNumber.Text, out insuranceNumber))
            {
                MessageBox.Show("TAJ-szám helytelen!");
                return;
            }
            TextLastName.Text = TextLastName.Text.Trim();
            if (!UserInputControl.CheckName(TextLastName.Text))
            {
                MessageBox.Show("Vezetéknév hibásan lett kitöltve!");
                return;
            }
            TextFirstName.Text = TextFirstName.Text.Trim();
            if (!UserInputControl.CheckName(TextFirstName.Text))
            {
                MessageBox.Show("Keresztnév hibásan lett kitöltve!");
                return;
            }

            patient = new Patient
            {
                FirstName = TextFirstName.Text,
                LastName = TextLastName.Text,
                dateOfBirth = DatePickerBirth.SelectedDate,
                Address = TextAddress.Text,
                InsuranceNumber = insuranceNumber,
                ID = -1
            };
            var admission = new Admission
            {
                Symptomes = TextSymptoms.Text,
                TimeOfAdmission = DateTime.Now,
                Diagnosis = "",
                PatientID = -1
            };
            ViewModel data = new ViewModel
            {
                Patient = patient,
                Admission = admission
            };
            var json = JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            using (var HttpClient = new HttpClient())
            {
                var result = HttpClient.PostAsync("http://localhost:8080/assistant/", stringContent);
                if (result.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Kész!\n");
                    TextSymptoms.Clear();
                }
                else
                    MessageBox.Show("Hiba!\n" + result.Result);
            }
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Patient patient = (Patient)PatientsList.SelectedItem;
            this.patient = patient;
            if (patient is null)
            {
                TextFirstName.Clear();
                TextLastName.Clear();
                DatePickerBirth.SelectedDate = DateTime.Now;
                TextAddress.Clear();
                TextInsuranceNumber.Clear();
            }
            else
            {
                TextFirstName.Text = patient.FirstName;
                TextLastName.Text = patient.LastName;
                DatePickerBirth.SelectedDate = patient.dateOfBirth;
                TextAddress.Text = patient.Address;
                TextInsuranceNumber.Text = UserInputControl.InsuranceNumToString(patient.InsuranceNumber);
            }
            TextSymptoms.Clear();
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            using (var HttpClient = new HttpClient())
            {
                try
                {
                    var result = HttpClient.GetAsync("http://localhost:8080/assistant").Result;
                    var jsonData = result.Content.ReadAsStringAsync().Result;
                    var patients = JsonConvert.DeserializeObject<IEnumerable<Patient>>(jsonData);
                    PatientsList.ItemsSource = patients;
                }
                catch (Exception)
                {  }
            }
        }
        private void ButtonAdmission_Click(object sender, RoutedEventArgs e)
        {
            if (patient is null)
            {
                MessageBox.Show("Válasszon beteget a listából!");
                return;
            }
            var admission = new Admission
            {
                Symptomes = TextSymptoms.Text,
                TimeOfAdmission = DateTime.Now,
                Diagnosis = "",
                PatientID = patient.ID
            };
            var json = JsonConvert.SerializeObject(admission);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            using (var HttpClient = new HttpClient())
            {
                var result = HttpClient.PostAsync("http://localhost:8080/assistant/" + patient.ID, stringContent);
                if (result.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Kész!\n");
                    TextSymptoms.Clear();
                }
                else
                    MessageBox.Show("Hiba!\n" + result.Result);
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (patient is null)
            {
                MessageBox.Show("Válasszon beteget a listából!");
                return;
            }
            if (MessageBox.Show("Biztos törli?\n" +
                patient.FirstName + " " + patient.LastName + "\n" +
                "Insurance number: " + patient.InsuranceNumber, "Figyelem!", MessageBoxButton.YesNo) ==
                MessageBoxResult.No)
            {
                return;
            }

            using (var httpClient = new HttpClient())
            {
                var result = httpClient.DeleteAsync("http://localhost:8080/assistant/" + patient.ID).Result;
                if (!result.IsSuccessStatusCode)
                    MessageBox.Show("Törlés sikertelen!\n" + result.StatusCode);
                else
                    RefreshList();
            }
        }
    }
}
