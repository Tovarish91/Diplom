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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Diplom1.Windows;
using static Diplom1.Classes.ClassHelper;

namespace Diplom1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            var authEmp = Context.Employee.ToList().Where(i => i.Email == tboxEmail.Text 
                                                    && i.Password == tboxPas.Text && i.IDPosition == 6).FirstOrDefault();

            if (authEmp != null)
            {
                Emp = authEmp;
                winChos wc = new winChos();
                wc.Show();
                this.Close();

                MessageBox.Show($"Успешно! Здравствуйте, {authEmp.FirstName} {authEmp.LastName}!", 
                    "Приветствие", MessageBoxButton.OK, MessageBoxImage.Information) ;
            }
            else MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        private void Label_MouseUp(object sender, MouseButtonEventArgs e)
        {
            winAdm wa = new winAdm();
            wa.Show();
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
