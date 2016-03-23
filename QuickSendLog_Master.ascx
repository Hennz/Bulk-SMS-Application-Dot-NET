<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuickSendLog_Master.ascx.cs" Inherits="QuickSendLog_Master" %>
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

    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
           
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
           
            return true;
        }</script>
</header>
<style type="text/css">

    .auto-style1 {
        width: 100%;
    }
    
    .auto-styl13 {
        width: 208px;
        height: 62px;
    }
    .auto-style6 {
        height: 62px;
        width: 319px;
    }
    .auto-style17 {
        height: 62px;
        width: 337px;
    }
    .auto-style19 {
        width: 520px;
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

    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"   runat="server">
        <ContentTemplate>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style13">
                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Enter Date (Format:  &lt;span style=&quot;color:red;&quot;&gt;&lt;b&gt;dd-mm-yyyy&lt;/b&gt;&lt;/span&gt;)"></asp:Label>
                    </td>
                    <td class="auto-style6">
                        <asp:TextBox ID="txtDatestart" runat="server" CssClass="date" AutoCompleteType="Disabled" MaxLength="10" Placeholder="Start Date"></asp:TextBox>
                        
                        <asp:RegularExpressionValidator ID="regexpDate" runat="server" ControlToValidate="txtDatestart" ErrorMessage="Date not in specified format" ForeColor="Red" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))-((0[1-9])|(1[0-2])))|((31-((0[13578])|(1[02])))|((29|30)-((0[1,3-9])|(1[0-2])))))-((20[0-9][0-9]))|(29-02-20(([02468][048])|([13579][26]))))$"></asp:RegularExpressionValidator>
                    </td>
                    <td class="auto-style17">
                        <asp:TextBox ID="txtDateend" runat="server" CssClass="date" AutoCompleteType="Disabled"  MaxLength="10" Placeholder="End Date"></asp:TextBox>
                       
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDateend" ErrorMessage="Date not in specified format" ForeColor="Red" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))-((0[1-9])|(1[0-2])))|((31-((0[13578])|(1[02])))|((29|30)-((0[1,3-9])|(1[0-2])))))-((20[0-9][0-9]))|(29-02-20(([02468][048])|([13579][26]))))$"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style20">
                        <asp:Label ID="lblstatus" Font-Bold="true" runat="server" Text="Select Message Status"></asp:Label>
                    </td>
                    <td class="auto-style21">
                        <asp:DropDownList ID="dropdown" runat="server"  Height="22px" Width="141px" >
                        </asp:DropDownList> (optional)
                    </td>
                    <td class="auto-style22"><div style="text-align:left;">
                 <asp:UpdateProgress id="UpdateProgress1" runat="server" associatedupdatepanelid="UpdatePanel1" dynamiclayout="true">
                        <progresstemplate>

                       Loading.....<img src="images/loading.gif">

                        </progresstemplate>
                    </asp:UpdateProgress>

                    </div></td>
                    <td class="auto-style20"></td>
                </tr>
                <tr>
                    <td class="auto-style13">
                        <asp:Label ID="lblmobile" Font-Bold="true" runat="server" Text="Search By Mobile Number "></asp:Label>
                    </td>
                    <td class="auto-style6">
                       +91 <asp:TextBox ID="txtmobile" runat="server" AutoCompleteType="Disabled" onkeypress="return isNumberKey(event)" Placeholder="Enter Mobile Number" MaxLength="10"></asp:TextBox>
                    <asp:BalloonPopupExtender ID="txtmobile_BalloonPopupExtender" runat="server" BalloonPopupControlID="panel1" BalloonStyle="Rectangle" DisplayOnFocus="true" Position="BottomRight" TargetControlID="txtmobile">
                        
                    </asp:BalloonPopupExtender>
                    <asp:Panel ID="panel1" runat="server">
                        Please enter the full mobile number or the part of the mobile number you want to search
                    </asp:Panel> (optional)
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style19">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary btn-md"  OnClick="Button1_Click" Text="Get Messages"  />
                    </td>
                    <td class="auto-style18">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label5" runat="server" Text="Total Messages Sent w.r.t given data"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtcount" runat="server" BackColor="#FFFF99" Height="21px" ReadOnly="True" Width="33px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style19">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="table table-striped tbale-condensed" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="189px" OnPageIndexChanging="GridView1_PageIndexChanging" style="margin-left: 0px" Width="446px" OnRowDataBound="GridView1_RowDataBound">
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
                                <asp:TemplateField HeaderText="Mobile">
                                    <ItemTemplate>
                                        <asp:Label ID="lbln" runat="server" text='<%#Eval("Mobile_Number") %>'></asp:Label>
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
                                        <asp:Label ID="lblt" runat="server" Width="200px" text='<%# Eval("Message_Text").ToString().Substring(0,Math.Min(25,Eval("Message_Text").ToString().Length)) + "...." %>'></asp:Label>
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
                    <td class="auto-style18">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
<asp:Label ID="lblrecord" runat="server" ForeColor="Red" Visible="False"></asp:Label>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Button1" />
        </Triggers>
    </asp:UpdatePanel>



    











