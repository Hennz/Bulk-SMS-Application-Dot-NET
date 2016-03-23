using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class smsMasterUser : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["user"] == null)
            Response.Redirect("default.aspx");
        
        if (!IsPostBack)
        {
            fillgridview();
            show();
            fillheader();
        }
    }

    protected void butlogout_Click(object sender, EventArgs e)
    {
        ChangeOnlineStatus();
        Session["user"] = null;
        Response.Redirect("default.aspx");
    }
    public void ChangeOnlineStatus()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlCommand cmd = new SqlCommand(("Update User_Master Set Online_Status='No' where User_Name=@username"), con);
        cmd.Parameters.Add("username", SqlDbType.VarChar).Value = Convert.ToString(Session["user"]);
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
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

    protected void butchangepass_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtopass.Text == "" || txtrpass.Text == "" || txtnpass.Text == "")
            {
                lblerror.Text = "All fields are required";
                return;
            }
            int result = validateorigpassword();
            if (result == 1)
            {
                int nresult = validatesamepassword();
                if (nresult == 1)
                {
                    changepassword();
                    lblerror.Text = "Password Changed Successfully";
                }
                else
                {
                    lblerror.Text = "New passwords do not match ";

                }
            }
            else
            {
                lblerror.Text = "Invalid Original Password";
            }
        }
        catch(Exception)
        {
            lblerror.Text = " Error Occured!! Sorry for the inconvenience caused . We will fix it soon";
        }
    }
    public int validatesamepassword()
    {
        if (txtnpass.Text.Equals(txtrpass.Text, StringComparison.Ordinal))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    public int validateorigpassword()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter dad = new SqlDataAdapter(("SELECT Password from User_Master where User_Name=@username"), con);
        dad.SelectCommand.Parameters.Add("username", SqlDbType.VarChar).Value = Session["user"].ToString();
        DataTable dtbl = new DataTable();
        dad.Fill(dtbl);
        con.Close();
        con.Dispose();
        if (Convert.ToString(dtbl.Rows[0].ItemArray[0]).Equals(txtopass.Text, StringComparison.Ordinal))
        {
            return 1;
        }
        else
        {
            return 0;
        }

    }
    public void changepassword()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlCommand cmd = new SqlCommand(("Update User_Master Set password=@pass where User_Name=@username"), con);
        cmd.Parameters.Add("username", SqlDbType.VarChar).Value = Session["user"].ToString();
        cmd.Parameters.Add("pass", SqlDbType.VarChar).Value = txtnpass.Text;
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();

    }
    public void fillgridview()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter dad = new SqlDataAdapter(("SELECT Totalmessages_issued,issue_date,amount from transaction_Master where user_id=(SElect User_iD from USER_MASTER where user_name ='" + Convert.ToString(Session["user"]) + "') order by transaction_id"), con);
        dad.SelectCommand.Parameters.Add("username", SqlDbType.VarChar).Value = Session["user"].ToString();
        DataTable dtbl = new DataTable();
        int count = dad.Fill(dtbl);
        con.Close();
        con.Dispose();
        if (count != 0)
        {
            GridView1.Visible = true;
           
            GridView1.DataSource = dtbl;
            GridView1.DataBind();
        }
        else
        {
            lblnorecord.Visible = true;
            lblnorecord.Text = "No Record Found";
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgridview();
    }
    public void show()
    {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            SqlDataAdapter dad = new SqlDataAdapter(("SELECT User_Name,First_Name,Last_Name,Mobile,Message_Remaining from User_MAster where User_NAme='" + Convert.ToString(Session["user"]) + "'"), con);
            DataTable dtbl = new DataTable();
            DataTable dtbl1 = new DataTable();
            con.Open();
            dad.Fill(dtbl);
            SqlDataAdapter dad1 = new SqlDataAdapter(("Select TotalMessages_Issued,Issue_Date,Amount from Transaction_Master where user_id=(SElect User_iD from USER_MASTER where user_name ='" + Convert.ToString(Session["user"]) + "') and Transaction_Id=(Select MAX(Transaction_id) from TRansaction_Master where user_id=( SElect User_iD from USER_MASTER where user_name ='" + Convert.ToString(Session["user"]) + "')) "), con);
            dad1.Fill(dtbl1);
            int totalmsg = gettotalmessages();
            Label3.Text = Convert.ToString(dtbl.Rows[0].ItemArray[0]);
            Label4.Text = Convert.ToString(dtbl.Rows[0].ItemArray[1]);
            Label5.Text = Convert.ToString(dtbl.Rows[0].ItemArray[2]);
            Label6.Text = Convert.ToString(dtbl.Rows[0].ItemArray[3]);
            lbltotalmsg.Text = Convert.ToString(totalmsg);
            try
            {
                Label7.Text = Convert.ToString(dtbl1.Rows[0].ItemArray[0]);
                Label8.Text = Convert.ToString(dtbl1.Rows[0].ItemArray[1]);
                Label9.Text = Convert.ToString(dtbl.Rows[0].ItemArray[4]);
                Label10.Text = Convert.ToString(dtbl1.Rows[0].ItemArray[2]);
            }
            catch (IndexOutOfRangeException)
            {
                Label7.Text = "0";
                Label8.Text = "-";
                Label9.Text = "0";
                Label10.Text = "0";
            }

            
            con.Close();
            con.Dispose();

    }
    public int gettotalmessages()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        DataTable dtbl1 = new DataTable();
        SqlDataAdapter dad1 = new SqlDataAdapter(("Select TotalMessages_Issued,Issue_Date,Amount from Transaction_Master where user_id=(SElect User_iD from USER_MASTER where user_name ='" + Convert.ToString(Session["user"]) + "')"), con);
        dad1.Fill(dtbl1);
        con.Close();
        con.Dispose();
        int count = 0;
        try
        {
            for (int i = 0; i < dtbl1.Rows.Count; i++)
            {
                count = count + Convert.ToInt32(dtbl1.Rows[i].ItemArray[0]);
            }
        }
        catch (IndexOutOfRangeException)
        {
        }
        return count;
    }
    public void fillheader()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        DataTable dtbl1 = new DataTable();
        SqlDataAdapter dad1 = new SqlDataAdapter(("Select header,footer from User_Master where user_id=(Select User_iD from USER_MASTER where user_name ='" + Convert.ToString(Session["user"]) + "')"), con);
        dad1.Fill(dtbl1);
        con.Close();
        con.Dispose();
        txtheader.Text = Convert.ToString(dtbl1.Rows[0].ItemArray[0]);
        txtfooter.Text = Convert.ToString(dtbl1.Rows[0].ItemArray[1]);
        ViewState["header"] = Convert.ToString(dtbl1.Rows[0].ItemArray[0]);
        ViewState["footer"] = Convert.ToString(dtbl1.Rows[0].ItemArray[1]);
        hiddenheader.Value = Convert.ToString(dtbl1.Rows[0].ItemArray[0]);
        hiddenfooter.Value = Convert.ToString(dtbl1.Rows[0].ItemArray[1]);
    }
    protected void butheader_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand(("update  User_Master  set Header=@header,Footer=@footer where user_name=@username"), con);
            cmd.Parameters.Add("username", SqlDbType.VarChar).Value = Session["user"].ToString();
            cmd.Parameters.Add("header", SqlDbType.VarChar).Value = txtheader.Text;
            cmd.Parameters.Add("footer", SqlDbType.VarChar).Value = txtfooter.Text;
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            hiddenfooter.Value = txtfooter.Text;
            hiddenheader.Value = txtheader.Text;
            lblresponse.Text = "Data updated!!";
         }
        catch(Exception)
        {
            lblresponse.Text = "Error Occured!! Please try again later";
        }
        /*if(txtfooter.Text!="")
        {
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
           // con.Open();
            SqlCommand cmd1 = new SqlCommand(("Insert into User_Master Footer values(@footer) where User_Name=@username"), con);
            cmd1.Parameters.Add("username", SqlDbType.VarChar).Value = Session["user"].ToString();
            cmd1.Parameters.Add("footer", SqlDbType.VarChar).Value = txtfooter.Text;
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
        }*/
    }


    protected void header_popout_Click(object sender, EventArgs e)
    {
        txtheader.Text = hiddenfooter.Value;
        txtfooter.Text = hiddenfooter.Value;
        lblresponse.Text = "";

    }
}
