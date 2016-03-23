<%@ Page Title="" Language="C#" MasterPageFile="~/smsMasterUser.master" AutoEventWireup="true" CodeFile="run_activeusers.aspx.cs" Inherits="run_activeusers" %>

<%@ Register Src="~/ActiveUsers_Master.ascx" TagPrefix="uc1" TagName="ActiveUsers_Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ActiveUsers_Master runat="server" ID="ActiveUsers_Master" />
</asp:Content>

