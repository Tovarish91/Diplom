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
using System.Threading;

namespace Diplom1.Windows
{
    /// <summary>
    /// Логика взаимодействия для winNo.xaml
    /// </summary>
    public partial class winNo : Window
    {
        public winNo()
        {
            InitializeComponent();

            var cltOrdCnt = Context.Order.ToList().Where(i => i.IDClient == Clt.ID).Count();
            decimal sum = 0;
            int minID = 0;

            for (int j = 0; j < cltOrdCnt; j++)
            {
                sum += Context.Order.ToList().Where(i => i.IDClient == Clt.ID && i.ID > minID).FirstOrDefault().TotalCost;
                minID = Context.Order.ToList().Where(i => i.IDClient == Clt.ID && i.ID > minID).FirstOrDefault().ID;
            }

            lb_FIO.Content = Clt.LastName + " " + Clt.FirstName + " " + Clt.Patronimic;
            lb_Ord.Content = "Заказов: " +cltOrdCnt;
            lb_Sum.Content = "Общая сумма: " + sum;
            lb_Dec.Content = "Откланенных запросов: " + Context.Application.ToList().Where(i => i.IDClient == Clt.ID && i.IsDone == true &&
                                                                (Context.ApplicationTag.ToList().Where(j => j.IDApplication == i.ID).Count() == 0)).Count();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            winStart ws = new winStart();
            ws.Show();
            this.Close();
        }

        private void btnBlock_Click(object sender, RoutedEventArgs e)
        {
            BlkOrNxtOrGd = 0;

            voteMithod();

            if (empVote == true)
            {
                empVote = false;
                anyVote = false;

                Client ct = new Client();

                ct.ID = Clt.ID;
                ct.IDGender = Clt.IDGender;
                ct.FirstName = Clt.FirstName;
                ct.LastName = Clt.LastName;
                ct.Patronimic = Clt.Patronimic;
                ct.Phone = Clt.Phone;
                ct.Email = Clt.Email;
                ct.Birthday = Clt.Birthday;
                ct.Unreliable = true;               //!
                ct.Avatar = Clt.Avatar;
                ct.WhyUnr = Clt.WhyUnr;

                Context.Client.AddOrUpdate(ct);
                Context.SaveChanges();

                winChos wc = new winChos();
                wc.Show();
                this.Close();
            }
            else
            {
                empVote = false;
                anyVote = false;
            }
        }

        private void btnCom_Click(object sender, RoutedEventArgs e)
        {
            admOrClt = false;
            winCom wc = new winCom();
            wc.Show();
            this.Close();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            BlkOrNxtOrGd = 1;

            voteMithod();

            if (empVote == true) 
            {
                empVote = false;
                anyVote = false;

                DB.Application a = new DB.Application();
                a.ID = Ap.ID;
                a.IDClient = Ap.IDClient;
                a.ApplicationDate = Ap.ApplicationDate;
                a.ApplicationText = Ap.ApplicationText;
                a.IsDone = true;
                a.WhyDec = Ap.WhyDec;
                Context.Application.AddOrUpdate(a);
                Context.SaveChanges();

                winChos wc = new winChos(); 
                wc.Show();
                this.Close();
            }
            else
            {
                empVote = false;
                anyVote = false;
            }
        }
    }
}
