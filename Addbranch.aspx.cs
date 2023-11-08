using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Addbranch : System.Web.UI.Page
{
    DS_BRANCH.BRANCH_SELECTDataTable BDT = new DS_BRANCH.BRANCH_SELECTDataTable();
    DS_BRANCHTableAdapters.BRANCH_SELECTTableAdapter BAdapter = new DS_BRANCHTableAdapters.BRANCH_SELECTTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (Page.IsPostBack == false)
        {
            BDT = BAdapter.SelectBranch();
            GridView1.DataSource = BDT;
            GridView1.DataBind();
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        BAdapter.Insert(txtaddbranch.Text);
        lblmsg.Text = "Branch Inserted";
        BDT = BAdapter.SelectBranch();
        GridView1.DataSource = BDT;
        GridView1.DataBind();
        txtaddbranch.Text = "";
        txtaddbranch.Focus();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int bid= Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

        BAdapter.Delete(bid);
        lblmsg.Text = "Branch Deleted";
        BDT = BAdapter.SelectBranch();
        GridView1.DataSource = BDT;
        GridView1.DataBind();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        BDT = BAdapter.SelectBranch();
        GridView1.DataSource = BDT;
        GridView1.DataBind();

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        BDT = BAdapter.SelectBranch();
        GridView1.DataSource = BDT;
        GridView1.DataBind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int bid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        TextBox bname = GridView1.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox;

        BAdapter.Update(bid, bname.Text);
        lblmsg.Text = "Branch Updated";
        GridView1.EditIndex = -1;
        BDT = BAdapter.SelectBranch();
        GridView1.DataSource = BDT;
        GridView1.DataBind();

    }
}