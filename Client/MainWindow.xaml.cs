using Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DummyDB Db { get; }
        ICollectionView listView;
        public MainWindow()
        {
            Db = new DummyDB();
            InitializeComponent();
            GetPatients();
            listView = CollectionViewSource.GetDefaultView(PatientList.ItemsSource);
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            //this.Close();
        }
        private void ViewPatient(object sender, MouseButtonEventArgs e)
        {
            Patient patient = (Patient)PatientList.SelectedItem;
            Window view = new ViewWindow(this, patient);
            view.Show();
        }
        private void AddPatient(object sender, RoutedEventArgs e)
        {
            Window record = new ModifyWindow(this);
            record.ShowDialog();
            //listView.Refresh();
        }

        private void ModifyPatient(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)PatientList.SelectedItem;
            if (patient == null)
                MessageBox.Show("Válasszon a listából!");
            else
            {
                Window record= new ModifyWindow(this, patient);
                record.ShowDialog();
            }
            
            listView.Refresh();
        }
        private void DeletePatient(object sender, RoutedEventArgs e)
        {
            if (!Db.Patients.Remove((Patient)PatientList.SelectedItem))
            {
                MessageBox.Show("Nothing selected!");
            }
           // listView.Refresh();
        }

        private void GetPatients()
        {
            PatientList.ItemsSource = Db.Patients;
        }
    }
}
