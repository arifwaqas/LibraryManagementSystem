using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookReturn : System.Web.UI.Page
{
    DS_BRANCH.BRANCH_SELECTDataTable BDT = new DS_BRANCH.BRANCH_SELECTDataTable();
    DS_BRANCHTableAdapters.BRANCH_SELECTTableAdapter BAdapter = new DS_BRANCHTableAdapters.BRANCH_SELECTTableAdapter();
    DS_PUBLICATION.PUBLICATION_SELECTDataTable PubDT = new DS_PUBLICATION.PUBLICATION_SELECTDataTable();
    DS_PUBLICATIONTableAdapters.PUBLICATION_SELECTTableAdapter PubAdapter = new DS_PUBLICATIONTableAdapters.PUBLICATION_SELECTTableAdapter();
    DS_BOOK.BOOK_SELECTDataTable BookDT = new DS_BOOK.BOOK_SELECTDataTable();
    DS_BOOKTableAdapters.BOOK_SELECTTableAdapter BookAdapter = new DS_BOOKTableAdapters.BOOK_SELECTTableAdapter();
    DS_STUDENT.STUDENT_SELECTDataTable SDT = new DS_STUDENT.STUDENT_SELECTDataTable();
    DS_STUDENTTableAdapters.STUDENT_SELECTTableAdapter SAdapter = new DS_STUDENTTableAdapters.STUDENT_SELECTTableAdapter();
    DS_RENT.RENT_SELECTDataTable RDT = new DS_RENT.RENT_SELECTDataTable();
    DS_RENTTableAdapters.RENT_SELECTTableAdapter RAdapter = new DS_RENTTableAdapters.RENT_SELECTTableAdapter();

    DS_STUDENT.STUDENT_SELECT_RENT_BOOKDataTable SRDT = new DS_STUDENT.STUDENT_SELECT_RENT_BOOKDataTable();
    DS_STUDENTTableAdapters.STUDENT_SELECT_RENT_BOOKTableAdapter SRAdapter = new DS_STUDENTTableAdapters.STUDENT_SELECT_RENT_BOOKTableAdapter();

    DS_PANALTY.PENALTY_SELECTDataTable PDT = new DS_PANALTY.PENALTY_SELECTDataTable();
    DS_PANALTYTableAdapters.PENALTY_SELECTTableAdapter PAdapter = new DS_PANALTYTableAdapters.PENALTY_SELECTTableAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lblbook.Text = "";
        if (Page.IsPostBack == false)
        {
            SRDT = SRAdapter.Selecttt(0);

            drppublication.DataSource = SRDT;
            drppublication.DataTextField = "StudentName";
            drppublication.DataValueField = "sid";
            drppublication.DataBind();
            drppublication.Items.Insert(0, "SELECT");
            drpbook.Items.Insert(0, "SELECT");
        }
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        if (drppublication.SelectedIndex == 0)
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

            SDT = SAdapter.Select_By_SID(Convert.ToInt32(drppublication.SelectedValue));
            lblstudent.Text = SDT.Rows[0]["Studentname"].ToString();

            RDT = RAdapter.Select_SID_BName_Status(drpbook.SelectedItem.Text, Convert.ToInt32(drppublication.SelectedValue),0);
            lbldays.Text = RDT.Rows[0]["Days"].ToString();
            lblidate.Text = RDT.Rows[0]["IssueDate"].ToString();
            ViewState["RRID"] = RDT.Rows[0]["rid"].ToString();
                                
           int iday = Convert.ToDateTime(RDT.Rows[0]["IssueDate"].ToString()).Day;
           int rday=System.DateTime.Now.Day;

           int pday=rday - iday;
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
    protected void drppublication_SelectedIndexChanged(object sender, EventArgs e)
    {
        RDT = RAdapter.SelectBook_by_sid_and_status(Convert.ToInt32(drppublication.SelectedValue),0);
        drpbook.DataSource = RDT;
        drpbook.DataTextField = "Bookname";
        drpbook.DataValueField = "rid";
        drpbook.DataBind();
        drpbook.Items.Insert(0, "SELECT");
    }
    protected void btnreturnbook_Click(object sender, EventArgs e)
    {
        if (lblpanalty.Text == "Yes")
        {
            lblbook.Text = "Please, first pay Penalty";
            lblbook.ForeColor = System.Drawing.Color.Red;

             PDT = PAdapter.Select_by_SID_Bookname(Convert.ToInt32(drppublication.SelectedValue), lblbname.Text);
             if (PDT.Rows.Count == 0)
             {
                 PAdapter.Insert(Convert.ToInt32(drppublication.SelectedValue), lblbname.Text, Convert.ToDouble(lblprice.Text), 0, "");
             }
             else
             {
                 for (int i = 0; i < PDT.Rows.Count; i++)
                 {

                     if (PDT.Rows[i]["panalty"].ToString() != "0")
                     {
                         PAdapter.Insert(Convert.ToInt32(drppublication.SelectedValue), lblbname.Text, Convert.ToDouble(lblprice.Text), 0, "");
                         break;
                     }
                 }


             }
        }
        else
        {
            RAdapter.RENT_SELECT_RETURN(Convert.ToInt32(ViewState["RRID"].ToString()), 1, Convert.ToInt32(ViewState["BBID"].ToString()));
            lblbook.Text = "Book Return Successfully";
            lblbook.ForeColor = System.Drawing.Color.Green;

        }
    }
}