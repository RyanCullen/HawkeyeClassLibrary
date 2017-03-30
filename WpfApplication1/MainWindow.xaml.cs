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
            List<int> petNumbers = new List<int>();
            petNumbers.Add(1);
            petNumbers.Add(2);

            HawkeyehvkBLL.Reservation reservation = new Reservation();
            reservation.addReservation(1, new DateTime(2022,3,13) , new DateTime(2022, 3, 25));


        }
    }
}
