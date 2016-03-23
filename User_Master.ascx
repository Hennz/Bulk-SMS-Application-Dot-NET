<%@ Control Language="C#" AutoEventWireup="true" CodeFile="User_Master.ascx.cs" Inherits="User_Master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<header>
<script type="text/javascript" >
    function DisableBackButton() {
        window.history.forward()
    }
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }
 </script>
</header>
<style type="text/css">
    #hidden
{
 display:none;
}

    .auto-style1 {
        
        : 100%;
    }
    .auto-style2 {
    }
    .auto-style3 {
        width: 93px;
        height: 25px;
    }
    .auto-style4 {
        width: 537px;
    }
    .auto-style5 {
        width: 115px;
        height: 25px;
    }
    .auto-style6 {
        width: 537px;
        height: 25px;
    }
    .auto-style7 {
        height: 25px;
    }
    .auto-style8 {
        width: 93px;
        height: 27px;
    }
    .auto-style9 {
        height: 25px;
        width: 462px;
    }
    .auto-style10 {
        width: 462px;
        height: 27px;
    }
    .auto-style11 {
        height: 27px;
    }
    .auto-style12 {
        width: 537px;
        height: 27px;
    }
    .auto-style13 {
        height: 30px;
    }
    .auto-style14 {
        width: 537px;
        height: 30px;
    }
    .auto-style15 {
        height: 24px;
        margin-left: 40px;
    }
    .auto-style16 {
        width: 537px;
        height: 24px;
    }
    
</style>
<script type="text/javascript">
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }

</script>
<table class="auto-style1">
    <tr>
        <td class="auto-style5">
            <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="First Name"></asp:Label>
        </td>
        <td class="auto-style6">
            <asp:TextBox ID="txtfname" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfname" ErrorMessage="Enter a valid name" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtfname" ErrorMessage="Please enter a valid  Name" ForeColor="Red" ValidationExpression="^[a-zA-Z'.\s]{1,50}"></asp:RegularExpressionValidator>
        </td>
        <td class="auto-style3">
            <asp:Label ID="Label8" Font-Bold="true" runat="server" Text="URL API"></asp:Label>
        </td>
        <td class="auto-style9">
            <asp:TextBox ID="txtapiurl" runat="server"  AutoCompleteType="Disabled"></asp:TextBox>
            <asp:BalloonPopupExtender ID="txtapiurl_BalloonPopupExtender" runat="server"  BalloonPopupControlID="Panel1"  TargetControlID="txtapiurl" Position="BottomRight"></asp:BalloonPopupExtender>
            <asp:Panel ID="Panel1" runat="server">e.g. http://trans.smsindustry.com/sendsms.jsp </asp:Panel>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtapiurl"></asp:RequiredFieldValidator>
            </td>
        <td class="auto-style7">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style2">
            <asp:Label ID="Label7" Font-Bold="true" runat="server" Text="Last Name"></asp:Label>
        </td>
        <td class="auto-style2">
            <asp:TextBox ID="txtlname" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtlname" ErrorMessage="Enter a valid name" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtlname" ErrorMessage="Please enter a valid  Name" ForeColor="Red" ValidationExpression="^[a-zA-Z'.\s]{1,50}"></asp:RegularExpressionValidator>
        </td>
        <td></td>
        <td></td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style11">
            <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="Username"></asp:Label>
        </td>
        <td class="auto-style12">
            <asp:TextBox ID="txtuname" runat="server"  AutoCompleteType="Disabled"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtuname" ErrorMessage="Username cannot be empty" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
        <td class="auto-style8">
            <asp:Label ID="lbluser" Font-Bold="true" runat="server" Text="User Id"></asp:Label>
        </td>
        <td class="auto-style10">
            <asp:TextBox ID="txtapiuser" runat="server"  AutoCompleteType="Disabled"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtapiuser"></asp:RequiredFieldValidator>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
        <td class="auto-style11">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style13">
            <asp:Label ID="Label3" Font-Bold="true" runat="server" Text="Password"></asp:Label>
        </td>
        <td class="auto-style14">
            <asp:TextBox ID="txtpassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtpassword" ErrorMessage="Password cannot be empty" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
        <td class="auto-style13">
            <asp:Label ID="lblpass" runat="server" Font-Bold="true" Text="API Password"></asp:Label>
        </td>
        <td class="auto-style13">

            <asp:TextBox ID="txtapipassword" runat="server"  AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtapipassword"></asp:RequiredFieldValidator>

        </td>
        

    </tr>
    <tr>
        <td class="auto-style15">
            <asp:Label ID="Label4" Font-Bold="true" runat="server" Text="Email"></asp:Label>
        </td>
        <td class="auto-style16">
            <asp:TextBox ID="txtemail" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtemail" ErrorMessage="Enter a valid Email Id" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtemail" ErrorMessage="Please enter a valid Email" ForeColor="Red" ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$"></asp:RegularExpressionValidator>
        </td>
        <td class="auto-style15">
            <asp:Label ID="lblsenderid" Font-Bold="true" runat="server" Text="Sender Id"></asp:Label>
        </td>
        <td class="auto-style15">

            <asp:TextBox ID="txtapisender" runat="server" AutoCompleteType="Disabled"></asp:TextBox>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtapisender"></asp:RequiredFieldValidator>

        </td>
    </tr>
    <tr>
        <td class="auto-style2">
            <asp:Label ID="Label5" Font-Bold="true" runat="server" Text="Mobile Number"></asp:Label>
        </td>
        <td class="auto-style4">
            <asp:TextBox ID="txtmobile" onkeypress="return isNumberKey(event)"  runat="server" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtmobile" ErrorMessage="Enter a valid Mobile Number" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtmobile" ErrorMessage="Please enter a valid Member Mobile" ForeColor="Red" ValidationExpression="^([7-9]{1})([0-9]{9})$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="auto-style2">
            <asp:Label ID="Label6" Font-Bold="true" runat="server" Text="Address"></asp:Label>
        </td>
        <td class="auto-style4">
            <asp:TextBox ID="txtaddress" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtaddress" ErrorMessage="Enter a valid Address" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="auto-style2" colspan="2">
            <asp:Button ID="butsubmit" CssClass="btn btn-primary btn-md" runat="server" style="margin-left: 100px" Text="Submit" Width="124px" OnClick="butsubmit_Click" />
            <asp:Button ID="butreset" runat="server" CssClass="btn btn-primary btn-md" style="margin-left: 139px" Text="Reset" CausesValidation="false" Width="124px" OnClick="butreset_Click" />
        </td>
    </tr>
    <tr>
        <td class="auto-style2" colspan="3">&nbsp;</td>
    </tr>
</table>
<asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-condensed" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="First Name">
              <ItemTemplate>
                                <asp:Label ID="lbluserfname" runat="server" text='<%#Eval("First_Name") %>'></asp:Label>
                            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Last Name">
              <ItemTemplate>
                                <asp:Label ID="lbluserlname" runat="server" text='<%#Eval("Last_Name") %>'></asp:Label>
                            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Username">
              <ItemTemplate>
                                <asp:Label ID="lblusername" runat="server" text='<%#Eval("User_Name") %>'></asp:Label>
                            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Password">
              <ItemTemplate>
                                <asp:Label ID="lbluserpwd" runat="server" text='<%#Eval("Password") %>'></asp:Label>
                            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Email">
              <ItemTemplate>
                                <asp:Label ID="lbluseremail" runat="server" text='<%#Eval("Email") %>'></asp:Label>
                            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Mobile Number">
              <ItemTemplate>
                                <asp:Label ID="lblusermobile" runat="server" text='<%#Eval("Mobile") %>'></asp:Label>
                            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Address">
              <ItemTemplate>
                                <asp:Label ID="lbluseraddress" runat="server" text='<%#Eval("Address") %>'></asp:Label>
                            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Messages Remaining">
              <ItemTemplate>
                                <asp:Label ID="lblusermsgrem" runat="server" text='<%#Eval("Message_Remaining") %>'></asp:Label>
                            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Verification Status">
            <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server" text='<%#Eval("Verification_Status") %>'></asp:Label>
                            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status">
              <ItemTemplate>
                                <asp:Label ID="lbluserstatus" runat="server" text='<%#Eval("Status") %>'></asp:Label>
                            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Edit">
            
                                <ItemTemplate>
                                <asp:LinkButton ID="lbluseredit" runat="server" CausesValidation="False"  CommandName="editrow" CommandArgument='<%#Eval("User_Id") %>' Text='Edit' ></asp:LinkButton>
                            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Change Status">
           
                                <ItemTemplate>
                                <asp:LinkButton ID="lbluserchangestatus" runat="server" CausesValidation="False"  CommandName="changestatus" CommandArgument='<%#Eval("User_Id") %>' Text='Change Status' ></asp:LinkButton>
                            </ItemTemplate>
        </asp:TemplateField>
                <asp:TemplateField HeaderText="Login Record">
                        <ItemTemplate>
                                <asp:LinkButton ID="lblloginrecord" runat="server" CausesValidation="False"  CommandName="viewloginrecord" CommandArgument='<%#Eval("User_Id") %>' Text='Last 10 Login' ></asp:LinkButton>
                            </ItemTemplate>

                    </asp:TemplateField>

    </Columns>
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
     
     
    
    
</asp:GridView>

<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Visible="False">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="Login Time Stamp">
            <ItemTemplate><asp:Label ID="lbltime" Width="150px" runat="server" text='<%#Eval("Login_TimeStamp") %>'></asp:Label>
                </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
     
     
    
    
</asp:GridView>


&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="buthide" runat="server" CssClass="btn btn-primary btn-md" OnClick="buthide_Click" CausesValidation="false" Text="Hide" Visible="False" />
<br />


<asp:Label ID="lblnologin" runat="server" ForeColor="Red" Text="No Log In Record  Found for the selected user" Visible="False"></asp:Label>
