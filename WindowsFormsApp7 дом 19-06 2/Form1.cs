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

namespace Training_test
{
    struct Questions
    {
        public string question;
        public string answer1;
        public string answer2;
        public string answer3;
        public Questions(string q, string a1, string a2, string a3)
        {
            question = q;
            answer1 = a1;
            answer2 = a2;
            answer3 = a3;
        }
    }
    public partial class Form1 : Form
    {
        List<Questions> list = new List<Questions>();
        string[] answers = new string[10];
        int score = 0;
        int k;
        int j;
        public Form1()
        {
            InitializeComponent();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            string[] fileText = (File.ReadAllText(filename)).Split('?', ';');
            int j = 0;
            Questions q = new Questions();
            for (int i = 0; i < fileText.Length - 1; i += 4)
            {
                q.question = fileText[i];
                q.answer1 = fileText[i + 1];
                q.answer2 = fileText[i + 2];
                q.answer3 = fileText[i + 3];
                if (q.answer1.Contains('*'))
                {
                    string[] temp = q.answer1.Split('*');
                    q.answer1 = temp[1];
                    answers[j] = q.answer1;
                    j++;
                }
                else if (q.answer2.Contains('*'))
                {
                    string[] temp = q.answer2.Split('*');
                    q.answer2 = temp[1];
                    answers[j] = q.answer2;
                    j++;
                }
                else if (q.answer3.Contains('*'))
                {
                    string[] temp = q.answer3.Split('*');
                    q.answer3 = temp[1];
                    answers[j] = q.answer3;
                    j++;
                }
                list.Add(q);
            }

            label1.Text = list[0].question;
            radioButton1.Text = list[0].answer1;
            radioButton2.Text = list[0].answer2;
            radioButton3.Text = list[0].answer3;
            k = 1;
            j = 0;

            progressBar1.Maximum = 10;

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Increment(1);
            for (int i = 0; i < list.Count; i++)
            {
                if (label1.Text == list[i].question)
                {
                    if (radioButton1.Checked == true && answers[j] == radioButton1.Text)
                    {
                        score++;
                        j++;
                    }
                    else if (radioButton2.Checked == true && answers[j] == radioButton2.Text)
                    {
                        score++;
                        j++;
                    }
                    else if (radioButton3.Checked == true && answers[j] == radioButton3.Text)
                    {
                        score++;
                        j++;
                    }
                    else
                        j++;
                }
            }
            for(int i = 0; i < list.Count; i++)
            {
                if (i == k)
                {
                    label1.Text = list[i].question;
                    radioButton1.Text = list[i].answer1;
                    radioButton2.Text = list[i].answer2;
                    radioButton3.Text = list[i].answer3;

                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                }
            }
            k++;
            if (progressBar1.Value == progressBar1.Maximum)
            {
                MessageBox.Show(score.ToString() + " from 10", "Results", MessageBoxButtons.OK);
                this.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
