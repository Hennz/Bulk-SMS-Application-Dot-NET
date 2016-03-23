using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class Member_Master : System.Web.UI.UserControl
{  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!Page.IsPostBack)
        {
            filldropdownlist();
            filldropdownlist2();
            reset();
        }
    }
    public void filldropdownlist()
    {
       
        DataTable dtbl = new DataTable();
        DataTable dtbl1 = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter(("Select  Group_Master.Group_Id,Group_Master.Group_Name from Group_Master Inner join User_Master on Group_Master.User_Id=User_Master.User_Id where User_Master.User_Name='" + Session["user"].ToString() + "' and Group_Master.Status='Active'"), con);
        con.Open();
        dad.Fill(dtbl);
        dtbl1.Columns.Add("Group_Id");
        dtbl1.Columns.Add("Group_Name");
        dtbl1.Rows.Add(-1, "Select Group Name");
        for (int i = 0; i < dtbl.Rows.Count; i++)
        {
            dtbl1.Rows.Add(dtbl.Rows[i].ItemArray[0], dtbl.Rows[i].ItemArray[1]);
        }
        dropdown1.DataSource = dtbl1;
        dropdown1.DataTextField = "Group_Name";
        dropdown1.DataValueField = "Group_Id";
        dropdown1.DataBind();
    }
    public void filldropdownlist2()
    {
        DataTable dtbl1 = new DataTable();
        dtbl1.Columns.Add("Category_Id");
        dtbl1.Columns.Add("Category_Name");
        dtbl1.Rows.Add(-1, "Select Category Name");
        dropdown.DataSource = dtbl1;
        dropdown.DataTextField = "Category_Name";
        dropdown.DataValueField = "Category_Id";
        dropdown.DataBind();
    }
    public void filldropdownlist1()
    {
        DataTable dtbl = new DataTable();
        DataTable dtbl1 = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter(("SELECT Category_Id,Category_Name from Category_Master  where Group_Id='" + dropdown1.SelectedItem.Value + "' and Status='Active'"), con);
        con.Open();
        dad.Fill(dtbl);
        dtbl1.Columns.Add("Category_Id");
        dtbl1.Columns.Add("Category_Name");
        dtbl1.Rows.Add(-1, "Select Category Name");
        for (int i = 0; i < dtbl.Rows.Count; i++)
        {
            dtbl1.Rows.Add(dtbl.Rows[i].ItemArray[0], dtbl.Rows[i].ItemArray[1]);
        }
        dropdown.DataSource = dtbl1;
        dropdown.DataTextField = "Category_Name";
        dropdown.DataValueField = "Category_Id";
        dropdown.DataBind();
        dropdown.Enabled = true;
    }

    protected void butmembersubmit_Click(object sender, EventArgs e)
    {
        insert();
        showmembers();
    }
    protected void butmemberreset_Click(object sender, EventArgs e)
    {
        reset();
    }
    public int validatemobile()
    {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            SqlDataAdapter dad = new SqlDataAdapter(("Select  Member_Mobile from Member_Master  where Member_Mobile= '" + Convert.ToInt64(txtmembermobile.Text.Trim()) + "' and Category_id='"+dropdown.SelectedItem.Value+"'"), con);
            DataTable dtbl = new DataTable();
            con.Open();
            int count = dad.Fill(dtbl);
            con.Close();
            con.Dispose();
            if (count == 0)
            {
                return 1;   
            }
            else
            {
                return 0;
            }
    }
    public void insertmember()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter check1 = new SqlDataAdapter(("Select MAX(Member_Id) from Member_Master "), con);
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
        SqlCommand cmd = new SqlCommand(("insert into Member_Master values (@id,@categoryid,@name,@mobile,@email,@address,'Active') "), con);
        cmd.Parameters.Add("id", SqlDbType.Int).Value = idinput;
        cmd.Parameters.Add("name", SqlDbType.VarChar).Value = txtmembername.Text.Trim().ToUpper();
        cmd.Parameters.Add("categoryid", SqlDbType.Int).Value = dropdown.SelectedItem.Value;
        cmd.Parameters.Add("mobile", SqlDbType.BigInt).Value = Convert.ToInt64(txtmembermobile.Text.Trim());
        cmd.Parameters.Add("email", SqlDbType.VarChar).Value = txtmemberemail.Text.Trim();
        cmd.Parameters.Add("address", SqlDbType.VarChar).Value = txtmemberaddress.Text.Trim();
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
    }
    public void insert()
    {
        if (butmembersubmit.Text == "Submit")
        {
            if (Convert.ToInt32(dropdown.SelectedItem.Value) != -1)
            {
                int valid = validatemobile();
                if (valid == 1)
                {
                    insertmember();
                    string A = "Data entered";
                    Response.Write(@"<script Language=""javascript"" >alert('" + A + "')</script>");
                }
                else
                {
                    Response.Write(@"<script Language=""javascript"" >alert('This Mobile Number already exists')</script>");
                }
            }
            else
            {
                Response.Write(@"<script Language=""javascript"" >alert('Please select the Category name')</script>");
            }
            reset();
        }
        else
        {

            if (Convert.ToInt32(dropdown.SelectedItem.Value) != -1)
            {
                updatemember();
            }
            else
            {
                Response.Write(@"<script Language=""javascript"" >alert('Please select the Category name')</script>");

            }
        }
    }
    public void updatemember()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlCommand cmd = new SqlCommand(("UPDATE Member_Master SET Category_Id=@id, Member_Name=@name ,Member_Mobile=@mobile, Member_Email=@email, Member_Address=@address where Member_Id=@uid "), con);
        cmd.Parameters.Add("uid", SqlDbType.Int).Value = Convert.ToInt32(ViewState["updateid"]);
        cmd.Parameters.Add("name", SqlDbType.VarChar).Value = txtmembername.Text.Trim().ToUpper();
        cmd.Parameters.Add("id", SqlDbType.Int).Value = dropdown.SelectedItem.Value;
        cmd.Parameters.Add("mobile", SqlDbType.BigInt).Value = Convert.ToInt64(txtmembermobile.Text.Trim());
        cmd.Parameters.Add("email", SqlDbType.VarChar).Value = txtmemberemail.Text.Trim();
        cmd.Parameters.Add("address", SqlDbType.VarChar).Value = txtmemberaddress.Text.Trim();
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
        Response.Write(@"<script Language=""javascript"" >alert('Data updated successfully')</script>");
        butmemberreset.Visible = true;
        butmembersubmit.Text = "Submit";
        txtmembername.Text = "";
        txtmemberaddress.Text = "";
        txtmemberemail.Text = "";
        txtmembermobile.Text = "";
    }
    public void reset()
    {
        butmemberreset.Visible = true;
        butmembersubmit.Text = "Submit";
        txtmembername.Text = "";
        txtmemberaddress.Text = "";
        txtmemberemail.Text = "";
        txtmembermobile.Text = "";
        filldropdownlist();
        filldropdownlist2();
        //txtcategoryname.Text = dropdown.SelectedItem.Text;
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(dropdown1.SelectedItem.Value) != -1)
        {
            filldropdownlist1();
        }
        else
        {
            filldropdownlist2();
            dropdown.Enabled = false;
        }
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editrow")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            SqlDataAdapter dad = new SqlDataAdapter("select Category_Id,Member_Name,Member_Mobile,Member_Email,Member_Address,Status,Member_Id from Member_Master Where Member_Id='" + Convert.ToInt32(e.CommandArgument) + "'", con);
            DataTable dtbl = new DataTable();
            con.Open();
            dad.Fill(dtbl);
            SqlDataAdapter dad1 = new SqlDataAdapter("select Group_Id from Category_Master Where Category_Id='" +dtbl.Rows[0].ItemArray[0] + "'", con);
            DataTable dtbl1 = new DataTable();
            dad1.Fill(dtbl1);
            con.Close();
            con.Dispose();
            try
            {
                dropdown1.SelectedValue = Convert.ToString(dtbl1.Rows[0].ItemArray[0]);
                filldropdownlist1();
                dropdown.SelectedValue = Convert.ToString(dtbl.Rows[0].ItemArray[0]);
                txtmembername.Text = Convert.ToString(dtbl.Rows[0].ItemArray[1]);
                txtmembermobile.Text = Convert.ToString(dtbl.Rows[0].ItemArray[2]);
                txtmemberemail.Text = Convert.ToString(dtbl.Rows[0].ItemArray[3]);
                txtmemberaddress.Text = Convert.ToString(dtbl.Rows[0].ItemArray[4]);
                butmembersubmit.Text = "Update";
                butmemberreset.Visible = false;
               ViewState["updateid"] = Convert.ToInt32(dtbl.Rows[0].ItemArray[6]);
            }
            catch (ArgumentOutOfRangeException )
            {
                Response.Write(@"<script Language=""javascript"" >alert('Error performing operation: This might have occured due to INACTIVE state of the group or category of the selected member')</script>");
            }
        }
        if (e.CommandName == "changestatus")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            SqlDataAdapter dad = new SqlDataAdapter(("SELECT Status from Member_Master where Member_Id='" + Convert.ToInt32(e.CommandArgument) +"'"), con);
            DataTable dtbl = new DataTable();
            con.Open();
            dad.Fill(dtbl);
            if (Convert.ToString(dtbl.Rows[0].ItemArray[0]) == "Active")
            {
                SqlCommand cmd = new SqlCommand(("UPDATE Member_Master SET Status='Inactive' Where Member_Id=@Member_Id"), con);
                cmd.Parameters.Add("Member_Id", SqlDbType.Int).Value = Convert.ToInt32(e.CommandArgument);
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                showmembers();
            }
            else
            {
                SqlCommand cmd = new SqlCommand(("UPDATE Member_Master SET Status='Active' Where Member_Id=@Member_Id"), con);
                cmd.Parameters.Add("Member_Id", SqlDbType.Int).Value = Convert.ToInt32(e.CommandArgument);
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                showmembers();
            }
        }
    }
    public void showmembers()
    {
        GridView1.Visible = true;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter("select Member_Master.Member_Id,Member_Master.Category_Id,Member_Master.Member_Name,Member_Master.Member_Mobile,Member_Master.Member_Email,Member_Master.Member_Address,Member_Master.Status from Member_Master inner join Category_Master on Category_master.CAtegory_Id=Member_MAster.Category_id inner join Group_Master on Group_MAster.Group_Id=Category_MAster.Group_id inner join User_MAster on User_Master.User_id = Group_Master.User_id where User_Master.user_id=(Select user_master.user_id where user_name='" + Convert.ToString(Session["user"]) + "') and member_master.category_id='"+dropdown.SelectedItem.Value+"' Order  by Member_Master.Member_Id", con);
        DataTable dtbl = new DataTable();
        con.Open();
        int intStatus = dad.Fill(dtbl);
        con.Close();
        con.Dispose();
        if (intStatus != 0)
        {
            GridView1.DataSource = dtbl;
            GridView1.DataBind();
        }
        else
        {
            GridView1.Visible = false;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        showmembers();
    }
    protected void butlink_Click(object sender, EventArgs e)
    {
        Response.Redirect("run_memberauto.aspx");
    }
    protected void dropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(dropdown.SelectedItem.Value) != -1)
        {
            showmembers();
        }
        else
        {
        }
    }
}

