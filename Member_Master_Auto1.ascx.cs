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
public partial class Member_Master_Auto1 : System.Web.UI.UserControl
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
            butinsert.Visible = false;
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
                         FileUploadToServer.SaveAs(Server.MapPath(FilePath) + "Member"+FileExt);
                         //Get file path
                         try
                         {
                             string filePath = Server.MapPath(FilePath) + "Member" + FileExt;
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
                         catch( IOException)
                         {
                         }
                         try
                         {
                             con.Open();
                         }
                         catch(System.InvalidOperationException)
                         {
                             lblMsg.Text = "Please save your Excel 2007 File AS Excel 2003 and then try uploading again";
                             return;
                         }
                         //Get the list of sheet available in excel sheet
                         DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                         //Get first sheet name
                         string getExcelSheetName = dt.Rows[0]["Table_Name"].ToString();
                         //Select rows from first sheet in excel sheet and fill into dataset
                         OleDbCommand ExcelCommand = new OleDbCommand(@"SELECT Member_Name,Member_Mobile,Member_Email,Member_Address FROM [" + getExcelSheetName + @"]", con);
                         OleDbDataAdapter ExcelAdapter = new OleDbDataAdapter(ExcelCommand);
                         con.Close();
                         try
                         {

                            DataTable dtbl = new DataTable();
                             ExcelAdapter.Fill(dtbl);
                             ViewState["maintable"] = dtbl;
                            // validatecategoryavailability();
                            //int check= fillerrorcategorygridview();
                            //if (check == 0)
                            //{
                               // ViewState["maintable"] = dtbl;
                            int    validate = validatemembermobile();
                             if (validate == 1)
                               {
                                   fillerrormobilegridview();
                               }
                               else
                               {
                                   ViewState["maintable"] = dtbl;
                             try
                             {
                                 insertmembers();
                             }
                             catch (InvalidCastException)
                             {
                             }
                                   showinsertedmembers();

                              }
                               
                           // }
                         }
                         catch (OleDbException)
                         {
                             lblMsg1.ForeColor = System.Drawing.Color.Red;
                             lblMsg1.Visible = true;
                             lblMsg1.Text = "Error uploading file: Uploaded file is not in correct format.<br>It must consist of columns named :<i>Member_Name,Member_Mobile,Member_Email,Member_Address</i><br>Correct your file and try uploading it again";
                         }
                     }
                }
            }
            else
            {
                lblMsg.Text = "Please select a file to upload.";
            }
        }
        else
        {

            btnUpload.Text = "Upload";
            FileUploadToServer.Visible = true;

        }
    }
  /*  public void validatecategoryavailability()
    {
        DataTable temp = new DataTable();
        temp =ViewState["maintable"] as DataTable;
        ViewState.Remove("maintable");
        DataTable dtbl2 = new DataTable();
        dtbl2.Columns.AddRange(new DataColumn[1] { new DataColumn("Category_Name") });
        for (int i = 0; i < temp.Rows.Count; i++)
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            conn.Open();
            SqlDataAdapter check = new SqlDataAdapter(("Select Category_Master.Category_Id from Category_Master inner join group_Master on group_Master.Group_id=Category_master.Group_Id inner join user_master on user_master.user_id=group_Master.user_id where Category_Master.Category_Name= '" + (Convert.ToString(temp.Rows[i].ItemArray[0])).ToUpper() + "' and user_master.user_id=(select user_id from user_master where user_name='" + Convert.ToString(Session["user"]) + "') "), conn);
            DataTable dtbl1 = new DataTable();
            int count1 = check.Fill(dtbl1);
            conn.Close();
            if (count1 == 0)
            {

                string d = Convert.ToString(temp.Rows[i].ItemArray[0]);
                dtbl2.Rows.Add(d);

            }
        }
        ViewState["categorytable"] = dtbl2;
    }
    public int fillerrorcategorygridview()
    {
        DataTable cat = new DataTable();
        cat = ViewState["categorytable"] as DataTable;
        ViewState.Remove("categorytable");
        if (cat.Rows.Count > 0)
        {
            lblMsg2.Visible = false;
            butinsert.Visible = false;
            GridView2.Visible = true;
            GridView3.Visible = false;
            GridView4.Visible = false;



            lblMsg1.ForeColor = System.Drawing.Color.Red;
            lblMsg1.Text = "Error uploading file: The following category names were not found in database.<br> Please first register these category names and then try uploading file again.";
            GridView2.DataSource = cat;
            GridView2.DataBind();
            return 1;
        }
        return 0;
    }*/
    public Boolean checkextension()
    {
        string[] allowdFile = { ".xls", ".xlsx" };
        //Here we are allowing only excel file so verifying selected file pdf or not
        string FileExt = System.IO.Path.GetExtension(FileUploadToServer.PostedFile.FileName);
        //Check whether selected file is valid extension or not
        bool isValidFile = allowdFile.Contains(FileExt);
        return isValidFile;

    }
    public int validatemembermobile()
    {
        DataTable temp = new DataTable();
        temp = ViewState["maintable"] as DataTable;
        butinsert.Visible = false;
        lblMsg2.Visible = false;
        DataTable invalidmob = new DataTable();
        invalidmob.Columns.AddRange(new DataColumn[2] { new DataColumn("Member_Name"), new DataColumn("Member_Mobile") });
        DataTable validmob = new DataTable();
        validmob.Columns.AddRange(new DataColumn[2] { new DataColumn("Member_Name"), new DataColumn("Member_Mobile") });

        for (int i = 0; i < temp.Rows.Count; i++)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
                conn.Open();
                SqlDataAdapter check = new SqlDataAdapter(("Select Member_Master.Member_Id from Member_Master inner join category_master on member_master.category_id=category_master.category_id inner join group_master on group_master.group_id=category_master.group_id inner join user_master on user_master.user_id=group_master.user_id where member_master.Member_Mobile= '" + Convert.ToInt64(temp.Rows[i].ItemArray[1]) + "' and member_master.category_id='" + ddcategory.SelectedItem.Value + "' and user_master.user_id=(select user_id from user_master where user_name='" + Convert.ToString(Session["user"]) + "') "), conn);
                DataTable tempor = new DataTable();
                int count1 = check.Fill(tempor);
                conn.Close();
                if (count1 != 0)
                {

                    string d = Convert.ToString(temp.Rows[i].ItemArray[0]);
                    Int64 num = Convert.ToInt64(temp.Rows[i].ItemArray[1]);
                    invalidmob.Rows.Add(d, num);

                }
                else
                {
                    lblMsg2.Visible = true;
                    string d = Convert.ToString(temp.Rows[i].ItemArray[0]);
                    Int64 num = Convert.ToInt64(temp.Rows[i].ItemArray[1]);
                    validmob.Rows.Add(d, num);
                    butinsert.Visible = true;
                }
            }
            catch (InvalidCastException)
            {
            }
        }
        if (invalidmob.Rows.Count > 0)
        {
            ViewState["invalidmobdata"] = invalidmob;
            ViewState["validmobdata"] = validmob;
            return 1;
        }
        else
            return 0;
       
    }
    public void fillerrormobilegridview()
    {
        DataTable invalidmob = ViewState["invalidmobdata"] as DataTable;
        DataTable validmob = ViewState["validmobdata"] as DataTable;
        ViewState.Remove("invalidmobdata");
        GridView2.Visible = false;
        GridView3.Visible = true;
        GridView4.Visible = true;

        lblMsg1.ForeColor = System.Drawing.Color.Red;
        lblMsg1.Text = "Error uploading file: The following Member(s) has the mobile number which is already registered in the database. <br>No two members can have same mobile numbers.<br>Please remove this entry and try uploading file again. ";
        GridView3.DataSource =invalidmob;
        GridView3.DataBind();
        GridView4.DataSource = validmob;
        GridView4.DataBind();
        lblMsg2.ForeColor = System.Drawing.Color.Red;
        lblMsg2.Text = " The following entries are valid. If you wish to insert these entries in the database, press the INSERT button.";
    }
    public void insertmembers()
    {
        DataTable temp = new DataTable();
        temp = ViewState["maintable"] as DataTable;
        ViewState.Remove("maintable");
        for (int i = 0; i < temp.Rows.Count; i++)
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            conn.Open();
            SqlDataAdapter check1 = new SqlDataAdapter(("Select MAX(Member_Id) from Member_Master "), conn);
            DataTable dtbl = new DataTable();
            check1.Fill(dtbl);
            int idinput;
            try
            {
                idinput = Convert.ToInt32(dtbl.Rows[0].ItemArray[0]) + 1;
            }
            catch (InvalidCastException)
            {
                idinput = 1;
            }
            if (i == 0)
            {
                ViewState["startid"] = idinput;
            }
            SqlCommand cmd = new SqlCommand(("insert into Member_Master values (@id,'"+ddcategory.SelectedItem.Value+"',@name,@mobile,@email,@address,'Active' ) "), conn);
            cmd.Parameters.Add("id", SqlDbType.Int).Value = idinput;
            cmd.Parameters.Add("name", SqlDbType.VarChar).Value = Convert.ToString(temp.Rows[i].ItemArray[0]).ToUpper();
            cmd.Parameters.Add("mobile", SqlDbType.BigInt).Value = Convert.ToInt64(temp.Rows[i].ItemArray[1]);
            cmd.Parameters.Add("email", SqlDbType.VarChar).Value = Convert.ToString(temp.Rows[i].ItemArray[2]);
            cmd.Parameters.Add("address", SqlDbType.VarChar).Value = Convert.ToString(temp.Rows[i].ItemArray[3]);
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
        }

    }
    public void showinsertedmembers()
    {
        int startid = Convert.ToInt32(ViewState["startid"]);
        ViewState.Remove("startid");
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter("select Member_Id,Category_Id,Member_Name,Member_Mobile,Member_Email,Member_Address,Status from Member_Master where member_id>='"+startid+"' and member_id<=(select MAX(member_id) from member_master) Order by Member_Id", con1);
        DataTable ndtbl = new DataTable();
        con1.Open();
        dad.Fill(ndtbl);
        con1.Close();
        con1.Dispose();
        GridView1.DataSource = ndtbl;
        GridView1.DataBind();
        lblMsg1.Text = "Following data has been uploaded successfully";
        GridView2.Visible = false;
        GridView3.Visible = false;
        GridView4.Visible = false;
        butinsert.Visible = false;
        lblMsg2.Visible = false;


    }
    protected void btninsert_Click(object sender, EventArgs e)
    {
        DataTable validmob = ViewState["validmobdata"] as DataTable;
        DataTable temp = ViewState["maintable"] as DataTable;
         butinsert.Visible = false;
         for (int i = 0; i < temp.Rows.Count; i++)
         {
             for (int j = 0; j < validmob.Rows.Count; j++)
             {

                 if (Convert.ToInt64(temp.Rows[i].ItemArray[1]) == Convert.ToInt64(validmob.Rows[j].ItemArray[1]))
                 {
                     lblMsg2.Text = "Data entered successfully";
                     SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
                     conn.Open();
                     SqlDataAdapter check1 = new SqlDataAdapter(("Select MAX(Member_Id) from Member_Master "), conn);
                     DataTable dtbl = new DataTable();
                     check1.Fill(dtbl);
                     int idinput;
                     try
                     {
                         idinput = Convert.ToInt32(dtbl.Rows[0].ItemArray[0]) + 1;
                     }
                     catch (InvalidCastException)
                     {
                         idinput = 1;
                     }
                     SqlCommand cmd = new SqlCommand(("insert into Member_Master values (@id,'"+ddcategory.SelectedItem.Value+"',@name,@mobile,@email,@address,'Active' ) "), conn);
                     cmd.Parameters.Add("id", SqlDbType.Int).Value = idinput;
                     cmd.Parameters.Add("name", SqlDbType.VarChar).Value = Convert.ToString(temp.Rows[i].ItemArray[0]).ToUpper();
                     cmd.Parameters.Add("mobile", SqlDbType.BigInt).Value = Convert.ToInt64(temp.Rows[i].ItemArray[1]);
                     cmd.Parameters.Add("email", SqlDbType.VarChar).Value = Convert.ToString(temp.Rows[i].ItemArray[2]);
                     cmd.Parameters.Add("address", SqlDbType.VarChar).Value = Convert.ToString(temp.Rows[i].ItemArray[3]);
                     cmd.ExecuteNonQuery();
                     conn.Close();
                     conn.Dispose();
                 }
             }
         }
         btnUpload.Text = "Upload again";
         FileUploadToServer.Visible = false;
    }

    protected void butlink_Click(object sender, EventArgs e)
    {
        Response.Redirect("run_member.aspx");
    }
    protected void download(object sender, EventArgs e)
    {
        Response.Redirect("./SpecimenFileForDownload/specimen.xlsx");
    }
   
}