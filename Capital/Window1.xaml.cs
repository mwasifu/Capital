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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private OleDbConnection Conn = new OleDbConnection();
        public Window1()
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

            shopNo.Content = "Shop No.:";
            tenantName.Content = "Tenant Name:";
            fatherName.Content = "Father Name:";
            ownerName.Content = "Owner Name:";
            address.Content = "Address:";
            phone.Content = "Phone:";
            size.Content = "Size:";
            rate.Content = "Rate:";
            rent.Content = "Rent:";
            address.Content = "Address:";
            Start.Content = "Start: ";
            End.Content = "End: ";
            Notes.Text = "Note";


            Conn.Close();
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
  
            OleDbCommand shopnocommand = Conn.CreateCommand();      // display shop number
            shopnocommand.CommandType = CommandType.Text;
            shopnocommand.CommandText = "SELECT ShopNo FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            OleDbDataReader reader = shopnocommand.ExecuteReader();
            reader.Read();
            shopNo.Content = shopNo.Content + " " + reader["ShopNo"].ToString();
            reader.Close();

            OleDbCommand tenantcommand = Conn.CreateCommand();      // display tenant name
            tenantcommand.CommandType = CommandType.Text;
            tenantcommand.CommandText = "SELECT TenantName FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = tenantcommand.ExecuteReader();
            reader.Read();
            tenantName.Content = tenantName.Content + " " + reader["TenantName"].ToString();

            OleDbCommand fathercommand = Conn.CreateCommand();      // display father name
            fathercommand.CommandType = CommandType.Text;
            fathercommand.CommandText = "SELECT FatherName FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = fathercommand.ExecuteReader();
            reader.Read();
            fatherName.Content = fatherName.Content + " " + reader["FatherName"].ToString();

            OleDbCommand ownercommand = Conn.CreateCommand();      // display owner name
            ownercommand.CommandType = CommandType.Text;
            ownercommand.CommandText = "SELECT OwnerName FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = ownercommand.ExecuteReader();
            reader.Read();
            ownerName.Content = ownerName.Content + " " + reader["OwnerName"].ToString();

            OleDbCommand addresscommand = Conn.CreateCommand();      // display address
            addresscommand.CommandType = CommandType.Text;
            addresscommand.CommandText = "SELECT Address FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = addresscommand.ExecuteReader();
            reader.Read();
            address.Content = address.Content + " " + reader["Address"].ToString();

            OleDbCommand phonecommand = Conn.CreateCommand();      // display phone
            phonecommand.CommandType = CommandType.Text;
            phonecommand.CommandText = "SELECT Phone FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = phonecommand.ExecuteReader();
            reader.Read();
            phone.Content = phone.Content + " " + reader["Phone"].ToString();

            OleDbCommand sizecommand = Conn.CreateCommand();      // display shop size
            sizecommand.CommandType = CommandType.Text;
            sizecommand.CommandText = "SELECT ShopSize FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = sizecommand.ExecuteReader();
            reader.Read();
            size.Content = size.Content + " " + reader["ShopSize"].ToString();

            OleDbCommand ratecommand = Conn.CreateCommand();      // display rate
            ratecommand.CommandType = CommandType.Text;
            ratecommand.CommandText = "SELECT Rate FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = ratecommand.ExecuteReader();
            reader.Read();
            rate.Content = rate.Content + " " + reader["Rate"].ToString();

            OleDbCommand rentcommand = Conn.CreateCommand();      // display rent
            rentcommand.CommandType = CommandType.Text;
            rentcommand.CommandText = "SELECT MonthlyRent FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = rentcommand.ExecuteReader();
            reader.Read();
            rent.Content = rent.Content + " " + reader["MonthlyRent"].ToString();

            OleDbCommand startdatecommand = Conn.CreateCommand();      // display start date
            startdatecommand.CommandType = CommandType.Text;
            startdatecommand.CommandText = "SELECT StartDate FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = startdatecommand.ExecuteReader();
            reader.Read();
            DateTime dt = DateTime.Parse(reader["StartDate"].ToString());
            Start.Content = Start.Content + " " + dt.ToShortDateString();


            OleDbCommand enddatecommand = Conn.CreateCommand();      // display end date
            enddatecommand.CommandType = CommandType.Text;
            enddatecommand.CommandText = "SELECT PeriodDate FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = enddatecommand.ExecuteReader();
            reader.Read();
            DateTime x = DateTime.Parse(reader["PeriodDate"].ToString());
            End.Content = End.Content + " " + x.ToShortDateString();

            OleDbCommand notescommand = Conn.CreateCommand();      // display notes
            notescommand.CommandType = CommandType.Text;
            notescommand.CommandText = "SELECT Notes FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = notescommand.ExecuteReader();
            reader.Read();
            Notes.Text = reader["Notes"].ToString();


            Conn.Close();
        }

        private void rentBtn_Click(object sender, RoutedEventArgs e)
        {


        }

        private void shopBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
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

        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
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

            OleDbCommand shopnocommand = Conn.CreateCommand();      // display shop number
            shopnocommand.CommandType = CommandType.Text;
            shopnocommand.CommandText = "SELECT ShopNo FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            OleDbDataReader reader = shopnocommand.ExecuteReader();
            reader.Read();
            shopNo.Content = shopNo.Content + " " + reader["ShopNo"].ToString();
            reader.Close();

            OleDbCommand tenantcommand = Conn.CreateCommand();      // display tenant name
            tenantcommand.CommandType = CommandType.Text;
            tenantcommand.CommandText = "SELECT TenantName FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = tenantcommand.ExecuteReader();
            reader.Read();
            tenantName.Content = tenantName.Content + " " + reader["TenantName"].ToString();

            OleDbCommand fathercommand = Conn.CreateCommand();      // display father name
            fathercommand.CommandType = CommandType.Text;
            fathercommand.CommandText = "SELECT FatherName FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = fathercommand.ExecuteReader();
            reader.Read();
            fatherName.Content = fatherName.Content + " " + reader["FatherName"].ToString();

            OleDbCommand ownercommand = Conn.CreateCommand();      // display owner name
            ownercommand.CommandType = CommandType.Text;
            ownercommand.CommandText = "SELECT OwnerName FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = ownercommand.ExecuteReader();
            reader.Read();
            ownerName.Content = ownerName.Content + " " + reader["OwnerName"].ToString();

            OleDbCommand addresscommand = Conn.CreateCommand();      // display address
            addresscommand.CommandType = CommandType.Text;
            addresscommand.CommandText = "SELECT Address FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = addresscommand.ExecuteReader();
            reader.Read();
            address.Content = address.Content + " " + reader["Address"].ToString();

            OleDbCommand phonecommand = Conn.CreateCommand();      // display phone
            phonecommand.CommandType = CommandType.Text;
            phonecommand.CommandText = "SELECT Phone FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = phonecommand.ExecuteReader();
            reader.Read();
            phone.Content = phone.Content + " " + reader["Phone"].ToString();

            OleDbCommand sizecommand = Conn.CreateCommand();      // display shop size
            sizecommand.CommandType = CommandType.Text;
            sizecommand.CommandText = "SELECT ShopSize FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = sizecommand.ExecuteReader();
            reader.Read();
            size.Content = size.Content + " " + reader["ShopSize"].ToString();

            OleDbCommand ratecommand = Conn.CreateCommand();      // display rate
            ratecommand.CommandType = CommandType.Text;
            ratecommand.CommandText = "SELECT Rate FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = ratecommand.ExecuteReader();
            reader.Read();
            rate.Content = rate.Content + " " + reader["Rate"].ToString();

            OleDbCommand rentcommand = Conn.CreateCommand();      // display rent
            rentcommand.CommandType = CommandType.Text;
            rentcommand.CommandText = "SELECT MonthlyRent FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = rentcommand.ExecuteReader();
            reader.Read();
            rent.Content = rent.Content + " " + reader["MonthlyRent"].ToString();

            OleDbCommand startdatecommand = Conn.CreateCommand();      // display start date
            startdatecommand.CommandType = CommandType.Text;
            startdatecommand.CommandText = "SELECT StartDate FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = startdatecommand.ExecuteReader();
            reader.Read();
            DateTime dt = DateTime.Parse(reader["StartDate"].ToString());
            Start.Content = Start.Content + " " + dt.ToShortDateString();


            OleDbCommand enddatecommand = Conn.CreateCommand();      // display end date
            enddatecommand.CommandType = CommandType.Text;
            enddatecommand.CommandText = "SELECT PeriodDate FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = enddatecommand.ExecuteReader();
            reader.Read();
            DateTime x = DateTime.Parse(reader["PeriodDate"].ToString());
            End.Content = End.Content + " " + x.ToShortDateString();

            OleDbCommand notescommand = Conn.CreateCommand();      // display notes
            notescommand.CommandType = CommandType.Text;
            notescommand.CommandText = "SELECT Notes FROM ShopInfo WHERE ShopNo='" + searchBar.Text + "'";
            reader = notescommand.ExecuteReader();
            reader.Read();
            Notes.Text = reader["Notes"].ToString();


            Conn.Close();

        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            string ShopNo = (string)shopNo.Content;
            string TenantName = (string)tenantName.Content;
            string OwnerName = (string)ownerName.Content;
            string FatherName = (string)fatherName.Content;
            string Address = (string)address.Content;
            string Phone = (string)phone.Content;
            bool PosRent = (bool)pos.IsChecked;
            string ShopSize = (string)size.Content;
            string Rate = (string)rate.Content;
            string MonthlyRent = (string)rent.Content;
            string Period = (string)period.Content;
            string note = (string)Notes.Text;


            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Rental SET [RentColDate] = @rentcoldate, [MonthName] = @monthname, [MonthlyRent] = @monthlyrent WHERE ShopNo=?";
            command.Connection = Conn;
            Conn.Open();

            if (Conn.State == ConnectionState.Open)
            {
                command.Parameters.Add("@rentcoldate", OleDbType.Date).Value = (DateTime)Start.Content;
                command.Parameters.Add("@monthname", OleDbType.Date).Value = (DateTime)End.Content;
                if ((string)rent.Content == "")
                {
                    command.Parameters.Add("@monthlyrent", DBNull.Value);
                }
                else
                {
                    command.Parameters.Add("@monthlyrent", OleDbType.VarChar).Value = rent.Content;
                }
                command.Parameters.AddWithValue("@shopno", OleDbType.VarChar).Value = searchBar.Text;


                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Data Updated Successfully!");
                    Conn.Close();
                    populate();
                }
                catch (OleDbException ex)
                {
                    MessageBox.Show(ex.Message);
                    Conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Connection Failed");
            }
        }
    }
}
