using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xml_save
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;  // Вывод формы по центру экрана
        }

        private void button1_Click(object sender, EventArgs e)  // Добавление данных в форму
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Заполните все поля!", "Ошибка");
            }
            else
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = textBox1.Text; // Фамилия
                dataGridView1.Rows[n].Cells[1].Value = textBox2.Text; // Имя
                dataGridView1.Rows[n].Cells[2].Value = numericUpDown1.Value; // Доход
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet(); // Создаем пустой кэш данных
                DataTable dt = new DataTable(); // Создаем пустую таблицу данных
                dt.TableName = "Report"; // Название таблицы
                dt.Columns.Add("Surname"); // Название колонок
                dt.Columns.Add("Name");
                dt.Columns.Add("Income");
                ds.Tables.Add(dt); // В ds создается таблица, с названием и колонками

                foreach (DataGridViewRow r in dataGridView1.Rows) //Пока в dataGridView есть строки
                {
                    DataRow row = ds.Tables["Report"].NewRow(); // Создаем новую строку в таблице, занесенной в ds
                    row["Surname"] = r.Cells[0].Value;  // В столбец этой строки заносим данные из первого столбца dataGridView1
                    row["Name"] = r.Cells[1].Value; // Со вторыми столбцами
                    row["Income"] = r.Cells[2].Value; // С третьими столбцами
                    ds.Tables["Report"].Rows.Add(row); // Добавление всей этой строки в таблицу ds
                }
                ds.WriteXml("D:\\Data.xml");
                MessageBox.Show("XML файл успешно сохранен!", "Выполнено");
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить XML файл!", "Ошибка");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0) // Если в таблице больше нуля строк
            {
                MessageBox.Show("Очистите поле перед загрузкой нового файла!", "Ошибка");
            }
            else
            {
                if (File.Exists("D:\\Data.xml")) // Если существует данный файл
                {
                    DataSet ds = new DataSet(); // Создаем пустой кэш данных
                    ds.ReadXml("D:\\Data.xml"); // Записываем в него XML-данные из файла

                    foreach (DataRow item in ds.Tables["Report"].Rows)
                    {
                        int n = dataGridView1.Rows.Add(); // Добавляем новую сроку в dataGridView
                        dataGridView1.Rows[n].Cells[0].Value = item["Surname"]; // Заносим в первый столбец созданной строки данные из первого столбца таблицы ds
                        dataGridView1.Rows[n].Cells[1].Value = item["Name"]; // Со вторыми столбцами
                        dataGridView1.Rows[n].Cells[2].Value = item["Income"]; // С третьими столбцами
                    }
                }
                else
                {
                    MessageBox.Show("XML файл не найден!", "Ошибка");
                }
            }
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            int n = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[2].Value);
            numericUpDown1.Value = n;
        }

        private void button2_Click(object sender, EventArgs e) // Редактирование
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int n = dataGridView1.SelectedRows[0].Index;
                dataGridView1.Rows[n].Cells[0].Value = textBox1.Text;
                dataGridView1.Rows[n].Cells[1].Value = textBox2.Text;
                dataGridView1.Rows[n].Cells[2].Value = numericUpDown1.Value;
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования!", "Ошибка");
            }
        }

        private void button3_Click(object sender, EventArgs e) // Удаление
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления!", "Ошибка");
            }
        }

        private void button6_Click(object sender, EventArgs e) // Очистка таблицы
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Таблица пустая!", "Ошибка");
            }
        }

        private void button7_Click(object sender, EventArgs e) // Подсчёт налога
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int n = dataGridView1.SelectedRows[0].Index;
                if (numericUpDown1.Value == 0)
                {
                    MessageBox.Show("Выберите строку и продолжите снова!", "Ошибка");
                }
                if (numericUpDown1.Value < 20000)
                {
                    dataGridView1.Rows[n].Cells[3].Value = numericUpDown1.Value /100 * 12;
                }
                else if (numericUpDown1.Value > 20000 && numericUpDown1.Value < 40000)
                {
                    dataGridView1.Rows[n].Cells[3].Value = numericUpDown1.Value / 100 * 20;
                }
                else if (numericUpDown1.Value > 40000)
                {
                    dataGridView1.Rows[n].Cells[3].Value = numericUpDown1.Value / 100 * 35;
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования!", "Ошибка");
            }
        }
    }
}
