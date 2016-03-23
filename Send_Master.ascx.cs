using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Net;
using System.Xml;
using System.Globalization;

public partial class Send_Master : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!Page.IsPostBack)
        {
            txtfinalmessage.Attributes.Add("readonly", "readonly");
            filldropdownlist();
            filldropdownlist2();
            fillhidden();
       }
    }
   public void filldropdownlist2()
    {
        DataTable dtbl1 = new DataTable();
        dtbl1.Columns.Add("Category_Id");
        dtbl1.Columns.Add("Category_Name");
        dtbl1.Rows.Add(-1, "Select Category Name");
        dropdown1.DataSource = dtbl1;
        dropdown1.DataTextField = "Category_Name";
        dropdown1.DataValueField = "Category_Id";
        dropdown1.DataBind();
    }
    public void filldropdownlist()
    {
        cbselectall.Visible = false;
        butsend.Visible = false;
        DataTable dtbl = new DataTable();
        DataTable dtbl1 = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter(("SELECT Group_Id,Group_Name from Group_Master where user_id=(Select user_id from User_master where user_name ='"+Convert.ToString(Session["user"])+"')"), con);
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
    }
    protected void DropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(dropdown.SelectedItem.Value) != -1)
        {
            filldropdownlist1();
            dropdown1.Enabled = true;
            butsend.Visible = false;
            cbselectall.Visible = false;
            GridView1.Visible = false;
        }
        else
        {
            filldropdownlist2();
            dropdown1.Enabled = false;
            butsend.Visible = false;
            cbselectall.Visible = false;
            GridView1.Visible = false;
        }
      
     
    }
    public void filldropdownlist1()
    {
        DataTable dtbl = new DataTable();
        DataTable dtbl1 = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter(("SELECT Category_Id,Category_Name from Category_Master where Group_Id='" + dropdown.SelectedItem.Value + "'"), con);
        con.Open();
        dad.Fill(dtbl);
        dtbl1.Columns.Add("Category_Id");
        dtbl1.Columns.Add("Category_Name");
        dtbl1.Rows.Add(-1, "Select Category Name");
        for (int i = 0; i < dtbl.Rows.Count; i++)
        {
            dtbl1.Rows.Add(dtbl.Rows[i].ItemArray[0], dtbl.Rows[i].ItemArray[1]);
        }
        dropdown1.DataSource = dtbl1;
        dropdown1.DataTextField = "Category_Name";
        dropdown1.DataValueField = "Category_Id";
        dropdown1.DataBind();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsgreport.Visible = false;
        DataTable dtbl = new DataTable();
        GridView1.Visible = true; 
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter(("SELECT Member_Id,Member_Name,Member_Mobile from Member_Master where Category_Id='" + dropdown1.SelectedItem.Value + "' and Status='Active'"), con);
        con.Open();
        int a = dad.Fill(dtbl);
        if (a != 0 )
        {
            lblMsg.Text = "";
            butsend.Visible = true;
            cbselectall.Visible = true;
            cbselectall.Checked = false;
        }
        else
        {
           butsend.Visible=false;
            if( Convert.ToInt32(dropdown1.SelectedItem.Value) != -1)
            {
            cbselectall.Visible = false;
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "No member exists in this category";
            }
           else
          {
            cbselectall.Visible = false;
            lblMsg.Text="";
           }
        }
        GridView1.DataSource = dtbl;
        GridView1.DataBind();
    }
    protected void cbselectall_CheckedChanged(object sender, EventArgs e)
    {

        if (cbselectall.Checked == true)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkCtrl") as CheckBox);
                    chkRow.Checked = true;
                }
            }
        }
        else
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkCtrl") as CheckBox);
                    chkRow.Checked = false;
                }
            }
        }
    }
    protected void chkCtrl_CheckedChanged(object sender, EventArgs e)
    {
        int counter=0;
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkCtrl") as CheckBox);
                if (chkRow.Checked == false)
                {
                    cbselectall.Checked = false;
                }

            }
        }
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkCtrl") as CheckBox);
                if (chkRow.Checked == false)
                {
                    counter = 1;
                }

            }
        }
        if (counter == 0)
        {
            cbselectall.Checked = true;
        }
    }
    protected void butsend_Click(object sender, EventArgs e)
    {
       
        lblmsgreport.Visible = false;
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Id"),new DataColumn("Name"),new DataColumn("Mobile")});
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkCtrl") as CheckBox);
                if (chkRow.Checked)
                {
                    string idinput = (row.Cells[1].FindControl("lblmemberid") as Label).Text;
                    string mobile = (row.Cells[2].FindControl("lblmembermobile") as Label).Text;
                    string nameinput = (row.Cells[3].FindControl("lblmembername") as Label).Text;
                    dt.Rows.Add(idinput,nameinput,mobile);
                }
            }
        }
        ViewState["table"] = dt;
        int countcheck=0;
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkCtrl") as CheckBox);
                if (chkRow.Checked)
                {
                    countcheck++;
                }
            }
        }  
        int pass=  validateavailablemessages(countcheck);
        if (countcheck != 0)
        {
            if (pass == 1)
            {
                sendmessage();
            }
            else
            {
                String TransferPage;
                TransferPage = "<script>alert('You do not have enough messages left to perform this operation')</script>";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", TransferPage, false);
                //Response.Write(@"<script Language=""javascript"" >alert('You do not have enough messages left to perform this operation ')</script>");
            }
        }
        else
        {
            String TransferPage;
            TransferPage = "<script>alert('Please select atleast one member')</script>";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", TransferPage, false);
            //Response.Write(@"<script Language=""javascript"" >alert('Please select atleast one member')</script>");
        }
    }
    public void sendmessage()
    {
        string mobile = ""; ;
        DataTable table = new DataTable();
        table=ViewState["table"] as DataTable;
        for (int i = 0; i < table.Rows.Count; i++)
        {
            if (i == 0)
            {
                mobile = Convert.ToString(table.Rows[0].ItemArray[2]);
            }
            else
            {
                mobile = mobile +","+ Convert.ToString(table.Rows[i].ItemArray[2]);
            }
        }
        using (var client = new WebClient())
        {
            getusercredentials();
            DataTable apitable = new DataTable();
            apitable = ViewState["api"] as DataTable;
            string msg = HttpUtility.UrlEncode(txtfinalmessage.Text);
            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            var data = "=Short Test...";
            string finalurl = apitable.Rows[0].ItemArray[0] + "?user=" + apitable.Rows[0].ItemArray[1] + "&password=" + apitable.Rows[0].ItemArray[2] + "&mobiles=" + mobile + "&sms=" + msg + "&senderid=" + apitable.Rows[0].ItemArray[3];
            var result = client.UploadString(finalurl, "POST", data);
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(result);
            XmlNodeReader xRead = new XmlNodeReader(xDoc);
            DataSet ds = new DataSet();
            ds.ReadXml(xRead);
            ViewState["smsdataset"] = ds;
            readsmsdataset();
        }
    }
    public void readsmsdataset()
    {
        DataSet smstable = new DataSet();
        smstable = ViewState["smsdataset"] as DataSet;
        if (smstable.Tables.Count == 1)
        {
            if (smstable.Tables[0].TableName == "sms")
            {
                lblmsgreport.Visible = true;
                insertintodatabase("Sent");
                lblmsgreport.Text = "Message sent to the selected member(s) successfully";
                txtmessage.Text = "";
            }
            else
            {
                lblmsgreport.Visible = true;
                insertintodatabase("Failed");
                lblmsgreport.Text = "Error: Message was not sent to any of the selected member(s). Please check the mobile numbers of the members.";
                txtmessage.Text = "";
            }
        }
        else
        {
            successfullsms();
            failedsms();
        }   
    }
    public void successfullsms()
    {
        DataSet smstable = new DataSet();
        smstable = ViewState["smsdataset"] as DataSet;
        DataTable smssend = new DataTable();
        smssend = smstable.Tables[1];
        for(int i=0; i < smssend.Rows.Count;i++)
        {
            insertintodatabase(Convert.ToInt64(Convert.ToString(smssend.Rows[i].ItemArray[2]).Substring(3,10)),"Sent");
        }
        updateremainingmessages(smssend.Rows.Count * Convert.ToInt16(Math.Ceiling((Convert.ToDouble((txtfinalmessage.Text).Length)) / 160.00)));
    }
    public void insertintodatabase(Int64 num,string status)
    {
        getmemberdetails(num);
        insert(status);   
    }
    public void failedsms()
    {
        string fail = "";
        DataSet smstable = new DataSet();
        smstable = ViewState["smsdataset"] as DataSet;
        DataTable smssend = new DataTable();
        smssend = smstable.Tables[0];
        for (int i = 0; i < smssend.Rows.Count; i++)
        {
            insertintodatabase(Convert.ToInt64(Convert.ToString(smssend.Rows[i].ItemArray[3]).Substring(3,10)), "Failed");
            if (i == 0)
            {
                fail = Convert.ToString(ViewState["membername"]);
            }
            else
            {
                fail = fail + Convert.ToString(ViewState["membername"]);
            }
        }
        lblmsgreport.Visible=true;
        lblmsgreport.Text = "Error: Message sent to all except "+fail;
     }
    public void insert(string status)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        DataTable dtbl = new DataTable();
        dtbl = ViewState["memberdetails"] as DataTable;
        ViewState["membername"]=dtbl.Rows[0].ItemArray[1];
        SqlDataAdapter check1 = new SqlDataAdapter(("Select MAX(Message_Id) from Send_Master "), con);
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
        SqlCommand cmd = new SqlCommand(("insert into Send_Master values (@id,@memberid,@name,@text,@date,@time,@length,@total,@sendstatus) "), con);
        cmd.Parameters.Add("memberid", SqlDbType.Int).Value = Convert.ToInt32(dtbl.Rows[0].ItemArray[0]);
        cmd.Parameters.Add("name", SqlDbType.VarChar).Value = Convert.ToString(dtbl.Rows[0].ItemArray[1]); 
         DateTime todaydate = DateTime.Today;
         DateTime dt = DateTime.ParseExact(todaydate.ToString(), "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
         string s = dt.ToString("dd-MM-yyyy");
        cmd.Parameters.Add("date", SqlDbType.VarChar).Value = s;
        cmd.Parameters.Add("text", SqlDbType.VarChar).Value = txtfinalmessage.Text;
        cmd.Parameters.Add("sendstatus", SqlDbType.VarChar).Value = status;
        cmd.Parameters.Add("id", SqlDbType.Int).Value = idinput;
        TimeZoneInfo IND_ZONE=TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
         DateTime daytime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IND_ZONE);
         cmd.Parameters.Add("time", SqlDbType.Time).Value = daytime.TimeOfDay;
        cmd.Parameters.Add("length", SqlDbType.Int).Value = (txtfinalmessage.Text).Length;
        cmd.Parameters.Add("total", SqlDbType.Int).Value = Math.Ceiling((Convert.ToDouble((txtfinalmessage.Text).Length)) / 160.00);
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
    }
    public void getmemberdetails(Int64 mobile)
    {
        DataTable dtbl = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter dad = new SqlDataAdapter(("SELECT Member_Id,Member_Name from Member_Master where Category_Id='" + dropdown1.SelectedItem.Value + "' and Member_Mobile='"+mobile+"'"), con);
        int a = dad.Fill(dtbl);
        ViewState["memberdetails"] = dtbl;
        con.Close();
        con.Dispose();
    }
    public void insertintodatabase(string status)
    {
        int msgcount = 0;
        int i = 0;
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkCtrl") as CheckBox);
                if (chkRow.Checked)
                {
                    DataTable dtbl = new DataTable();
                    dtbl = ViewState["table"] as DataTable;
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
                    con.Open();
                    SqlDataAdapter check1 = new SqlDataAdapter(("Select MAX(Message_Id) from Send_Master "), con);
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
                    SqlCommand cmd = new SqlCommand(("insert into Send_Master values (@id,@memberid,@name,@text,@date,@time,@length,@total,@sendstatus) "), con);
                    cmd.Parameters.Add("memberid", SqlDbType.Int).Value = Convert.ToInt32(dtbl.Rows[i].ItemArray[0]);
                    cmd.Parameters.Add("name", SqlDbType.VarChar).Value = Convert.ToString(dtbl.Rows[i].ItemArray[1]);
                    DateTime todaydate = DateTime.Today;
                 //  DateTime dt = DateTime.ParseExact(todaydate.ToString(), "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                   // string s = dt.ToString("dd-MM-yyyy");
                    cmd.Parameters.Add("date", SqlDbType.VarChar).Value = "25-12-2015";
                    cmd.Parameters.Add("text", SqlDbType.VarChar).Value = txtfinalmessage.Text;
                    cmd.Parameters.Add("sendstatus", SqlDbType.VarChar).Value = status;
                    cmd.Parameters.Add("id", SqlDbType.Int).Value = idinput;
                    TimeZoneInfo IND_ZONE=TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime daytime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IND_ZONE);
                    cmd.Parameters.Add("time", SqlDbType.Time).Value = daytime.TimeOfDay;
                    cmd.Parameters.Add("length", SqlDbType.Int).Value = (txtfinalmessage.Text).Length;
                    cmd.Parameters.Add("total", SqlDbType.Int).Value = Math.Ceiling((Convert.ToDouble((txtfinalmessage.Text).Length)) / 160.00);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();
                    i++;
                    msgcount = msgcount + Convert.ToInt32(Math.Ceiling((Convert.ToDouble((txtfinalmessage.Text).Length)) / 160.00));
                }
            }
        }
        if (status == "Sent")
        {
            updateremainingmessages(msgcount);
        }

    }
    public void getusercredentials()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter check1 = new SqlDataAdapter(("Select API_URL,API_USER_ID,API_USER_PASSWORD,API_SENDER_ID from user_master where user_name='" + Convert.ToString(Session["user"]) + "' "), con);
        DataTable apitable = new DataTable();
        check1.Fill(apitable);
        con.Close();
        con.Dispose();
        ViewState["api"] = apitable;
    }
    public int validateavailablemessages(int count)
    {
        int rmsg = getremainingmessages();
        int totalmsgtobesent = count * Convert.ToInt32(Math.Ceiling((Convert.ToDouble((txtfinalmessage.Text).Length)) / 160.00));
        if (rmsg >= totalmsgtobesent)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    public void updateremainingmessages(int msg)
    {
        int rmsg = getremainingmessages();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlCommand cmd = new SqlCommand(("Update User_MAster Set Message_Remaining=@msg where user_name='" + Convert.ToString(Session["user"])  +"' "), con);
        cmd.Parameters.Add("msg", SqlDbType.Int).Value = rmsg - msg;
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
    }
    public int getremainingmessages()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter cmd = new SqlDataAdapter(("Select   Message_Remaining from User_Master where user_name='"+Convert.ToString(Session["user"])+"' "), con);
        DataTable dtbl = new DataTable();
        cmd.Fill(dtbl);
        con.Close();
        con.Dispose();
        return Convert.ToInt32(dtbl.Rows[0].ItemArray[0]);
    }
    public void fillhidden()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        DataTable dtbl1 = new DataTable();
        SqlDataAdapter dad1 = new SqlDataAdapter(("Select header,footer from User_Master where user_id=(Select User_iD from USER_MASTER where user_name ='" + Convert.ToString(Session["user"]) + "')"), con);
        dad1.Fill(dtbl1);
        con.Close();
        con.Dispose();
        ViewState["header"] = Convert.ToString(dtbl1.Rows[0].ItemArray[0]);
        ViewState["footer"] = Convert.ToString(dtbl1.Rows[0].ItemArray[1]);
        hdnheader.Value = Convert.ToString(dtbl1.Rows[0].ItemArray[0]);
        hdnfooter.Value = Convert.ToString(dtbl1.Rows[0].ItemArray[1]);
    }
}













