using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Addbook : System.Web.UI.Page
{
    DS_BRANCH.BRANCH_SELECTDataTable BDT = new DS_BRANCH.BRANCH_SELECTDataTable();
    DS_BRANCHTableAdapters.BRANCH_SELECTTableAdapter BAdapter = new DS_BRANCHTableAdapters.BRANCH_SELECTTableAdapter();

    DS_BOOK.BOOK_SELECTDataTable BookDT = new DS_BOOK.BOOK_SELECTDataTable();
    DS_BOOKTableAdapters.BOOK_SELECTTableAdapter BookAdapter = new DS_BOOKTableAdapters.BOOK_SELECTTableAdapter();
    DS_PUBLICATION.PUBLICATION_SELECTDataTable PubDT = new DS_PUBLICATION.PUBLICATION_SELECTDataTable();
    DS_PUBLICATIONTableAdapters.PUBLICATION_SELECTTableAdapter PubAdapter = new DS_PUBLICATIONTableAdapters.PUBLICATION_SELECTTableAdapter();
   

    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (Page.IsPostBack == false)
        {

            BDT = BAdapter.SelectBranch();
            //drpbranch.DataSource = BDT;
            //drpbranch.DataTextField = "Branchname";
            //drpbranch.DataValueField = "Branchid";
            //drpbranch.DataBind();
            //drpbranch.Items.Insert(0, "SELECT");


            CheckBoxList1.DataSource = BDT;
            CheckBoxList1.DataTextField = "Branchname";
            CheckBoxList1.DataValueField = "branchid";
            CheckBoxList1.DataBind();


            PubDT = PubAdapter.Select();
            drppublication.DataSource = PubDT;
            drppublication.DataTextField = "Publication";
            drppublication.DataValueField = "pid";
            drppublication.DataBind();
            drppublication.Items.Insert(0, "SELECT");
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            
            FileUpload1.SaveAs(Server.MapPath("~/Book/") + FileUpload1.FileName);

            int a = 0;
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected == true)
                {
                    BookAdapter.Insert(txtbname.Text, txtauthor.Text, txtdetail.Text, Convert.ToDouble(txtprice.Text), drppublication.SelectedItem.Text, CheckBoxList1.Items[i].Text, Convert.ToInt32(txtqnt.Text), Convert.ToInt32(txtqnt.Text), 0, "~/Book/" + FileUpload1.FileName.ToString());
                    a = 1;
                }
            
            }
            if (a == 0)
            {
                lblmsg.Text = "Please, Select branch";
            }
            else
            {
                lblmsg.Text = "Book Added";
                txtauthor.Text = "";
                txtbname.Text = "";
                txtdetail.Text = "";
                txtprice.Text = "";
                txtqnt.Text = "";
                // drpbranch.SelectedIndex = 0;
                drppublication.SelectedIndex = 0;
                txtbname.Focus();
            }
        }
        else
        {
            lblmsg.Text = "Please, Select Book Photo";
        }
    }
}