using Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ViewWindow : Window
    {
        MainWindow caller;
        Person record;

        public ViewWindow(MainWindow caller, Person record)
        {
            InitializeComponent();
            this.record = record;
            this.caller = caller;
            TextName.Text = this.record.Name;
            TextInsurance.Text = InsuranceNumToString(this.record.InsuranceNumber);
            TextAddress.Text = this.record.Address;
            TextSymptoms.Text = this.record.Symptoms;
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private int InsuranceNumToInt(string insNum)
        {
            string[] members = insNum.Split('-');
            return int.Parse(string.Concat(members));
        }

        private string InsuranceNumToString(int insNum)
        {
            string str = insNum.ToString();
            string[] members = new string[3];
            for (int i = 0; i < 3; i++)
            {
                members[i] = str.Substring(i * 3, 3);
            }
            return members[0] + "-" + members[1] + "-" + members[2];
        }
    }
}
