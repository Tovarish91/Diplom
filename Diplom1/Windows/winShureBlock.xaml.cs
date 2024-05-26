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
using Diplom1.DB;
using static Diplom1.Classes.ClassHelper;
using System.Data.Entity.Migrations;
using System.Threading;

namespace Diplom1.Windows
{
    /// <summary>
    /// Логика взаимодействия для winShureBlock.xaml
    /// </summary>
    public partial class winShureBlock : Window
    {
        public winShureBlock()
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            anyVote = true;
            this.Close();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tboxWhy.Text) || tboxWhy.Text == "Причина") 
                MessageBox.Show("Указание причины обязательно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                empVote = true;
                anyVote = true;

                if (BlkOrNxtOrGd == 0)
                {
                    Clt.WhyUnr = tboxWhy.Text;
                }
                else if (BlkOrNxtOrGd == 1)
                {
                    Ap.WhyDec = tboxWhy.Text;
                }
                this.Close();
            }
        }
    }
}
