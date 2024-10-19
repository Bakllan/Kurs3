using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using wwwttff.Entities;
namespace wwwttff   
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Entities.KursEntities database;
        public Window1(Entities.KursEntities DataBase)
        {
            InitializeComponent();
            this.database = DataBase;
        }
        public int count = 0;
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckLoginInfo())
            {
                string Login = LogTxt.Text;
                string Password = PassTxt.Text;

                // Поиск пользователя в базе данных,
                userData user = database.userDatas.FirstOrDefault(u => u.login == Login && u.password == Password);

                if (user != null)
                {
                    switch (user.id_type)
                    {
                        case 1: // Администратор
                            Window2 adminWindow = new Window2();
                            adminWindow.Show();
                            break;
                        case 2: // Сотрудник
                            Window3 employeeWindow = new Window3();
                            employeeWindow.Show();
                            break;
                        case 3: // Гость
                            Window4 guestWindow = new Window4();
                            guestWindow.Show();
                            break;
                        default:
                            MessageBox.Show("Вы не принадлежите ни к одной группе!");
                            break;
                    }

                    // Закрытие текущего окна
                    this.Close();
                }
                else
                {
                    count++;
                    if (count == 2)
                    {
                        count = 0;
                        Captcha captcha = new Captcha();
                        captcha.Show();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль. Пожалуйста, попробуйте снова.");
                    }
                }
            }
        }
        private bool CheckLoginInfo()
        {
            string message = "";

            if (string.IsNullOrEmpty(LogTxt.Text))
            {
                message += "Логин не введен\n";
            }

            if (string.IsNullOrEmpty(PassTxt.Text))
            {
                message += "Пароль не введен\n";
            }

            if (message == "")
            {
                return true;
            }
            else
            {
                MessageBox.Show(message);
                return false;
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            var newUser = new Entities.userData();
            App.DataBase.userDatas.Add(newUser);
            registration reg = new registration(App.DataBase, newUser);
            reg.Show();
            this.Close();
        }

        
        private void PassTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PassTxt.Visibility == Visibility.Visible)
            {
                PassMask.Password = PassTxt.Text;
            }            
        }
        private void PassMask_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PassMask.Visibility == Visibility.Visible)
            {
                PassTxt.Text = PassMask.Password;
            }
        }
        private void ShowConPassword_Click(object sender, RoutedEventArgs e)
        {
            if (PassTxt.Visibility == Visibility.Hidden && PassMask.Visibility == Visibility.Visible)
            {
                PassMask.Visibility = Visibility.Hidden;
                PassTxt.Visibility = Visibility.Visible;
            }
            else
            {
                PassMask.Visibility = Visibility.Visible;
                PassTxt.Visibility = Visibility.Hidden;
            }
        }

       
    }
}
