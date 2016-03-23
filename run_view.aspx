<%@ Page Title="" Language="C#" MasterPageFile="~/smsMasterUser.master" AutoEventWireup="true" CodeFile="run_view.aspx.cs" Inherits="run_view" %>

<%@ Register Src="~/Viewsend_Master.ascx" TagPrefix="uc1" TagName="Viewsend_Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Viewsend_Master runat="server" ID="Viewsend_Master" />
</asp:Content>

