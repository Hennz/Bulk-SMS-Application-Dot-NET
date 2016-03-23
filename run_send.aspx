<%@ Page Title="" Language="C#" MasterPageFile="~/smsMasterUser.master" AutoEventWireup="true" CodeFile="run_send.aspx.cs" Inherits="run_send" %>

<%@ Register Src="~/Send_Master.ascx" TagPrefix="uc1" TagName="Send_Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Send_Master runat="server" ID="Send_Master" />
</asp:Content>

