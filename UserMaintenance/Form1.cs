using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            label2.Text = Resource1.FullName;
            button1.Text = Resource1.Add;
            button2.Text = Resource1.FileSave;

            //listbox
            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new User();
            {
                
                u.FullName = textBox2.Text;
            };
            users.Add(u);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog svf = new SaveFileDialog();
            svf.FileName = "Mentett nevek";
            svf.DefaultExt = ".text";
            svf.Filter = "Text documents (.txt)|*.txt";
            if(svf.ShowDialog()== DialogResult.OK)
            {
                foreach (var item in users)
                {
                    File.AppendAllText(svf.FileName, item.ID.ToString() + " " + item.FullName.ToString()+"\n");
                }
            
            
            }

        }
    }
}
