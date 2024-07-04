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
using WinFormsApp4;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace FinalkProject
{
    public partial class Form3 : Form
    {
        int receiverMessageheight = 10;
        int userMessageheight = 10;
        static private string connectionString = "Server=sql7.freesqldatabase.com;Port=3306;Database=sql7717504;User=sql7717504;Password=v4GgVVETDJ;";
        string User = null;
        string Receiver = null;
        public Form3(string user, string receiver)
        {
            InitializeComponent();
            User = user;
            Receiver = receiver;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "INSERT INTO Messages (Message, Sender, Receiver, DateOfMessage)\r\nVALUES (@Message, @Sender, @Receiver, @DateOfMessage);";
                        DateTime dateTime = DateTime.Now;
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Message", textBox1.Text);
                            cmd.Parameters.AddWithValue("@Sender", User);
                            cmd.Parameters.AddWithValue("@Receiver", Receiver);
                            cmd.Parameters.AddWithValue("@DateOfMessage", dateTime);
                            CreateMessageBox(textBox1.Text, User, Receiver, true, false);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Data inserted successfully!");

                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
        public void CreateMessageBox(string message,string user, string receiver, bool userMessage, bool receiverMessage)
        {
            if (userMessage)
            {
                Label label = new Label();
                label.Text = message;
                label.Dock = DockStyle.Right;
                label.Height = userMessageheight;
                Controls.Add(label);
                userMessageheight += 50;
            }
            if (receiverMessage)
            {
                Label label = new Label();
                label.Text = message;
                label.Dock = DockStyle.Left;
                label.Height = receiverMessageheight;
                Controls.Add(label);
                receiverMessageheight += 50;
            }
        }
    }
}
