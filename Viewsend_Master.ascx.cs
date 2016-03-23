using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class Viewsend_Master : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            Label5.Visible = false;
            txtcount.Visible = false;
            DateTime todaydate = DateTime.Today;
           //DateTime dt = DateTime.ParseExact(todaydate.ToString(), "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
          //string s = dt.ToString("dd-MM-yyyy");
           txtDatestart.Text = "11-12-2015"; 
            txtDateend.Text = "11-12-2015";
           ViewState["date"]="11-12-2015";
            filldropdownlist();
            filldropdownlist2();
            filldropdown();
        }
    }
    public void filldropdownlist2()
    {
        DataTable dtbl1 = new DataTable();
        dtbl1.Columns.Add("Category_Id");
        dtbl1.Columns.Add("Category_Name");
        dtbl1.Rows.Add(-1, "Select Category Name");
        ddcategory.DataSource = dtbl1;
        ddcategory.DataTextField = "Category_Name";
        ddcategory.DataValueField = "Category_Id";
        ddcategory.DataBind();
    }
    public void filldropdown()
    {
        DataTable dtbl = new DataTable();
        dtbl.Columns.Add("Status_Id");
        dtbl.Columns.Add("Status_Name");
        dtbl.Rows.Add("All", "All");
        dtbl.Rows.Add("Sent", "Sent");
        dtbl.Rows.Add("Failed", "Failed");

        dropdown.DataSource = dtbl;
        dropdown.DataTextField = "Status_Name";
        dropdown.DataValueField = "Status_Id";
        dropdown.DataBind();
    }
   
    public void filldropdownlist()
    {
        DataTable dtbl = new DataTable();
        DataTable dtbl1 = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter(("SELECT Group_Id,Group_Name from Group_Master where user_id=(Select user_id from user_master where user_name='" + Convert.ToString(Session["user"]) + "' )"), con);
        con.Open();
        dad.Fill(dtbl);
        dtbl1.Columns.Add("Group_Id");
        dtbl1.Columns.Add("Group_Name");
        dtbl1.Rows.Add(-1, "Select Group Name");
        for (int i = 0; i < dtbl.Rows.Count; i++)
        {
            dtbl1.Rows.Add(dtbl.Rows[i].ItemArray[0], dtbl.Rows[i].ItemArray[1]);
        }
        ddgroup.DataSource = dtbl1;
        ddgroup.DataTextField = "Group_Name";
        ddgroup.DataValueField = "Group_Id";
        ddgroup.DataBind();
    }
    protected void ddgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddgroup.SelectedItem.Value) != -1)
        {
            filldropdownlist1();
            ddcategory.Enabled = true;
        }
        else
        {
            filldropdownlist2();
            ddcategory.Enabled = false;
        }
    }
    public void filldropdownlist1()
    {
        DataTable dtbl = new DataTable();
        DataTable dtbl1 = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter(("SELECT Category_Id,Category_Name from Category_Master where Group_Id='" + ddgroup.SelectedItem.Value + "'"), con);
        con.Open();
        dad.Fill(dtbl);
        dtbl1.Columns.Add("Category_Id");
        dtbl1.Columns.Add("Category_Name");
        dtbl1.Rows.Add(-1, "Select Category Name");
        for (int i = 0; i < dtbl.Rows.Count; i++)
        {
            dtbl1.Rows.Add(dtbl.Rows[i].ItemArray[0], dtbl.Rows[i].ItemArray[1]);
        }
        ddcategory.DataSource = dtbl1;
        ddcategory.DataTextField = "Category_Name";
        ddcategory.DataValueField = "Category_Id";
        ddcategory.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        AssignValuesToDateStrings();//Gives a default value to date if no value is entered
        txtmember.Text = txtmember.Text.Trim();
        StartCase();
    }
    public void StartCase()
    {
        int c = check(Convert.ToString(ViewState["txtDatestart"]), Convert.ToString(ViewState["txtDateend"]));
        if (c < 0 || c == 0)
        {
            if (Convert.ToInt32(ddgroup.SelectedItem.Value) == -1)
            {

              case1();//Date is entered and No Group is selected and Member name is entered or not entered
            }
            else
            {
               case2();//Date is entered and Group with or without Category is selected and Member name is entered or not entered
            }
        }
        else
        {
GridView1.Visible = false;
        txtcount.Visible = false;
        Label5.Visible = false;
        lblrecord.Visible = true;
        lblrecord.Text = "Error: Invalid Date Entry";
        }
    }
    public void case1()
    {
        loadallusermessages();
        try
        {
            int pass = CheckDateInputOutOfLimits();
            if (pass == 1)
            {
                getStartAndEndId();
                CollectMsgsForCase1();
                int allow = CountMsg();
                if (allow != 0)
                {
                    ShowCollectedMsgs(allow);
                }
                else
                {
                    nodatafound();
                    return;
                }
            }
            else
            {
                nodatafound();
                return;
            }
        }
        catch (IndexOutOfRangeException)
        {
            nodatafound();
            return;
        }
      
    }
    public void case2()
    {
       loadallusermessages();
        try
        {
            int pass = CheckDateInputOutOfLimits();
            if (pass == 1)
            {
                getStartAndEndId();
                CollectMsgsForCase2();
                int allow = CountMsg();
                if (allow != 0)
                {
                    ShowCollectedMsgs(allow);
                }
                else
                {
                    nodatafound();
                    return;
                }
            }
            else
            {
                nodatafound();
                return;
            }
        }
        catch(IndexOutOfRangeException)
        {
            nodatafound();
            return;
        }
    }
    public void CollectMsgsForCase1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());//load all messages of the user
        con.Open();
        string query;
        if (dropdown.SelectedItem.Value == "All")
        {
            query = "Select Send_Master.Member_Name,Send_Master.Message_Text,Send_Master.Message_Date,Send_Master.Message_Time,Send_Master.Message_Length,Send_Master.Message_SendTotal,Send_Master.Status from Send_Master inner join Member_Master on member_master.member_id=send_master.Member_id inner join category_master on category_master.category_id=member_master.category_id inner join group_master on group_master.Group_id=category_master.group_id inner join user_master on user_master.user_id=group_Master.user_id where (send_master.Message_Id>='" + Convert.ToInt32(ViewState["idstart"]) + "') and (send_master.Message_Id<='" + Convert.ToInt32(ViewState["idend"]) + "') and member_master.member_Name LIKE '" + txtmember.Text + "%'  and user_master.User_id=(Select user_id from user_master where user_name='" + Convert.ToString(Session["user"]) + "' )  Order by send_master.Message_Id";
        }
        else
        {
            query = "Select Send_Master.Member_Name,Send_Master.Message_Text,Send_Master.Message_Date,Send_Master.Message_Time,Send_Master.Message_Length,Send_Master.Message_SendTotal,Send_Master.Status from Send_Master inner join Member_Master on member_master.member_id=send_master.Member_id inner join category_master on category_master.category_id=member_master.category_id inner join group_master on group_master.Group_id=category_master.group_id inner join user_master on user_master.user_id=group_Master.user_id where (send_master.Message_Id>='" + Convert.ToInt32(ViewState["idstart"]) + "') and (send_master.Message_Id<='" + Convert.ToInt32(ViewState["idend"]) + "') and member_master.member_Name LIKE '" + txtmember.Text + "%' and send_master.status='"+dropdown.SelectedItem.Value+"' and user_master.User_id=(Select user_id from user_master where user_name='" + Convert.ToString(Session["user"]) + "' )  Order by send_master.Message_Id";
        }
        SqlDataAdapter dad1 = new SqlDataAdapter(query, con);
        DataTable dtbl1 = new DataTable();
        dad1.Fill(dtbl1);
        con.Close();
        con.Dispose();
        ViewState["finalmsg"] = dtbl1;
    }
    public void CollectMsgsForCase2()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());//load all messages of the user
        con.Open();
        DataTable dtbl1 = new DataTable();
        if (Convert.ToInt32(ddcategory.SelectedItem.Value) == -1)
        {
        string query;
        if (dropdown.SelectedItem.Value == "All")
        {
            query = "Select Send_Master.Member_Name,Send_Master.Message_Text,Send_Master.Message_Date,Send_Master.Message_Time,Send_Master.Message_Length,Send_Master.Message_SendTotal,Send_Master.Status from Send_Master inner join Member_Master on member_master.member_id=send_master.Member_id inner join category_master on category_master.category_id=member_master.category_id inner join group_master on group_master.Group_id=category_master.group_id inner join user_master on user_master.user_id=group_Master.user_id where (send_master.Message_Id>='" + Convert.ToInt32(ViewState["idstart"]) + "') and (send_master.Message_Id<='" + Convert.ToInt32(ViewState["idend"]) + "') and group_master.group_id='" + ddgroup.SelectedItem.Value + "' and member_master.member_Name LIKE '" + txtmember.Text + "%'  and user_master.User_id=(Select user_id from user_master where user_name='" + Convert.ToString(Session["user"]) + "' )  Order by send_master.Message_Id";
        }
        else
        {
            query = "Select Send_Master.Member_Name,Send_Master.Message_Text,Send_Master.Message_Date,Send_Master.Message_Time,Send_Master.Message_Length,Send_Master.Message_SendTotal,Send_Master.Status from Send_Master inner join Member_Master on member_master.member_id=send_master.Member_id inner join category_master on category_master.category_id=member_master.category_id inner join group_master on group_master.Group_id=category_master.group_id inner join user_master on user_master.user_id=group_Master.user_id where (send_master.Message_Id>='" + Convert.ToInt32(ViewState["idstart"]) + "') and (send_master.Message_Id<='" + Convert.ToInt32(ViewState["idend"]) + "') and group_master.group_id='" + ddgroup.SelectedItem.Value + "' and member_master.member_Name LIKE '" + txtmember.Text + "%' and send_master.status='"+dropdown.SelectedItem.Value+"' and user_master.User_id=(Select user_id from user_master where user_name='" + Convert.ToString(Session["user"]) + "' )  Order by send_master.Message_Id";
        }
            SqlDataAdapter dad1 = new SqlDataAdapter(query, con);
            dad1.Fill(dtbl1);
        }
        else
        {
            string query;
            if (dropdown.SelectedItem.Value == "All")
            {
                query = "Select Send_Master.Member_Name,Send_Master.Message_Text,Send_Master.Message_Date,Send_Master.Message_Time,Send_Master.Message_Length,Send_Master.Message_SendTotal,Send_Master.Status from Send_Master inner join Member_Master on member_master.member_id=send_master.Member_id inner join category_master on category_master.category_id=member_master.category_id inner join group_master on group_master.Group_id=category_master.group_id inner join user_master on user_master.user_id=group_Master.user_id where (send_master.Message_Id>='" + Convert.ToInt32(ViewState["idstart"]) + "') and (send_master.Message_Id<='" + Convert.ToInt32(ViewState["idend"]) + "') and category_master.category_id='" + ddcategory.SelectedItem.Value + "' and member_master.member_Name LIKE '" + txtmember.Text + "%' and user_master.User_id=(Select user_id from user_master where user_name='" + Convert.ToString(Session["user"]) + "' )  Order by send_master.Message_Id";
            }
            else
            {
                query = "Select Send_Master.Member_Name,Send_Master.Message_Text,Send_Master.Message_Date,Send_Master.Message_Time,Send_Master.Message_Length,Send_Master.Message_SendTotal,Send_Master.Status from Send_Master inner join Member_Master on member_master.member_id=send_master.Member_id inner join category_master on category_master.category_id=member_master.category_id inner join group_master on group_master.Group_id=category_master.group_id inner join user_master on user_master.user_id=group_Master.user_id where (send_master.Message_Id>='" + Convert.ToInt32(ViewState["idstart"]) + "') and (send_master.Message_Id<='" + Convert.ToInt32(ViewState["idend"]) + "') and category_master.category_id='" + ddcategory.SelectedItem.Value + "' and member_master.member_Name LIKE '" + txtmember.Text + "%' and send_master.status='" + dropdown.SelectedItem.Value + "' and user_master.User_id=(Select user_id from user_master where user_name='" + Convert.ToString(Session["user"]) + "' )  Order by send_master.Message_Id";
            }
            SqlDataAdapter dad1 = new SqlDataAdapter(query, con);
            dad1.Fill(dtbl1);
        }
        con.Close();
        con.Dispose();
        ViewState["finalmsg"] = dtbl1;

    }
    
    public void loadallusermessages()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());//load all messages of the user
        con.Open();
        SqlDataAdapter dad = new SqlDataAdapter(("Select send_master.message_id,send_master.message_date from send_master inner join member_master on member_master.member_id=send_master.member_id inner join category_master on category_master.category_id=member_master.category_id inner join group_master on group_master.group_id=category_master.group_id inner join user_master on user_master.user_id=group_master.user_id where user_master.user_name='" + Convert.ToString(Session["user"]) + "' order by send_master.message_id"), con);
        DataTable dtbl = new DataTable();
        dad.Fill(dtbl);
        ViewState["fullusermsg"] = dtbl;
        con.Close();
        con.Dispose();
    }
    public int CheckDateInputOutOfLimits()
    {
        string ds = Convert.ToString(ViewState["txtDatestart"]);
        string de = Convert.ToString(ViewState["txtDateend"]);
        DataTable dtbl = new DataTable();
        dtbl = ViewState["fullusermsg"] as DataTable;
        if (!(((check(ds, Convert.ToString(dtbl.Rows[0].ItemArray[1])) < 0) && (check(de, Convert.ToString(dtbl.Rows[0].ItemArray[1])) < 0)) || ((check(ds, Convert.ToString(dtbl.Rows[dtbl.Rows.Count - 1].ItemArray[1])) > 0) && (check(de, Convert.ToString(dtbl.Rows[dtbl.Rows.Count - 1].ItemArray[1])) > 0))))
        {
            return 1;
        }
        else
            return 0;
    }
    public void getStartAndEndId()
    {
        string ds = Convert.ToString(ViewState["txtDatestart"]);
        string de = Convert.ToString(ViewState["txtDateend"]);
        int idstart = 0;
        int idend = 0;
        DataTable dtbl = new DataTable();
        dtbl = ViewState["fullusermsg"] as DataTable;
        ViewState.Remove("fullusermsg");
        for (int i = 0; i < dtbl.Rows.Count; i++)
        {
            int c = check(ds, Convert.ToString(dtbl.Rows[i].ItemArray[1]));
            if (c <= 0)
            {
                idstart = Convert.ToInt32(dtbl.Rows[i].ItemArray[0]);
                break;
            }

        }

        for (int i = 0; i < dtbl.Rows.Count; i++)
        {
            int c = check(de, Convert.ToString(dtbl.Rows[i].ItemArray[1]));
            if (c < 0)
            {
                idend = Convert.ToInt32(dtbl.Rows[i - 1].ItemArray[0]);
                break;
            }

        }
        if (idend == 0)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());//load all messages of the user
            con.Open();
            SqlDataAdapter check1 = new SqlDataAdapter(("Select MAX(Message_Id) from Send_Master "), con);
            DataTable dtbl2 = new DataTable();
            check1.Fill(dtbl2);
            idend = Convert.ToInt32(dtbl2.Rows[0].ItemArray[0]);
            con.Close();
            con.Dispose();
        }
        ViewState["idstart"] = idstart;
        ViewState["idend"] = idend;
    }
    public int CountMsg()
    {
        int countm = 0;
        for (int i = 0; i < (ViewState["finalmsg"] as DataTable).Rows.Count; i++)
        {
            countm = countm + Convert.ToInt32((ViewState["finalmsg"] as DataTable).Rows[i].ItemArray[5]);
        }
        return countm;
    }
   
    public void ShowCollectedMsgs(int total)
    {
         lblrecord.Visible = false;
        GridView1.Visible = true;
        txtcount.Visible = true;
        Label5.Visible = true;
        txtcount.Text = Convert.ToString(total);
        GridView1.DataSource =ViewState["finalmsg"] as DataTable;
        GridView1.DataBind();
        ViewState.Remove("finalmsg");
    }
    public int check(String d1, String d2)
    {
            String MyString =d1;
            DateTime MyDateTime = new DateTime();
            MyDateTime = DateTime.ParseExact(MyString, "dd-MM-yyyy", null);
            String MyString_new = MyDateTime.ToString("MM-dd-yyyy");
            DateTime d3 = Convert.ToDateTime(MyString_new);


             String MyString1 =d2;
            DateTime MyDateTime2 = new DateTime();
            MyDateTime2 = DateTime.ParseExact(MyString1, "dd-MM-yyyy", null);
            String MyString_new1 = MyDateTime2.ToString("MM-dd-yyyy");
        DateTime d4 = Convert.ToDateTime(MyString_new1);
        int year = d3.Year;
        int m = d3.Month;
        int d = d3.Day;

        DateTime date1 = new DateTime(year, m, d, 0, 0, 0);
        year = d4.Year;
        m = d4.Month;
        d = d4.Day;

        DateTime date2 = new DateTime(year, m, d, 0, 0, 0);
        int result = DateTime.Compare(date1, date2);
        if (result < 0)
            return -1;
        else if (result == 0)
            return 0;
        else
            return 1;
    }
    public void AssignValuesToDateStrings()
    {
        string ds, de;
        txtmember.Text = txtmember.Text.Trim();
        if (txtDatestart.Text == "")
        {
            ds = "01-01-1990";
        }
        else
        {
            ds = txtDatestart.Text;
        }
        if (txtDateend.Text == "")
        {
          
            de = Convert.ToString(ViewState["date"]);
        }
        else
        {
            de = txtDateend.Text;
        }
        ViewState["txtDatestart"] = ds;
        ViewState["txtDateend"] = de;
    }
    public void nodatafound()
    {

        GridView1.Visible = false;
        txtcount.Visible = false;
        Label5.Visible = false;
        lblrecord.Visible = true;
        lblrecord.Text = "No Record found matching the requested information";
    }
   
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        ViewState["index"] = e.NewPageIndex;
        Button1_Click(new object(), new EventArgs());
    }
    protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            int index = e.Row.RowIndex;
            Label lblt = (Label)e.Row.FindControl("lblt") as Label;
            DataTable dtbl = new DataTable();
            try
            {
                lblt.ToolTip = Convert.ToString((ViewState["finalmsg"] as DataTable).Rows[((Convert.ToInt32(ViewState["index"])) * 10) + index].ItemArray[1]);
            }
            catch (IndexOutOfRangeException)
            {
            }
        }

    }
}

