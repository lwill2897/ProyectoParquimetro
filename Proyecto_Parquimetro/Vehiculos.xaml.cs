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
using System.Timers;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Threading;

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
            hora();
    

        }
        public class Estacionados
        {
            public string NumPlaca { get; set; }
            public string Tipo_Vehiculo { get; set; }
            public string Hora_Ingreso { get; set; }
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
                sqlCommand.Parameters.AddWithValue("@tipovehiculo", cmbtipovehiculo.SelectedValue);
                sqlCommand.ExecuteNonQuery();
           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlconnection.Close();
                MostrarVehiculosEstacionados();
            }
        }

        private void Btn_salida_Click(object sender, RoutedEventArgs e)
        {
            if (lbVehiculosAparacados.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un Vehiculo");
            }
            else
            {

                try
                {
                    String query = "Delete FROM Parqueo.Vehiculo WHERE Num_Placa = @Num_Placa";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);

                    // Abrir la conexión
                    sqlconnection.Open();

                    // Agregar el parámetro
                    sqlCommand.Parameters.AddWithValue("@Num_Placa", lbVehiculosAparacados.SelectedValue);
                    
                    
                    // Ejecutar un query scalar
                    sqlCommand.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    sqlconnection.Close();
                    MostrarVehiculosEstacionados();
                }


            }
        }
        

        

        private void MostrarVehiculosEstacionados()
        {
            try
            {
                //El query a realizar la base de datos
                String query = @"SELECT * FROM Parqueo.Vehiculo,Parqueo.Tipo_Vehiculo WHERE Id = IdTipo_Vehiculo";
                //SqlDataAdapter es una interfaz ejtr las tablas y los objetos
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlconnection);

                using (sqlDataAdapter)
                {
                    //Objeto en c# que refleja una tabla de una base de datos
                    DataTable tablaEstacionados = new DataTable();

                    //Llenar el objeto de tipo DataTable
                    sqlDataAdapter.Fill(tablaEstacionados);

                    //lbVehiculosAparacados.DisplayMemberPath = "Num_Placa";
                    lbVehiculosAparacados.SelectedValuePath = "Num_Placa";                 
                  
                    // Los tiene el DataTable
                    lbVehiculosAparacados.ItemsSource = tablaEstacionados.DefaultView;

                    IList<Estacionados> Lista = new List<Estacionados>();
                    foreach (DataRow row in tablaEstacionados.Rows )
                    {
                        Lista.Add(new Estacionados
                        {
                        
                            NumPlaca = row[0].ToString(),
                            Tipo_Vehiculo = row[4].ToString(),
                            Hora_Ingreso = row[2].ToString()
                        });
                    }
                    lbVehiculosAparacados.ItemsSource = Lista;



                }
            }
            catch (Exception e)
            {
                // Este indicaria el error que ha ocurrido
                MessageBox.Show(e.Message.ToString());
            }


        }

        public void hora()
        {
           DispatcherTimer dispatcher = new DispatcherTimer();
            dispatcher.Interval = new TimeSpan(0, 0, 1);
            dispatcher.Tick += (s, a) =>
            {
                txthora.Text = DateTime.Now.ToString("hh:mm:ss");

            };
            dispatcher.Start();
        }

        private void Btnreloj_Click(object sender, RoutedEventArgs e)
        {
            txthora.IsEnabled = true;

        }
    }
}

