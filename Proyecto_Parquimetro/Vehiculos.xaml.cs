﻿using System;
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

using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace Proyecto_Parquimetro
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Vehiculos : Window
    {
        SqlConnection sqlconnection;
        
        public Vehiculos()
        {
            InitializeComponent();
            
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Parquimetro.Properties.Settings.EstacionamientoConnectionString"].ConnectionString;
            sqlconnection = new SqlConnection(connectionString);
            MostrarTipo();
            MostrarVehiculosEstacionados();
    

        }
        public class Estacionados
        {
            private string numPlaca;
            private int idTipoVehiculo;
 

   
            public Estacionados()
            {

            }
            public int IdTipoVehiculo
            {
                get { return idTipoVehiculo; }
                set { idTipoVehiculo = value; }
            }

            public string NumPlaca
            {
                get { return numPlaca; }
                set { numPlaca = value; }
            }


        }

        private void TextFieldAssist_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Btnregresar_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow principal = new MainWindow();
            principal.Show();
            this.Close();
        }


        public void MostrarTipo()
        {
            try
            {

                string query = @"select * from Parqueo.Tipo_Vehiculo ";

                // Comando SQL
                 SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection );

                using (sqlDataAdapter)
                {

                   DataTable tablaTipoVehiculo = new DataTable();

                    sqlDataAdapter.Fill(tablaTipoVehiculo);
                    cmbtipovehiculo.DisplayMemberPath = "Tipo";
                    cmbtipovehiculo.SelectedValuePath = "Id";
                    cmbtipovehiculo.ItemsSource = tablaTipoVehiculo.DefaultView;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void Btn_entrada_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = @"INSERT INTO Parqueo.Vehiculo(Num_Placa,IdTipo_Vehiculo)
                                VALUES (@numero_placa,@tipovehiculo)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
                // Conexión sql
                sqlconnection.Open();

                
                sqlCommand.Parameters.AddWithValue("@numero_placa", txtplaca.Text);
                sqlCommand.Parameters.AddWithValue("@tipovehiculo", Convert.ToString(cmbtipovehiculo.SelectedValue));
                sqlCommand.ExecuteNonQuery();
           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlconnection.Close();
            }
        }
                
        private void Btn_salida_Click(object sender, RoutedEventArgs e)
        {
            if (lbVehiculosAparacados.SelectedValue == null)
                MessageBox.Show("Debe seleccionar un Vehiculo");
            else
            {

            }

        }

        public DataTable MostrarEstacionados()
        {
            DataTable tablaEstacionados = new DataTable();
            try
            {

                string query = "SELECT * from Parqueo.Vehiculo";

                // Comando SQL
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

                using (sqlDataAdapter)
                {

                    sqlDataAdapter.Fill(tablaEstacionados);
                    lbVehiculosAparacados.DisplayMemberPath = "Num_Placa";

                    lbVehiculosAparacados.SelectedValuePath = " Num_Placa";
                    lbVehiculosAparacados.ItemsSource = tablaEstacionados.DefaultView;
                }



            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return tablaEstacionados;

        }

        private void MostrarVehiculosEstacionados()
        {
            try
            {
                //El query a realizar la base de datos
                String query = "SELECT * FROM Parqueo.Vehiculo";
                //SqlDataAdapter es una interfaz ejtr las tablas y los objetos
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

                using (sqlDataAdapter)
                {
                    //Objeto en c# que refleja una tabla de una base de datos
                    DataTable tablaEstacionados = new DataTable();

                    //Llenar el objeto de tipo DataTable
                    sqlDataAdapter.Fill(tablaEstacionados);

                    //lbVehiculosAparacados.DisplayMemberPath = "Num_Placa";
                    lbVehiculosAparacados.SelectedValuePath = "IdTipo_Vehiculo";                 
                  
                    // Los tiene el DataTable
                    lbVehiculosAparacados.ItemsSource = tablaEstacionados.DefaultView;

                    

                }
            }
            catch (Exception e)
            {
                // Este indicaria el error que ha ocurrido
                MessageBox.Show(e.Message.ToString());
            }

            
        }


    }
}

