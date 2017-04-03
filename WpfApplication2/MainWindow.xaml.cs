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
namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Owner> ownList = new List<HawkeyehvkBLL.Owner>();
            Owner own = HawkeyehvkBLL.Owner.getFullOwner("bque@gmail.com");
            ownList.Add(own);
            dataGrid.ItemsSource = ownList;
            dataGrid2.ItemsSource = own.petList;
            dataGrid3.ItemsSource = own.reservationList;
            dataGrid4.ItemsSource = own.reservationList[0].petReservationList;
            lbl_pet.Content = own.reservationList[0].petReservationList[0].run.runNumber+"$$$$$$$";
        }
    }
}
