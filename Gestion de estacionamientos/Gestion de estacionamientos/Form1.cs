using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpreadsheetLight;

namespace Gestion_de_estacionamientos
{
    public partial class Form1 : Form
    {
        public int n = 256; //esta variable asigna el tamaño de la tabla
        public string[] patente; //esta vector guarda las patentes
        public DateTime[] horaLlegada;
        public DateTime horaSalida;
        public double differenceMinutes;
        public int precio = 30;
        public int eraseCount = 0;
        public List<string> excelPatente;
        public List<string> excelLlegada;
        public List<string> excelSalida;
        public List<string> excelPrecio;
        public int cambiosExcel = 0;
        public int verificaCambios = 0;

        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.Text = "Gestor de estacionamientos";
            precio = System.Convert.ToInt32(System.IO.File.ReadAllText(@"C:\Users\Alejandro\source\repos\Gestion de estacionamientos\config.txt"));
            label5.Text = "VALOR POR MINUTO: $" + precio;
            

            patente = new String[256];
            horaLlegada = new DateTime[256];
            excelPatente = new List<string>();
            excelLlegada = new List<string>();
            excelSalida = new List<string>();
            excelPrecio = new List<string>();

            Console.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //textbox matricula
        }



        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //Btn de marcar llegada
            if (!string.IsNullOrEmpty(textBox1.Text))
            {

                for (int i = listView1.Items.Count; i < 256 ; i++)
                {
                    if (string.IsNullOrEmpty(patente[i])) {
                        patente[i] = textBox1.Text; //variable patente recibe el valor de lo que hay en el textbox
                        Console.WriteLine("patente = " + patente[i]);

                        horaLlegada[i] = DateTime.Now; //horaLlegada variable interna

                        if (String.IsNullOrEmpty(textBox3.Text)) //verifica si el text box esta vacio, si lo esta, lo escribe solo
                        {
                            textBox3.Text = DateTime.Now.ToString("hh:mm");
                        }
                        Console.WriteLine("Hora Llegada: " + horaLlegada[i]);

                        //Pasa valores a la lista
                        listView1.Items.Add(textBox1.Text);
                        listView1.Items[listView1.Items.Count - 1].SubItems.Add(DateTime.Now.ToString("hh:mm"));

                        //limpia textbox
                        textBox1.Clear();
                        textBox3.Clear();

                        break;
                    }
                
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)//btn marcar salida
        {
            var item = listView1.FindItemWithText(textBox2.Text);
            if (item.Text == textBox2.Text && !string.IsNullOrEmpty(item.Text))
            {
                //Guarda valores para el excel

                excelPatente.Add(textBox2.Text);
                excelLlegada.Add(label10.Text);
                excelSalida.Add(label13.Text);
                excelPrecio.Add(label14.Text);

                Console.WriteLine("Lista :" + excelPatente[0] + ", " + excelLlegada[0] + ", " + excelSalida[0] + ", " + excelPrecio[0]);

                //------------------------

                cambiosExcel++;
                item.Remove();
                eraseCount++;
                int index = item.Index;
                textBox2.Clear(); //limpia label
            }
            //listView1.Items[i].Remove();

            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)//Al clickear algo en la lista
        {
            if(listView1.SelectedItems.Count > 0)
            {
                RellenarCamposSalida();
            }
            
        }

        private void button3_Click(object sender, EventArgs e) //btn buscar
        {
            for(int i = 0; i < listView1.Items.Count; i++) 
            {
                listView1.Items[i].Selected = false;
            }
            var item = listView1.FindItemWithText(textBox4.Text, false, 0, true);
            if (item != null)
            {
                for(int j = 0; j < listView1.Items.Count; j++)
                {
                    listView1.Items[j].ForeColor = System.Drawing.Color.Black; //devuelve el color a negro
                }
                item.ForeColor = System.Drawing.Color.Blue;
                item.EnsureVisible();
                //item.Selected = true;
                //listView1.Select();
                
                //textBox2.Text = item.Text;
            }
            else
            {
                MessageBox.Show("La patente " + textBox4.Text + " no se encuentra", "Error");
            }
            textBox4.Clear();
        }

        private void RellenarCamposSalida() //Pide el text box desde el cual se escribe
        {
            textBox2.Clear();
            textBox2.Text = listView1.FocusedItem.Text; //patente
            label10.Text = listView1.FocusedItem.SubItems[1].Text; //hra de llegada
            label13.Text = DateTime.Now.ToString("hh:mm"); //hra de salida

            horaSalida = DateTime.Now; //variable interna

           // Console.WriteLine("INDEX FOCUS:" + listView1.FocusedItem.Index);

            for (int i = 0; i < listView1.Items.Count + eraseCount; i++)
            {
                if (patente[i] == textBox2.Text)
                {
                    TimeSpan ts = horaSalida - horaLlegada[i];
                    differenceMinutes = ts.TotalMinutes;
                    //Console.WriteLine(differenceMinutes);
                    //Console.WriteLine("INDEX : " + listView1.Items[i].Index);

                }

            }



            int valorFinal = Convert.ToInt32(differenceMinutes) * precio;

            label14.Text = ("$") + valorFinal.ToString();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 priceform = new Form2();
            priceform.Show();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == (int)Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void button3_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == (int)Keys.Enter)
            {
                button3.PerformClick();
            }
        }

        private void button4_Click(object sender, EventArgs e)//btn generar excel
        {
            var fecha = DateTime.Now.Date.ToShortDateString();
            string pathFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Registro (" + fecha + ").xlsx";
            //string pathFile = AppDomain.CurrentDomain.BaseDirectory + "Registro (" + fecha + ").xlsx";

            if (System.IO.File.Exists(pathFile))
            {
                if (MessageBox.Show("Ya existe un registro, ¿Desea reemplazarlo?",
                       "Advertencia",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    GenerarExcel(pathFile);
                }
                //MessageBox.Show("Ya existe un registro para este dia, borrelo si quiere hacer uno nuevo", "Error");
            }
            else
            {
                GenerarExcel(pathFile);
            }

            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(cambiosExcel != verificaCambios)
            {
                if (MessageBox.Show("Existen cambios sin guardar, ¿Desea salir igualmente?",
                       "Advertencia",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        public void GenerarExcel(string pathFile)
        {
            SLDocument oSLDocument = new SLDocument();

            System.Data.DataTable dt = new System.Data.DataTable();

            //Columnas
            dt.Columns.Add("Patente", typeof(string));
            dt.Columns.Add("Hora Llegada", typeof(string));
            dt.Columns.Add("Hora Salida", typeof(string));
            dt.Columns.Add("Valor", typeof(string));

            //Filas
            for (int i = 0; i < excelPatente.Count(); i++)
            {
                dt.Rows.Add(excelPatente[i], excelLlegada[i], excelSalida[i], excelPrecio[i]);
            }
            //dt.Rows.Add("", "", "", System.Convert.ToString(precioTotal));



            oSLDocument.ImportDataTable(1, 1, dt, true);
            oSLDocument.SaveAs(pathFile);

            MessageBox.Show("Excel creado en la ruta: \n" + pathFile, "Mensaje");

            verificaCambios = cambiosExcel; //iguala las variables para confirmar que estan todos los cambios guardados
        }
    }

    

}