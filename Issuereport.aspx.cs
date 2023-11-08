using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Issuereport : System.Web.UI.Page
{
    DS_BRANCH.BRANCH_SELECTDataTable BDT = new DS_BRANCH.BRANCH_SELECTDataTable();
    DS_BRANCHTableAdapters.BRANCH_SELECTTableAdapter BAdapter = new DS_BRANCHTableAdapters.BRANCH_SELECTTableAdapter();
    DS_RENT.RENT_SELECTDataTable RDT = new DS_RENT.RENT_SELECTDataTable();
    DS_RENTTableAdapters.RENT_SELECTTableAdapter RAdapter = new DS_RENTTableAdapters.RENT_SELECTTableAdapter();
   
    DS_STUDENT.STUDENT_SELECTDataTable SDT = new DS_STUDENT.STUDENT_SELECTDataTable();
    DS_STUDENTTableAdapters.STUDENT_SELECTTableAdapter SAdapter = new DS_STUDENTTableAdapters.STUDENT_SELECTTableAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
       // lblmsg.Text = "";
        if (Page.IsPostBack == false)
        {
            BDT = BAdapter.SelectBranch();
            drpbranch.DataSource = BDT;
            drpbranch.DataTextField = "Branchname";
            drpbranch.DataValueField = "Branchid";
            drpbranch.DataBind();
            drpbranch.Items.Insert(0, "SELECT");
           // MultiView1.ActiveViewIndex = -1;
        }

    }
    protected void drpbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        SDT = SAdapter.Select_By_Branch(drpbranch.SelectedItem.Text);
        drpstudent.DataSource = SDT;
        drpstudent.DataTextField = "Studentname";
        drpstudent.DataValueField = "sid";
        drpstudent.DataBind();
        drpstudent.Items.Insert(0, "SELECT");
    }
    protected void Button11_Click(object sender, EventArgs e)
    {

    }
    protected void btnseach_Click(object sender, EventArgs e)
    {
        RDT = RAdapter.Select_by_Status_and_SID(Convert.ToInt32(drpstudent.SelectedValue), 0);
        GridView1.DataSource = RDT;
        GridView1.DataBind();
    }
}