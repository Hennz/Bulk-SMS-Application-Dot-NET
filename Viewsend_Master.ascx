<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Viewsend_Master.ascx.cs" Inherits="Viewsend_Master" %>
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

    .auto-style1 {
        width: 100%;
    }
    .auto-style2 {
    }
    .auto-style6 {
        height: 62px;
        width: 298px;
    }
    .auto-style7 {
        width: 208px;
        height: 45px;
    }
    .auto-style8 {
        width: 298px;
        height: 45px;
    }
    .auto-style9 {
        width: 208px;
        height: 40px;
    }
    .auto-style10 {
        width: 298px;
        height: 40px;
    }
    .auto-style11 {
        width: 208px;
        height: 55px;
    }
    .auto-style12 {
        width: 298px;
        height: 55px;
    }
    .auto-style13 {
        width: 208px;
        height: 62px;
    }
    .auto-style15 {
        width: 298px;
    }
    .auto-style16 {
    }
    .auto-style17 {
        height: 62px;
        width: 337px;
    }
    .auto-style18 {
        width: 337px;
    }
    .auto-style20 {
        height: 40px;
    }
    .auto-style21 {
        height: 40px;
        width: 319px;
    }
    .auto-style22 {
        height: 40px;
        width: 337px;
    }
    </style>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <table class="auto-style1">
            <tr>
                <td class="auto-style13">
                    <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Enter Date (Format:  &lt;span style=&quot;color:red;&quot;&gt;&lt;b&gt;dd-mm-yyyy&lt;/b&gt;&lt;/span&gt;)"></asp:Label>
                </td>
                <td class="auto-style6">
                    <asp:TextBox ID="txtDatestart" runat="server" AutoCompleteType="Disabled" CssClass="date"  MaxLength="10" Placeholder="Start Date"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="regexpDate" runat="server" ControlToValidate="txtDatestart" ErrorMessage="Date not in specified format" ForeColor="Red" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))-((0[1-9])|(1[0-2])))|((31-((0[13578])|(1[02])))|((29|30)-((0[1,3-9])|(1[0-2])))))-((20[0-9][0-9]))|(29-02-20(([02468][048])|([13579][26]))))$"></asp:RegularExpressionValidator>
                </td>
                <td class="auto-style17">
                    <asp:TextBox ID="txtDateend" runat="server" AutoCompleteType="Disabled" CssClass="date"  MaxLength="10" Placeholder="End Date"></asp:TextBox>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDateend" ErrorMessage="Date not in specified format" ForeColor="Red" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))-((0[1-9])|(1[0-2])))|((31-((0[13578])|(1[02])))|((29|30)-((0[1,3-9])|(1[0-2])))))-((20[0-9][0-9]))|(29-02-20(([02468][048])|([13579][26]))))$"></asp:RegularExpressionValidator>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">
                    <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="Group"></asp:Label>
                </td>
                <td class="auto-style8">
                    <asp:DropDownList ID="ddgroup" runat="server" AutoPostBack="true" Height="30px" OnSelectedIndexChanged="ddgroup_SelectedIndexChanged" Width="165px">
                    </asp:DropDownList> (optional)
                    &nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style9">
                    <asp:Label ID="Label3" Font-Bold="true"  runat="server" Text="Category"></asp:Label>
                </td>
                <td class="auto-style10">
                    <asp:DropDownList ID="ddcategory" runat="server"  Height="30px" Width="165px" Enabled="False">
                    </asp:DropDownList> (optional)
                </td>
            </tr>
            <tr>
                <td class="auto-style20">
                    <asp:Label ID="lblstatus" Font-Bold="true" runat="server" Text="Select Message Status"></asp:Label>
                </td>
                <td class="auto-style21">
                    <asp:DropDownList ID="dropdown" runat="server"  Height="22px"  Width="141px">
                    </asp:DropDownList> (optional)
                </td>
                <td class="auto-style22"><div style="text-align:left;">
    <asp:UpdateProgress id="UpdateProgress1" runat="server" associatedupdatepanelid="UpdatePanel1" dynamiclayout="true">
                        <progresstemplate>

                           Loading....<img src="images/loading.gif">

                        </progresstemplate>
                    </asp:UpdateProgress>

                    </div></td>
                <td class="auto-style20"></td>
            </tr>
            <tr>
                <td class="auto-style11">
                    <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="Search By Keyword(Name)"></asp:Label>
                </td>
                <td class="auto-style12">
                    <asp:TextBox ID="txtmember" runat="server" AutoCompleteType="Disabled" Height="24px" Width="154px"></asp:TextBox>
                    <asp:BalloonPopupExtender ID="txtmember_BalloonPopupExtender" runat="server" BalloonPopupControlID="panel1" BalloonStyle="Cloud" DisplayOnFocus="true" Position="BottomRight" TargetControlID="txtmember">
                        
                    </asp:BalloonPopupExtender>
                    <asp:Panel ID="panel1" runat="server">
                        Please enter the full name or the part of the name you want to search
                    </asp:Panel> (optional)
                </td>
                <td class="auto-style15"><asp:Button ID="Button1" runat="server"  CssClass="btn btn-primary btn-md" OnClick="Button1_Click" Text="Get Messages" /></td>
                <td></td>
            </tr>
            <tr>
                <td class="auto-style16">&nbsp;</td>
                <td class="auto-style18">
                    </td>
                <td class="auto-style18">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label5" runat="server" Text="Total Messages Sent w.r.t given data"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtcount" runat="server" BackColor="#FFFF99" Height="21px" ReadOnly="True" Width="33px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1" colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2" colspan="4">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="table table-striped table-condensed" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="189px" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" style="margin-left: 0px" Width="446px" >
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Message Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldate" runat="server" text='<%#Eval("Message_Date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Message Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblti" runat="server" text='<%#Eval("Message_Time") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Member Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbln" runat="server" Width="120px" text='<%#Eval("Member_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Message Count">
                                <ItemTemplate>
                                    <asp:Label ID="lblst" runat="server" text='<%#Eval("Message_SendTotal") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Message Length">
                                <ItemTemplate>
                                    <asp:Label ID="lbll" runat="server" text='<%#Eval("Message_Length") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Message Text">
                                <ItemTemplate>
                                    <asp:Label ID="lblt" runat="server" text='<%# Eval("Message_Text").ToString().Substring(0,Math.Min(25,Eval("Message_Text").ToString().Length)) + "...." %>' Width="200px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lbls" runat="server" text='<%#Eval("Status") %>'></asp:Label>
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
                </td>
            </tr>
        </table>
<asp:Label ID="lblrecord" runat="server" ForeColor="Red" Visible="False"></asp:Label>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Button1" />
    </Triggers>
</asp:UpdatePanel>





