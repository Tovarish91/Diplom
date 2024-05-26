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
    /// Логика взаимодействия для winStart.xaml
    /// </summary>
    public partial class winStart : Window
    {
        public winStart()
        {
            InitializeComponent();

            var empApCnt = Context.Application.ToList().Where(i => i.IDClient == Ap.IDClient).Count();

            lb_Cnt.Content = "Всего обращений у этого клиента: " + empApCnt;
            tblk_Req.Text = Context.Application.ToList().Where(i => i.ID == Ap.ID).FirstOrDefault().ApplicationText;
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            winNo wn = new winNo();
            wn.Show();
            this.Close();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            winYes wy = new winYes();
            wy.Show();
            this.Close();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            admOrClt = true;
            winCom wc = new winCom();
            wc.Show();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            winChos wc = new winChos();
            wc.Show();
            this.Close();
        }
    }
}
