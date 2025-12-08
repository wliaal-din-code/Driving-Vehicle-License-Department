using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;

namespace DVLD_Project_Wla.Tests.Test_Types
{
    public partial class frmEditTestType : Form
    {
       private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
       private clsTestType _TestType;

        public frmEditTestType(clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            _TestType = clsTestType.Find(_TestTypeID);

            if (_TestType != null)
            {
                lblTestTypeID.Text = ((int)_TestType.ID).ToString();
                txtTitle.Text = _TestType.Title;
                txtFees.Text = _TestType.Fees.ToString();
                txtDescription.Text = _TestType.Description;
            }
            else
            {
                MessageBox.Show("Could not find Test Type with id = " + _TestTypeID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _TestType.Title = txtTitle.Text;
            _TestType.Fees = Convert.ToSingle(txtFees.Text);
            _TestType.Description = txtDescription.Text;

            if(_TestType.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
