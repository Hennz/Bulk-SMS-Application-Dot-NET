using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ActiveUsers_Master : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("default.aspx");
        }

        if (Session["user"].ToString() != "Admin")
            Response.Redirect("run_home.aspx");
        show();
    }
    public void show()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter dad = new SqlDataAdapter(("SELECT User_Id,User_Name,First_Name + ' ' + Last_Name as name,Email,Mobile,Message_Remaining,Login_TimeStamp from User_Master where Online_Status='Yes' order by User_id"), con);
        DataTable dtbl = new DataTable();
        int count = dad.Fill(dtbl);
        con.Close();
        con.Dispose();
        if (count != 0)
        {
            GridView1.DataSource = dtbl;
            GridView1.DataBind();
        }
    }
    
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "viewloginrecord")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            SqlDataAdapter dad = new SqlDataAdapter("select Login_TimeStamp From LoginRecord_Master Where User_Id='" + Convert.ToInt32(e.CommandArgument) + "' order by login_recordid", con);
            DataTable dtbl = new DataTable();
            con.Open();
            int numrows=dad.Fill(dtbl);
            con.Close();
            con.Dispose();
            DataTable final = new DataTable();
            final.Columns.Add("Login_TimeStamp");
            int counter = 1;
            int lastlogin = numrows - 1;
            for (int i = 0; i < numrows; i++)
            {
                if(counter==11)
                    break;
                final.Rows.Add(dtbl.Rows[lastlogin].ItemArray[0]);
                counter++;
                lastlogin--;
            }
            if (counter != 1)
            {
                GridView2.Visible = true;
                lblnologin.Visible = false;
                GridView2.DataSource = final;
                GridView2.DataBind();
                buthide.Visible = true;
            }
            else
            {
                buthide.Visible = false;
                GridView2.Visible = false;
                lblnologin.Visible = true;
            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        show();
    }
    protected void buthide_Click(object sender, EventArgs e)
    {
        GridView2.Visible = false;
        buthide.Visible = false;
    }
}