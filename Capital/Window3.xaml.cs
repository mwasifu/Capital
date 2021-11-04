using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

namespace Capital
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        private OleDbConnection Conn = new OleDbConnection();
        public Window3()
        {
            InitializeComponent();
            Conn = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source =" + System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Rent.mdb"));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            populate();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void populate()     // populate datagrid view with shop overview
        {
            DataTable dt = new DataTable();
            Conn.Open();
            OleDbCommand command = Conn.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Rental";
            using (var adapter = new OleDbDataAdapter(command))
            {
                adapter.Fill(dt);
            }

            grid1.ItemsSource = dt.DefaultView;

            Conn.Close();
        }

        private void refreshBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            populate();
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            Conn.Open();
            OleDbCommand command = Conn.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Rental WHERE ShopNo like('" + searchBar.Text + "%')";
            command.ExecuteNonQuery();
            DataTable d = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            da.Fill(d);
            grid1.ItemsSource = d.DefaultView;
            Conn.Close();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
