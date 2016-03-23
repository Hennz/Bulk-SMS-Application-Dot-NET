<%@ Page Title="" Language="C#" MasterPageFile="~/smsMasterUser.master" AutoEventWireup="true" CodeFile="run_quicksend.aspx.cs" Inherits="run_quicksend" %>

<%@ Register Src="~/QuickSend_Master.ascx" TagPrefix="uc1" TagName="QuickSend_Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:QuickSend_Master runat="server" ID="QuickSend_Master" />
</asp:Content>

