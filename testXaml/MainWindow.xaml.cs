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
            /* addToReservation Test Cases  */
            // reservation# 603 , owner# 17 , pet in reservation 31 , 32 
            //Input : pet# 30   Expected : 1 row inserted  
            int  i = Reservation.addToReservation(603,30);
               
            

        }
    }
}
