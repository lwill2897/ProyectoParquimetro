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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Proyecto_Parquimetro
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection sqlconnection;
        public MainWindow()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Parquimetro.Properties.Settings.EstacionamientoConnectionString"].ConnectionString;
            sqlconnection = new SqlConnection(connectionString);

        }


        private void Btn_vehiculo_Click(object sender, RoutedEventArgs e)
        {
            Vehiculos ingreso = new Vehiculos();
            ingreso.Show();
            this.Close();
        }

        private void Btnapagar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Creado Por Willito Y Rudy") ;  
        }
    }
}
