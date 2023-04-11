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
using System.Data;
using System.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection = new SqlConnection("Server=LAB707-19\\SQLEXPRESS02;Database=School;Integrated Security=True;");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Person> people = new List<Person>();

            connection.Open();
            SqlCommand sqlCommand = new SqlCommand("USP_GetPeople", connection);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.SqlDbType = SqlDbType.VarChar;
            parameter1.Size = 50;
            parameter1.Value = "";
            parameter1.ParameterName = "@LastName";

            SqlParameter parameter2 = new SqlParameter();
            parameter2.SqlDbType = SqlDbType.VarChar;
            parameter2.Size = 50;
            parameter2.Value = "";
            parameter2.ParameterName = "@FirstName";

            sqlCommand.Parameters.Add(parameter1);
            sqlCommand.Parameters.Add(parameter2);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                people.Add(new Person
                {
                    PersonId = reader["PersonID"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    FirstName = reader["FirstName"].ToString(),
                    FullName = string.Concat(reader["FirstName"].ToString(), " ", reader["LastName"].ToString())
                });
            }

            connection.Close();
            dgvPeople.ItemsSource = people;
        }
    }
}
