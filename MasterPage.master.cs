using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["aid"] == null)
        {

            Response.Redirect("Default.aspx");
        }

        lblname.Text = Session["name"].ToString();
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        Session["aid"] = null;
        Session["name"] = null;
        Session["email"] = null;
        Response.Redirect("Default.aspx");
    }
    protected void Button11_Click(object sender, EventArgs e)
    {

    }
}
