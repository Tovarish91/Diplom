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
using static Diplom1.Classes.ClassHelper;
using Diplom1.DB;
using System.Data.Entity.Migrations;

namespace Diplom1.Windows
{
    /// <summary>
    /// Логика взаимодействия для winCom.xaml
    /// </summary>
    public partial class winCom : Window
    {
        public winCom()
        {
            InitializeComponent();
            if (admOrClt) tblk_Em.Text = "Админу";
            else tblk_Em.Text = Clt.Email;
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (admOrClt == true)
            {
                winStart ws = new winStart();
                ws.Show();
                this.Close();
            }
            else
            {
                winNo wn = new winNo();
                wn.Show();
                this.Close();
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            
                if (admOrClt == true)
                {
                    ForAdmin fa = new ForAdmin();
                    fa.IDEmployee = Emp.ID;
                    fa.Date = DateTime.Now;
                    fa.Text = tblk_ReqNew.Text;
                    fa.IsDone = false;
                    Context.ForAdmin.Add(fa);
                    Context.SaveChanges();
                winStart ws = new winStart();
                ws.Show();
                this.Close();
            }
                else
                {
                    DB.Application a = new DB.Application();
                    a.ID = Ap.ID;
                    a.IDClient = Ap.IDClient;
                    a.ApplicationDate = Ap.ApplicationDate;
                    a.ApplicationText = Ap.ApplicationText;
                    a.IsDone = true;                                   //!
                    Context.Application.AddOrUpdate(a);
                    Context.SaveChanges();
                winChos wc = new winChos();
                wc.Show();
                this.Close();
            }
                MessageBox.Show("Сообщение отправлено", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            

        }
    }
}
