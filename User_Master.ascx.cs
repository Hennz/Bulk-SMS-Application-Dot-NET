
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
public partial class User_Master : System.Web.UI.UserControl
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
       if (Session["user"] == null)
      {
         Response.Redirect("default.aspx");
       }
       if (Session["user"].ToString() != "Admin")
        Response.Redirect("run_home.aspx");
        show();
        if (!IsPostBack)
        {
            reset();
        }
    }
    protected void butsubmit_Click(object sender, EventArgs e)
    {
        insert();
        show();
    }
    protected void butreset_Click(object sender, EventArgs e)
    {
        reset();
    }
    public void insert()
    {
        if (butsubmit.Text == "Submit")
        {
            int pass = validatemobilenumber();
            if (pass == 0)
            {
                Response.Write(@"<script Language=""javascript"" >alert('This Mobile Number already exists')</script>");
                return;
            }
            else
            {
                int allow = validateusername();
                if (allow == 0)
                {
                    Response.Write(@"<script Language=""javascript"" >alert('This User Name already exists. Choose another one')</script>");
                    return;
                }
                else
                {
                    insertintodatabase();
                }
            }

        }
        else
        {
            updateintodatabase();
        }
    }
    public void insertintodatabase()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter check1 = new SqlDataAdapter(("Select MAX(User_Id) from User_Master "), con);
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
        SqlCommand cmd = new SqlCommand(("insert into User_Master values (@id,@username,@fname,@lname,@pwd,@email,@emailhash,@mobile,@address,0,'Not Verified','Active','No',NULL,@apiurl,@apiuserid,@apipwd,@apisenderid,NULL,NULL) "), con);
        cmd.Parameters.Add("id", SqlDbType.Int).Value = idinput;
        cmd.Parameters.Add("username", SqlDbType.VarChar).Value = txtuname.Text.Trim();
        cmd.Parameters.Add("fname", SqlDbType.VarChar).Value = txtfname.Text.Trim();
        cmd.Parameters.Add("lname", SqlDbType.VarChar).Value = txtlname.Text.Trim();
        ViewState["hash"] = GetSHA1HashData(txtemail.Text.Trim());
        cmd.Parameters.Add("emailhash", SqlDbType.VarChar).Value = GetSHA1HashData(txtemail.Text.Trim());
        cmd.Parameters.Add("pwd", SqlDbType.VarChar).Value = txtpassword.Text;
        cmd.Parameters.Add("mobile", SqlDbType.BigInt).Value = Convert.ToInt64(txtmobile.Text.Trim());
        cmd.Parameters.Add("email", SqlDbType.VarChar).Value = txtemail.Text.Trim();
        cmd.Parameters.Add("address", SqlDbType.VarChar).Value = txtaddress.Text.Trim();
        cmd.Parameters.Add("apiurl", SqlDbType.VarChar).Value = txtapiurl.Text;
        cmd.Parameters.Add("apiuserid", SqlDbType.VarChar).Value = txtapiuser.Text;
        cmd.Parameters.Add("apipwd", SqlDbType.VarChar).Value = txtapipassword.Text;
        cmd.Parameters.Add("apisenderid", SqlDbType.VarChar).Value = txtapisender.Text;
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
        sendmail();
        reset();
        Response.Write(@"<script Language=""javascript"" >alert('Data entered successfully')</script>");
    }
    public void updateintodatabase()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlCommand cmd = new SqlCommand(("UPDATE User_Master SET User_Name=@username,First_Name=@fname,Last_Name=@lname,Password=@pwd, Mobile=@mobile,Email_Hash=@emailhash,Email=@email, Address=@address ,API_URL=@apiurl,API_USER_ID=@apiuserid,API_USER_PASSWORD=@apipwd,API_SENDER_ID=@apisenderid where User_Id=@uid "), con);
        cmd.Parameters.Add("uid", SqlDbType.Int).Value = Convert.ToInt32(ViewState["updateid"]);
        cmd.Parameters.Add("username", SqlDbType.VarChar).Value = txtuname.Text.Trim();
        cmd.Parameters.Add("fname", SqlDbType.VarChar).Value = txtfname.Text.Trim();
        cmd.Parameters.Add("lname", SqlDbType.VarChar).Value = txtlname.Text.Trim();
        cmd.Parameters.Add("pwd", SqlDbType.VarChar).Value = txtpassword.Text;
        cmd.Parameters.Add("emailhash", SqlDbType.VarChar).Value = GetSHA1HashData(txtemail.Text.Trim());
        cmd.Parameters.Add("mobile", SqlDbType.BigInt).Value = Convert.ToInt64(txtmobile.Text.Trim());
        cmd.Parameters.Add("email", SqlDbType.VarChar).Value = txtemail.Text.Trim();
        cmd.Parameters.Add("address", SqlDbType.VarChar).Value = txtaddress.Text.Trim();
        cmd.Parameters.Add("apiurl", SqlDbType.VarChar).Value = txtapiurl.Text;
        cmd.Parameters.Add("apiuserid", SqlDbType.VarChar).Value = txtapiuser.Text;
        cmd.Parameters.Add("apipwd", SqlDbType.VarChar).Value = txtapipassword.Text;
        cmd.Parameters.Add("apisenderid", SqlDbType.VarChar).Value = txtapisender.Text;
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
        Response.Write(@"<script Language=""javascript"" >alert('Data updated successfully')</script>");
        reset();
        show();
    }
    public int validatemobilenumber()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter dad = new SqlDataAdapter(("Select Mobile from User_Master where Mobile=@mobile "), con);
        dad.SelectCommand.Parameters.Add("mobile", SqlDbType.BigInt).Value = Convert.ToInt64(txtmobile.Text.Trim());
        DataTable dtbl = new DataTable();
        int count = dad.Fill(dtbl);
        if (count == 0)
            return 1;
        else
            return 0;
    }
    public int validateusername()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter dad = new SqlDataAdapter(("Select User_Name from User_Master where  User_Name='" + txtuname.Text.Trim() + "'"), con);
        DataTable dtbl = new DataTable();
        int count = dad.Fill(dtbl);
        if (count == 0)
            return 1;
        else
            return 0;
    }
    public void show()
    {
        GridView1.Visible = true;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter("select User_Name,First_Name,Last_Name,Password,Email,Mobile,Address,Message_Remaining,Verification_Status,Status,User_Id from User_Master Order by User_Id", con);
        DataTable dtbl = new DataTable();
        con.Open();
        int intStatus = dad.Fill(dtbl);
        con.Close();
        con.Dispose();

        if (intStatus > 0 )
        {
            GridView1.DataSource = dtbl;
            GridView1.DataBind();
        }
    }
    public void reset()
    {
        butreset.Visible = true;
        butsubmit.Text = "Submit";
        txtfname.Text = "";
        txtlname.Text = "";
        txtuname.Text = "";
        txtpassword.Text = "";
        txtaddress.Text = "";
        txtemail.Text = "";
        txtmobile.Text = "";
        txtapiurl.Text = "";
        txtapisender.Text = "";
        txtapipassword.Text = "";
        txtapiuser.Text = "";
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editrow")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            SqlDataAdapter dad = new SqlDataAdapter("select User_Name,First_Name,Last_Name,Password,Email,Mobile,Address,User_Id,API_URL,API_USER_ID,API_USER_PASSWORD,API_SENDER_ID from User_Master Where User_Id='" + Convert.ToInt32(e.CommandArgument) + "'", con);
            DataTable dtbl = new DataTable();
            con.Open();
            dad.Fill(dtbl);
            con.Close();
            con.Dispose();
            txtuname.Text = Convert.ToString(dtbl.Rows[0].ItemArray[0]);      
            txtfname.Text = Convert.ToString(dtbl.Rows[0].ItemArray[1]);
            txtlname.Text = Convert.ToString(dtbl.Rows[0].ItemArray[2]);
            txtpassword.Text = Convert.ToString(dtbl.Rows[0].ItemArray[3]);
            txtemail.Text = Convert.ToString(dtbl.Rows[0].ItemArray[4]);
            txtmobile.Text = Convert.ToString(dtbl.Rows[0].ItemArray[5]);
            txtaddress.Text= Convert.ToString(dtbl.Rows[0].ItemArray[6]);
            txtapiurl.Text = Convert.ToString(dtbl.Rows[0].ItemArray[8]);
            txtapiuser.Text = Convert.ToString(dtbl.Rows[0].ItemArray[9]);
            txtapipassword.Text = Convert.ToString(dtbl.Rows[0].ItemArray[10]);
            txtapisender.Text = Convert.ToString(dtbl.Rows[0].ItemArray[11]);
            butsubmit.Text = "Update";
            butreset.Visible = false;
            ViewState["updateid"] = Convert.ToInt32(dtbl.Rows[0].ItemArray[7]);

        }
        if (e.CommandName == "changestatus")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            SqlDataAdapter dad = new SqlDataAdapter(("SELECT Status from User_Master where User_Id='" + Convert.ToInt32(e.CommandArgument) + "'"), con);
            DataTable dtbl = new DataTable();
            con.Open();
            dad.Fill(dtbl);
            if (Convert.ToString(dtbl.Rows[0].ItemArray[0]) == "Active")
            {


                SqlCommand cmd = new SqlCommand(("UPDATE User_Master SET Status='Inactive' Where User_Id=@user_Id"), con);
                cmd.Parameters.Add("user_Id", SqlDbType.Int).Value = Convert.ToInt32(e.CommandArgument);

                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                // Response.Write(@"<script Language=""javascript"" >alert('Status Changed to INACTIVE successfully')</script>");
                show();
            }
            else
            {

                SqlCommand cmd = new SqlCommand(("UPDATE User_Master SET Status='Active' Where User_Id=@user_Id"), con);
                cmd.Parameters.Add("user_Id", SqlDbType.Int).Value = Convert.ToInt32(e.CommandArgument);

                cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();
                //Response.Write(@"<script Language=""javascript"" >alert('Status changed to ACTIVE successfully')</script>");
                show();
            }

        }
      
        if (e.CommandName == "viewloginrecord")
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            SqlDataAdapter dad = new SqlDataAdapter("select Login_TimeStamp From LoginRecord_Master Where User_Id='" + Convert.ToInt32(e.CommandArgument) + "' order by login_recordid", con);
            DataTable dtbl = new DataTable();
            con.Open();
            int numrows=dad.Fill(dtbl);
            con.Close();
            con.Dispose();
            DataTable final = new DataTable();
            final.Columns.Add("Login_TimeStamp");
            int counter = 1;
            int lastlogin = numrows - 1;
            for (int i = 0; i < numrows; i++)
            {
                if(counter==11)
                    break;
                final.Rows.Add(dtbl.Rows[lastlogin].ItemArray[0]);
                counter++;
                lastlogin--;
            }
            if (counter != 1)
            {
                GridView2.Visible = true;
                lblnologin.Visible = false;
                GridView2.DataSource = final;
                GridView2.DataBind();
                buthide.Visible = true;
            
            }
            else
            {
                buthide.Visible = false;
                GridView2.Visible = false;
                lblnologin.Visible = true;
            }
        }
    

    }
    private string GetSHA1HashData(string data)
    {
        //create new instance of md5
        SHA1 sha1 = SHA1.Create();

        //convert the input text to array of bytes
        byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));

        //create new instance of StringBuilder to save hashed data
        StringBuilder returnValue = new StringBuilder();

        //loop for each byte and add it to StringBuilder
        for (int i = 0; i < hashData.Length; i++)
        {
            returnValue.Append(hashData[i].ToString());
        }

        // return hexadecimal string
        return returnValue.ToString();
    }
    public void sendmail()
    {
        try
        {
            MailMessage Msg = new MailMessage();
            // Sender e-mail address.
            Msg.From = new MailAddress("noreply@smssewa.com");
            // Recipient e-mail address.
            Msg.To.Add(txtemail.Text);
            Msg.Subject = "Verify your SMS Sender Account";
            Msg.Body = "Hi " + txtfname.Text + "  " + txtlname.Text + " <br/>Please verify your SMS Sender Account by clicking on this link <a href =\"http://smssewa.com/smssewa/run_confirmaccount.aspx?clientid=" + ViewState["hash"].ToString() + "\">LINK</a><br/><br/>Warm Regards,<br/>SLR Technologies<br/><img src=\"http://www.slrtechnologies.com/images/logo.png \">";
            Msg.IsBodyHtml = true;
            // your remote SMTP server IP.
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "relay-hosting.secureserver.net";
            smtp.Port = 25;
            smtp.Credentials = new System.Net.NetworkCredential("noreply@smssewa.com", "sendsms@slr");
            smtp.EnableSsl = false;
            smtp.Send(Msg);

            //}
            Response.Write(@"<script Language=""javascript"" >alert('Mail sent')</script>");
        }
        catch (Exception )
        {
            Response.Write(@"<script Language=""javascript"" >alert('Mail sending failed')</script>");
            //Console.WriteLine("{0} Exception caught.", ex);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        show();
    }

    protected void buthide_Click(object sender, EventArgs e)
    {
        GridView2.Visible = false;
        buthide.Visible = false;
    }
}
