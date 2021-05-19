using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_de_estacionamientos
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.Text = "Cambiar precio";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllText(@"C:\Users\Alejandro\source\repos\Gestion de estacionamientos\config.txt", textBox1.Text);
            MessageBox.Show("Para que los cambios surgan efecto debe reiniciar la aplicacion", "Advertencia");
            this.Close();
        }
    }
}
