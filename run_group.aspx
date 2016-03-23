<%@ Page Title="" Language="C#" MasterPageFile="~/smsMasterUser.master" AutoEventWireup="true" CodeFile="run_group.aspx.cs" Inherits="run_group" %>

<%@ Register Src="~/Group_Master.ascx" TagPrefix="uc1" TagName="Group_Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Group_Master runat="server" id="Group_Master" />
</asp:Content>

