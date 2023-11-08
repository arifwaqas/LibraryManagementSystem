using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    DS_STUDENT.STUDENT_SELECTDataTable SDT = new DS_STUDENT.STUDENT_SELECTDataTable();
    DS_STUDENTTableAdapters.STUDENT_SELECTTableAdapter SAdapter = new DS_STUDENTTableAdapters.STUDENT_SELECTTableAdapter();
    DS_ADMIN.ADMIN_SELECTDataTable ADT = new DS_ADMIN.ADMIN_SELECTDataTable();
    DS_ADMINTableAdapters.ADMIN_SELECTTableAdapter AAdapter = new DS_ADMINTableAdapters.ADMIN_SELECTTableAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl.Text = "";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (rdolibrary.Checked == true)
        {
            ADT = AAdapter.Select_for_LOGIN(txtuname.Text, txtupass.Text);
            if (ADT.Rows.Count > 0)
            {
                Session["aid"] = ADT.Rows[0]["aid"].ToString();
                Session["email"] = txtuname.Text;
                Session["name"] = ADT.Rows[0]["name"].ToString();
                Response.Redirect("Home.aspx");

            }
            else
            {
                lbl.Text = "Invalid Credential";
            }
        }
        else
        {
            SDT = SAdapter.Select_for_LOGIN(txtuname.Text, txtupass.Text);
            if (SDT.Rows.Count > 0)
            {

                Session["sid"] = SDT.Rows[0]["sid"].ToString();
                Session["email"]=txtuname.Text;
                Response.Redirect("Student/Default.aspx");
            }
            else
            {
                lbl.Text = "Invalid Credential";
            }

        }

    }
}