<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Confirm_Master.ascx.cs" Inherits="Confirm_Master" %>
<style type="text/css">
    .auto-style1 {
        width: 100%;
    }
    .auto-style2 {
        width: 351px;
    }
</style>

<table class="auto-style1">
    <tr>
        <td class="auto-style2">
            <asp:Label ID="Label1" runat="server" Text="Hi " Visible="False"></asp:Label>
            &nbsp;<asp:Label ID="lblname" runat="server" Visible="False"></asp:Label>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style2">
            <asp:Button ID="butlogin" runat="server" CssClass="btn btn-primary btn-md"  OnClick="butlogin_Click" Text="Login" Visible="False"  />
        </td>
        <td>&nbsp;</td>
    </tr>
</table>

