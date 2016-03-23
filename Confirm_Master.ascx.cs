using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Text;
using System.Security.Cryptography;

public partial class Confirm_Master : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        verifyurl();
    }
    public void verifyurl()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            con.Open();
            String id = Convert.ToString(Request.QueryString["clientid"]);
            if (id == null||id== "")
            {
                Response.Redirect("run_login.aspx");
            }
            SqlDataAdapter dad = new SqlDataAdapter(("Select  First_Name as name from User_Master where Email_Hash=@id  and Verification_Status='Not Verified' "), con);
            dad.SelectCommand.Parameters.AddWithValue("@id", id);
            DataTable dtbl = new DataTable();
            int count = dad.Fill(dtbl);
            con.Close();
            con.Dispose();
            if (count == 1)
            {
                Label1.Visible = true;
                lblname.Visible = true;
                lblname.Text = Convert.ToString(dtbl.Rows[0].ItemArray[0]) + " \nYou can now login to your account";
                butlogin.Visible = true;
                validateaccount();
            }
            else
            {
                lblname.Visible = true;
                lblname.Text = "INVALID OR BAD REQUEST";
            }
        }
        catch(Exception)
        {
            lblname.Text = "INVALID OR BAD REQUEST";
        }

    }
    protected void butlogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    public void validateaccount()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        String id = Convert.ToString(Request.QueryString["clientid"]);
        SqlCommand cmd=new SqlCommand(("UPDATE User_Master SET Verification_Status='Verified' where Email_Hash='"+id+"'"), con);
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();

    }
    

}




