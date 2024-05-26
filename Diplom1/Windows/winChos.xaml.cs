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
    /// Логика взаимодействия для winChos.xaml
    /// </summary>
    public partial class winChos : Window
    {
        public winChos()
        {
            InitializeComponent();

            Context = new Entities();

            dgl.ItemsSource = Context.Application.ToList();
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private DB.Application a;

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            if (tbxInf.Text == "Запрос не обработан")
            {
                Ap = Context.Application.ToList().Where(i => i.ID == a.ID).FirstOrDefault();
                Clt = Context.Client.ToList().Where(i => i.ID == Ap.IDClient).FirstOrDefault();

                winStart ws = new winStart();
                ws.Show();
                this.Close();
            }
            else MessageBox.Show("Возможна обработка только не обработанного запроса", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void dgl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            a = (DB.Application)dgl.SelectedItem;

            if (a != null) //Если выделим и начнём что-то вводить в поиск, то будет
            {
                dgR.ItemsSource = Context.ApplicationTag.ToList().Where(i => i.IDApplication == a.ID);

                var badClt = Context.Client.ToList().Where(i => i.ID == a.IDClient).FirstOrDefault().Unreliable;

                var wU = Context.Client.ToList().Where(i => i.ID == a.IDClient).FirstOrDefault().WhyUnr;

                var dec = Context.Application.ToList().Where(i => i.ID == a.ID && i.IsDone == true && (Context.ApplicationTag.ToList().Where(
                                                             j => j.IDApplication == i.ID).Count() == 0)).FirstOrDefault();

                var aNew = Context.Application.ToList().Where(i => i.ID == a.ID && i.IsDone == false && (Context.ApplicationTag.ToList().Where(
                                                             j => j.IDApplication == i.ID).Count() == 0)).FirstOrDefault();

                var wD = Context.Application.ToList().Where(i => i.ID == a.ID).FirstOrDefault().WhyDec;

                var aD = Context.Application.ToList().Where(i => i.ID == a.ID).FirstOrDefault().IsDone;

                if (badClt) tbxInf.Text = "Клиент был заблокирован. Причина: " + wU;
                else if (dec != null) tbxInf.Text = "Запрос был отклонён. Причина: " + wD;
                else if (aD) tbxInf.Text = "Запрос выполнен";
                else if (aNew != null) tbxInf.Text = "Запрос не обработан";
                else tbxInf.Text = "Запрос в обработке";
            }
        }

        private void tbxSrch_KeyUp(object sender, KeyEventArgs e)
        {
            dgR.ItemsSource = null;

            var a = Context.Client.ToList().Where(j => j.FirstName.Contains(tbxSrch.Text)).FirstOrDefault();
            var b = Context.Client.ToList().Where(j => j.LastName.Contains(tbxSrch.Text)).FirstOrDefault();
            var c = Context.Client.ToList().Where(j => j.Patronimic.Contains(tbxSrch.Text)).FirstOrDefault();



            if (a != null && b != null && c!=null) //
            {
                dgl.ItemsSource = Context.Application.ToList().Where(i => i.ID.ToString().Contains(tbxSrch.Text) ||
                                                                 i.ApplicationDate.ToString().Contains(tbxSrch.Text) ||
                                                                 i.ApplicationText.Contains(tbxSrch.Text) ||
                                                                 i.IDClient == a.ID ||
                                                                 i.IDClient == b.ID ||
                                                                 i.IDClient == c.ID);
            }
            else if(a==null && b != null && c != null) //a
            {
                dgl.ItemsSource = Context.Application.ToList().Where(i => i.ID.ToString().Contains(tbxSrch.Text) ||
                                                                 i.ApplicationDate.ToString().Contains(tbxSrch.Text) ||
                                                                 i.ApplicationText.Contains(tbxSrch.Text) ||
                                                                 i.IDClient == b.ID ||
                                                                 i.IDClient == c.ID);
            }
            else if (a == null && b == null && c != null) //ab
            {
                dgl.ItemsSource = Context.Application.ToList().Where(i => i.ID.ToString().Contains(tbxSrch.Text) ||
                                                                 i.ApplicationDate.ToString().Contains(tbxSrch.Text) ||
                                                                 i.ApplicationText.Contains(tbxSrch.Text) ||
                                                                 i.IDClient == c.ID);
            }
            else if (a == null && b != null && c == null) //ac
            {
                dgl.ItemsSource = Context.Application.ToList().Where(i => i.ID.ToString().Contains(tbxSrch.Text) ||
                                                                 i.ApplicationDate.ToString().Contains(tbxSrch.Text) ||
                                                                 i.ApplicationText.Contains(tbxSrch.Text) ||
                                                                 i.IDClient == b.ID);
            }
            else if (a != null && b == null && c != null) //b
            {
                dgl.ItemsSource = Context.Application.ToList().Where(i => i.ID.ToString().Contains(tbxSrch.Text) ||
                                                                 i.ApplicationDate.ToString().Contains(tbxSrch.Text) ||
                                                                 i.ApplicationText.Contains(tbxSrch.Text) ||
                                                                 i.IDClient == a.ID ||
                                                                 i.IDClient == c.ID);
            }
            else if (a != null && b == null && c == null) //bc
            {
                dgl.ItemsSource = Context.Application.ToList().Where(i => i.ID.ToString().Contains(tbxSrch.Text) ||
                                                                 i.ApplicationDate.ToString().Contains(tbxSrch.Text) ||
                                                                 i.ApplicationText.Contains(tbxSrch.Text) ||
                                                                 i.IDClient == a.ID);
            }
            else if (a != null && b != null && c == null) //c
            {
                dgl.ItemsSource = Context.Application.ToList().Where(i => i.ID.ToString().Contains(tbxSrch.Text) ||
                                                                 i.ApplicationDate.ToString().Contains(tbxSrch.Text) ||
                                                                 i.ApplicationText.Contains(tbxSrch.Text) ||
                                                                 i.IDClient == a.ID ||
                                                                 i.IDClient == b.ID);
            }
            else  //abc
            {
                dgl.ItemsSource = Context.Application.ToList().Where(i => i.ID.ToString().Contains(tbxSrch.Text) ||
                                                                 i.ApplicationDate.ToString().Contains(tbxSrch.Text) ||
                                                                 i.ApplicationText.Contains(tbxSrch.Text));
            }
        }

        private void dgl_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DB.Application a = (DB.Application)e.Row.DataContext;

            var dec = Context.Application.ToList().Where(i => i.ID == a.ID && i.IsDone == true && ((Context.ApplicationTag.ToList().Where(
                                                             j => j.IDApplication == i.ID).Count() == 0) || 
                                                             Context.Client.ToList().Where(k => k.ID == 
                                                             i.IDClient).FirstOrDefault().Unreliable == true)).FirstOrDefault();
            var tr = Context.Application.ToList().Where(i => i.ID == a.ID && i.IsDone == true && (Context.ApplicationTag.ToList().Where(
                                                             j => j.IDApplication == i.ID).Count() != 0)).FirstOrDefault();
            var obr = Context.Application.ToList().Where(i => i.ID == a.ID && i.IsDone == false && (Context.ApplicationTag.ToList().Where(
                                                             j => j.IDApplication == i.ID).Count() != 0)).FirstOrDefault();
            if (dec != null)
            {
                e.Row.Background = new SolidColorBrush(Colors.Red);
            }
            else if (tr != null)
            {
                e.Row.Background = new SolidColorBrush(Colors.Green);
            }
            else if (obr != null)
            {
                e.Row.Background = new SolidColorBrush(Colors.Yellow);
            }
        } 

        private void cbxGd_Click(object sender, RoutedEventArgs e)
        {
            if (cbxGd.IsChecked == true)
            {
                dgl.ItemsSource = Context.Application.ToList().Where(i => i.IsDone == true && (Context.ApplicationTag.ToList().Where(
                                                             j => j.IDApplication == i.ID).Count() != 0) &&
                                                             Context.Client.ToList().Where(k => k.ID == i.IDClient).FirstOrDefault().Unreliable == false);
            }
            else dgl.ItemsSource = Context.Application.ToList();
        }

        private void cbxNm_Click(object sender, RoutedEventArgs e)
        {
            if (cbxNm.IsChecked == true)
            {
                dgl.ItemsSource = Context.Application.ToList().Where(i => i.IsDone == false && (Context.ApplicationTag.ToList().Where(
                                                             j => j.IDApplication == i.ID).Count() != 0) &&
                                                             Context.Client.ToList().Where(k => k.ID == i.IDClient).FirstOrDefault().Unreliable == false);
            }
            else dgl.ItemsSource = Context.Application.ToList();
        }

        private void cbxDc_Click(object sender, RoutedEventArgs e)
        {
                if (cbxDc.IsChecked == true)
                {
                    dgl.ItemsSource = Context.Application.ToList().Where(i => i.IsDone == true && (Context.ApplicationTag.ToList().Where(
                                                             j => j.IDApplication == i.ID).Count() == 0) ||
                                                             Context.Client.ToList().Where(k => k.ID == i.IDClient).FirstOrDefault().Unreliable == true);
            }
            else dgl.ItemsSource = Context.Application.ToList();
        }

        private void cbxNl_Click(object sender, RoutedEventArgs e)
        {
            if (cbxNl.IsChecked == true)
            {
                dgl.ItemsSource = Context.Application.ToList().Where(i => i.IsDone == false && (Context.ApplicationTag.ToList().Where(
                                                         j => j.IDApplication == i.ID).Count() == 0) &&
                                                         Context.Client.ToList().Where(k => k.ID == i.IDClient).FirstOrDefault().Unreliable != true);
            }
            else dgl.ItemsSource = Context.Application.ToList();
        }
    }
}
