<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Member_Master.ascx.cs" Inherits="Member_Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    .auto-style2 {
        width: 119px;
    }
    .auto-style3 {
        height: 46px;
    }
    .auto-style7 {
        height: 51px;
    }
    .auto-style9 {
        height: 46px;
        width: 48px;

    }
    .auto-style10 {
        width: 48px;
         height: 46px;
    }
    .auto-style11 {
        width: 570px;
    }
    .auto-style12 {
        height: 46px;
        width: 570px;
    }
    .auto-style13 {
        width: 119px;
        height: 46px;
    }
    .auto-style14 {
        width: 119px;
        height: 26px;
    }
    .auto-style15 {
        width: 48px;
        height: 26px;
    }
    .auto-style16 {
        width: 570px;
        height: 26px;
    }
    .auto-style17 {
        height: 26px;
    }
</style>

<table class="auto-style1">
    <tr>
        <td class="auto-style13">
            <asp:Label ID="lblgroupname" Font-Bold="true" runat="server" Text="Group Name"></asp:Label>
        </td>
        <td class="auto-style9">
            <asp:DropDownList ID="dropdown1" runat="server"  CssClass="form-control" Height="32px" Width="193px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" style="margin-top: 0px">
            </asp:DropDownList><asp:DropDownExtender ID="DropDownExtender1" TargetControlID="dropdown1" runat="server"></asp:DropDownExtender>

        </td>
        <td class="auto-style12">
            <asp:Label ID="lblgroupname0" Font-Italic="true" runat="server" Text="If you do not see your group or category in the list, reload the page or activate that group or category in the register page "></asp:Label>
        </td>
        <td class="auto-style3">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style13">
            <asp:Label ID="lblcategoryname" Font-Bold="true" runat="server" Text="Category Name"></asp:Label>
        </td>
        <td class="auto-style9">
            <asp:DropDownList ID="dropdown" runat="server" CssClass="form-control" Height="32px" Width="193px" AutoPostBack="True"  style="margin-top: 0px" Enabled="False" OnSelectedIndexChanged="dropdown_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:DropDownExtender ID="DropDownExtender2" TargetControlID="dropdown" runat="server"></asp:DropDownExtender>
        </td>
        <td class="auto-style12">
            &nbsp;</td>
        <td class="auto-style3">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style14">
            <asp:Label ID="lblmembername" runat="server"  Font-Bold="true" Text="Member Name"></asp:Label>
        </td>
        <td class="auto-style15">
            <asp:TextBox ID="txtmembername" runat="server" CssClass="form-control" Width="305px" AutoCompleteType="Disabled" Height="32px"></asp:TextBox>
        </td>
        <td class="auto-style16">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtmembername" ErrorMessage="Please enter a valid Member Name" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtmembername" ErrorMessage="Please enter a valid Member Name" ForeColor="Red" ValidationExpression="^[a-zA-Z'.\s]{1,50}"></asp:RegularExpressionValidator>
        </td>
        <td class="auto-style17">
            </td>
    </tr>
    <tr>
        <td class="auto-style2">
            <asp:Label ID="lblmembermobile" runat="server" Font-Bold="true" Text="Mobile Number"></asp:Label>
        </td>
        <td class="auto-style10">
            <asp:TextBox ID="txtmembermobile" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" Placeholder="Enter mobile number without using +91 or 91 or 0" Height="32px"  Width="305px" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
        </td>
        <td class="auto-style11">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtmembermobile" ErrorMessage="Please enter a valid Mobile Number" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtmembermobile" ErrorMessage="Please enter a valid Member Mobile" ForeColor="Red" ValidationExpression="^([7-9]{1})([0-9]{9})$"></asp:RegularExpressionValidator>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style2">
            <asp:Label ID="lblmemberemail" Font-Bold="true" runat="server" Text=" E-mail"></asp:Label>
        </td>
        <td class="auto-style10">
            <asp:TextBox ID="txtmemberemail" runat="server" CssClass="form-control" Width="305px"  Height="32px" AutoCompleteType="Disabled"></asp:TextBox>
        </td>
        <td class="auto-style11">
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtmemberemail" ErrorMessage="Please enter a valid Email" ForeColor="Red" ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$"></asp:RegularExpressionValidator>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style3">
            <asp:Label ID="lblmemberaddress" Font-Bold="true" runat="server" Text="Address"></asp:Label>
        </td>
        <td class="auto-style9">
            <asp:TextBox ID="txtmemberaddress" runat="server" CssClass="form-control" Height="32px" Width="308px" AutoCompleteType="Disabled"></asp:TextBox>
        </td>
        <td class="auto-style12">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtmemberaddress" ErrorMessage="Please enter a valid Address" ForeColor="Red"></asp:RequiredFieldValidator>
        </td>
        <td class="auto-style3">
            </td>
    </tr>
    <tr>
        <td class="auto-style7" colspan="3">
            <asp:Button ID="butmembersubmit" runat="server" CssClass="btn btn-primary btn-md" OnClick="butmembersubmit_Click" style="margin-left: 51px; margin-right: 71px" Text="Submit" Width="155px" />
            <asp:Button ID="butmemberreset" runat="server" CssClass="btn btn-primary btn-md" OnClick="butmemberreset_Click" CausesValidation="false" Text="Reset" Width="183px" />
            <asp:Button ID="butlink" runat="server" CssClass="btn btn-primary btn-md" CausesValidation="false" OnClick="butlink_Click" style="margin-left: 161px" Text="Upload Excel File" Width="215px" />
        </td>
        <td class="auto-style7">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style3" colspan="4">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-condensed table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" Height="213px" Width="958px"  style="margin-right: 19px" OnRowCommand="GridView1_RowCommand" AllowPaging="True"   OnPageIndexChanging="GridView1_PageIndexChanging" >
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="MEMBER NAME">
                                <ItemTemplate>
                                <asp:Label ID="lblmembername" runat="server" text='<%#Eval("Member_Name") %>'></asp:Label>
                            </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOBILE NO.">
                                <ItemTemplate>
                                <asp:Label ID="lblmembermobile" runat="server" text='<%#Eval("Member_Mobile") %>'></asp:Label>
                            </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EMAIL">
                                <ItemTemplate>
                                <asp:Label ID="lblmemberemail" runat="server" text='<%#Eval("Member_Email") %>'></asp:Label>
                            </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ADDRESS">
                                <ItemTemplate>
                                <asp:Label ID="lblmemberaddress" runat="server" text='<%#Eval("Member_Address") %>'></asp:Label>
                            </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="STATUS">
                                <ItemTemplate>
                                <asp:Label ID="lblmemberstatus" runat="server" text='<%#Eval("Status") %>'></asp:Label>
                            </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EDIT">
                                <ItemTemplate>
                                <asp:LinkButton ID="lblmemberedit" runat="server" CausesValidation="False"  CommandName="editrow" CommandArgument='<%#Eval("Member_Id") %>' Text='Edit' ></asp:LinkButton>
                            </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CHANGE STATUS">
                        <ItemTemplate>
                                <asp:LinkButton ID="lblmemberchangestatus" runat="server" CausesValidation="False"  CommandName="changestatus" CommandArgument='<%#Eval("Member_Id") %>' Text='Change Status' ></asp:LinkButton>
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
