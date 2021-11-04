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
using System.Data.OleDb;
using System.Windows.Media.Animation;

namespace Capital
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private OleDbConnection Conn = new OleDbConnection();
        public MainWindow()
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
            command.CommandText = "SELECT * FROM ShopInfo";
            using (var adapter = new OleDbDataAdapter(command))
            {
                adapter.Fill(dt);
            }

            grid1.ItemsSource = dt.DefaultView;

            Conn.Close();
        }

    

            private void rentBtn_Click(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Show();
            this.Close();
            
        }

        private void shopBtn_Click(object sender, RoutedEventArgs e)
        {
        

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
            Window1 win1 = new Window1();
            win1.Show();

        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (shopnorad.IsChecked == true)      // search by shop number
            {
                Conn.Open();
                OleDbCommand command = Conn.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM ShopInfo WHERE ShopNo like('" + searchBar.Text + "%')";
                command.ExecuteNonQuery();
                DataTable d = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(d);
                grid1.ItemsSource = d.DefaultView;
                Conn.Close();
            }
            else if (namerad.IsChecked == true)       // search by name
            {
                Conn.Open();
                OleDbCommand command = Conn.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM ShopInfo WHERE TenantName like('" + searchBar.Text + "%')";
                command.ExecuteNonQuery();
                DataTable d = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(d);
                grid1.ItemsSource = d.DefaultView;
                Conn.Close();
            }

            else
            {
                MessageBox.Show("Select a category.");
            }
        }

        private void shopnoradChecked(object sender, RoutedEventArgs e)
        {
            shopnorad.IsChecked = true;
        }

        private void nameradChecked(object sender, RoutedEventArgs e)
        {
            namerad.IsChecked = true;
        }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void refreshBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            populate();
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Conn.Open();
                OleDbCommand command = Conn.CreateCommand();
                command.CommandType = CommandType.Text;

                command.CommandText = "DELETE FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
                command.ExecuteNonQuery();
                Conn.Close();
                MessageBox.Show("Data Deleted Successfully!");
                populate();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Conn.Close();
            }
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {

            
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
