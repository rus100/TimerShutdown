using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace TimerShutdown
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string messag = "";
        bool v = true;
        int time1 = 0;
        int hour = 0;
        int minute = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            time1 = 0;
            hour = DateTime.Now.Hour;
            minute = DateTime.Now.Minute;
            timer1.Start();
            timer1.Interval = 1000;
            messag = textBox1.Text;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                v = false;
            }
            else { v = true; }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            time1++;
            if (v) {
                if (time1 >= 3600 * (numericUpDown1.Value - hour) + 60 * (numericUpDown2.Value - minute)) { timer1.Stop(); }
                progressBar1.Value = (int)(((double)time1 / (double)(3600 * (numericUpDown1.Value - hour) + 60*(numericUpDown2.Value - minute))) * 100);
            }
            else {
                if (time1 >= 3600 * numericUpDown1.Value + 60 * numericUpDown2.Value) { timer1.Stop(); }
                progressBar1.Value = (int)(((double)time1 / (double)(3600 * numericUpDown1.Value + 60*numericUpDown2.Value)) * 100);
            }
            if (v)
            {
                if (60 * hour + minute >= 60 * numericUpDown1.Value +numericUpDown2.Value)
                {    timer1.Stop();
                    MessageBox.Show("Время уже прошло");
                    time1 = 0;
                }
                else
                {
                    if (time1 >= 3600 * (numericUpDown1.Value - hour) + 60 * (numericUpDown2.Value - minute))
                    {
                        if (comboBox1.SelectedIndex==0)
                        {
                            string command = "shutdown -r";
                            System.Diagnostics.Process.Start("cmd.exe", "/C " + command);
                        }
                        if (comboBox1.SelectedIndex == 1)
                        {
                            string command = "shutdown -s";
                            System.Diagnostics.Process.Start("cmd.exe", "/C " + command);
                        }
                        if (comboBox1.SelectedIndex == 2)
                        {
                            MessageBox.Show(messag);
                        }
                    }

                }
            }
            else
            {
                if ((hour + numericUpDown1.Value) * 60 + minute + numericUpDown2.Value >= 1439)
                { timer1.Stop();
                    MessageBox.Show("Установите другое время");
                time1 = 0;
                }
                else
                {
                    if (time1 >= 3600 * numericUpDown1.Value + 60 * numericUpDown2.Value)
                    {
                        if (comboBox1.SelectedIndex == 0)
                        {
                            string command = "shutdown -r";
                            System.Diagnostics.Process.Start("cmd.exe", "/C " + command);
                        }
                        if (comboBox1.SelectedIndex == 1)
                        {
                            string command = "shutdown -s";
                            System.Diagnostics.Process.Start("cmd.exe", "/C " + command);
                        }
                        if (comboBox1.SelectedIndex == 2)
                        {
                            MessageBox.Show(messag);
                        }
                    }
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 2) { textBox1.Enabled = true; }
            if (comboBox1.SelectedIndex == 1) { textBox1.Text = "";
                textBox1.Enabled = false; }
            if (comboBox1.SelectedIndex == 0) { textBox1.Text = "";
                textBox1.Enabled = false; }
        }
private void notifyIcon1_Click(object sender, EventArgs e)
{
        Show();
        this.WindowState = FormWindowState.Normal;
}

private void Form1_Resize(object sender, EventArgs e)
{        notifyIcon1.Icon= TimerShutdown.Properties.Resources.shutdown;
    if (this.WindowState == FormWindowState.Minimized)
    {
        Hide();
    } 
}

private void radioButton1_CheckedChanged(object sender, EventArgs e)
{
    if (radioButton1.Checked)
    {
        v = true;
    }
    else { v = false; }
}
    }
}
