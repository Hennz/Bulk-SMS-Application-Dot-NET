<%@ Page Title="" Language="C#" MasterPageFile="~/smsMaster.master" AutoEventWireup="true" CodeFile="run_login.aspx.cs" Inherits="run_login" %>

<%@ Register Src="~/Login_Master.ascx" TagPrefix="uc1" TagName="Login_Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Login_Master runat="server" ID="Login_Master" />
</asp:Content>

