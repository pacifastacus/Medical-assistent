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
        private bool _isRefreshAutomatically;
        public MainWindow()
        {
            InitializeComponent();
            //listView = CollectionViewSource.GetDefaultView(PersonsList.ItemsSource);
            _timer = new System.Windows.Threading.DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(5);
            _timer.Tick += RefreshList;
            _timer.Start();
            _isRefreshAutomatically = true;
        }
        private void RefreshList(object sender, EventArgs e)
        {
            GetPersons();
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
        private void RefreshClicked(object sender, RoutedEventArgs e)
        {
            //restart the automatic refresh
            if (!_isRefreshAutomatically)
            {
                _timer.Tick += RefreshList;
                _isRefreshAutomatically = true;
                ButtonRefresh.BorderThickness = new Thickness(1);
                ButtonRefresh.BorderBrush = Brushes.Gray;
            }
            GetPersons();
        }
        private void GetPersons()
        {
            using(var HttpClient = new HttpClient())
            {
                try
                {
                    var result = HttpClient.GetAsync("http://localhost:8080/doctor").Result;
                    var jsonData = result.Content.ReadAsStringAsync().Result;
                    var db = JsonConvert.DeserializeObject<IEnumerable<Record>>(jsonData);
                    PersonsList.ItemsSource = db;
                }
                catch (Exception e)
                {
                    //Stop automatic refresh
                    _timer.Tick -= RefreshList;
                    _isRefreshAutomatically = false;
                    ButtonRefresh.BorderThickness = new Thickness(3);
                    ButtonRefresh.BorderBrush = Brushes.Green;
                    MessageBox.Show("Nem sikerült a kiszolgálót elérni.\n" +
                        "Az automatikus lista lekérdezés fel lesz függesztve!",
                        "Kiszolgáló elérhetetlen!");
                }
            }
        }
    }
}
