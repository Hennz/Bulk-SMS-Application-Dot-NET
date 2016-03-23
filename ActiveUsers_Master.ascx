<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ActiveUsers_Master.ascx.cs" Inherits="ActiveUsers_Master" %>
<header id="Header1" runat="server">
        <style type="text/css">
    #hidden
{
 display:none;
}
        </style>
    </header>
<p>
    Given below is the list of active users<br />
    </p>
<asp:GridView ID="GridView1" runat="server"  CssClass="table table-striped table-condensed table-bordered" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="Username">
             <ItemTemplate>
                                <asp:Label ID="lbluser" runat="server" text='<%#Eval("User_Name") %>'></asp:Label>
                            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Name">
             <ItemTemplate>
                                <asp:Label ID="lblname" runat="server" text='<%#Eval("name") %>'></asp:Label>
                            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Email">
             <ItemTemplate>
                                <asp:Label ID="lblemail" runat="server" text='<%#Eval("Email") %>'></asp:Label>
                            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Mobile Number">
             <ItemTemplate>
                                <asp:Label ID="lblmobile" runat="server" text='<%#Eval("Mobile") %>'></asp:Label>
                            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Message Remaining">
             <ItemTemplate>
                                <asp:Label ID="lblmsgrem" runat="server" text='<%#Eval("Message_Remaining") %>'></asp:Label>
                            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Last Login">
            <ItemTemplate><asp:Label ID="lbltimestamp"  runat="server" text='<%#Eval("Login_TimeStamp") %>'></asp:Label>
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
            <ItemTemplate><asp:Label ID="lbltime"  runat="server" text='<%#Eval("Login_TimeStamp") %>'></asp:Label>
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


&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="buthide" runat="server" CssClass="btn btn-primary btn-md" CausesValidation="False" Text="Hide"  OnClick="buthide_Click" Visible="False" />
<br />
<asp:Label ID="lblnologin" runat="server" ForeColor="Red" Text="No Log In Record  Found for the selected user" Visible="False"></asp:Label>



