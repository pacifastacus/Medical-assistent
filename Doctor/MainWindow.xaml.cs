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

namespace Doctor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _warningSelection = "Válasszon a listából!";
        public DummyDB Db { get; }
        ICollectionView listView;
        public MainWindow()
        {
            Db = new DummyDB();
            InitializeComponent();
            GetPersons();
            listView = CollectionViewSource.GetDefaultView(PersonsList.ItemsSource);
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if(PersonsList.SelectedItem == null)
            {
                MessageBox.Show(_warningSelection);
                return;
            }

            MessageBoxResult res = MessageBox.Show("Biztos, hogy törölni akarja a bejegyzést?",
                "Törlés megerősítése",
                MessageBoxButton.YesNo);
            if (res == MessageBoxResult.No)
            {
                return;
            }

            if (!Db.Persons.Remove((Person)PersonsList.SelectedItem))
            {
                MessageBox.Show("Törlés sikertelen!");
            }
            // listView.Refresh();
        }

        private void Modify(object sender, RoutedEventArgs e)
        {
            Person Person = (Person)PersonsList.SelectedItem;
            if (Person == null)
                MessageBox.Show(_warningSelection);
            else
            {
                Window dialog = new ModifyWindow(this, Person);
                dialog.ShowDialog();
            }

            listView.Refresh();
        }

        private void RefreshList(object sender, RoutedEventArgs e)
        {
            listView.Refresh();
        }
        private void GetPersons()
        {
            using(var HttpClient = new HttpClient())
            {
                var result = HttpClient.GetAsync("http://localhost:8080/assistant/patients").Result;

                var List<>
            }
            PersonsList.ItemsSource = Db.Persons;
        }
    }
}
