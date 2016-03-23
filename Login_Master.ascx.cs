using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;

public partial class Login_Master : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null)
        {
            Response.Redirect("run_home.aspx");
        }
        if (!IsPostBack)
        {
            reset();
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Button1.Visible = false;
        lblerror.Text = "";
        butsendmail.Visible = false;
        Label1.Text = "Enter Your Registered E-mail ID";
        Label2.Visible = false;
        txtpassword.Visible = false;
        butlogin.Text = "Submit";
    
       
    }
    public void reset()
    {
        txtpassword.Text = "";
        txtusername.Text = "";

    }

    protected void butlogin_Click(object sender, EventArgs e)
    {
        if (butlogin.Text == "Login")
        {
                      
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
            SqlDataAdapter dad = new SqlDataAdapter(("SELECT User_Name,Password from User_Master where User_Name=@name and Password=@pwd and Status='Active' "), con);
            dad.SelectCommand.Parameters.Add("name", SqlDbType.VarChar).Value = txtusername.Text;
            dad.SelectCommand.Parameters.Add("pwd", SqlDbType.VarChar).Value = txtpassword.Text;
            con.Open();
            DataTable dtbl = new DataTable();
            int count = dad.Fill(dtbl);
            con.Close();
            con.Dispose();
            if (count > 0 && txtusername.Text.Equals(Convert.ToString(dtbl.Rows[0].ItemArray[0]), StringComparison.Ordinal) && txtpassword.Text.Equals(Convert.ToString(dtbl.Rows[0].ItemArray[1]), StringComparison.Ordinal))
            {
                int check = checkverification();

                if (check == 1)
                {
                    Session["user"] = txtusername.Text;
                    ChangeOnlineStatus();
                    FillLoginTimeStamp();
                    FillLoginRecord();
                    String TransferPage;
                    TransferPage = "<script>window.open('run_home.aspx','_self');</script>";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", TransferPage, false);
                    //Server.Transfer("run_home.aspx");
                }
                else
                {
                    ViewState["username"] = txtusername.Text;
                    lblerror.Visible = true;
                    lblerror.Text = "Your account is not verified.Please check your registered email inbox for verification email .\nIf you wish to receive the verification email again press SEND EMAIL button";
                    butsendmail.Visible = true;

                }

            }
            else
            {
                lblerror.Visible = true;
                lblerror.Text = "Invalid Username/Password";
                butsendmail.Visible = false;
            }
       }
       else
       {
           sendpassword();
           txtusername.Text = "";
       }
       
    }
    public void FillLoginRecord()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlDataAdapter check1 = new SqlDataAdapter(("Select MAX(Login_RecordId) from LoginRecord_Master "), con);
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
        SqlCommand cmd = new SqlCommand(("INSERT into LoginRecord_Master values(@id,@timestamp,(Select user_id from user_master where user_name='" + Convert.ToString(Session["user"]) + "' ))"), con);
        cmd.Parameters.Add("id", SqlDbType.Int).Value = idinput;
        cmd.Parameters.Add("timestamp", SqlDbType.DateTime2).Value = DateTime.Now;
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
           
    }
    public void FillLoginTimeStamp()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlCommand cmd = new SqlCommand(("Update User_Master Set Login_TimeStamp=@timestamp where User_Name=@username"), con);
        cmd.Parameters.Add("username", SqlDbType.VarChar).Value = txtusername.Text;
        cmd.Parameters.Add("timestamp", SqlDbType.DateTime2).Value = DateTime.Now;
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
    }
    public void ChangeOnlineStatus()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        con.Open();
        SqlCommand cmd = new SqlCommand(("Update User_Master Set Online_Status='Yes' where User_Name=@username"), con);
        cmd.Parameters.Add("username", SqlDbType.VarChar).Value = txtusername.Text;
        cmd.ExecuteNonQuery();
        con.Close();
        con.Dispose();
    }
    public int  checkverification()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter(("SELECT Verification_Status from User_Master where User_Name=@name and Password=@pwd and Verification_Status='Verified' "), con);
        dad.SelectCommand.Parameters.Add("name", SqlDbType.VarChar).Value = txtusername.Text;
        dad.SelectCommand.Parameters.Add("pwd", SqlDbType.VarChar).Value = txtpassword.Text;
        con.Open();
        DataTable dtbl = new DataTable();
        int count = dad.Fill(dtbl);
        con.Close();
        con.Dispose();
        if (count > 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
      
    }

 
public void sendmail()
{
    try
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter(("SELECT First_Name,Last_Name,Email,Email_Hash from User_Master where User_Name=@name  "), con);
        dad.SelectCommand.Parameters.Add("name", SqlDbType.VarChar).Value = Convert.ToString(ViewState["username"]);
        con.Open();
        DataTable dtbl = new DataTable();
        dad.Fill(dtbl);
        con.Close();
        con.Dispose();
 



        MailMessage Msg = new MailMessage();
        // Sender e-mail address.
        Msg.From = new MailAddress("noreply@smssewa.com");
        // Recipient e-mail address.
        Msg.To.Add(Convert.ToString(dtbl.Rows[0].ItemArray[2]));
        Msg.Subject = "Verify your SMS Sender Account";
        Msg.Body = "Hi " + Convert.ToString(dtbl.Rows[0].ItemArray[0]) + "  " + Convert.ToString(dtbl.Rows[0].ItemArray[1]) + " <br/>Please verify your SMS Sender Account by clicking on this link <a href =\"http://smssewa.com/smssewa/run_confirmaccount.aspx?clientid=" + Convert.ToString(dtbl.Rows[0].ItemArray[3]) + "\">LINK</a><br/>Warm Regards,<br/>SLR Technologies<br/><img src=\"http://www.slrtechnologies.com/images/logo.png \">";
        Msg.IsBodyHtml = true;
        // your remote SMTP server IP.
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "relay-hosting.secureserver.net";
        smtp.Port = 25;
        smtp.Credentials = new System.Net.NetworkCredential("noreply@smssewa.com", "sendsms@slr");
        smtp.EnableSsl = false;
        smtp.Send(Msg);


        lblerror.Text = "Mail Sent successfully. Check your email account for the email";
    }
catch (Exception e )
{
    lblerror.Text = Convert.ToString(e);
}
}
protected void butsendmail_Click(object sender, EventArgs e)
{
    //sendpassword();
    sendmail();
    butsendmail.Visible = false;
    ViewState.Remove("username");

}
public void sendpassword()
{
    try
    {
        if (txtusername.Text == "")
        {
           return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString.ToString());
        SqlDataAdapter dad = new SqlDataAdapter(("SELECT First_Name,Last_Name,Email,User_Name,Password from User_Master where Email=@email"), con);
        dad.SelectCommand.Parameters.Add("email", SqlDbType.VarChar).Value = txtusername.Text;
        con.Open();
        DataTable dtbl = new DataTable();
        int rowsAffected = dad.Fill(dtbl);
        con.Close();
        con.Dispose();
        if (rowsAffected == 0)
        { 
            lblerror.Text="Invalid Details Provided";
            lblerror.Visible=true;
            return;
        }
        MailMessage Msg = new MailMessage();
        // Sender e-mail address.
        Msg.From = new MailAddress("noreply@smssewa.com");
        // Recipient e-mail address.
        Msg.To.Add(Convert.ToString(dtbl.Rows[0].ItemArray[2]));
       // Msg.To.Add(Convert.ToString("mayankvikhona@gmail.com"));
        Msg.Subject = "Login details : SMS Sender Account";
        Msg.Body = "Hi " + Convert.ToString(dtbl.Rows[0].ItemArray[0]) + "  " + Convert.ToString(dtbl.Rows[0].ItemArray[1]) + " ,<br/>As you requested,here are your account login details:<br/>Username:- " + Convert.ToString(dtbl.Rows[0].ItemArray[3]) + "<br/>Password:- " + Convert.ToString(dtbl.Rows[0].ItemArray[4]) + " <br/>If you didn't request for it, someone else must have done it.You don't have to worry , your account is secure<br/>Warm Regards,<br/>SLR Technologies<br/><img  src=\"http://www.slrtechnologies.com/images/logo.png \">";
        //Msg.Body = "Hiiiii";
        Msg.IsBodyHtml = true;
        // your remote SMTP server IP.
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "relay-hosting.secureserver.net";
        smtp.Port = 25;
        smtp.Credentials = new System.Net.NetworkCredential("noreply@smssewa.com", "sendsms@slr");
        smtp.EnableSsl = false;
        smtp.Send(Msg);
        //Response.Write(@"<script Language=""javascript"" >alert('Login details have been sent to your email-id')</script>");
        lblerror.Text = "Password have been successfully sent to your Email-Id";
        lblerror.Visible = true;
    }
    catch (Exception e)
    {
        lblerror.Visible = true;
        lblerror.Text = Convert.ToString(e);
       // Response.Write(@"<script Language=""javascript"" >alert('"+Convert.ToString(e)+"')</script>");
    }

}
}





