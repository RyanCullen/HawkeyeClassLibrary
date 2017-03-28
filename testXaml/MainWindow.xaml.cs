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
            lblNum.Content = "Between march 3rd and 4th 2017 there are : "+re.checkRunAvailability(new DateTime(2017,3,3), new DateTime(2017,3,4), 'L')+" Large Runs Available.";

            //Make Object And Call Method
            //Set datagrid or label to returned data
            Owner own = new Owner();
            datagrid.ItemsSource = own.listTheOwners(); 
        }
    }
}
