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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HawkeyehvkBLL;
using System.Data;

namespace testXaml
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Set Parameters
            Reservation re = new Reservation();
            //lblNum.Content = "Between march 3rd and 4th 2017 there are : "+re.checkRunAvailability(new DateTime(2017,3,3), new DateTime(2017,3,4), 'L')+" Large Runs Available.";
            // PetVaccination petVac = new PetVaccination();
            //lblNum.Content = petVac.checkVaccinations(1, 100)[0].isValidated;
            //Make Object And Call Method
            //Set datagrid or label to returned data
            Reservation control = new Reservation();
            lblNum.Content = control.addReservation(35, new DateTime(2017, 3, 30), new DateTime(2017, 3, 31));
            //PetVaccination petVac = new PetVaccination();
            //datagrid.ItemsSource = petVac.checkVaccinations(9, DateTime.Now); 
        }
    }
}
