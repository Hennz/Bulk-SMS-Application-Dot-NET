using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Net;
using System.Xml;
using System.Globalization;
public partial class QuickSend_Master : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
            txtfinalmessage.Attributes.Add("readonly", "readonly");
            filldropdown();
            fillhidden();
        }
    }

    protected void txtsend_Click(object sender, EventArgs e)
    {
        if (numberpanel.Visible == false && memberpanel.Visible == false)
        {
            return;
        }
       if (numberpanel.Visible == true)
        {
            Int64[] nums = Array.ConvertAll<string,Int64>(Convert.ToString(hdnmobiles.Value).Split(','),Int64.Parse);
            ViewState["nums"] = nums;
        }
        else
        {
            Int64[] nums = Array.ConvertAll<string, Int64>(hdnmobilesforMembers.Value.ToString().Split(','), Int64.Parse);
            ViewState["nums"] = nums;
        }


      // String TransferPage;
     ////  TransferPage = "<script>alert('"+hdnmobilesforMembers.Value.ToString()+"')</script>";
      // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", TransferPage, false);
        
       int pass = validateavailablemessages((ViewState["nums"] as Int64[]).Length);
        if (pass == 1)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            con.Open();
            SqlDataAdapter check1 = new SqlDataAdapter(("Select MAX(IndividualMsg_Id) from IndividualSend_Master "), con);
            DataTable dtbl2 = new DataTable();
            check1.Fill(dtbl2);
            con.Close();
            con.Dispose();
            int idinput;
            try
            {
                idinput = Convert.ToInt32(dtbl2.Rows[0].ItemArray[0]) + 1;
            }
            catch (InvalidCastException)
            {
                idinput = 1;
            }
            ViewState["idinput"] = idinput;
            sendmessage();

        }
        else
        {
            String TransferPage;
            TransferPage = "<script>alert('You do not have enough messages left to perform this operation')</script>";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", TransferPage, false);
           // Response.Write(@"<script Language=""javascript"" >alert('You do not have enough messages left to perform this operation ')</script>");
        }
        
    }
    public void sendmessage()
    {
        string numbers;
        if (numberpanel.Visible == true)
        {
            numbers = hdnmobiles.Value.ToString();
        }
        else
        {
            numbers = hdnmobilesforMembers.Value.ToString();
        }
        using (var client = new WebClient())
        {
            getusercredentials();
            DataTable apitable = new DataTable();
            apitable = ViewState["api"] as DataTable;
            string msg = HttpUtility.UrlEncode(txtfinalmessage.Text);
            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            var data = "=Short Test...";
            string finalurl = apitable.Rows[0].ItemArray[0] + "?user=" + apitable.Rows[0].ItemArray[1] + "&password=" + apitable.Rows[0].ItemArray[2] + "&mobiles=" +numbers+ "&sms=" + msg + "&senderid=" + apitable.Rows[0].ItemArray[3];
            try
            {
                var result = client.UploadString(finalurl, "POST", data);
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(result);
                XmlNodeReader xRead = new XmlNodeReader(xDoc);
                DataSet ds = new DataSet();
                ds.ReadXml(xRead);
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.TableName == "sms")
                    {
                        send(Convert.ToInt32(ViewState["idinput"]), "Sent");
                    }
                    else
                    {

                        send(Convert.ToInt32(ViewState["idinput"]), "Failed");
                    }

                }
            }
            catch (WebException)
            {

                lblmsgreport.Visible = true;
                lblmsgreport.Text = "Error sending message: Connection Problem";
            }
        }
    }
    public void getusercredentials()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter check1 = new SqlDataAdapter(("Select API_URL,API_USER_ID,API_USER_PASSWORD,API_SENDER_ID from user_master where user_name='"+Convert.ToString(Session["user"])+"' "), con);
        DataTable apitable = new DataTable();
        check1.Fill(apitable);
        con.Close();
        con.Dispose();
        ViewState["api"] = apitable;
    }
    public int validateavailablemessages(int l)
    {

        int rmsg = getremainingmessages();
        int totalmsgtobesent = Convert.ToInt32(Math.Ceiling((Convert.ToDouble((txtfinalmessage.Text).Length)) / 160.00));
        if (rmsg >= (totalmsgtobesent*l))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    public int getremainingmessages()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter cmd = new SqlDataAdapter(("Select   Message_Remaining from User_Master where user_name='" + Convert.ToString(Session["user"]) + "' "), con);
        DataTable dtbl = new DataTable();
        cmd.Fill(dtbl);
        con.Close();
        con.Dispose();
        return Convert.ToInt32(dtbl.Rows[0].ItemArray[0]);
    }
    public void filldropdown()
    {
        DataTable dtbl = new DataTable();
        DataTable dtbl1 = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter(("Select distinct mobile_number from individualsend_master where user_id=(Select user_id from user_master where user_name='" + Convert.ToString(Session["user"]) + "') "), con);
        con.Open();
        dad.Fill(dtbl1) ;
        List<string> MobileList = new List<string>();
        dtbl.Columns.Add("Number");
        dtbl.Columns.Add("Mobile_Number");
        dtbl.Rows.Add(-1,"Select Mobile Number");
        for (int i = 0; i < dtbl1.Rows.Count; i++)
        {
            dtbl.Rows.Add(dtbl1.Rows[i].ItemArray[0],Convert.ToString(dtbl1.Rows[i].ItemArray[0]));
            MobileList.Add(Convert.ToString(dtbl1.Rows[i].ItemArray[0]));
        }
        ddhistory.DataSource = dtbl;
        ddhistory.DataTextField = "Mobile_Number";
        ddhistory.DataValueField = "Number";
        ddhistory.DataBind();
        con.Close();
        con.Dispose();
    }
   
    public void send(int id,string status)
    {
        Int64[] nums = ViewState["nums"] as Int64[];
        try
        {
            for (int i = 0; i < nums.Length; i++)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand(("INSERT into individualsend_Master values(@msgid,(Select user_id from user_master where user_name='" + Convert.ToString(Session["user"]) + "' ),@mobile,@text,@date,@time,@length,@sendtotal,@sendstatus)"), con);
                cmd.Parameters.Add("msgid", SqlDbType.Int).Value = id;
              //  if (numberpanel.Visible == true)
              //  {
                    cmd.Parameters.Add("mobile", SqlDbType.BigInt).Value = Convert.ToInt64(nums[i]);
             //   }
              //  else
              //  {
              //      cmd.Parameters.Add("mobile", SqlDbType.BigInt).Value = Convert.ToInt64(txtmobilebyname.Text.Trim());
             //   }
                DateTime todaydate = DateTime.Today;
                DateTime dt = DateTime.ParseExact(todaydate.ToString(), "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                  string s = dt.ToString("dd-MM-yyyy");
                cmd.Parameters.Add("text", SqlDbType.VarChar).Value = txtfinalmessage.Text;
                cmd.Parameters.Add("date", SqlDbType.VarChar).Value = s;
                TimeZoneInfo IND_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime daytime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IND_ZONE);
                cmd.Parameters.Add("time", SqlDbType.Time).Value = daytime.TimeOfDay;
                cmd.Parameters.Add("length", SqlDbType.Int).Value = txtfinalmessage.Text.Length;
                cmd.Parameters.Add("sendtotal", SqlDbType.Int).Value = Math.Ceiling((Convert.ToDouble((txtfinalmessage.Text).Length)) / 160.00);
                cmd.Parameters.Add("sendstatus", SqlDbType.VarChar).Value = status;
                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                
                id++;
            }
            if (status == "Sent")
            {
                updateremainingmessages(nums.Length);
                lblmsgreport.Visible = true;
                lblmsgreport.Text = "Message sent Successfully";
                txtmessage.Text = "";
            }
            else
            {
                lblmsgreport.Visible = true;
                lblmsgreport.Text = "Error sending message: Check mobile number(s) again";

            }
        }
        catch (System.FormatException)
        {
            lblmsgreport.Visible = true;
            lblmsgreport.Text = "Error sending message: No Mobile Number Selected";
            
        }
      
    }
    public void updateremainingmessages(int l)
    {
        int rmsg = getremainingmessages();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlCommand cmd = new SqlCommand(("Update User_Master Set Message_Remaining=@msg where user_name='" + Convert.ToString(Session["user"]) + "' "), con);
        cmd.Parameters.Add("msg", SqlDbType.Int).Value = rmsg - l*(Convert.ToInt32(Math.Ceiling((Convert.ToDouble((txtfinalmessage.Text).Length)) / 160.00)));
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
    }
    public void fillddmember()
    {
        DataTable dtbl = new DataTable();
        DataTable dtbl1 = new DataTable();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter dad = new SqlDataAdapter(("select member_master.member_name + ' ' +group_master.group_name+'( '+category_master.category_name+' ) -'+STR(member_master.member_mobile) as name,member_master.member_mobile as mobile from member_master inner join category_master on category_master.category_id=member_master.category_id inner join group_master on group_master.Group_id=category_master.group_id inner join user_master on user_master.user_id=group_Master.user_id where user_master.User_id=(Select user_id from user_master where user_name='" + Convert.ToString(Session["user"]) + "' ) and member_master.status='Active'  order by group_master.group_name"), con);
        dad.Fill(dtbl1);
        dtbl.Columns.Add("name");
        dtbl.Columns.Add("mobile");
        dtbl.Rows.Add("Select Member",-1);
        for (int i = 0; i < dtbl1.Rows.Count; i++)
        {
            dtbl.Rows.Add(dtbl1.Rows[i].ItemArray[0], Convert.ToString(dtbl1.Rows[i].ItemArray[1]));
        }
        ddmember.DataSource = dtbl;
        ddmember.DataTextField = "name";
        ddmember.DataValueField = "mobile";
        ddmember.DataBind();
        con.Close();
        con.Dispose();
    }
    protected void linknumber_Click(object sender, EventArgs e)
    {
        optionpanel.Visible = false;
        numberpanel.Visible = true;
        memberpanel.Visible = false;
        requirednumber.Enabled = true;
        txtmobile.Text = "";
    }
    protected void linkmember_Click(object sender, EventArgs e)
    {
        txtmobile.Text = "";
        optionpanel.Visible = false;
        numberpanel.Visible = false;
        memberpanel.Visible = true;
        requirednumber.Enabled = false;
        fillddmember();
        txtmobile.Text = "";
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







