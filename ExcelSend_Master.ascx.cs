using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Xml;
using System.Globalization;


public partial class ExcelSend_Master : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("run_login.aspx");
        }
        if (!IsPostBack)
        {
            lblMsg2.Visible = false;
            filldropdownlist();
            filldropdownlist2();
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
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt64(ddcategory.SelectedItem.Value) == -1)
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please select Group and the Category first";
            return;
        }
        if (btnUpload.Text == "Upload")
        {
            refresh.Visible = true;
            btnUpload.Visible = false;
            //Get path from web.config file to upload
            string FilePath = "~//excell//";
            string filename = string.Empty;
            //To check whether file is selected or not to uplaod
            if (FileUploadToServer.HasFile)
            {

                bool isvalidfile = checkextension();
                string FileExt = System.IO.Path.GetExtension(FileUploadToServer.PostedFile.FileName);
                if (!isvalidfile)
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Please upload only Excel";
                }
                else
                {

                    int FileSize = FileUploadToServer.PostedFile.ContentLength;
                    if (FileSize <= 10485760)//1048576 byte = 1MB
                    {
                        //Get file name of selected file
                        filename = Path.GetFileName(Server.MapPath(FileUploadToServer.FileName));
                        OleDbConnection con = null;
                            //Save selected file into server location
                            FileUploadToServer.SaveAs(Server.MapPath(FilePath) + "ExcelSend" + FileExt);//Get file path
                            try
                            {
                                string filePath = Server.MapPath(FilePath) + "ExcelSend" + FileExt;
                                //Open the connection with excel file based on excel version

                               
                                if (FileExt == ".xls")
                                {
                                    con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=Excel 8.0;");

                                }
                                else if (FileExt == ".xlsx")
                                {
                                    con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;");
                                }
                            }

                   
                            catch(IOException)
                            {

                            }
                        try
                        {
                            con.Open();
                        }
                        catch (System.InvalidOperationException)
                        {
                            lblMsg.Text = "Please save your Excel 2007 File AS Excel 2003 and then try uploading again";
                            return;
                        }
                        //Get the list of sheet available in excel sheet
                        DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        //Get first sheet name
                        string getExcelSheetName = dt.Rows[0]["Table_Name"].ToString();
                        //Select rows from first sheet in excel sheet and fill into dataset
                        OleDbCommand ExcelCommand = new OleDbCommand(@"SELECT [Party Name],Items,Place,[Delivery Terms],[Qty(bags)],[Rate/Qtl],Pack  FROM [" + getExcelSheetName + @"]", con);
                        OleDbDataAdapter ExcelAdapter = new OleDbDataAdapter(ExcelCommand);
                      
                        try
                        {
                            DataTable dtbl = new DataTable();
                            ExcelAdapter.Fill(dtbl);
                            ViewState["maintable"] = dtbl;
                            sendmessage();
                            con.Close();
                            con.Dispose();
                        }
                        catch(Exception)
                        {
                            lblMsg1.ForeColor = System.Drawing.Color.Red;
                            lblMsg1.Visible = true;
                            lblMsg1.Text = "Error uploading file: Uploaded file is not in correct format.<br>It must atleast consist of columns named :<i>Party Name,Items,Place,Delivery Terms,Qty(bags),Rate/Qtl,Pack</i><br>Correct your file and try uploading it again";
                       }
                    }
                }
            }
            else
            {
                Response.Redirect("run_excelsend.aspx");
            }
        }
    }
    public void sendmessage()
    {
        DataTable temp = new DataTable();
        temp = ViewState["maintable"] as DataTable;

        lblMsg2.Visible = false;
        bool val=checkMessageAvailability(temp.Rows.Count);
        if (val == true)
        {
            DataTable invalidmob = new DataTable();
            invalidmob.Columns.AddRange(new DataColumn[3] { new DataColumn("Member_Name"), new DataColumn("Member_Mobile"), new DataColumn("Row_Number") });
            DataTable unavailablemob = new DataTable();
            unavailablemob.Columns.AddRange(new DataColumn[2] { new DataColumn("Member_Name"),  new DataColumn("Row_number") });
            getusercredentials();
            for (int i = 0; i < temp.Rows.Count; i++)
            {
                Int64 membMob = checkmember(temp.Rows[i].ItemArray[0].ToString().ToUpper());
                if (membMob != 0)
                {
                   string msgtext= getfinalmsgtext(i);
                  bool status=  message(membMob,msgtext);
                  if (status == false)
                  {
                      invalidmob.Rows.Add(temp.Rows[i].ItemArray[0].ToString().ToUpper(), membMob,i+1);
                  }
                }
                else
                {
                    unavailablemob.Rows.Add(temp.Rows[i].ItemArray[0].ToString().ToUpper(), i + 1);
                }
            }
            lblMsg1.Visible=false;
            lblMsg2.Visible = false;
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Visible = true;
            
            if (invalidmob.Rows.Count != 0)
            {
                lblMsg1.ForeColor = System.Drawing.Color.Red;
                lblMsg1.Visible = true;
                lblMsg1.Text = "Error sending message to following users:-"+"\n"+" It may be due to their invalid mobile numbers, connection problems etc";
                GridView3.DataSource = invalidmob;
                GridView3.DataBind();
            }
            if (unavailablemob.Rows.Count != 0)
            {
                lblMsg2.Visible = true;
                lblMsg2.Text = "Following members are not registered in your database:- If you still want to send them the message :- Type their mobile numbers and press send  for each one of them";
                ViewState["unavailablemob"] = unavailablemob;
                showUnavailablemob();
            }
            if ((invalidmob.Rows.Count != 0) || (unavailablemob.Rows.Count != 0))
            {
                lblMsg.Text = " Messages Sent to all<br/> Except to the members shown below due to mentioned reasons below. Please look into them. ";
            }
            else
            {
                 lblMsg.Text = " Messages Sent to all successfully";
            }
        }
        else
        {
            lblMsg2.Visible = true;
            lblMsg2.Text = "Error sending message:You do not have enough messages left to perform this operation. <br/>Kindly contact admin ";
        }
    }
    public void showUnavailablemob()
    {
        DataTable newdata = ViewState["unavailablemob"] as DataTable;
        GridView1.DataSource = newdata;
        GridView1.DataBind();
    }
    public string getfinalmsgtext(int rowindex)
    {
        getHeaderFooter();

        DataTable temp = new DataTable();
        temp = ViewState["maintable"] as DataTable;
        string text = "Item:- " + Convert.ToString(temp.Rows[rowindex].ItemArray[1]) + "\n" + "Party Name:- " + Convert.ToString(temp.Rows[rowindex].ItemArray[0]).ToUpper() + "\n" + "Place:- " + Convert.ToString(temp.Rows[rowindex].ItemArray[2]) + "\n" + "Delivery Terms:- " + Convert.ToString(temp.Rows[rowindex].ItemArray[3]) + "\n" + "Qty:- " + Convert.ToString(temp.Rows[rowindex].ItemArray[4]) + "\n" + "Rate:- " + Convert.ToString(temp.Rows[rowindex].ItemArray[5]) + "\n" + "Packs:- " + Convert.ToString(temp.Rows[rowindex].ItemArray[6]);
        if (chkheader.Checked == true && chkfooter.Checked == false)
        {
            return Convert.ToString(ViewState["header"]) + "\n" + text;
        }
        else if (chkheader.Checked == true && chkfooter.Checked == true)
        {
            return Convert.ToString(ViewState["header"]) + "\n" + text + "\n" + Convert.ToString(ViewState["footer"]);
        }
        else if (chkheader.Checked == false && chkfooter.Checked == true)
        {
            return text + "\n" + Convert.ToString(ViewState["footer"]);
        }
        else
        {
            return text;
        }
    }
    public void getHeaderFooter()
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
    }

    public bool checkMessageAvailability(int msgToBeSent)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter cmd = new SqlDataAdapter(("Select   Message_Remaining from User_Master where user_name='"+Convert.ToString(Session["user"])+"' "), con);
        DataTable dtbl = new DataTable();
        cmd.Fill(dtbl);
        con.Close();
        con.Dispose();
        if (Convert.ToInt32(dtbl.Rows[0].ItemArray[0]) >= msgToBeSent)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool message(Int64 mob,string msg)
    {
        using (var client = new WebClient())
        {
            DataTable apitable = new DataTable();
            apitable = ViewState["api"] as DataTable;
            string finaltext = HttpUtility.UrlEncode(msg);
            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            var data = "=Short Test...";
            string finalurl = apitable.Rows[0].ItemArray[0] + "?user=" + apitable.Rows[0].ItemArray[1] + "&password=" + apitable.Rows[0].ItemArray[2] + "&mobiles=" + mob + "&sms=" + finaltext + "&senderid=" + apitable.Rows[0].ItemArray[3];
            var result = client.UploadString(finalurl, "POST", data);
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(result);
            XmlNodeReader xRead = new XmlNodeReader(xDoc);
            DataSet ds = new DataSet();
            ds.ReadXml(xRead);
            ViewState["smsdataset"] = ds;
           bool status= readsmsdataset(msg,mob);
           return status;

        }
    }
    public bool readsmsdataset(string msg,Int64 mob)
    {
        DataSet smstable = new DataSet();
        smstable = ViewState["smsdataset"] as DataSet;
            if (smstable.Tables[0].TableName == "sms")
            {
                insertintodatabase("Sent",msg,mob);
                return true;
            }
            else
            {
                insertintodatabase("Failed",msg,mob);
                return false;
            }
      
    }
    public void insertintodatabase(string status,string msg,Int64 mob)
    {
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
        cmd.Parameters.Add("memberid", SqlDbType.Int).Value = Convert.ToInt32(ViewState["memberid"]);
        cmd.Parameters.Add("name", SqlDbType.VarChar).Value = Convert.ToString(ViewState["membername"]);
        DateTime todaydate = DateTime.Today;
       // DateTime dt = DateTime.ParseExact(todaydate.ToString(), "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
        //string s = dt.ToString("dd-MM-yyyy");
        cmd.Parameters.Add("date", SqlDbType.VarChar).Value = "29-11-2015";
        cmd.Parameters.Add("text", SqlDbType.VarChar).Value = msg;
        cmd.Parameters.Add("sendstatus", SqlDbType.VarChar).Value = status;
        cmd.Parameters.Add("id", SqlDbType.Int).Value = idinput;
        TimeZoneInfo IND_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime daytime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IND_ZONE);
        cmd.Parameters.Add("time", SqlDbType.Time).Value = daytime.TimeOfDay;
        cmd.Parameters.Add("length", SqlDbType.Int).Value = (msg).Length;
        cmd.Parameters.Add("total", SqlDbType.Int).Value = 1;
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
        if (status == "Sent")
        {
            updateremainingmessages(1);
        }

    }

    public void updateremainingmessages(int msg)
    {
        int rmsg = getremainingmessages();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlCommand cmd = new SqlCommand(("Update User_MAster Set Message_Remaining=@msg where user_name='" + Convert.ToString(Session["user"]) + "' "), con);
        cmd.Parameters.Add("msg", SqlDbType.Int).Value = rmsg - msg;
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
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
    public Int64 checkmember(string membername)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        conn.Open();
        SqlDataAdapter check = new SqlDataAdapter(("Select Member_Master.Member_Mobile,Member_Master.Member_Id from Member_Master where UPPER(Member_Master.Member_Name)='" + membername + "' and Member_Master.Category_Id='" + ddcategory.SelectedItem.Value + "' "), conn);
        DataTable tempor = new DataTable();
        int count1 = check.Fill(tempor);
        conn.Close();
        if (count1 != 0)
        {
            ViewState["memberid"] = Convert.ToInt32(tempor.Rows[0].ItemArray[1]);
            ViewState["membername"] = membername;
            return Convert.ToInt64(tempor.Rows[0].ItemArray[0]);
            
        }
        else
        {
            return 0;
        }
    }
    public Boolean checkextension()
    {
        string[] allowdFile = { ".xls", ".xlsx" };
        //Here we are allowing only excel file so verifying selected file pdf or not
        string FileExt = System.IO.Path.GetExtension(FileUploadToServer.PostedFile.FileName);
        //Check whether selected file is valid extension or not
        bool isValidFile = allowdFile.Contains(FileExt);
        return isValidFile;

    }
    protected void download(object sender, EventArgs e)
    {
        Response.Redirect("./SpecimenFileForDownload/specimen2.xlsx");
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "sendmessage")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int RowIndex = gvr.RowIndex;
            GridViewRow row = GridView1.Rows[RowIndex];
            string mob = (row.FindControl("txtmob") as TextBox).Text;
            string msg = getfinalmsgtext(Convert.ToInt32(e.CommandArgument)-1);
           bool status= nomembermessage(msg,mob);
           if (status == true)
           {
               (row.FindControl("lblmembermsgstatus") as Label).Text = "Sent";
               (row.FindControl("txtmob") as TextBox).Enabled = false;
               (row.FindControl("lblsendmsg") as LinkButton).Enabled = false;
               (row.FindControl("lblsendmsg") as LinkButton).Text = "SMS successful";
               (row.FindControl("lblsendmsg") as LinkButton).Attributes["style"] = "color:black;text-decoration:none";
               updateremainingmessages(1);
               //    Response.Write(@"<script Language=""javascript"" >alert('"+name+"')</script>");
           }
           else
           {
               (row.FindControl("lblmembermsgstatus") as Label).Text = "Failed";
           }
        }
    }
    public bool nomembermessage(string msg, string mob)
    {
        using (var client = new WebClient())
        {
            DataTable apitable = new DataTable();
            apitable = ViewState["api"] as DataTable;
            string finaltext = HttpUtility.UrlEncode(msg);
            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            var data = "=Short Test...";
            string finalurl = apitable.Rows[0].ItemArray[0] + "?user=" + apitable.Rows[0].ItemArray[1] + "&password=" + apitable.Rows[0].ItemArray[2] + "&mobiles=" + mob + "&sms=" + finaltext + "&senderid=" + apitable.Rows[0].ItemArray[3];
            var result = client.UploadString(finalurl, "POST", data);
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(result);
            XmlNodeReader xRead = new XmlNodeReader(xDoc);
            DataSet ds = new DataSet();
            ds.ReadXml(xRead);
            ViewState["nomembermessagedataset"] = ds;
            bool status = readsmsset();
            if (status == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public bool readsmsset()
    {
        DataSet smstable = new DataSet();
        smstable = ViewState["nomembermessagedataset"] as DataSet;
        if (smstable.Tables[0].TableName == "sms")
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        showUnavailablemob();
    }
}