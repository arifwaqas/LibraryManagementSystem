using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookIssue : System.Web.UI.Page
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

   
    protected void Page_Load(object sender, EventArgs e)
    {
        lblissue.Text = "";
        lblmsg.Text = "";
        if (Page.IsPostBack == false)
        {
             BDT = BAdapter.SelectBranch();
                drpbranch.DataSource = BDT;
                drpbranch.DataTextField = "Branchname";
                drpbranch.DataValueField = "Branchid";
                drpbranch.DataBind();
                drpbranch.Items.Insert(0, "SELECT");
                
            
            PubDT = PubAdapter.Select();
            drppublication.DataSource = PubDT;
            drppublication.DataTextField = "Publication";
            drppublication.DataValueField = "pid";
            drppublication.DataBind();
            drppublication.Items.Insert(0, "SELECT");
            drpbook.Items.Insert(0, "SELECT");
        }
    }
    protected void drppublication_SelectedIndexChanged(object sender, EventArgs e)
    {
        BookDT = BookAdapter.Select_By_Publication(drppublication.SelectedItem.Text);
        drpbook.DataSource = BookDT;
        drpbook.DataTextField = "Bookname";
        drpbook.DataValueField = "Bookid";
        drpbook.DataBind();
        drpbook.Items.Insert(0, "SELECT");
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        if (drppublication.SelectedIndex == 0)
        {
           
            lblmsg.Text = "Select Publication";
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
            lblqnt.Text = BookDT.Rows[0]["Quantities"].ToString();
            lblaqnt.Text = BookDT.Rows[0]["availableqnt"].ToString();
            lblrqnt.Text = BookDT.Rows[0]["rentqnt"].ToString();
            lbldetail.Text = BookDT.Rows[0]["Detail"].ToString();
            Image2.ImageUrl = BookDT.Rows[0]["Image"].ToString();

            drpstudent.Items.Clear();
            drpstudent.Items.Insert(0, "SELECT");
        }
    }
    protected void btnissue_Click(object sender, EventArgs e)
    {
        try
        {
            if (TextBox1.Text == "")
            {
                lblissue.Text = "Enter Days";
            }
            else
            {
                if (Convert.ToInt32(lblaqnt.Text) == 0)
                {
                    lblissue.Text = "Book Stock Empty";
                }
                else
                {
                    RDT = RAdapter.Select_SID_BName_Status(lblbname.Text, Convert.ToInt32(drpstudent.SelectedValue), 0);

                    if (RDT.Rows.Count != 0)
                    {
                        lblissue.Text = "Student can't get copies of same book !!";
                    }
                    else
                    {

                        RDT = RAdapter.Select_by_Status_and_SID(Convert.ToInt32(drpstudent.SelectedValue), 0);
                        if (RDT.Rows.Count == 3)
                        {
                            lblissue.Text = "A student has maximum 3 books";
                        }
                        else
                        {
                            RAdapter.Insert(lblbname.Text, Convert.ToInt32(drpstudent.SelectedValue), Convert.ToInt32(TextBox1.Text));


                            BookAdapter.BOOK_ISSUE_TO_STUDENT(Convert.ToInt32(ViewState["BBID"].ToString()));
                            lblissue.Text = "Book Issued to " + drpstudent.SelectedItem.Text;

                            BookDT = BookAdapter.Select_BY_BID(Convert.ToInt32(ViewState["BBID"]));
                            ViewState["BBID"] = BookDT.Rows[0]["BookID"].ToString();
                            lblbname.Text = BookDT.Rows[0]["Bookname"].ToString();
                            lblauthor.Text = BookDT.Rows[0]["author"].ToString();
                            lblbran.Text = BookDT.Rows[0]["branch"].ToString();
                            lblpub.Text = BookDT.Rows[0]["publication"].ToString();
                            lblprice.Text = BookDT.Rows[0]["price"].ToString();
                            lblqnt.Text = BookDT.Rows[0]["Quantities"].ToString();
                            lblaqnt.Text = BookDT.Rows[0]["availableqnt"].ToString();
                            lblrqnt.Text = BookDT.Rows[0]["rentqnt"].ToString();
                            lbldetail.Text = BookDT.Rows[0]["Detail"].ToString();
                            Image2.ImageUrl = BookDT.Rows[0]["Image"].ToString();

                            TextBox1.Text = "";
                            drpstudent.Items.Clear();
                            drpstudent.Items.Insert(0, "SELECT");
                            BDT = BAdapter.SelectBranch();
                            drpbranch.DataSource = BDT;
                            drpbranch.DataTextField = "Branchname";
                            drpbranch.DataValueField = "Branchid";
                            drpbranch.DataBind();
                            drpbranch.Items.Insert(0, "SELECT");
                        }
                    }
                }
            }
        }
        catch {
            lblissue.Text = "Sorry !!! Error !!!";
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
}