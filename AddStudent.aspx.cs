using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddStudent : System.Web.UI.Page
{
    DS_BRANCH.BRANCH_SELECTDataTable BDT = new DS_BRANCH.BRANCH_SELECTDataTable();
    DS_BRANCHTableAdapters.BRANCH_SELECTTableAdapter BAdapter = new DS_BRANCHTableAdapters.BRANCH_SELECTTableAdapter();
    DS_STUDENT.STUDENT_SELECTDataTable SDT = new DS_STUDENT.STUDENT_SELECTDataTable();
    DS_STUDENTTableAdapters.STUDENT_SELECTTableAdapter SAdapter = new DS_STUDENTTableAdapters.STUDENT_SELECTTableAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (Page.IsPostBack == false)
        {
            BDT = BAdapter.SelectBranch();
            drpbranch.DataSource = BDT;
            drpbranch.DataTextField = "Branchname";
            drpbranch.DataValueField = "Branchid";
            drpbranch.DataBind();
            drpbranch.Items.Insert(0, "SELECT");
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (drpdd.SelectedIndex == 0 || drpmm.SelectedIndex == 0 || drpyyyy.SelectedIndex == 0)
        {
            lblmsg.Text = "Select Proper BithDate";
        }
        else
        {
            string gen="";
            if(rdomale.Checked==true)
            {
            gen="Male";
            }
            else
            {
            gen="Female";
            }
            if (FileUpload1.HasFile)
            {
                FileUpload1.SaveAs(Server.MapPath("~/img/") + FileUpload1.FileName);
                DateTime bdate = Convert.ToDateTime(drpdd.SelectedItem.Text + " " + drpmm.SelectedItem.Text + " " + drpyyyy.SelectedItem.Text);
                SAdapter.Insert(txtsname.Text, drpbranch.SelectedItem.Text, txtmobile.Text, txtaddress.Text, txtcity.Text, txtpincode.Text, bdate, gen, txtemail.Text, txtpass.Text,"~/img/"+FileUpload1.FileName);
                lblmsg.Text = "Student Added Successfully";
           
            }
            else
            {
                DateTime bdate = Convert.ToDateTime(drpdd.SelectedItem.Text + " " + drpmm.SelectedItem.Text + " " + drpyyyy.SelectedItem.Text);
                SAdapter.Insert(txtsname.Text, drpbranch.SelectedItem.Text, txtmobile.Text, txtaddress.Text, txtcity.Text, txtpincode.Text, bdate, gen, txtemail.Text, txtpass.Text, "~/img/std.png");
                lblmsg.Text = "Student Added Successfully";
           
            
            }
            txtsname.Text=""; drpbranch.SelectedIndex=0;
            txtmobile.Text="";
            txtaddress.Text=""; txtcity.Text="";
            txtpincode.Text="";
            txtemail.Text="";
             txtpass.Text="";
             drpdd.SelectedIndex = 0;
             drpmm.SelectedIndex = 0;
             drpyyyy.SelectedIndex = 0;
          

        }
    }
}