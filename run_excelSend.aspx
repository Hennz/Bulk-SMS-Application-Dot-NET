<%@ Page Title="" Language="C#" MasterPageFile="~/smsMasterUser.master" AutoEventWireup="true" CodeFile="run_excelSend.aspx.cs" Inherits="run_excelSend" %>

<%@ Register Src="~/ExcelSend_Master.ascx" TagPrefix="uc1" TagName="ExcelSend_Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ExcelSend_Master runat="server" ID="ExcelSend_Master" />
</asp:Content>

