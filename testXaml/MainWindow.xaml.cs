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
            
         //  datagrid.ItemsSource = new DataView(re.numberOfRunsReserved(new DateTime(2017,03,03), new DateTime(2017, 03, 04), 'L').Tables[0]);
            //Make Object And Call Method
            //Set datagrid to returned data
          
        }
    }
}
