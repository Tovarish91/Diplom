using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diplom1.DB;
using System.Threading;
using Diplom1.Windows;
using System.Windows;

namespace Diplom1.Classes
{
    internal class ClassHelper
    {
        public static Entities Context { get; set; } = new Entities();
        public static Employee Emp { get; set; } = new Employee();
        public static Client Clt { get; set; } = new Client();
        public static DB.Application Ap { get; set; } = new DB.Application();

        public static bool admOrClt; 

        public static int BlkOrNxtOrGd;

        public static bool empVote = false;
        public static bool anyVote = false;
        public static void voteMithod() //Окно подтверждения действия
        {
            Thread wsht = new Thread(new ThreadStart(() =>
            {
                if (BlkOrNxtOrGd == 2)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены?", "Подтверждение действия", 
                        MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        empVote = true;
                        anyVote = true;
                    }
                    else anyVote = true;
                }
                else
                {
                    winShureBlock wshb = new winShureBlock();
                    wshb.Show();
                }

                System.Windows.Threading.Dispatcher.Run(); //Без него окно мгновенно закроется
            }));
            wsht.SetApartmentState(ApartmentState.STA); //Без него при вызове окна или месбокса будет ошибка
            wsht.IsBackground = true;

            Thread ift = new Thread(new ThreadStart(() => { while (true) { if (anyVote == true) break; }})); //Нужен для блокировки основного потока

            wsht.Start();
            ift.Start(); 
            ift.Join(); //Блокировка основного потока
        }
    }
}
