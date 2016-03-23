using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


public partial class Group_Master : System.Web.UI.UserControl
{
   protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("default.aspx");
        }
        show();
    }
    protected void butgroupsubmit_Click(object sender, EventArgs e)
    {
        insert();
        show();
        
    }

    public void insert()
    {
        if (butgroupsubmit.Text == "Submit")
        {
            int validate = validategroupname();
            if (validate == 1)
            {
                insertintodatabase();
                string A = "Data entered";
                Response.Write(@"<script Language=""javascript"" >alert('" + A + "')</script>");
            }
            else
            {
                Response.Write(@"<script Language=""javascript"" >alert('Group name already exists')</script>");
            }
            reset();
        } 
        else
        {
            int check=checknochange();
            if (check == 1)
            {
                int validate = validategroupname();
                if (validate == 1)
                {
                    updateintodatabase();
                }
                else
                {
                    Response.Write(@"<script Language=""javascript"" >alert('Group Name already exists')</script>");
                    reset();
                    show();
                }
            }
            else
            {
                reset();
                show();
            }
        }
    }
    public int checknochange()
    {
         SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
         SqlDataAdapter dad = new SqlDataAdapter(("Select  Group_Master.Group_Name from Group_Master Inner join User_Master on Group_Master.User_Id=User_Master.User_Id where Group_Master.Group_Id= '" + Convert.ToInt32(ViewState["updateid"]) + "'"), con);
         DataTable dtbl = new DataTable();
         con.Open();
         dad.Fill(dtbl);
         con.Close();
         con.Dispose();
         string name = Convert.ToString(dtbl.Rows[0].ItemArray[0]);
         if ((txtgroupname.Text.Trim()).ToUpper() != name)
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
        SqlCommand cmd = new SqlCommand(("UPDATE Group_Master SET Group_Name=@name where Group_Id=@uid "), con);
        cmd.Parameters.Add("uid", SqlDbType.Int).Value = Convert.ToInt32(ViewState["updateid"]);
        cmd.Parameters.Add("name", SqlDbType.VarChar).Value = (txtgroupname.Text.Trim()).ToUpper();
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
        Response.Write(@"<script Language=""javascript"" >alert('Data updated successfully')</script>");
        reset();
        show();
    }
    public int validategroupname()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter(("Select  Group_Master.Group_Name from Group_Master Inner join User_Master on Group_Master.User_Id=User_Master.User_Id where Group_Master.Group_Name= '" + (txtgroupname.Text.Trim()).ToUpper() + "' and User_Master.User_Name='" + Session["user"].ToString() + "'"), con);
        DataTable dtbl = new DataTable();
        con.Open();
        int count = dad.Fill(dtbl);
        con.Close();
        con.Dispose();
        if (count != 0)
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
        SqlDataAdapter check1 = new SqlDataAdapter(("Select MAX(Group_Id) from Group_Master "), con);
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
        int id=getuserid();
        SqlCommand cmd = new SqlCommand(("insert into Group_Master values (@id,@uid,@name,'Active') "), con);
        cmd.Parameters.Add("name", SqlDbType.VarChar).Value = (txtgroupname.Text.Trim()).ToUpper();
        cmd.Parameters.Add("id", SqlDbType.Int).Value = idinput;
        cmd.Parameters.Add("uid", SqlDbType.Int).Value = id;
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
    }
    public int getuserid()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter dad = new SqlDataAdapter("Select User_Id from User_Master where User_Name='" + Session["user"].ToString() + "'",con);
        DataTable dtbl = new DataTable();
        dad.Fill(dtbl);
        return Convert.ToInt32(dtbl.Rows[0].ItemArray[0]);
    }
    public void show()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter("Select  Group_Master.Group_Id,Group_Master.Group_Name,Group_Master.Status from Group_Master Inner join User_Master on Group_Master.User_Id=User_Master.User_Id where User_Master.User_Name='" + Session["user"].ToString() + "'Order by Group_Master.Group_Id", con);
        DataTable dtbl = new DataTable();
        con.Open();
        int intStatus = dad.Fill(dtbl);
        con.Close();
        con.Dispose();
        if (intStatus > 0)
        {
            GridView1.DataSource = dtbl;
            GridView1.DataBind();
        }
    }
    public void reset()
    {
    txtgroupname.Text = "";
    butgroupsubmit.Text = "Submit";
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editrow")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            SqlDataAdapter dad = new SqlDataAdapter("select Group_Id,Group_Name From Group_Master Where Group_Id='" + Convert.ToInt32(e.CommandArgument) + "'", con);
            DataTable dtbl = new DataTable();
            con.Open();
            dad.Fill(dtbl);
            con.Close();
            con.Dispose();
            txtgroupname.Text = Convert.ToString(dtbl.Rows[0].ItemArray[1]);
            butgroupsubmit.Text = "Update";
            ViewState["updateid"] = Convert.ToInt32(dtbl.Rows[0].ItemArray[0]);
        }
        if (e.CommandName == "changestatus")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            SqlDataAdapter dad = new SqlDataAdapter(("SELECT Status from Group_Master where Group_Id='" + Convert.ToInt32(e.CommandArgument) + "'"), con);
            DataTable dtbl = new DataTable();
            con.Open();
            dad.Fill(dtbl);
            if (Convert.ToString(dtbl.Rows[0].ItemArray[0]) == "Active")
            {
                SqlCommand cmd = new SqlCommand(("UPDATE Group_Master SET Status='Inactive' Where Group_Id=@Group_Id"), con);
                cmd.Parameters.Add("Group_Id", SqlDbType.Int).Value = Convert.ToInt32(e.CommandArgument);
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                show();
            }
            else
            {
                SqlCommand cmd = new SqlCommand(("UPDATE Group_Master SET Status='Active' Where Group_Id=@Group_Id"), con);
                cmd.Parameters.Add("Group_Id", SqlDbType.Int).Value = Convert.ToInt32(e.CommandArgument);
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                show();
            }
        }
    }
}