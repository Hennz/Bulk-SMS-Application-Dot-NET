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


public partial class QuickSendLog_Master : System.Web.UI.UserControl
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
          //  DateTime todaydate = DateTime.Today;
            ///DateTime dt = DateTime.ParseExact(todaydate.ToString(), "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
         // string s = dt.ToString("dd-MM-yyyy");
           // txtDatestart.Text = s;
        //    txtDateend.Text =s;
         //   ViewState["date"]=s;
            filldropdown();
        }
        
    }
    public void filldropdown()
    {
        DataTable dtbl = new DataTable();
        dtbl.Columns.Add("Status_Id");
        dtbl.Columns.Add("Status_Name");
        dtbl.Rows.Add("All", "All");
        dtbl.Rows.Add("Sent","Sent");
        dtbl.Rows.Add("Failed","Failed");
       
        dropdown.DataSource = dtbl;
        dropdown.DataTextField = "Status_Name";
        dropdown.DataValueField = "Status_Id";
        dropdown.DataBind();
    }
   

    protected void Button1_Click(object sender, EventArgs e)
    {
        
    String ds = "";

    String de = "";
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
            de =Convert.ToString( ViewState["date"]);
        }
        else
        {
            de = txtDateend.Text;
        }
        
      ViewState["txtDatestart"] = ds;
        ViewState["txtDateend"] = de;
        int c = check(ds, de);
        if (c < 0 || c == 0)
        {
        getlog();
        }
        else
        {
          invisible();
                    lblrecord.Text = "Error:Invalid Date Entry";
                    lblrecord.Visible = true;
        }
    }
    public void getlog()
    {
        string ds = Convert.ToString(ViewState["txtDatestart"]);
        string de = Convert.ToString(ViewState["txtDateend"]);
         int idstart = 0;
        int idend = 0;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter dad = new SqlDataAdapter(("Select IndividualMsg_Id,Message_Date from IndividualSend_Master Order by IndividualMsg_Id "), con);
        DataTable dtbl = new DataTable();
        dad.Fill(dtbl);
        try
        {
            if (!(((check(ds, Convert.ToString(dtbl.Rows[0].ItemArray[1])) < 0) && (check(de, Convert.ToString(dtbl.Rows[0].ItemArray[1])) < 0)) || ((check(ds,         Convert.ToString(dtbl.Rows[dtbl.Rows.Count - 1].ItemArray[1])) > 0) && (check(de, Convert.ToString(dtbl.Rows[dtbl.Rows.Count - 1].ItemArray[1])) > 0))))
            {
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
                        idend = Convert.ToInt32(dtbl.Rows[i-1].ItemArray[0]);
                        break;
                    }

                }

                 if (idend == 0)
                {
                    SqlDataAdapter check1 = new SqlDataAdapter(("Select MAX(IndividualMsg_Id) from IndividualSend_Master "), con);
                    DataTable dtbl2 = new DataTable();
                    check1.Fill(dtbl2);
                    idend = Convert.ToInt32(dtbl2.Rows[0].ItemArray[0]);
                }
                string query;
                 if (dropdown.SelectedItem.Value == "All")
                 {
                      query = "Select Mobile_NUmber,Message_Text,Message_Date,Message_Time,Message_Length,Message_SendTotal,Status from IndividualSend_Master where (IndividualMsg_Id>='" + idstart + "') and (IndividualMsg_Id<='" + idend + "')  and individualsend_master.mobile_number LIKE '" + txtmobile.Text.Trim() + "%'  and User_id=(Select user_id from user_master where user_name='" + Convert.ToString(Session["user"]) + "' )  Order by IndividualMsg_Id";
                 }
                 else
                 {
                     query = "Select Mobile_NUmber,Message_Text,Message_Date,Message_Time,Message_Length,Message_SendTotal,Status from IndividualSend_Master where (IndividualMsg_Id>='" + idstart + "') and (IndividualMsg_Id<='" + idend + "')  and individualsend_master.mobile_number LIKE '" + txtmobile.Text.Trim() + "%'  and User_id=(Select user_id from user_master where user_name='" + Convert.ToString(Session["user"]) + "' ) and status='" + dropdown.SelectedItem.Value + "' Order by IndividualMsg_Id";
                 }
                SqlDataAdapter dad1 = new SqlDataAdapter(query, con);
                DataTable dtbl1 = new DataTable();
                dad1.Fill(dtbl1);
                int countm = 0;
                for (int i = 0; i < dtbl1.Rows.Count; i++)
                {
                    countm = countm + Convert.ToInt32(dtbl1.Rows[i].ItemArray[5]);
                }
                if (countm != 0)
                {
                     lblrecord.Visible=false;
                    GridView1.Visible = true;
                    txtcount.Visible = true;
                    Label5.Visible = true;
                    txtcount.Text = Convert.ToString(countm);
                    GridView1.DataSource = dtbl1;
                    ViewState["data"] = dtbl1;
                    GridView1.DataBind();
                    UpdatePanel1.Update();
                    con.Close();
                }
                else
                {
                    invisible();
                    lblrecord.Text = "No Record found matching the requested information";
                    lblrecord.Visible = true;
                }
            }
            else
            {
               invisible();
                    lblrecord.Text = "No Record found matching the requested information";
                    lblrecord.Visible = true;
            }
        }
        catch (IndexOutOfRangeException )
        {
            invisible();
                    lblrecord.Text = "No Record found matching the requested information";
                    lblrecord.Visible = true;
        }
    }
    public void invisible()
    {
        GridView1.Visible = false;
        txtcount.Visible = false;
        Label5.Visible = false;
    }
    public int check(String d1, String d2)
    {
      String MyString =d1;
            DateTime MyDateTime = new DateTime();
            MyDateTime = DateTime.ParseExact(MyString, "dd-MM-yyyy", null);
            String MyString_new = MyDateTime.ToString("MM-dd-yyyy");
        DateTime d3 = Convert.ToDateTime(MyString_new);
 MyString =d2;
            DateTime MyDateTime2 = new DateTime();
            MyDateTime2 = DateTime.ParseExact(MyString, "dd-MM-yyyy", null);
             MyString_new = MyDateTime2.ToString("MM-dd-yyyy");
        DateTime d4 = Convert.ToDateTime(MyString_new);
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
                lblt.ToolTip = Convert.ToString((ViewState["data"] as DataTable).Rows[((Convert.ToInt32(ViewState["index"])) * 10) + index].ItemArray[1]);
            }
            catch (IndexOutOfRangeException)
            {

            }
        }
    }

   
}














