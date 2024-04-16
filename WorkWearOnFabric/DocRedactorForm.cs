using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkWearOnFabric
{
    public partial class DocRedactorForm : Form
    {
        public DocRedactorForm()
        {
            InitializeComponent();
        }

      

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Red;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.White;
        }
        Point lastPoint;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void buttonPostuplenie_Click(object sender, EventArgs e)
        {
            if (textBoxDate.Text == "")
            {
                MessageBox.Show("Введите дату");
                return;
            }

            if (textBoxDocNum.Text == "")
            {
                MessageBox.Show("Введите номер входяшего документа");
                return;
            }

            if (textBoxProvider.Text == "")
            {
                MessageBox.Show("Введите поставщика");
                return;
            }


            if (textBoxQuantity.Text == "")
            {
                MessageBox.Show("Введите кол-во");
                return;
            }
            if (textBoxPrice.Text == "")
            {
                MessageBox.Show("Введите стоимость");
                return;
            }

            DB db = new DB();

            // Проверяем корректность введенной даты
            DateTime dateValue;
            if (!DateTime.TryParse(textBoxDate.Text, out dateValue))
            {
                MessageBox.Show("Неверный формат даты. Введите дату в формате ДД.ММ.ГГГГ");
                return;
            }
            int quantityValue;
            if (!int.TryParse(textBoxQuantity.Text, out quantityValue))
            {
                MessageBox.Show("Количество должно быть целым числом.");
                return;
            }
            int priceValue;
            if (!int.TryParse(textBoxPrice.Text, out priceValue))
            {
                MessageBox.Show("Цена должно быть целым числом.");
                return;
            }
            int docNumValue;
            if (!int.TryParse(textBoxDocNum.Text, out docNumValue))
            {
                MessageBox.Show("Номер документа должно быть целым числом.");
                return;
            }


            MySqlCommand command = new MySqlCommand("INSERT INTO `receipt`(`date`, `docNum`, `provider`, `quantity`, `price`) VALUES (@date, @docNum, @provider, @quantity, @price)", db.getConnection());

            command.Parameters.AddWithValue("@date", textBoxDate.Text);
            command.Parameters.AddWithValue("@docNum", textBoxDocNum.Text);
            command.Parameters.AddWithValue("@provider", textBoxProvider.Text);
            command.Parameters.AddWithValue("@quantity", textBoxQuantity.Text);
            command.Parameters.AddWithValue("@price", textBoxPrice.Text);
            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Данные внесены");

                // Очистка содержимого всех TextBox
                textBoxDate.Text = "";
                textBoxDocNum.Text = "";
                textBoxProvider.Text = "";
                textBoxQuantity.Text = "";
                textBoxPrice.Text = "";
            }
            else
            {
                MessageBox.Show("Ошибка");
            }

            db.closeConnection();
        }

        private void buttonInfoOfForm_Click(object sender, EventArgs e)
        {
            if (textBoxNameForm.Text == "")
            {
                MessageBox.Show("Введите наиминование");
                return;
            }

            if (textBoxType.Text == "")
            {
                MessageBox.Show("Введите вид");
                return;
            }

            if (textBoxPriceOfUnit.Text == "")
            {
                MessageBox.Show("Введите стоимость");
                return;
            }

            int priceOfUnitValue;

            if (!int.TryParse(textBoxPriceOfUnit.Text, out priceOfUnitValue))
            {
                MessageBox.Show("Цена должно быть целым числом.");
                return;
            }

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `infoofworkform`(`nameForm`, `type`, `priceOfUnit`) VALUES (@nameForm, @type, @priceOfUnit)", db.getConnection());

            command.Parameters.AddWithValue("@nameForm", textBoxNameForm.Text);
            command.Parameters.AddWithValue("@type", textBoxType.Text);
            command.Parameters.AddWithValue("@priceOfUnit", textBoxPriceOfUnit.Text);
           
            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Данные внесены");

                // Очистка содержимого всех TextBox
                textBoxNameForm.Text = "";
                textBoxType.Text = "";
                textBoxPriceOfUnit.Text = "";
                
            }
            else
            {
                MessageBox.Show("Ошибка");
            }



            db.closeConnection();

        }
    }
}
