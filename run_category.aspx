<%@ Page Title="" Language="C#" MasterPageFile="~/smsMasterUser.master" AutoEventWireup="true" CodeFile="run_category.aspx.cs" Inherits="run_category" %>

<%@ Register Src="~/Category_Master.ascx" TagPrefix="uc1" TagName="Category_Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Category_Master runat="server" ID="Category_Master" />
</asp:Content>

