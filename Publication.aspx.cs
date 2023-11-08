using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Publication : System.Web.UI.Page
{
    DS_PUBLICATION.PUBLICATION_SELECTDataTable PubDT = new DS_PUBLICATION.PUBLICATION_SELECTDataTable();
    DS_PUBLICATIONTableAdapters.PUBLICATION_SELECTTableAdapter PubAdapter = new DS_PUBLICATIONTableAdapters.PUBLICATION_SELECTTableAdapter();

    DS_BOOK.BOOK_SELECTDataTable BDT = new DS_BOOK.BOOK_SELECTDataTable();
    DS_BOOKTableAdapters.BOOK_SELECTTableAdapter BAdapter = new DS_BOOKTableAdapters.BOOK_SELECTTableAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (Page.IsPostBack == false)
        {

            PubDT = PubAdapter.Select();
            GridView1.DataSource = PubDT;
            GridView1.DataBind();
        }

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        PubAdapter.Insert(txtpub.Text);
        lblmsg.Text = "Publication Inserted";

        PubDT = PubAdapter.Select();
        GridView1.DataSource = PubDT;
        GridView1.DataBind();
        txtpub.Text = "";
        txtpub.Focus();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int bid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

        PubDT = PubAdapter.Select_By_PID(bid);

        BDT = BAdapter.Select_By_Publication(PubDT.Rows[0]["publication"].ToString());
        if (BDT.Rows.Count > 0)
        {

            lblmsg.Text = "Please delete first books";
        }
        else
        {

            PubAdapter.Delete(bid);
            lblmsg.Text = "Publication Deleted";
            PubDT = PubAdapter.Select();
            GridView1.DataSource = PubDT;
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        PubDT = PubAdapter.Select();
        GridView1.DataSource = PubDT;
        GridView1.DataBind();

    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        PubDT = PubAdapter.Select();
        GridView1.DataSource = PubDT;
        GridView1.DataBind();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int pid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        TextBox pname = GridView1.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox;

        PubAdapter.Update(pid, pname.Text);
        lblmsg.Text = "Publication Updated";
        GridView1.EditIndex = -1;
        PubDT = PubAdapter.Select();
        GridView1.DataSource = PubDT;
        GridView1.DataBind();

    }
}