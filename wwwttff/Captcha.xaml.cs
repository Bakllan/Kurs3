﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace wwwttff
{
    public partial class Captcha : Window
    {
        public Captcha()
        {
            InitializeComponent();
            String allowchar = "";
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            String[] ar = allowchar.Split(a);
            String pwd = "";
            string temp = "";
            Random r = new Random();

            for (int i = 0; i < 4; i++)
            {
                temp = ar[(r.Next(0, ar.Length))];

                pwd += temp;
            }
            textBox1.Text = pwd;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            String allowchar = "";
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            char[] a = {','};
            String[] ar = allowchar.Split(a);
            String pwd = "";
            string temp = "";
            Random r = new Random();

            for (int i = 0; i < 4; i++)
            {
                temp = ar[(r.Next(0, ar.Length))];

                pwd += temp;
            }
            textBox1.Text = pwd;
        }

        private void captchaClick(object sender, RoutedEventArgs e)
        {
            string a = inputText.Text;
            string b = textBox1.Text;
            if (b == a)
            {
                MessageBox.Show("Капча пройдена");
                this.Close();
            }
            else 
            {
                MessageBox.Show("Неправильно введена капча");
            }
        }
    }
}