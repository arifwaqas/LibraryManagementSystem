using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Studenteport : System.Web.UI.Page
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
            MultiView1.ActiveViewIndex = -1;
        }
    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        if (drpbranch.SelectedIndex == 0)
        {
            lblmsg.Text = "Select Branch first";
            MultiView1.ActiveViewIndex = -1;
        }
        else
        {
            SDT = SAdapter.Select_By_Branch(drpbranch.SelectedItem.Text);
            GridView1.DataSource = SDT;
            GridView1.DataBind();
            MultiView1.ActiveViewIndex = 0;
            lbl.Text = GridView1.Rows.Count.ToString() + " Student Found";
        }
    }
    protected void btnseach_Click(object sender, EventArgs e)
    {
        if (txtsearch.Text == "")
        {
            lblmsg.Text = "Enter student name";
        }
        else
        {
            SDT = SAdapter.Select_For_SEARCH(txtsearch.Text + "%");
            GridView1.DataSource = SDT;
            GridView1.DataBind();
            MultiView1.ActiveViewIndex = 0;
            lbl.Text = GridView1.Rows.Count.ToString() + " Student Found";
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        SDT = SAdapter.Select_By_SID(Convert.ToInt32(e.CommandArgument.ToString()));
        lblid.Text = SDT.Rows[0]["sid"].ToString();
        lblname.Text = SDT.Rows[0]["studentname"].ToString();
        lblbranch.Text = SDT.Rows[0]["branchname"].ToString();
        lblmobile.Text = SDT.Rows[0]["mobile"].ToString();
        lbladdress.Text = SDT.Rows[0]["address"].ToString();
        lblcity.Text = SDT.Rows[0]["city"].ToString();
        lblpin.Text = SDT.Rows[0]["pincode"].ToString();
        DateTime dobb = Convert.ToDateTime(SDT.Rows[0]["dob"].ToString());
        lbldob.Text = dobb.GetDateTimeFormats()[7].ToString();
        lblemai.Text = SDT.Rows[0]["email"].ToString();
        lblpass.Text = SDT.Rows[0]["password"].ToString();
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
}