using Models;
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
            GetPersons();
            listView = CollectionViewSource.GetDefaultView(PersonList.ItemsSource);
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            //this.Close();
        }
        private void ViewPerson(object sender, MouseButtonEventArgs e)
        {
            Person Person = (Person)PersonList.SelectedItem;
            Window view = new ViewWindow(this, Person);
            view.Show();
        }
        private void AddPerson(object sender, RoutedEventArgs e)
        {
            Window record = new ModifyWindow(this);
            record.ShowDialog();
            //listView.Refresh();
        }

        private void ModifyPerson(object sender, RoutedEventArgs e)
        {
            Person Person = (Person)PersonList.SelectedItem;
            if (Person == null)
                MessageBox.Show("Válasszon a listából!");
            else
            {
                Window record= new ModifyWindow(this, Person);
                record.ShowDialog();
            }
            
            listView.Refresh();
        }
        private void DeletePerson(object sender, RoutedEventArgs e)
        {
            if (!Db.Persons.Remove((Person)PersonList.SelectedItem))
            {
                MessageBox.Show("Nothing selected!");
            }
           // listView.Refresh();
        }

        private void GetPersons()
        {
            PersonList.ItemsSource = Db.Persons;
        }
    }
}
