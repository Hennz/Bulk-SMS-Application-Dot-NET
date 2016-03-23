 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Home_Master : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (Session["user"] == null)
           Response.Redirect("default.aspx");
        else
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            con.Open();
            SqlDataAdapter dad = new SqlDataAdapter(("SELECT First_Name from User_Master where User_Name=@username"), con);
            dad.SelectCommand.Parameters.Add("username", SqlDbType.VarChar).Value = Session["user"].ToString();
            DataTable dtbl = new DataTable();
            dad.Fill(dtbl);
            con.Close();
            con.Dispose();
            lbluser.Text = Convert.ToString(dtbl.Rows[0].ItemArray[0]);
        }
        if (Session["user"].ToString() == "Admin")
        {
            butexcelSend.Visible = false;
            butcategory.Visible = false;
            butgroup.Visible = false;
            butmember.Visible = false;
            butsend.Visible = false;
            butsentlog.Visible = false;
            butquicksend.Visible = false;
            butquicksendlog.Visible = false;
            
            butuser.Visible = true;
            buttransact.Visible = true;
            butactiveusers.Visible = true;
        }
        
    }
    protected void butgroup_Click(object sender, EventArgs e)
    {
        Response.Redirect("run_group.aspx");
    }
    protected void butcategory_Click(object sender, EventArgs e)
    {
        Response.Redirect("run_category.aspx");
    }
    protected void butsend_Click(object sender, EventArgs e)
    {
        Response.Redirect("run_send.aspx");

    }
    protected void butsentlog_Click(object sender, EventArgs e)
    {
        Response.Redirect("run_view.aspx");
    }
    protected void butmember_Click(object sender, EventArgs e)
    {
        Response.Redirect("run_member.aspx");
    }
   

    protected void butuser_Click(object sender, EventArgs e)
    {
        Response.Redirect("run_registeruser.aspx");

    }
    protected void buttransact_Click(object sender, EventArgs e)
    {
        Response.Redirect("run_userdata.aspx");
    }
    protected void butquicksend_Click(object sender, EventArgs e)
    {
        Response.Redirect("run_quicksend.aspx");
    }
    protected void butquicksendlog_Click(object sender, EventArgs e)
    {
        Response.Redirect("run_quicksendlog.aspx");
    }

    protected void butactiveusers_Click(object sender, EventArgs e)
    {
        Response.Redirect("run_activeusers.aspx");
    }
    protected void butexcelSend_Click(object sender, EventArgs e)
    {
        Response.Redirect("run_excelSend.aspx");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("run_memberauto.aspx");
    }
}

