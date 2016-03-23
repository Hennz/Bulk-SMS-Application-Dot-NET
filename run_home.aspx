<%@ Page Title="" Language="C#" MasterPageFile="~/smsMasterUser.master" AutoEventWireup="true" CodeFile="run_home.aspx.cs" Inherits="run_home" %>

<%@ Register Src="~/Home_Master.ascx" TagPrefix="uc1" TagName="Home_Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Home_Master runat="server" ID="Home_Master" />
</asp:Content>

