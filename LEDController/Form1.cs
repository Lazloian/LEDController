using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO.Ports;
using System.Windows.Forms;
using System.Diagnostics;

namespace LEDController
{
    public partial class Form1 : Form
    {
        SerialPort sp = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
        IDictionary<string, int> r = new Dictionary<string, int>();
        IDictionary<string, int> g = new Dictionary<string, int>();
        IDictionary<string, int> b = new Dictionary<string, int>();
        Random random = new Random();

        int random_button = 0;
        int r_wave = 85;
        int g_wave = 170;
        int b_wave = 255;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sp.Open();
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Public\Documents\Saves.txt");
            int g_place = 0,
                b_place = 0,
                space_place = 0,
                red = 0,
                green = 0,
                blue = 0,
                green_stop = 0,
                blue_stop = 0;
            string color = "";


            foreach (string line in lines)
            {
                if (line == "") //skips the new line character
                {

                }
                else if (line.Substring(0, 1) == "r")
                {
                    System.Diagnostics.Debug.WriteLine(line.Length);
                    System.Diagnostics.Debug.WriteLine(line.Substring(1, 1));
                    for (int i = 0; i < line.Length; i++)
                    {
                        if ((line.Substring(i, 1) == "g") && (green_stop == 0))
                        {
                            g_place = i;
                            red = Int32.Parse(line.Substring(1, g_place - 1));
                            green_stop = 1;
                        }
                        else if ((line.Substring(i, 1) == "b") && (blue_stop == 0))
                        {
                            b_place = i;
                            green = Int32.Parse(line.Substring(g_place + 1, b_place - g_place - 1));
                            blue_stop = 1;
                        }
                        else if (line.Substring(i, 1) == " ")
                        {
                            space_place = i;
                            blue = Int32.Parse(line.Substring(b_place + 1, space_place - b_place - 1));
                            color = line.Substring(space_place + 1, line.Length - space_place - 1);
                        }
                    }
                    r.Add(color, red);
                    g.Add(color, green);
                    b.Add(color, blue);
                    comboBox1.Items.Add(color);
                    System.Diagnostics.Debug.WriteLine("Color " + color + " Added (" + red + " " + green + " " + blue + ")");
                    green_stop = 0;
                    blue_stop = 0;
                }
            }
            System.Diagnostics.Debug.WriteLine("Form Loaded");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sp.Write("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sp.Write("0");
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = "" + trackBar1.Value;
            System.Diagnostics.Debug.WriteLine("r" + trackBar1.Value);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = "" + trackBar2.Value;
            System.Diagnostics.Debug.WriteLine("g" + trackBar2.Value);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            textBox3.Text = "" + trackBar3.Value;
            System.Diagnostics.Debug.WriteLine("b" + trackBar3.Value);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            trackBar1.Value = Int32.Parse(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            trackBar2.Value = Int32.Parse(textBox2.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            trackBar3.Value = Int32.Parse(textBox3.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            sp.Write("r" + trackBar1.Value + "g" + trackBar2.Value + "b" + trackBar3.Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            r.Add(textBox4.Text, trackBar1.Value);
            g.Add(textBox4.Text, trackBar2.Value);
            b.Add(textBox4.Text, trackBar3.Value);
            comboBox1.Items.Add(textBox4.Text);
            System.IO.File.AppendAllText(@"C:\Users\Public\Documents\Saves.txt", "r" + trackBar1.Value + "g" + trackBar2.Value + "b" + trackBar3.Value + " " + textBox4.Text + "\r\n");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sp.Write("r" + r[comboBox1.Text] + "g" + g[comboBox1.Text] + "b" + b[comboBox1.Text]);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Timers.Timer rTimer = new System.Timers.Timer(2000);
            rTimer.AutoReset = true;

            rTimer.Enabled = true;
            rTimer.AutoReset = true;
            rTimer.Elapsed += RandomColors;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Public\Documents\Saves.txt");

            string color = "";

            foreach (string line in lines) {
                if (line == "") //skips new line character
                {
                }
                else if (line.Substring(0 ,1) == "r") {
                    for (int i = 0; i < line.Length; i++) {
                        if (line.Substring(i, 1) == " ") {
                            color = line.Substring(i + 1, line.Length - i - 1);
                            System.Diagnostics.Debug.WriteLine(color);
                            if (color != comboBox1.Text) {
                                System.IO.File.AppendAllText(@"C:\Users\Public\Documents\Overwrite.txt", line + "\r\n");

                            }
                        }
                    }
                }
            }
            r.Remove(comboBox1.Text);
            g.Remove(comboBox1.Text);
            b.Remove(comboBox1.Text);
            comboBox1.Items.Remove(comboBox1.Text);

            System.IO.File.WriteAllText(@"C:\Users\Public\Documents\Saves.txt", "");
            string[] newLines = System.IO.File.ReadAllLines(@"C:\Users\Public\Documents\Overwrite.txt");

            foreach (string line in newLines)
            {
                System.IO.File.AppendAllText(@"C:\Users\Public\Documents\Saves.txt", line + "\r\n");
            }
            //Clear the overwrite file after deletion is complete
            System.IO.File.WriteAllText(@"C:\Users\Public\Documents\Overwrite.txt", "");
        }

        private void RandomColors(Object source, ElapsedEventArgs e)
        {
            int r = random.Next(0, 256);
            int g = random.Next(0, 256);
            int b = random.Next(0, 256);
            sp.Write("r" + r + "g" + g + "b" + b);
        }

        private void ColorWave(Object source, ElapsedEventArgs e)
        {
            r_wave += 17;
            g_wave += 17;
            b_wave += 17;

            if (r_wave > 255)
            {
                r_wave = r_wave - 255;
            }
            if (g_wave > 255)
            {
                g_wave = g_wave - 255;
            }
            if (b_wave > 255)
            {
                b_wave = b_wave - 255;
            }

            sp.Write("r" + r_wave + "g" + g_wave + "b" + b_wave);
        }
    }
}