using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Panalty : System.Web.UI.Page
{
    DS_RENT.RENT_SELECTDataTable RDT = new DS_RENT.RENT_SELECTDataTable();
    DS_RENTTableAdapters.RENT_SELECTTableAdapter RAdapter = new DS_RENTTableAdapters.RENT_SELECTTableAdapter();

    DS_PANALTY.PENALTY_SELECTDataTable PDT = new DS_PANALTY.PENALTY_SELECTDataTable();
    DS_PANALTYTableAdapters.PENALTY_SELECTTableAdapter PAdapter = new DS_PANALTYTableAdapters.PENALTY_SELECTTableAdapter();

    DS_STUDENT.STUDENT_SELECTDataTable SDT = new DS_STUDENT.STUDENT_SELECTDataTable();
    DS_STUDENTTableAdapters.STUDENT_SELECTTableAdapter SAdapter = new DS_STUDENTTableAdapters.STUDENT_SELECTTableAdapter();
    DS_BOOK.BOOK_SELECTDataTable BookDT = new DS_BOOK.BOOK_SELECTDataTable();
    DS_BOOKTableAdapters.BOOK_SELECTTableAdapter BookAdapter = new DS_BOOKTableAdapters.BOOK_SELECTTableAdapter();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblpay.Text = "";
        if (Page.IsPostBack == false)
        {
            SDT = SAdapter.Select_Student_for_panalty();
            drpstudent.DataSource = SDT;
            drpstudent.DataTextField = "StudentName";
            drpstudent.DataValueField = "sid";
            drpstudent.DataBind();
            drpstudent.Items.Insert(0, "SELECT");
        }
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        if (drpstudent.SelectedIndex == 0)
        {

            lblmsg.Text = "Select Student";
            lblmsg.ForeColor = System.Drawing.Color.Red; MultiView1.ActiveViewIndex = -1;
        }
        else if (drpbook.SelectedIndex == 0)
        {
            lblmsg.Text = "Select Book";
            lblmsg.ForeColor = System.Drawing.Color.Red; MultiView1.ActiveViewIndex = -1;
        }
        else
        {
            MultiView1.ActiveViewIndex = 0;
            BookDT = BookAdapter.Select_By_BNAM(drpbook.SelectedItem.Text);
            ViewState["BBID"] = BookDT.Rows[0]["BookID"].ToString();
            lblbname.Text = BookDT.Rows[0]["Bookname"].ToString();
            lblauthor.Text = BookDT.Rows[0]["author"].ToString();
            lblbran.Text = BookDT.Rows[0]["branch"].ToString();
            lblpub.Text = BookDT.Rows[0]["publication"].ToString();
            lblprice.Text = BookDT.Rows[0]["price"].ToString();

            Image2.ImageUrl = BookDT.Rows[0]["Image"].ToString();

            SDT = SAdapter.Select_By_SID(Convert.ToInt32(drpstudent.SelectedValue));
            lblstudent.Text = SDT.Rows[0]["Studentname"].ToString();

            RDT = RAdapter.Select_SID_BName_Status(drpbook.SelectedItem.Text, Convert.ToInt32(drpstudent.SelectedValue), 0);
            lbldays.Text = RDT.Rows[0]["Days"].ToString();
            lblidate.Text = RDT.Rows[0]["IssueDate"].ToString();
            ViewState["RRID"] = RDT.Rows[0]["rid"].ToString();

            int iday = Convert.ToDateTime(RDT.Rows[0]["IssueDate"].ToString()).Day;
            int rday = System.DateTime.Now.Day;

            int pday = rday - iday;
            if (pday > Convert.ToInt32(lbldays.Text))
            {
                lblpanalty.Text = "Yes";
            }
            else
            {
                lblpanalty.Text = "NO";
            }
        }
    }
    protected void drpstudent_SelectedIndexChanged(object sender, EventArgs e)
    {
        PDT = PAdapter.Select_by_SID_Panalty_0(Convert.ToInt32(drpstudent.SelectedValue));
        drpbook.DataSource = PDT;
        drpbook.DataTextField = "bookname";
        drpbook.DataValueField = "pid";
        drpbook.DataBind();
        drpbook.Items.Insert(0, "SELECT");
        
    }
    protected void btnpaypanalty_Click(object sender, EventArgs e)
    {
        if (txtpanalty.Text == "")
        {
            lblpay.Text = "Enter amount";
        }
        else if (txtdetail.Text == "")
        {
            lblpay.Text = "Enter detail";
        }
        else
        {
            PAdapter.PENALTY_PAY_NOW(Convert.ToDouble(txtpanalty.Text), txtdetail.Text, Convert.ToInt32(drpbook.SelectedValue));
            lblmsg.Text = "Amount paid successfully";
            RAdapter.RENT_SELECT_RETURN(Convert.ToInt32(ViewState["RRID"].ToString()), 1, Convert.ToInt32(ViewState["BBID"].ToString()));
            txtdetail.Text = "";
            txtpanalty.Text = "";
            MultiView1.ActiveViewIndex = -1;
            SDT = SAdapter.Select_Student_for_panalty();
            drpstudent.DataSource = SDT;
            drpstudent.DataTextField = "StudentName";
            drpstudent.DataValueField = "sid";
            drpstudent.DataBind();
            drpstudent.Items.Insert(0, "SELECT");
            drpbook.Items.Clear();
            drpbook.Items.Insert(0, "SELECT");
        }
    }
}