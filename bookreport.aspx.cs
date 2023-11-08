using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class bookreport : System.Web.UI.Page
{
    DS_PUBLICATION.PUBLICATION_SELECTDataTable PubDT = new DS_PUBLICATION.PUBLICATION_SELECTDataTable();
    DS_PUBLICATIONTableAdapters.PUBLICATION_SELECTTableAdapter PubAdapter = new DS_PUBLICATIONTableAdapters.PUBLICATION_SELECTTableAdapter();
  
    DS_BRANCH.BRANCH_SELECTDataTable BDT = new DS_BRANCH.BRANCH_SELECTDataTable();
    DS_BRANCHTableAdapters.BRANCH_SELECTTableAdapter BAdapter = new DS_BRANCHTableAdapters.BRANCH_SELECTTableAdapter();
    DS_BOOK.BOOK_SELECTDataTable BookDT = new DS_BOOK.BOOK_SELECTDataTable();
    DS_BOOKTableAdapters.BOOK_SELECTTableAdapter BookAdapter = new DS_BOOKTableAdapters.BOOK_SELECTTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblmsg0.Text = "";
        if (Page.IsPostBack == false)
        {
            BDT = BAdapter.SelectBranch();
            drpbranch.DataSource = BDT;
            drpbranch.DataTextField = "Branchname";
            drpbranch.DataValueField = "Branchid";
            drpbranch.DataBind();
            drpbranch.Items.Insert(0, "SELECT");
            MultiView1.ActiveViewIndex = -1;

            PubDT = PubAdapter.Select();
            drppublication.DataSource = PubDT;
            drppublication.DataTextField = "Publication";
            drppublication.DataValueField = "pid";
            drppublication.DataBind();
            drppublication.Items.Insert(0, "SELECT");
            
        }
    }
    protected void btnviewbranch_Click(object sender, EventArgs e)
    {
        if (drpbranch.SelectedIndex == 0)
        {
            lblmsg.Text = "Select Branch";
            lblmsg.ForeColor = System.Drawing.Color.Red;
            GridView1.DataSource = null;
            GridView1.DataBind(); MultiView1.ActiveViewIndex = -1;
        }
        else
        {
            BookDT = BookAdapter.Select_By_Branch(drpbranch.SelectedItem.Text);
            GridView1.DataSource = BookDT;
            GridView1.DataBind();
            lblmsg0.Text = GridView1.Rows.Count.ToString()+ " - Records Found";
           
            MultiView1.ActiveViewIndex = 0;
        }
    }
    protected void btnviewpublication_Click(object sender, EventArgs e)
    {
        if (drppublication.SelectedIndex == 0)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            lblmsg.Text = "Select Publication";
            lblmsg.ForeColor = System.Drawing.Color.Red; MultiView1.ActiveViewIndex = -1;
        }
        else
        {
            MultiView1.ActiveViewIndex = 0;
            BookDT = BookAdapter.Select_By_Publication(drppublication.SelectedItem.Text);
            GridView1.DataSource = BookDT;
            GridView1.DataBind();
            lblmsg0.Text = GridView1.Rows.Count.ToString() + " - Records Found";
           
        }
    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        BookDT = BookAdapter.Select_BY_BID(Convert.ToInt32(e.CommandArgument.ToString()));
        lblbname.Text = BookDT.Rows[0]["Bookname"].ToString();
        lblauthor.Text = BookDT.Rows[0]["author"].ToString();
        lblbran.Text = BookDT.Rows[0]["branch"].ToString();
        lblpub.Text = BookDT.Rows[0]["publication"].ToString();
        lblprice.Text = BookDT.Rows[0]["price"].ToString();
        lblqnt.Text = BookDT.Rows[0]["Quantities"].ToString();
        lblaqnt.Text =  BookDT.Rows[0]["availableqnt"].ToString();
        lblrqnt.Text = BookDT.Rows[0]["rentqnt"].ToString();
        lbldetail.Text = BookDT.Rows[0]["Detail"].ToString();
        Image2.ImageUrl = BookDT.Rows[0]["Image"].ToString();

    }
}