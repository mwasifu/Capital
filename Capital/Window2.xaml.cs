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
using System.Data;
using System.Data.OleDb;

namespace Capital
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void go_Click(object sender, RoutedEventArgs e)
        {
            OleDbConnection Conn = new OleDbConnection();
            Conn = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source =" + System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Rent.mdb"));
            OleDbCommand command = Conn.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Permission WHERE LoginName=? and Passwrd=?";
            command.Connection = Conn;

            // check for correct username and password
            try
            {
                Conn.Open();
                command.Parameters.AddWithValue("@loginname", OleDbType.VarChar).Value = userText.Text.Trim();
                command.Parameters.AddWithValue("@password", OleDbType.VarChar).Value = passText.Text.Trim();
                OleDbDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("Invalid Credentials");
                    return;
                }
                Conn.Close();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Message);
                Conn.Close();
            }

            bool admin = false;
            command = Conn.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT PermissionLevel FROM Permission WHERE LoginName=? and Passwrd=?";
            command.Connection = Conn;
            Conn.Open();
            try
            {
                if (Conn.State == ConnectionState.Open)
                {
                    command.Parameters.AddWithValue("@loginname", OleDbType.VarChar).Value = userText.Text.Trim();
                    command.Parameters.AddWithValue("@password", OleDbType.VarChar).Value = passText.Text.Trim();
                    if ((userText.Text == "") || (passText.Text == ""))
                    {
                        MessageBox.Show("Invalid credentials");
                        return;
                    }
                    DataTable d = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    da.Fill(d);
                    if (d.Rows[0][0].ToString() == "Administrator")
                    {
                        admin = true;

                    }
                    else if (d.Rows[0][0].ToString() == "Operator")
                    {
                        admin = false;
                    }

                    if (admin)
                    {
                       
                        MainWindow m = new MainWindow();
                        m.Show();
                        this.Close();
                    }
                    else if (admin == false)
                    {

                        Window3 w = new Window3();
                        w.Show();
                        this.Close();

                    }

                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Message);
                Conn.Close();
            }

            Conn.Close();
        }

        private void PackIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void draggable(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}

