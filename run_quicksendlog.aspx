<%@ Page Title="" Language="C#" MasterPageFile="~/smsMasterUser.master" AutoEventWireup="true" CodeFile="run_quicksendlog.aspx.cs" Inherits="run_quicksendlog" %>

<%@ Register Src="~/QuickSendLog_Master.ascx" TagPrefix="uc1" TagName="QuickSendLog_Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:QuickSendLog_Master runat="server" ID="QuickSendLog_Master" />
</asp:Content>

