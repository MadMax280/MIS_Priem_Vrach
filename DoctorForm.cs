using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIS
{
    public partial class DoctorForm : Form
    {
        string i;
        string d;
        public DoctorForm()
        {
            
            InitializeComponent();
            DB db = new DB();
           MySqlCommand command = new MySqlCommand("SELECT * FROM `simptomi` limit 1", db.getConnection());
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                i = table.Rows[0].ItemArray[1].ToString();
                diagBox.Text = table.Rows[0].ItemArray[2].ToString();
                d= table.Rows[0].ItemArray[0].ToString();
            }
            else
                buttonLogin.Enabled = false;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `mis`.`diagnoz` (`id patient`, `diagnoz`) VALUES (@idp, @diag)", db.getConnection());
            command.Parameters.Add("@diag", MySqlDbType.VarChar).Value = zaprosBox.Text;
            command.Parameters.Add("@idp", MySqlDbType.VarChar).Value = i;
            db.openConnection();
            command.ExecuteNonQuery();
            db.closeConnection();
            command = new MySqlCommand("DELETE FROM `mis`.`simptomi` WHERE  `id zaprosa`=@idd", db.getConnection());
            command.Parameters.Add("@idd", MySqlDbType.VarChar).Value = d;
            db.openConnection();
            command.ExecuteNonQuery();
            db.closeConnection();
            command = new MySqlCommand("SELECT * FROM `simptomi` limit 1", db.getConnection());
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                i = table.Rows[0].ItemArray[1].ToString();
                diagBox.Text = table.Rows[0].ItemArray[2].ToString();
                d = table.Rows[0].ItemArray[0].ToString();
            }
            else
                buttonLogin.Enabled = false;
        }
    }
}
