using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectPAOO
{
    public partial class ReLogin : Form
    {
        public ReLogin()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                MySqlConnection con;
                if (!(textBox1.Text == string.Empty))
                {
                    if (!(textBox2.Text == string.Empty))
                    {
                        String str = "server=localhost;port=3306;database=bgmusic;UID=root;password=oracle";
                        String query = "SELECT *FROM user WHERE username = '" + textBox1.Text + "' and password = '" + textBox2.Text + "';";
                        con = new MySqlConnection(str);
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand(query, con);
                        MySqlDataReader dbr;
                        dbr = cmd.ExecuteReader();
                        int count = 0;
                        while (dbr.Read())
                        {
                            count = count + 1;
                        }
                        if (count == 1)
                        {
                            Form2 f2 = new Form2();
                            f2.Show();
                            Hide();
                        }
                        else if (count > 1)
                        {
                            MessageBox.Show("Username si parola duplicat", "ReLogin");
                        }
                        else
                        {
                            MessageBox.Show("Username si parola incorecte", "ReLogin");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nu ați completat password", "ReLogin");
                    }
                }
                else
                {
                    MessageBox.Show("Nu ați completat username", "ReLogin");
                }
             //   con.Close();  
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
            
        }

        private void ReLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection con;
                        if (!(textBox3.Text == string.Empty) && !(textBox4.Text == string.Empty) && !(textBox5.Text == string.Empty))
                        {
                            String str = "server=localhost;port=3306;database=bgmusic;UID=root;password=oracle";
                            String query = "INSERT INTO user(nume,username,password,nivel_acces) VALUES('"+textBox3.Text+ "','"+textBox4.Text+"','"+textBox5.Text+"',2);";
                            con = new MySqlConnection(str);
                            con.Open();
                            MySqlCommand cmd = new MySqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    /*int count = 0;
                    while (dbr.Read())
                    {
                        count = count + 1;
                    }
                    if (count == 1)
                    {
                        Form2 f2 = new Form2();
                        f2.Show();
                        Hide();
                    }*/

                }
                    //   con.Close();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.PasswordChar = '*';
        }
    }
}
