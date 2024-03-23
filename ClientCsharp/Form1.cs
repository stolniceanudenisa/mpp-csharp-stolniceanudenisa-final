using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientCsharp.service;
using mpp_csharp_stolniceanudenisa_final.model;

namespace ClientCsharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        public Service service { get; set; }

        

        private void button1_Click(object sender, EventArgs e)
        {
            int gasit = 0;
            String username = usernameText.Text;
            String password = passwordText.Text;
            if(username=="" || password == "")
            {
                MessageBox.Show("Completati username-ul si parola!");
                gasit = 1;
            }
            foreach (User r in service.getAllUsers())
            {
                if (r.Username == username && r.Password == password)
                {
                    Home home = new Home();
                    home.user = r;
                    home.srv = this.service;
                    home.Show();
                    usernameText.Text = "";
                    passwordText.Text = "";
                    gasit = 1;
                }
            }
            
            if (gasit == 0)
            {
                MessageBox.Show("Date de conectare incorecte!");
                usernameText.Text = "";
                passwordText.Text = "";
            }
        }

        // private void Form1_Load(object sender, EventArgs e)
        // {
        //     this.BackgroundImage = Properties.Resources.fundal3;
        // }


    }
}