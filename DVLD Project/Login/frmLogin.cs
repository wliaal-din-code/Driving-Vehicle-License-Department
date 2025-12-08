using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;
using DVLD_Project_Wla.Global_Classes;
using DVLD_Project_Wla.People.Controls;

namespace DVLD_Project_Wla.Login
{
    public partial class frmLogin : Form
    {
        
        public frmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //ctrlPersonCard1.PersonID = 1026;
            //ctrlPersonCardWithFilter1.LoadPersonInfo(1);
            string UserName = "", Password = "";

            if (clsGlobal.GetStoredCredential(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
                chkRememberMe.Checked = false;

        }
        void Eventlog()
        {
            string DVLDApp = "DVLD";

            if (!EventLog.Exists(DVLDApp))
            {
                EventLog.CreateEventSource(DVLDApp, "Application");
                Console.WriteLine("Event Source Create");
            }

            EventLog.WriteEntry(DVLDApp, "This my Information", EventLogEntryType.Information);
        }
        private void ctrlPersonCard1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            
        }

        private void ctrlPersonCardWithFilter1_Load(object sender, EventArgs e)
        {
            //ctrlPersonCardWithFilter1.
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser user = clsUser.FindByUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            if( user != null)
            {
                if(chkRememberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    clsGlobal.RememberUsernameAndPassword("", "");
                }
                if (!user.IsActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                clsGlobal.CurrentUser = user;
                
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();

            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
