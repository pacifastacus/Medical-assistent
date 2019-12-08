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
        private List<Patient> patients;
        private Patient patientToRegister;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void RegisterPatient_Click(object sender, RoutedEventArgs e)
        {
            if(new Patient
                {
                    FirstName = TextFirstName.Text,
                    LastName = TextLastName.Text,
                    dateOfBirth = DatePickerBirth.SelectedDate,
                    Address = TextAddress.Text,
                    InsuranceNumber = int.Parse(TextInsuranceNumber.Text)
            }.Equals(patientToRegister))
            {
                MessageBox.Show("Nothing changed!");
                return;
            }
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Patient patient = (Patient) PatientsList.SelectedItem;
            this.patientToRegister = patient;
            TextFirstName.Text = patient.FirstName;
            TextLastName.Text = patient.LastName;
            DatePickerBirth.SelectedDate = patient.dateOfBirth;
            TextAddress.Text = patient.Address;
            TextInsuranceNumber.Text = patient.InsuranceNumber.ToString();  //TODO format Insurance number
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
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
            if(patientToRegister is null)
            {
                patientToRegister = new Patient
                {
                    ID = -1,
                    FirstName = TextFirstName.Text,
                    LastName = TextLastName.Text,
                    dateOfBirth = DatePickerBirth.SelectedDate,
                    Address = TextAddress.Text,
                    InsuranceNumber = int.Parse(TextInsuranceNumber.Text)
                };
            }
            //TODO
            //var json = JsonConvert.SerializeObject(new
            //{
            //    patient = patientToRegister,
            //    symptomes = TextSymptoms.Text,
            //    lastModified = DateTime.Now
            //});
            //var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var json = new JObject();
            json.Add("patient", JToken.FromObject(patientToRegister));
            json.Add("syptomes", TextSymptoms.Text);
            json.Add("lastModified",DateTime.Now);
            var stringContent = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");
            using (var HttpClient = new HttpClient())
            {
                var result = HttpClient.PostAsync("http://localhost:8080/assistant/", stringContent);
                MessageBox.Show("Done!");
            }
        }
    }
}
