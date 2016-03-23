<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Login_Master.ascx.cs" Inherits="Login_Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="row">
		<div class="col-md-6">

                <div class="form-group">
                     <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">
                         <ContentTemplate>
			         <div class="col-md-6">

                        <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="Username"></asp:Label>
                        <asp:TextBox ID="txtusername" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox><div><asp:Label ID="lblerror" runat="server" ForeColor="Red" Visible="False"></asp:Label> <asp:Button ID="butsendmail" runat="server" CssClass="btn btn-primary btn-md" OnClick="butsendmail_Click" Text="SEND EMAIL" Visible="False"  /></div>
                         
                        <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="Password"></asp:Label>
                        <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>

                           <div class="col-md-3" style="float:left;padding-top:10px;">
                            <asp:Button ID="butlogin" runat="server"  CssClass="btn btn-primary btn-md" OnClick="butlogin_Click" Text="Login" />
                                     </div>
                        <div class="col-md-9" style="padding-top:10px; float:right">
                                <asp:Button ID="Button1"  CssClass="btn btn-primary btn-md" runat="server" OnClick="Button1_Click"  Text="Forgot Username/Password" />
                               </div>
                         </div>
                        <asp:UpdateProgress id="UpdateProgress1" runat="server" associatedupdatepanelid="UpdatePanel1" dynamiclayout="true">
                        <progresstemplate>

                       Processing ....Please wait.....<img src="images/loading.gif"/>

                        </progresstemplate>
                    </asp:UpdateProgress>
                            </ContentTemplate>
                          <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="butlogin" />
                      </Triggers>
                         <Triggers>
                        <asp:PostBackTrigger ControlID="Button1" />
                      </Triggers>
                      </asp:UpdatePanel>

                   </div>
            </div>

    </div>
