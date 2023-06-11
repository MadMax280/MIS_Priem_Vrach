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
    public partial class PatientForm : Form
    {
        string i;
        public PatientForm(string v)
        {
            DB db = new DB();
            i = v;
            InitializeComponent();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `diagnoz` WHERE `id patient`="+i, db.getConnection());
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                DiagBox.Text= table.Rows[0].ItemArray[2].ToString();
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `mis`.`simptomi` (`id patient`, `simptomi`) VALUES (@idp, @simp)", db.getConnection());
            
            command.Parameters.Add("@idp", MySqlDbType.VarChar).Value = i;
            command.Parameters.Add("@simp", MySqlDbType.VarChar).Value = SimpBox.Text;
            db.openConnection();
            command.ExecuteNonQuery();
            db.closeConnection();
        }
    }
}

