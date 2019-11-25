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

namespace AssistentWPF
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();

        }

        private void btnBack(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void btnRegister(object sender, RoutedEventArgs e)
        {
            //Only for testing
            StringBuilder sb = new StringBuilder();
            sb.Append("Name of the patient: ").Append(txtFirstName.Text).Append("\n").Append(txtLastName.Text).Append("\n").Append(txtDateOfBirth.Text)
                .Append("Address of the patient: ").Append(txtAddress.Text).Append("\n")
                .Append("ID of the patient: ").Append(txtInsuranceNumber.Text);

            txtTmp.Text = sb.ToString();
        }
    }
}
