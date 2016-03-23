<%@ Page Title="" Language="C#" MasterPageFile="~/smsMasterUser.master" AutoEventWireup="true" CodeFile="run_userdata.aspx.cs" Inherits="run_userdata" %>

<%@ Register Src="~/Transaction_Master1.ascx" TagPrefix="uc1" TagName="Transaction_Master1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Transaction_Master1 runat="server" ID="Transaction_Master1" />
</asp:Content>

