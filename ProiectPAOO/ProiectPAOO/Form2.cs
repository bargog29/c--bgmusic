using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectPAOO
{
    public partial class Form2 : Form
    {
        private static ArrayList idprodus = new ArrayList();
        private static ArrayList categorie = new ArrayList();
        private static ArrayList denumire = new ArrayList();
        private static ArrayList greutate = new ArrayList();
        private static ArrayList garantie = new ArrayList();
        private static ArrayList pret = new ArrayList();
        private object txtFile;

        public Form2()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            getData();
            if (idprodus.Count > 0)
            {
                updateDataGrid();
            }
            else
            {
                MessageBox.Show("Data not found");
            }
        }

        private void getData()
        {
            try
            {
                String str = "server=localhost;port = 3306; database = bgmusic;UID = root;password = oracle";
                String query = "SELECT *FROM produs;";
                MySqlConnection con = new MySqlConnection(str);
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader dbr;
                dbr = cmd.ExecuteReader();
                if (dbr.HasRows)
                {
                    while (dbr.Read())
                    {
                        idprodus.Add(dbr["id_produs"]);
                        categorie.Add(dbr["categorie"].ToString());
                        denumire.Add(dbr["denumire"].ToString());
                        greutate.Add(dbr["greutate"]);
                        garantie.Add(dbr["garantie"].ToString());
                        pret.Add(dbr["pret"]);
                    }
                }
                else
                {
                    MessageBox.Show("Data not found");
                }

                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void updateDataGrid()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < idprodus.Count; i++)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(dataGridView1);
                newRow.Cells[0].Value = idprodus[i];
                newRow.Cells[1].Value = categorie[i];
                newRow.Cells[2].Value = denumire[i];
                newRow.Cells[3].Value = greutate[i];
                newRow.Cells[4].Value = garantie[i];
                newRow.Cells[5].Value = pret[i];
                dataGridView1.Rows.Add(newRow);

            }
            idprodus.Clear();
            categorie.Clear();
            denumire.Clear();
            garantie.Clear();
            pret.Clear();
            greutate.Clear();
        }

        private void getDataFiltered()
        {
            try
            {
                //int pretmin = Convert.ToInt32(textBox1);
                //int pretmax = Convert.ToInt32(textBox5);
                String str = "server=localhost;port = 3306;database = bgmusic;UID = root;password = oracle";
                String query = "SELECT *FROM produs WHERE pret >= " + textBox1.Text + "" +
                    " AND pret <= " + textBox5.Text + ";";
                MySqlConnection con;
                con = new MySqlConnection(str);
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader dbr;
                dbr = cmd.ExecuteReader();
                if (dbr.HasRows)
                {
                    while (dbr.Read())
                    {
                        idprodus.Add(dbr["id_produs"].ToString());
                        categorie.Add(dbr["categorie"].ToString());
                        denumire.Add(dbr["denumire"].ToString());
                        greutate.Add(dbr["greutate"].ToString());
                        garantie.Add(dbr["garantie"].ToString());
                        pret.Add(dbr["pret"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Nu există rezultate");
                }

                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void getDataCautare()
        {
            try
            {
                String str = "server=localhost;port = 3306;database = bgmusic;UID = root;password = oracle";
                String query = "SELECT *FROM produs WHERE denumire LIKE '%" + textBox2.Text + "%'";
                Console.WriteLine(query);
                MySqlConnection con;
                con = new MySqlConnection(str);
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader dbr;
                dbr = cmd.ExecuteReader();
                if (dbr.HasRows)
                {
                    while (dbr.Read())
                    {
                        idprodus.Add(dbr["id_produs"].ToString());
                        categorie.Add(dbr["categorie"].ToString());
                        denumire.Add(dbr["denumire"].ToString());
                        greutate.Add(dbr["greutate"].ToString());
                        garantie.Add(dbr["garantie"].ToString());
                        pret.Add(dbr["pret"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Nu există rezultate");
                }

                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getDataFiltered();
            if (idprodus.Count > 0)
            {
                updateDataGrid();
                textBox1.Text = "";
                textBox5.Text = "";
            }
            else
            {
                MessageBox.Show("Data not found");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection con;
                if (!(textBox4.Text == string.Empty)
                    && !(textBox6.Text == string.Empty)
                    && !(textBox7.Text == string.Empty)
                    && !(textBox8.Text == string.Empty)
                    && !(textBox9.Text == string.Empty))
                {
                    int greutate = Convert.ToInt32(textBox7.Text);
                    int pret = Convert.ToInt32(textBox9.Text);
                    String str = "server=localhost;port=3306;database=bgmusic;UID=root;password=oracle";
                    String query = "INSERT INTO produs(categorie,denumire,greutate,garantie,pret) " +
                        "VALUES('" + textBox4.Text + "','" + textBox6.Text + "','" + greutate + "','" + textBox8.Text + "','" + pret + "');";
                    con = new MySqlConnection(str);
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ai adaugat produsul cu:\n" +
                        "categoria: " + textBox4.Text + "\n" +
                        "denumirea: " + textBox6.Text + "\n" +
                        "greutatea: " + textBox7.Text + "\n" +
                        "garantie: " + textBox8.Text + "\n" +
                        "pret: " + textBox9.Text,
                        "Adaugare produs");
                    textBox4.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
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
            tabControl1.SelectedIndex = 0;
            button2_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection con;
                if (!(textBox3.Text == string.Empty))
                {
                    int id = Convert.ToInt32(textBox3.Text);
                    String str = "server=localhost;port=3306;database=bgmusic;UID=root;password=oracle";
                    String query = "DELETE FROM produs WHERE id_produs=" + id + ";";
                    con = new MySqlConnection(str);
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    textBox3.Text = "";
                    MessageBox.Show("Ai șters produsul cu id-ul " + id.ToString(), "Sterge produs");
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
            button2_Click(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            int id = Convert.ToInt32(textBox3.Text);
            MySqlConnection con;
            String str = "server=localhost;port=3306;database=bgmusic;UID=root;password=oracle";
            String query = "SELECT *FROM produs WHERE id_produs=" + id + ";";
            con = new MySqlConnection(str);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader dbr;
            dbr = cmd.ExecuteReader();
            dbr.Read();
            textBox14.Text = dbr["categorie"].ToString();
            textBox13.Text = dbr["denumire"].ToString();
            textBox12.Text = dbr["greutate"].ToString();
            textBox11.Text = dbr["garantie"].ToString();
            textBox10.Text = dbr["pret"].ToString();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox3.Text);
            int greutate1 = Convert.ToInt32(textBox12.Text);
            int pret1 = Convert.ToInt32(textBox10.Text);
           
            String query = "UPDATE produs " +
                            "SET categorie = '" + textBox14.Text + "',denumire = '" + textBox13.Text + "'," +
                            "greutate = '" + greutate1 + "',garantie='" + textBox11.Text + "'," +
                            "pret = '" + pret1 + "'" +
                        "WHERE id_produs = '" + id + "';";
            MySqlConnection con;
            String str = "server=localhost;port=3306;database=bgmusic;UID=root;password=oracle";
            con = new MySqlConnection(str);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.ExecuteNonQuery();
            textBox14.Text = "";
            textBox13.Text = "";
            textBox12.Text = "";
            textBox11.Text = "";
            textBox10.Text = "";
            MessageBox.Show("Ai actualizat produsul cu id-ul " + id.ToString(), "Actualizare produs");
            tabControl1.SelectedIndex = 0;
            textBox3.Text = "";
            button2_Click(sender, e);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // creating Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Exported from gridview";
            // storing header part in Excel  
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
            // save the application  
            workbook.SaveAs("C:\\Users\\georg\\Desktop\\produse.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            //app.Quit();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                //Build the CSV file data as a Comma separated string.
                string csv = string.Empty;

                //Add the Header row for CSV file.
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    csv += column.HeaderText + ',';
                }
                //Add new line.
                csv += "\r\n";

                //Adding the Rows

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null)
                        {
                            //Add the Data rows.
                            csv += cell.Value.ToString().Replace(",", ";") + ',';
                        }
                        // break;
                    }
                    //Add new line.
                    csv += "\r\n";
                }

                //Exporting to CSV.
                string folderPath = "C:\\Users\\georg\\Desktop\\";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                File.WriteAllText(folderPath + "produse.csv", csv);
                MessageBox.Show("Fisierul a fost creat si exportat pe Desktop");
            }
            catch
            {
                MessageBox.Show("");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MySqlConnection con;
            String str = "server=localhost;port=3306;database=bgmusic;UID=root;password=oracle";
            con = new MySqlConnection(str);
            con.Open();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                try
                {
                    //textBox15.Text = System.IO.Path.GetFullPath(fileName);
                    using (StreamReader reader = new StreamReader(System.IO.Path.GetFullPath(fileName)))
                    {
                        var lineNumber = 0;
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            if (lineNumber != 0)
                            {
                                var values = line.Split(',');

                                string sql = "INSERT INTO bgmusic.produs(categorie,denumire,greutate,garantie,pret) " +
                                "VALUES ('" + values[0] + "','" + values[1] + "'," + values[2] + ",'" + values[3] + "','" + values[4] + "')";

                                MySqlCommand cmd = new MySqlCommand(sql, con);
                                cmd.CommandText = sql;
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.Connection = con;
                                cmd.ExecuteNonQuery();
                            }
                            lineNumber++;
                        }
                    }
                    con.Close();
                }
                catch (System.Exception)
                {
                }
                MessageBox.Show("Datele au fost importate");
                button2_Click(sender, e);
            }
            //Put your file location here:
            

            
            Console.ReadLine();
        }
    

        private void BindData(string filePath)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            getDataCautare();
            if (idprodus.Count > 0)
            {
                updateDataGrid();
            }
            else
            {
                MessageBox.Show("Data not found");
            }
            textBox2.Text = "";
        }



        private void button11_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Series.Add("Categorie");
            string Query = "SELECT categorie, COUNT(id_produs) as NumProd " +
                "FROM produs GROUP BY categorie";
            MySqlConnection con;
            String str = "server=localhost;port=3306;database=bgmusic;UID=root;password=oracle";
            con = new MySqlConnection(str);
            
            MySqlCommand cmdDataBase = new MySqlCommand(Query, con);
            MySqlDataReader myReader;
            
            try
            {
                con.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    this.chart1.Series["Categorie"].Points.AddXY(myReader.GetString("categorie"), myReader.GetString("NumProd"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
