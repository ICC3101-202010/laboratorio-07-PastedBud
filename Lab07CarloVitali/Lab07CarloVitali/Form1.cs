using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab07CarloVitali
{
    public partial class calculadora : Form
    {
        double primero;
        double segundo;
        double resultado;
        string operacion;
        

        

        public calculadora()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void b0_Click(object sender, EventArgs e)
        {
            pantalla.Text = pantalla.Text + "0";
        }

        private void b1_Click(object sender, EventArgs e)
        {
            pantalla.Text = pantalla.Text + "1";
        }

        private void b2_Click(object sender, EventArgs e)
        {
            pantalla.Text = pantalla.Text + "2";
        }

        private void b3_Click(object sender, EventArgs e)
        {
            pantalla.Text = pantalla.Text + "3";
        }

        private void b4_Click(object sender, EventArgs e)
        {
            pantalla.Text = pantalla.Text + "4";
        }

        private void b5_Click(object sender, EventArgs e)
        {
            pantalla.Text = pantalla.Text + "5";
        }

        private void b6_Click(object sender, EventArgs e)
        {
            pantalla.Text = pantalla.Text + "6";
        }

        private void b7_Click(object sender, EventArgs e)
        {
            pantalla.Text = pantalla.Text + "7";
        }

        private void b8_Click(object sender, EventArgs e)
        {
            pantalla.Text = pantalla.Text + "8";
        }

        private void b9_Click(object sender, EventArgs e)
        {
            pantalla.Text = pantalla.Text + "9";
        }

        private void bpunto_Click(object sender, EventArgs e)
        {
            pantalla.Text = pantalla.Text + ",";
        }

        private void bmas_Click(object sender, EventArgs e)
        {
            try
            {
                operacion = "+";
                primero = double.Parse(pantalla.Text);
                pantalla.Clear();
            }
            catch (System.FormatException)
            {
                pantalla.Text = "Syntax ERROR";
                
            }
            
        }
        

        private void bmenos_Click(object sender, EventArgs e)
        {
            try
            {
                operacion = "-";
                primero = double.Parse(pantalla.Text);
                pantalla.Clear();
            }
            catch (System.FormatException)
            {
                pantalla.Text = "Syntax ERROR";
               
            }
            
        }

        private void bpor_Click(object sender, EventArgs e)
        {
            try
            {
                operacion = "X";
                primero = double.Parse(pantalla.Text);
                pantalla.Clear();
            }
            catch (System.FormatException)
            {
                pantalla.Text = "Syntax ERROR";
                
            }
            
        }

        private void bdiv_Click(object sender, EventArgs e)
        {
            try
            {
                operacion = "/";
                primero = double.Parse(pantalla.Text);
                pantalla.Clear();
            }
            catch (System.FormatException)
            {
                pantalla.Text = "Syntax ERROR";
                
            }
            
        }

        private void bigual_Click(object sender, EventArgs e)
        {
            segundo = double.Parse(pantalla.Text);
            switch (operacion)
            {
                case "+":
                    resultado = primero + segundo;
                    pantalla.Text = resultado.ToString();
                    break;

                case "-":
                    resultado = primero - segundo;
                    pantalla.Text = resultado.ToString();
                    break;

                case "/":
                    if (segundo.ToString() == "0")
                    {
                        pantalla.Text = "Math ERROR";
                        break;
                    }
                    else
                    {
                        resultado = primero / segundo;
                        pantalla.Text = resultado.ToString();
                        break;
                    }
                    

                case "X":
                    resultado = primero * segundo;
                    pantalla.Text = resultado.ToString();
                    break;
            }
            
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("resultados.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                Resultados resultados = (Resultados)formatter.Deserialize(stream);
                stream.Close();

                resultados.resultados.Add(resultado.ToString());

                IFormatter formatter1 = new BinaryFormatter();
                Stream stream1 = new FileStream("resultados.bin", FileMode.Create, FileAccess.Write, FileShare.None);
                formatter1.Serialize(stream1, resultados);
                stream1.Close();
            }
            catch (FileNotFoundException ex)
            {
                Resultados resultados = new Resultados();

                resultados.resultados.Add(resultado.ToString());

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("resultados.bin", FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, resultados);
                stream.Close();
            }
        }

        private void bac_Click(object sender, EventArgs e)
        {
            pantalla.Clear();
        }

        private void braiz_Click(object sender, EventArgs e)
        {
            operacion = "raiz";
            primero = double.Parse(pantalla.Text);
            resultado = primero;
            pantalla.Text = Math.Sqrt(primero).ToString();
        }

        private void bdel_Click(object sender, EventArgs e)
        {
            if (pantalla.Text.Length == 1)
            {
                pantalla.Text = "";
            }
            else if (pantalla.Text.Length > 1)
            {
                pantalla.Text = pantalla.Text.Remove(pantalla.Text.Length - 1, 1);
            }
        }

        private void bans_Click(object sender, EventArgs e)
        {
            pantalla.Text = resultado.ToString();
        }

        private void bhistorial_Click(object sender, EventArgs e)
        {
            if (pantalla2.Visible == false)
            {
                bborrarhistorial.Visible = true;
                pantalla2.Visible = true;
                IFormatter formatter1 = new BinaryFormatter();
                Stream stream1 = new FileStream("resultados.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                Resultados resultados = (Resultados)formatter1.Deserialize(stream1);
                stream1.Close();
                pantalla2.Text = string.Join( "    Resultado:  ", resultados.resultados );
                 
                
            }
            else if (pantalla2.Visible == true)
            {
                pantalla2.Visible = false;
                bborrarhistorial.Visible = false;
            }
            
        }

        private void bborrarhistorial_Click(object sender, EventArgs e)
        {
            Resultados resultados = new Resultados();

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("resultados.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, resultados);
            stream.Close();

            pantalla2.Visible = false;
            bborrarhistorial.Visible = false;
        }
    }
}
