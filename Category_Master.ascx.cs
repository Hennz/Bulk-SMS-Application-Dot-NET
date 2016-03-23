using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Category_Master : System.Web.UI.UserControl
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
            reset();
        }
        
    }

    public void filldropdownlist()
    {
        DataTable dtbl = new DataTable();
        DataTable dtbl1 = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter(("Select  Group_Master.Group_Id,Group_Master.Group_Name from Group_Master Inner join User_Master on Group_Master.User_Id=User_Master.User_Id where User_Master.User_Name='"+Session["user"].ToString()+"' and Group_Master.Status='Active'"), con);
        con.Open();
        dad.Fill(dtbl);
        dtbl1.Columns.Add("Group_Id");
        dtbl1.Columns.Add("Group_Name");
        dtbl1.Rows.Add(-1, "Select Group Name");
        for (int i = 0; i < dtbl.Rows.Count; i++)
        {
            dtbl1.Rows.Add(dtbl.Rows[i].ItemArray[0], dtbl.Rows[i].ItemArray[1]);
        }
        dropdown.DataSource = dtbl1;
        dropdown.DataTextField = "Group_Name";
        dropdown.DataValueField = "Group_Id";
        dropdown.DataBind();
        con.Close();
        con.Dispose();
    }
    protected void butcategorysubmit_Click(object sender, EventArgs e)
    {
        insert();
        show();
    }
    public void insert()
    {
        int selectedid = Convert.ToInt32(dropdown.SelectedItem.Value);
        int check=check_in_active_state_of_the_group();
        if (check == 1)
        {
            if (butcategorysubmit.Text == "Submit")
            {
                int allow = checkcategorynameavailability();
                if (allow == 1)
                {
                    insertintodatabase();
                    string A = "Data entered";
                    Response.Write(@"<script Language=""javascript"" >alert('" + A + "')</script>");
                    reset();
                    dropdown.SelectedValue = Convert.ToString(selectedid);
                }
                else
                {
                    Response.Write(@"<script Language=""javascript"" >alert('Category with this name already exists in this group')</script>");
                }
            }
            else
            {
                try
                {
                    int allow = checknochange();
                    if (allow == 1)
                    {
                        int validate = checkcategorynameavailability();
                        if (validate == 1)
                        {
                            updateintodatabase();
                        }
                        else
                        {
                            Response.Write(@"<script Language=""javascript"" >alert('Category Name already exists')</script>");
                            txtcategoryname.Text = "";
                            show();
                        }
                    }
                    else
                    {
                        txtcategoryname.Text = "";
                        show();
                    }
                }
                catch(ArgumentOutOfRangeException)
                {
                    Response.Redirect(Request.Url.ToString());
                }
            }
        }
        else
        {
            Response.Write(@"<script Language=""javascript"" >alert('Error occured while performing the operation:This might have occured due to the INACTIVE state of the selected group')</script>");
            Response.Redirect(Request.Url.ToString());
        }
    }
    public int checknochange()
    {
         SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
         SqlDataAdapter dad = new SqlDataAdapter(("Select  Category_Name,Group_Id from Category_Master where Category_Id= '" + Convert.ToInt32(ViewState["updateid"]) + "'"), con);
         DataTable dtbl = new DataTable();
         con.Open();
         dad.Fill(dtbl);
         string name = Convert.ToString(dtbl.Rows[0].ItemArray[0]);
         if (((txtcategoryname.Text).ToUpper() != name) || (dropdown.SelectedValue != Convert.ToString(dtbl.Rows[0].ItemArray[1])))
         {
             return 1;
         }
         else
             return 0;
    }
    public int checkcategorynameavailability()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter(("Select  Category_Name from Category_Master where Category_Name= '" + (txtcategoryname.Text.Trim()).ToUpper() + "' and Group_Id='" + Convert.ToInt32(dropdown.SelectedItem.Value) + "'"), con);
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
    public void updateintodatabase()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlCommand cmd = new SqlCommand(("UPDATE Category_Master SET Group_Id=@id, Category_Name=@name where Category_Id=@uid "), con);
        cmd.Parameters.Add("uid", SqlDbType.Int).Value = Convert.ToInt32(ViewState["updateid"]);
        cmd.Parameters.Add("name", SqlDbType.VarChar).Value = (txtcategoryname.Text.Trim()).ToUpper();
        cmd.Parameters.Add("id", SqlDbType.Int).Value = Convert.ToInt32(dropdown.SelectedItem.Value);
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
        Response.Write(@"<script Language=""javascript"" >alert('Data updated successfully')</script>");
        int selectedid = Convert.ToInt32(dropdown.SelectedItem.Value);
        reset();
        dropdown.SelectedValue = Convert.ToString(selectedid);
    }
    public int check_in_active_state_of_the_group()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter(("Select STATUS  from GROUP_Master where group_id= '" + dropdown.SelectedItem.Value + "'"), con);
        DataTable dtbl = new DataTable();
        con.Open();
        dad.Fill(dtbl);
        con.Close();
        con.Dispose();
        if (Convert.ToString(dtbl.Rows[0].ItemArray[0]) == "Inactive")
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
    public void insertintodatabase()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter check1 = new SqlDataAdapter(("Select MAX(Category_Id) from Category_Master "), con);
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
        SqlCommand cmd = new SqlCommand(("insert into Category_Master values (@id,@groupid,@name,'Active') "), con);
        cmd.Parameters.Add("name", SqlDbType.VarChar).Value = (txtcategoryname.Text.Trim()).ToUpper();
        cmd.Parameters.Add("id", SqlDbType.Int).Value = idinput;
        cmd.Parameters.Add("groupid", SqlDbType.Int).Value = dropdown.SelectedItem.Value;
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
    }
    public void show()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter("select Category_Id,Group_Id,Category_Name,Status from Category_Master where group_id='"+Convert.ToInt32(dropdown.SelectedItem.Value)+"' Order by Category_Id", con);
        DataTable dtbl = new DataTable();
        con.Open();
        int intStatus = dad.Fill(dtbl);
        con.Close();
        con.Dispose();
        GridView1.DataSource = dtbl;
        GridView1.DataBind();
    }
    public void reset()
    {
        butcategorysubmit.Text = "Submit";
        txtcategoryname.Text = "";
        filldropdownlist();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (butcategorysubmit.Text == "Update")
        {
        }
        else
        {
            show();
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editrow")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            SqlDataAdapter dad = new SqlDataAdapter("select Group_Id,Category_Name,Category_Id,Status from Category_Master Where Category_Id='" + Convert.ToInt32(e.CommandArgument) + "'", con);
            DataTable dtbl = new DataTable();
            con.Open();
            dad.Fill(dtbl);
            con.Close();
            con.Dispose();
            if (Convert.ToString(dtbl.Rows[0].ItemArray[3]) != "Inactive")
            {
                txtcategoryname.Text = Convert.ToString(dtbl.Rows[0].ItemArray[1]);
                dropdown.SelectedValue = Convert.ToString(dtbl.Rows[0].ItemArray[0]);
                butcategorysubmit.Text = "Update";
               ViewState["updateid"] = Convert.ToInt32(dtbl.Rows[0].ItemArray[2]);
            }
            else
            {
                Response.Write(@"<script Language=""javascript"" >alert('Unable to edit details: This category is Inactive')</script>");
            }
        }
        if (e.CommandName == "changestatus")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            SqlDataAdapter dad = new SqlDataAdapter(("SELECT Status from Category_Master where Category_Id='" + Convert.ToInt32(e.CommandArgument) + "'"), con);
            DataTable dtbl = new DataTable();
            con.Open();
            dad.Fill(dtbl);
            if (Convert.ToString(dtbl.Rows[0].ItemArray[0]) == "Active")
            {
                SqlCommand cmd = new SqlCommand(("UPDATE Category_Master SET Status='Inactive' Where Category_Id=@Category_Id"), con);
                cmd.Parameters.Add("Category_Id", SqlDbType.Int).Value = Convert.ToInt32(e.CommandArgument);
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                show();
            }
            else
            {
                SqlCommand cmd = new SqlCommand(("UPDATE Category_Master SET Status='Active' Where Category_Id=@Category_Id"), con);
                cmd.Parameters.Add("Category_Id", SqlDbType.Int).Value = Convert.ToInt32(e.CommandArgument);
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                show();
            }
        }
    }
  }