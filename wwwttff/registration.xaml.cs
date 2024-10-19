using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using wwwttff.Entities;

namespace wwwttff
{
    /// <summary>
    /// Логика взаимодействия для registration.xaml
    /// </summary>
    public partial class registration : Window
    {
        Entities.KursEntities database;
        public registration(Entities.KursEntities DataBase, Entities.userData currentuserdata)
        {
            InitializeComponent();
            this.database = DataBase;
            this.DataContext = currentuserdata; ;
        }

        private void authbtn_Click(object sender, RoutedEventArgs e)
        {
            Window1 auth = new Window1(App.DataBase);
            auth.Show();
            this.Close();

        }

        private void regbtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckRegistrationInfo())
            {
                // Создание нового пользователя и добавление в базу данных
                userData newUserDatas = new userData
                {
                    sName = secondName.Text,
                    name = name.Text,
                    patronymic = patronymic.Text,
                    login = login.Text,
                    password = password.Text
                };

                // Проверка на существование логина
                if (IsLoginAvailable(newUserDatas.login))
                {
                    database.SaveChanges();
                    MessageBox.Show("Регистрация успешна!");
                    Window1 auth = new Window1(App.DataBase);
                    auth.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Логин уже занят. Пожалуйста, выберите другой логин.");
                }
            }
        }
    


        private void ShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (password.Visibility == Visibility.Hidden && ppBOX.Visibility == Visibility.Visible) 
            {
                password.Text = ppBOX.Password;
                ppBOX.Visibility = Visibility.Hidden;
                password.Visibility = Visibility.Visible;
            }
            else 
            {
                ppBOX.Password = password.Text;
                ppBOX.Visibility = Visibility.Visible;
                password.Visibility = Visibility.Hidden;
            }
        }


        private bool CheckRegistrationInfo()
        {
            string message = "";

            if (string.IsNullOrEmpty(secondName.Text))
            {
                message += "Фамилия не введена\n";
            }
            if (string.IsNullOrEmpty(name.Text))
            {
                message += "Имя не введено\n";
            }
            if (string.IsNullOrEmpty(patronymic.Text))
            {
                message += "Отчество не введено\n";
            }
            if (string.IsNullOrEmpty(login.Text))
            {
                message += "Логин не введен\n";
            }

            if (string.IsNullOrEmpty(password.Text))
            {
                message += "Пароль не введен\n";
            }
            if (password.Text != conPassword.Text)
            {
                message += "Пароли не совпадают\n";
            }

            if (password.Text.Length < 8 && !string.IsNullOrEmpty(password.Text))
            {
                message += "Пароль должен содержать минимум 8 символов\n";
            }
            if (password.Text.Length > 16)
            {
                message += "Пароль должен содержать не больше 16 символов\n";
            }
            if (!IsValidPassword(password.Text))
            {
                message += ("Пароль должен в себя включать буквы и в нижнем и в верхнем регистре, цифры, специальные символы\n");
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
        static bool IsValidPassword(string password)    
        {
            Regex pattern = new Regex (@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{1,16}$"
);
            return pattern.IsMatch(password);
        }

        private bool IsLoginAvailable(string Login)
        {
            return !    database.userDatas.Any(u => u.login == Login);
        }

        private void ShowConPassword_Click(object sender, RoutedEventArgs e)
        {
            if (conPassword.Visibility == Visibility.Hidden && ppConBOX.Visibility == Visibility.Visible)
            {
                ppConBOX.Visibility = Visibility.Hidden;
                conPassword.Visibility = Visibility.Visible;
            }
            else
            {
                ppConBOX.Visibility = Visibility.Visible;
                conPassword.Visibility = Visibility.Hidden;
            }
        }

        
        private void conPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (conPassword.Visibility == Visibility.Visible)
            {
                ppConBOX.Password = conPassword.Text;
            }
        }
        private void ppConBOX_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (ppConBOX.Visibility == Visibility.Visible)
            {
                conPassword.Text = ppConBOX.Password;
            }
        }
        private void ppBOX_PasswordChanged(object sender, RoutedEventArgs e)
        {
            password.Text = ppBOX.Password;
        }


    }
}
