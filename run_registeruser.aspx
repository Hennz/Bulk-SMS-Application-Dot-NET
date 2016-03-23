<%@ Page Title="" Language="C#" MasterPageFile="~/smsMasterUser.master" AutoEventWireup="true" CodeFile="run_registeruser.aspx.cs" Inherits="run_registeruser" %>

<%@ Register Src="~/User_Master.ascx" TagPrefix="uc1" TagName="User_Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:User_Master runat="server" ID="User_Master" />
</asp:Content>

