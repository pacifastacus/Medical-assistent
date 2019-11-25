using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
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
using Newtonsoft.Json;

namespace Doctor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private ICollectionView listView;
        private System.Windows.Threading.DispatcherTimer _timer;
        public MainWindow()
        {
            InitializeComponent();
            //listView = CollectionViewSource.GetDefaultView(PersonsList.ItemsSource);
            _timer = new System.Windows.Threading.DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(5);
            _timer.Tick += RefreshList;
            _timer.Start();
        }

        private void Modify(object sender, RoutedEventArgs e)
        {
            Record Person = (Record)PersonsList.SelectedItem;
            if (Person == null)
                MessageBox.Show("Előbb válasszon a listából!");
            else
            {
                Window dialog = new ModifyWindow(Person);
                dialog.ShowDialog();
                GetPersons();
            }
        }

        private void RefreshList(object sender, EventArgs e)
        {
            GetPersons();
        }
        private void GetPersons()
        {
            using(var HttpClient = new HttpClient())
            {
                var result = HttpClient.GetAsync("http://localhost:8080/doctor").Result;

                var jsonData = result.Content.ReadAsStringAsync().Result;
                var db = JsonConvert.DeserializeObject<IEnumerable<Record>>(jsonData);
                PersonsList.ItemsSource = db;
            }
        }

        
    }
}
