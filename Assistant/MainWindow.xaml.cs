using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            RefreshList();
            patient = new Patient
            {
                FirstName = TextFirstName.Text,
                LastName = TextLastName.Text,
                dateOfBirth = DatePickerBirth.SelectedDate,
                Address = TextAddress.Text,
                InsuranceNumber = UserInputControll.InsuranceNumToInt(TextInsuranceNumber.Text),
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
                    MessageBox.Show("Done!\n");
                    TextSymptoms.Clear();
                }
                else
                    MessageBox.Show("Error!\n" + result.Result);
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
                TextInsuranceNumber.Text = UserInputControll.InsuranceNumToString(patient.InsuranceNumber);
            }
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            using (var HttpClient = new HttpClient())
            {
                var result = HttpClient.GetAsync("http://localhost:8080/assistant").Result;
                var jsonData = result.Content.ReadAsStringAsync().Result;
                var patients = JsonConvert.DeserializeObject<IEnumerable<Patient>>(jsonData);
                PatientsList.ItemsSource = patients;
            }
        }
        private void ButtonAdmission_Click(object sender, RoutedEventArgs e)
        {
            if(patient is null)
            {
                MessageBox.Show("Choose patient!");
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
                var result = HttpClient.PostAsync("http://localhost:8080/assistant/"+patient.ID, stringContent);
                if (result.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Done!\n");
                    TextSymptoms.Clear();
                }
                else
                    MessageBox.Show("Error!\n" + result.Result);
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (patient is null)
            {
                MessageBox.Show("Choose patient!");
                return;
            }
            if (MessageBox.Show("Do you want to delet\n" +
                patient.FirstName + " " + patient.LastName + "\n" +
                "Insurance number: " + patient.InsuranceNumber + "?", "Waraning", MessageBoxButton.YesNo) ==
                MessageBoxResult.No)
            {
                return;
            }

            using (var httpClient = new HttpClient())
            {
                var result = httpClient.DeleteAsync("http://localhost:8080/assistant/" + patient.ID).Result;
                if (!result.IsSuccessStatusCode)
                    MessageBox.Show("Deletion failed!\n"+result.StatusCode);
                else
                    RefreshList();
            }
        }
    }
}
