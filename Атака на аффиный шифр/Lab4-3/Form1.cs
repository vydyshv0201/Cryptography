using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab4_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string RusAL = ".абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        string RusALZ = ".АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        string[] labels = { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
        int[] count1 = new int[33];
        int[] count2 = new int[33];


        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 33; i++)
            {
                count1[i] = 0;
                count2[i] = 0;
            }


            for (int i = 0; i < textBox1.Text.Length; i++)
            {
                int l = -1;
                if (Regex.IsMatch(Convert.ToString(textBox1.Text[i]), @"^[а-яА-ЯёЁ]+$") == true)
                {
                    int j = 0;
                    while (l == -1)
                    {
                        if (textBox1.Text[i] == RusAL[j])
                        {
                            l = j;
                            count1[j - 1]++;
                        }
                        if (textBox1.Text[i] == RusALZ[j])
                        {
                            l = j;
                            count1[j - 1]++;
                        }
                        j++;
                    }

                }



            }

            for (int i = 0; i < textBox2.Text.Length; i++)
            {
                int l = -1;
                if (Regex.IsMatch(Convert.ToString(textBox2.Text[i]), @"^[а-яА-ЯёЁ]+$") == true)
                {
                    int j = 0;
                    while (l == -1)
                    {
                        if (textBox2.Text[i] == RusAL[j])
                        {
                            l = j;
                            count2[j - 1]++;
                        }
                        if (textBox2.Text[i] == RusALZ[j])
                        {
                            l = j;
                            count2[j - 1]++;
                        }
                        j++;
                    }

                }
            }
            int[] sort1 = new int[33];

            for (int i = 0; i < 33; i++)
            {
                sort1[i] = count1[i];
            }

            for (int i = 0; i < sort1.Length - 1; i++)
            {
                for (int j = i + 1; j < sort1.Length; j++)
                {
                    if (sort1[i] < sort1[j])
                    {
                        int temp = sort1[i];
                        sort1[i] = sort1[j];
                        sort1[j] = temp;
                    }
                }
            }

            int[] sort2 = sort1.Distinct().ToArray();
            List<string> zn = new List<string>();

            for (int i = 0; i < sort2.Length; i++)
            {
                string str1 = "", str2 = "";
                for (int j = 0; j < count1.Length; j++)
                {
                    if (sort2[i] == count1[j])
                    {
                        str1 = str1 + labels[j] + " ";
                    }
                    if (sort2[i] == count2[j])
                    {
                        str2 = str2 + labels[j] + " ";
                    }
                }
                zn.Add(str1 + " = " + str2);
            }

            for (int i = 0; i < zn.Count; i++)
            {
                textBox3.AppendText(zn[i] + "\r\n");
            }

            for (int i = 0; i < 33; i++)
            {
                chart1.Series[0].Points.AddY(count1[i]);
                chart1.ChartAreas[0].AxisX.CustomLabels.Add(new CustomLabel(i, i + 2, labels[i], 0, LabelMarkStyle.LineSideMark));
            }
            for (int i = 0; i < 33; i++)
            {
                chart2.Series[0].Points.AddY(count2[i]);
                chart2.ChartAreas[0].AxisX.CustomLabels.Add(new CustomLabel(i, i + 2, labels[i], 0, LabelMarkStyle.LineSideMark));
            }






            int[,] bgr1 = new int[33, 33];
            int[,] bgr2 = new int[33, 33];

            for (int i = 0; i < textBox1.Text.Length - 1; i++)
            {
                int l = -1;
                int n = -1;
                if ((Regex.IsMatch(Convert.ToString(textBox1.Text[i]), @"^[а-яА-ЯёЁ]+$") == true) && (Regex.IsMatch(Convert.ToString(textBox1.Text[i + 1]), @"^[а-яА-ЯёЁ]+$") == true))
                {
                    int j = 0;
                    while (l == -1)
                    {
                        if (textBox1.Text[i] == RusAL[j])
                        {
                            l = j;
                        }
                        if (textBox1.Text[i] == RusALZ[j])
                        {
                            l = j;
                        }
                        j++;
                    }

                    int g = 0;
                    while (n == -1)
                    {
                        if (textBox1.Text[i + 1] == RusAL[g])
                        {
                            n = g;
                        }
                        if (textBox1.Text[i + 1] == RusALZ[g])
                        {
                            n = g;
                        }
                        g++;
                    }
                    bgr1[l-1, n-1]++;

                }

            }

            for (int i = 0; i < textBox2.Text.Length - 1; i++)
            {
                int l = -1;
                int n = -1;
                if ((Regex.IsMatch(Convert.ToString(textBox2.Text[i]), @"^[а-яА-ЯёЁ]+$") == true) && (Regex.IsMatch(Convert.ToString(textBox2.Text[i + 1]), @"^[а-яА-ЯёЁ]+$") == true))
                {
                    int j = 0;
                    while (l == -1)
                    {
                        if (textBox2.Text[i] == RusAL[j])
                        {
                            l = j;
                        }
                        if (textBox2.Text[i] == RusALZ[j])
                        {
                            l = j;
                        }
                        j++;
                    }

                    int g = 0;
                    while (n == -1)
                    {
                        if (textBox2.Text[i + 1] == RusAL[g])
                        {
                            n = g;
                        }
                        if (textBox2.Text[i + 1] == RusALZ[g])
                        {
                            n = g;
                        }
                        g++;
                    }
                    bgr2[l-1, n-1]++;

                }

            }

            

            for (int i=0; i<33; i++)
            {
                dataGridView1.Rows.Add(labels[i], bgr1[i, 0], bgr1[i, 1], bgr1[i, 2], bgr1[i, 3], bgr1[i, 4], bgr1[i, 5], bgr1[i, 6], bgr1[i, 7], bgr1[i, 8], bgr1[i, 9], bgr1[i, 10], bgr1[i, 11], bgr1[i, 12], bgr1[i, 13], bgr1[i, 14], bgr1[i, 15], bgr1[i, 16], bgr1[i, 17], bgr1[i, 18], bgr1[i, 19], bgr1[i, 20], bgr1[i, 21], bgr1[i, 22], bgr1[i, 23], bgr1[i, 24], bgr1[i, 25], bgr1[i, 26], bgr1[i, 27], bgr1[i, 28], bgr1[i, 29], bgr1[i, 30], bgr1[i, 31], bgr1[i, 32]);
                dataGridView2.Rows.Add(labels[i], bgr2[i, 0], bgr2[i, 1], bgr2[i, 2], bgr2[i, 3], bgr2[i, 4], bgr2[i, 5], bgr2[i, 6], bgr2[i, 7], bgr2[i, 8], bgr2[i, 9], bgr2[i, 10], bgr2[i, 11], bgr2[i, 12], bgr2[i, 13], bgr2[i, 14], bgr2[i, 15], bgr2[i, 16], bgr2[i, 17], bgr2[i, 18], bgr2[i, 19], bgr2[i, 20], bgr2[i, 21], bgr2[i, 22], bgr2[i, 23], bgr2[i, 24], bgr2[i, 25], bgr2[i, 26], bgr2[i, 27], bgr2[i, 28], bgr2[i, 29], bgr2[i, 30], bgr2[i, 31], bgr2[i, 32]);
            }


            for (int i = 0; i < 33; i++)
            {
                dataGridView1.Columns[i].Width = 20;
                dataGridView2.Columns[i].Width = 20;

            }



        }
    }
}
