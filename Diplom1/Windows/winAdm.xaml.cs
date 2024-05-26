using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Diplom1.Windows;
using Diplom1.DB;
using static Diplom1.Classes.ClassHelper;
using System.Data.Entity.Migrations;

namespace Diplom1.Windows
{
    /// <summary>
    /// Логика взаимодействия для winAdm.xaml
    /// </summary>
    public partial class winAdm : Window
    {
        public winAdm()
        {
            InitializeComponent();
        }
        public void RemoveText(object sender, EventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (instance.Text == instance.Tag.ToString())
                instance.Text = "";
        }

        public void AddText(object sender, EventArgs e)
        {
            TextBox instance = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(instance.Text))
                instance.Text = instance.Tag.ToString();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            var noAuthEmp = Context.Employee.ToList().Where(i => i.FirstName == tboxFN.Text 
                            && i.LastName == tboxLN.Text).FirstOrDefault();

            if (noAuthEmp != null)
            {
                ForAdmin fa = new ForAdmin();
                fa.IDEmployee = noAuthEmp.ID;
                fa.Date = DateTime.Now;
                fa.Text = "Сотрудник забыл пароль";
                fa.IsDone = false;
                Context.ForAdmin.Add(fa);
                Context.SaveChanges();

                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();

                MessageBox.Show("Запрос отправлен", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Сотрудник не найден, проверьте введенную информацию", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
