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
using System.Data.SQLite;
using System.Data;
using System.Windows.Documents.DocumentStructures;
using System.Data.SqlClient;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Security.Permissions;
using System.Data.Common;

namespace LAB_2
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var connectionString = "Data Source=C:\\Prog\\FIO.db;Version=3;";
            var connection = new SQLiteConnection(connectionString);

            connection.Open();


            // ____________________________________________________________________________________

            string sql = "SELECT * FROM Students";
            DataTable dataTable = new DataTable();
            string sqlo = "SELECT * FROM Ocenki";
            DataTable dataTableo = new DataTable();
            try
            {
                connection = new SQLiteConnection(connectionString);

                SQLiteCommand command = new SQLiteCommand(sql, connection);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

                SQLiteCommand commando = new SQLiteCommand(sqlo, connection);
                SQLiteDataAdapter adaptero = new SQLiteDataAdapter(commando);

                connection.Open();

                adapter.Fill(dataTable);
                myDataGrid.ItemsSource = dataTable.DefaultView;
                //connection.Open();
                adaptero.Fill(dataTableo);
                oDataGrid.ItemsSource = dataTableo.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null) connection.Close();
            }

            //---------------------------
            myDataGrid.Items.Refresh();
            oDataGrid.Items.Refresh();

            // ____________________________________________________________________________________

            connection.Close();
        }
        // --------------------------------------------------------------------------------------------
        // Добавление
        // --------------------------------------------------------------------------------------------
        private void Button_ClickADD(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=C:\\Prog\\FIO.db;Version=3;";

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO Students (FIO, Number) VALUES (@Value1, @Value2)";
                    string insertQueryO = "INSERT INTO Ocenki (Physics, Math) VALUES (@Value3, @Value4)";

                    using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Value1", FIO.Text);
                        command.Parameters.AddWithValue("@Value2", Number.Text);
                        command.ExecuteNonQuery();
                    }
                    using (SQLiteCommand command = new SQLiteCommand(insertQueryO, connection))
                    {
                        command.Parameters.AddWithValue("@Value3", Phi.Text);
                        command.Parameters.AddWithValue("@Value4", Math.Text);
                        command.ExecuteNonQuery();
                    }
                }

                // После выполнения операции вставки, обновите данные в вашем DataGrid
                string selectQuery = "SELECT * FROM Students"; // Замените на ваш запрос выборки

                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectQuery, connectionString))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    myDataGrid.ItemsSource = dataTable.DefaultView;
                }
                string selectQueryO = "SELECT * FROM Ocenki"; // Замените на ваш запрос выборки

                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectQueryO, connectionString))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    oDataGrid.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                // Обработка исключения, например, вывод сообщения об ошибке
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }
        // --------------------------------------------------------------------------------------------
        // Удаление
        // --------------------------------------------------------------------------------------------
        private void Button_ClickDel(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=C:\\Prog\\FIO.db;Version=3;";

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM Students WHERE ID = @StudentID";
                    string deleteQueryO = "DELETE FROM Ocenki WHERE ID = @StudentID";

                    using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", ID.Text);
                        command.ExecuteNonQuery();
                    }
                    using (SQLiteCommand command = new SQLiteCommand(deleteQueryO, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", ID.Text);
                        command.ExecuteNonQuery();
                    }

                }

                // После удаления данных можно обновить отображаемую таблицу
                // Например, вызовите метод для обновления данных в вашем DataGrid
                myDataGrid.Items.Refresh();
                oDataGrid.Items.Refresh();

                string selectQuery = "SELECT * FROM Students"; // Замените на ваш запрос выборки

                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectQuery, connectionString))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    myDataGrid.ItemsSource = dataTable.DefaultView;
                }
                string selectQueryO = "SELECT * FROM Ocenki"; // Замените на ваш запрос выборки

                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectQueryO, connectionString))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    oDataGrid.ItemsSource = dataTable.DefaultView;
                }

            }
            catch (Exception ex)
            {
                // Обработка исключения, например, вывод сообщения об ошибке
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }


        // --------------------------------------------------------------------------------------------
        // Изменение
        // --------------------------------------------------------------------------------------------

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=C:\\Prog\\FIO.db;Version=3;";
            myDataGrid.Items.Refresh();
            oDataGrid.Items.Refresh();

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Здесь вы можете выполнить SQL-запрос для обновления таблицы
                    string updateQuery = "UPDATE Students SET FIO = @Value1, Number = @Value2 WHERE ID = @StudentID";

                    using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", ID.Text);
                        command.Parameters.AddWithValue("@Value1", FIO.Text);
                        command.Parameters.AddWithValue("@Value2", Number.Text);
                        command.ExecuteNonQuery();
                    }

                    string updateQueryO = "UPDATE Ocenki SET Physics = @Value3, Math = @Value4 WHERE ID = @StudentID";

                    using (SQLiteCommand command = new SQLiteCommand(updateQueryO, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", ID.Text);
                        command.Parameters.AddWithValue("@Value3", Phi.Text);
                        command.Parameters.AddWithValue("@Value4", Math.Text);
                        command.ExecuteNonQuery();
                    }
                }
                myDataGrid.Items.Refresh();
                oDataGrid.Items.Refresh();
                string selectQuery = "SELECT * FROM Students"; // Замените на ваш запрос выборки

                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectQuery, connectionString))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    myDataGrid.ItemsSource = dataTable.DefaultView;
                }
                string selectQueryO = "SELECT * FROM Ocenki"; // Замените на ваш запрос выборки

                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectQueryO, connectionString))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    oDataGrid.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                // Обработка исключения, например, вывод сообщения об ошибке
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }
    }
}