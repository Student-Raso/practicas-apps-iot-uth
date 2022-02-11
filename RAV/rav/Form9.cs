﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace rav
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }
        WebBrowser webBrowser1 = new WebBrowser();
        private void Form9_Load(object sender, EventArgs e)
        {
            webBrowser1.Left = 12;
            webBrowser1.Top = 53;
            webBrowser1.Width = 754;
            webBrowser1.Height = 480;
            webBrowser1.ScrollBarsEnabled = true;
            webBrowser1.Visible = true;
            webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.Controls.Add(this.webBrowser1);
            button1_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
        string archivo = Directory.GetCurrentDirectory() + "\\ReporteAplicacion.htm";
        private void button1_Click(object sender, EventArgs e)
        {//Generar
            int total = 0;
            StreamWriter arch = new StreamWriter(archivo);
            arch.WriteLine("<html>REPORTE DE APLICACIONES<br>Fecha: " + System.DateTime.Now.ToString() + "<br><br>");
            arch.WriteLine("<table border=1 cellspacing=0>");
            arch.WriteLine("<tr><td>ID_APLICACION</td><td>RESPONSABLE</td><td>ID_PRUEBA</td><td>APLICADOR</td>" +
                "<td>FOLIO</td><td>APLICADA</td><td>FIRMA</td><td>VERSION</td></tr>");
            string connectionString = "datasource=localhost;port=3306;username=root;password=123456;database=repositorio;";
            string query = "Select * from aplicaciones";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            MySqlDataReader reader;
            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        arch.WriteLine("<tr><td>" + reader.GetString(0) + "</td><td>" + reader.GetString(1)
                                    + "</td><td>" + reader.GetString(2) + "</td><td>" + reader.GetString(3)
                                    + "</td><td>" + reader.GetString(4) + "</td><td>" + reader.GetString(5)
                                    + "</td><td>" + reader.GetString(6) + "</td><td>" + reader.GetString(7) + "</td></tr>");
                        total++;
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron datos.");
                }
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            arch.WriteLine("</table></html>");
            arch.WriteLine("Total de registros: " + total.ToString());
            arch.Close();
            Uri dir = new Uri(archivo);
            webBrowser1.Url = dir;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Program Files\Microsoft Office\root\Office16\excel",
                "\"" + archivo + "\"");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome",
                "\"" + archivo + "\"");
        }
    }
}
