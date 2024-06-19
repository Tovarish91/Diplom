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
    /// Логика взаимодействия для winYes.xaml
    /// </summary>
    public partial class winYes : Window
    {
        public winYes()
        {
            InitializeComponent();

            tblk_Req.Text = Ap.ApplicationText;

            cb_Tag1.ItemsSource = Context.Tag.ToList().Where(i => i.ID > 1);
            cb_Tag1.SelectedIndex = 0;
            cb_Tag1.DisplayMemberPath = "Title";
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            winStart ws = new winStart();
            ws.Show();
            this.Close();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tblk_ReqNew.Text) != true && tblk_ReqNew.Text != "Введите отредактированный текст")
            {
                BlkOrNxtOrGd = 2;

                voteMithod();

                if (empVote == true)
                {
                    empVote = false;
                    anyVote = false;

                    DB.Application a = new DB.Application();
                    a.ID = Ap.ID;
                    a.IDClient = Ap.IDClient;
                    a.ApplicationDate = Ap.ApplicationDate;
                    a.ApplicationText = "(редакт.) " + tblk_ReqNew.Text;        //!
                    a.IsDone = false;
                    Context.Application.AddOrUpdate(a);
                    Context.SaveChanges();

                    ApplicationTag at = new ApplicationTag();
                    at.IDApplication = Ap.ID;
                    at.IDTag = cb_Tag1.SelectedIndex + 2;
                    at.IDEmployee = Emp.ID;
                    at.Date = DateTime.Now;
                    Context.ApplicationTag.Add(at);
                    Context.SaveChanges();

                    winChos ws = new winChos();
                    ws.Show();
                    this.Close();
                }
                else
                {
                    empVote = false;
                    anyVote = false;
                }
            }
            else MessageBox.Show("Введите отредактированный запрос", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
    }
}
