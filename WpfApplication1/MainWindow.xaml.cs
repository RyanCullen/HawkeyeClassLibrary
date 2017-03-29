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
namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            HawkeyehvkBLL.Search search = new HawkeyehvkBLL.Search();
            //dataGrid.ItemsSource =  reservation.listReservation(); 
            //dataGrid.ItemsSource = reservation.listReservations(4); 
            bool result = search.validateReservationForPet(10);


        }
    }
}
