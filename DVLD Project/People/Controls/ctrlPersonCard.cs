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
using DVLD_Buisness;
using DVLD_Project_Wla.Global_Classes;
using DVLD_Project_Wla.Properties;

namespace DVLD_Project_Wla.People.Controls
{
    public partial class ctrlPersonCard : UserControl
    {

        private clsPerson _Person;

        private int _PersonID =-1;

        public int PersonID
            {

            get { return _PersonID; }
           
            }

        public clsPerson SelectPersonInfo
        {
            get { return _Person; }
        }

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        private void ctrlPersonCard_Load(object sender, EventArgs e)
        {
            
        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblFullName.Text = "[????]";
            lblPersonID.Text = "[????]";
            lblGendor.Text = "[????]";
            pbGendor.Image = Resources.Man_32;
            lblNationalNo.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblEmail.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            lblPhone.Text = "[????]";
            pbPersonImage.Image = Resources.Male_512;

        }

        private void _LoadPersonImage()
        {
            if (_Person.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
            else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void _FillPersonInfo()
        {
            _PersonID = _Person.PersonID;
            lblFullName.Text = _Person.FullName;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";
            lblNationalNo.Text = _Person.NotionalNo;
            lblDateOfBirth.Text = clsFormat.DateToShort(_Person.DateOfBirth);
            lblEmail.Text = _Person.Email;
            lblCountry.Text = clsCountry.Find(_Person.NationalityCountryID).CountryName;
            lblAddress.Text = _Person.Address;
            lblPhone.Text = _Person.Phone;
            _LoadPersonImage();
            //pbPersonImage.Image = Resources.Male_512;
        }

        public void LoadPersonInf(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with Person ID. = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        public void LoadPersonInf(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with National No. = " + NationalNo.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(_PersonID);
            frm.ShowDialog();
            LoadPersonInf(_PersonID);
        }
    }
}
