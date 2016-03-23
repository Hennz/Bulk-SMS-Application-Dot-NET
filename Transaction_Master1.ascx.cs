using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;


public partial class Transaction_Master1 : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["user"] == null)
        {
            Response.Redirect("default.aspx");
        }

        if (Session["user"].ToString() != "Admin")
            Response.Redirect("run_home.aspx");
       
        if(!IsPostBack)
        {
            if (Panel1.Visible == true)
            {
                show();

                filldropdown();
            }
        }
    


    }
    public void show()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter("select User_Id,User_Name,First_Name +' ' +Last_Name as name ,Mobile from User_Master Order by User_Id", con);
        DataTable dtbl = new DataTable();
        con.Open();
        int count= dad.Fill(dtbl);
        con.Close();
        con.Dispose();
        GridView1.DataSource = dtbl;
        GridView1.DataBind();
        if (count > 0)
        {
            lblnorecord.Visible = false;
        }
        if (count == 0)
        {
            lblnorecord.Visible = true;
            lblnorecord.Text = "No record found";
        }
    }
    public void filldropdown()
    {
        DataTable dtbl1 = new DataTable();
        dtbl1.Columns.Add("Id");
        dtbl1.Columns.Add("Keyword");
        dtbl1.Rows.Add(-1, "Select Keyword");
        dtbl1.Rows.Add(0,"Username");
        dtbl1.Rows.Add(1, "Full Name");
        dtbl1.Rows.Add(2, "Mobile");
        dropdown.DataSource = dtbl1;
        dropdown.DataTextField = "Keyword";
        dropdown.DataValueField = "Id";
        dropdown.DataBind();
    }
    protected void dropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        Setup();

    }
    public void Setup()
    {
        int selection = Convert.ToInt32(dropdown.SelectedItem.Value);
        switch (selection)
        {
            case -1:
               
                lblkeyword.Text = "";
                txtkeyword.Enabled = false;
                butsearch.Enabled = false;
                show();
                break;
            case 0:
                
                lblkeyword.Text = "[Username]";
                txtkeyword.Enabled = true;
                butsearch.Enabled = true;
                break;
            case 1:
                
                lblkeyword.Text = "[Full Name]";
                txtkeyword.Enabled = true;
                butsearch.Enabled = true;
                break;
            case 2:
                
                lblkeyword.Text = "[Mobile]";
                txtkeyword.Enabled = true;
                butsearch.Enabled = true;
                break;

        }

    }
    protected void butsearch_Click(object sender, EventArgs e)
    {
        string statement=getsqldataadapterstatement();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter(statement, con);
        DataTable dtbl = new DataTable();
        con.Open();
        int count=dad.Fill(dtbl);
        con.Close();
        con.Dispose();
        GridView1.DataSource = dtbl;
        GridView1.DataBind();
        if(count==0)
        {
            lblnorecord.Visible = true;
            lblnorecord.Text = "No record found";
        }
        if (count > 0)
        {
            lblnorecord.Visible = false;
        }
    }
    public string getsqldataadapterstatement()
    {   string stat="";
         int selection = Convert.ToInt32(dropdown.SelectedItem.Value);
         switch (selection)
         {
             case 0:
                 stat = "select User_Id,User_Name,First_Name +' ' +Last_Name as name ,Mobile from User_Master where User_Name LIKE '" + txtkeyword.Text.Trim() + "%' Order by User_Id";
                 return stat;
                
             case 1:
                 stat = "select User_Id,User_Name,First_Name +' ' +Last_Name as name ,Mobile from User_Master where First_Name +' '+Last_Name LIKE '" + txtkeyword.Text.Trim() + "%' Order by User_Id";
                 return stat;
                
             case 2:
                 stat = "select User_Id,User_Name,First_Name +' ' +Last_Name as name ,Mobile from User_Master where Mobile LIKE '" + txtkeyword.Text.Trim() + "%' Order by User_Id";
                 return stat;
             default:
                 return stat;
         }

    }
   
    protected void butcancel_Click(object sender, EventArgs e)
    {
        ViewState.Remove("id");
        Panel1.Visible = true;
        Panel2.Visible = false;
        Response.Redirect("run_userdata.aspx");
        
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        if (Convert.ToInt32(dropdown.SelectedItem.Value) == -1)
        {
            show();
        }
        else
        {
            butsearch_Click(new object(), new EventArgs());
        }
    }
    protected void GridRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "issuemessage")
        {
            Panel1.Visible = false;
            Panel2.Visible = true;
            ViewState["id"] = Convert.ToInt32(e.CommandArgument);
            fillformview();
        }
    }
    public void fillformview()
    {
        int id = Convert.ToInt32(ViewState["id"]);
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter("select User_Name,First_Name,Last_Name,Mobile,Email,Message_Remaining,Verification_Status,Status from User_Master where user_id='"+id+"'", con);
        DataTable dtbl = new DataTable();
        con.Open();
        dad.Fill(dtbl);
        con.Close();
        con.Dispose();
        FormView1.DataSource = dtbl;
        FormView1.DataBind();
    }


    protected void butconfirm_Click(object sender, EventArgs e)
    {
        insertintotrans();
    }
    public void insertintotrans()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter check1 = new SqlDataAdapter(("Select MAX(Transaction_Id) from Transaction_Master "), con);
        DataTable dtbl2 = new DataTable();
        check1.Fill(dtbl2);
        int idinput;
        try
        {
            idinput = Convert.ToInt32(dtbl2.Rows[0].ItemArray[0]) + 1;
        }
        catch (InvalidCastException)
        {
            idinput = 1;
        }

        SqlCommand cmd = new SqlCommand(("insert into Transaction_Master values (@id,@uid,@nummsg,@date,@amount,@remarks) "), con);
        cmd.Parameters.Add("id", SqlDbType.Int).Value = idinput;
        cmd.Parameters.Add("uid", SqlDbType.Int).Value = Convert.ToInt32(ViewState["id"]);
        cmd.Parameters.Add("nummsg", SqlDbType.Int).Value = Convert.ToInt32(txtissue.Text.Trim());
        cmd.Parameters.Add("date", SqlDbType.VarChar).Value = ((DateTime.Now.Date)).ToShortDateString();
        cmd.Parameters.Add("amount", SqlDbType.Int).Value = Convert.ToInt32(txtamount.Text.Trim());
        cmd.Parameters.Add("remarks", SqlDbType.VarChar).Value = txtremarks.Text;
        try
        {

            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            insertintousermaster();
            Response.Write(@"<script Language=""javascript"" >alert('Transaction Successfull')</script>");
            reset();
        }
        catch (SqlException)
        {
            Response.Write(@"<script Language=""javascript"" >alert('Some error occured. Try again')</script>");
            Response.Redirect("run_home.aspx");

        }
   }
   public void insertintousermaster()
   {
        int msg = getremainingmessages();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlCommand cmd = new SqlCommand(("Update User_MAster Set Message_Remaining=@msg where user_id='" + Convert.ToInt32(ViewState["id"]) + "' "), con);
        cmd.Parameters.Add("msg", SqlDbType.Int).Value = Convert.ToInt32(txtissue.Text.Trim()) + msg;
        cmd.ExecuteNonQuery();

        con.Close();
        con.Dispose();

   }
    public int getremainingmessages()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter cmd = new SqlDataAdapter(("Select   Message_Remaining from User_Master where user_id='" +Convert.ToInt32(ViewState["id"]) + "' "), con);
        DataTable dtbl = new DataTable();
        cmd.Fill(dtbl);
        con.Close();
        con.Dispose();
        return Convert.ToInt32(dtbl.Rows[0].ItemArray[0]);
    }
    public void reset()
    {
        txtamount.Text = "";
        txtissue.Text = "";
        txtremarks.Text = "";
        fillformview();
        fillgridview();
    }


    protected void viewselecteduser_transactions(object sender, EventArgs e)
    {
        if (LinkButton1.Text == "View previous transactions of the selected user")
        {
            LinkButton1.Text = "Hide transactions list";
            GridView2.Visible = true;
            fillgridview();
        }
        else
        {
            LinkButton1.Text = "View previous transactions of the selected user";
            GridView2.Visible = false;
        }
    }
    public void fillgridview()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter dad = new SqlDataAdapter(("SELECT Totalmessages_issued,issue_date,amount,remarks from transaction_Master where user_id='"+ Convert.ToInt32(ViewState["id"])+"' order by transaction_id"), con);
        DataTable dtbl = new DataTable();
        dad.Fill(dtbl);
        con.Close();
        con.Dispose();
        GridView2.DataSource = dtbl;
        GridView2.DataBind();
    }
}