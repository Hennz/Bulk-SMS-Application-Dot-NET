<%@ Page Title="" Language="C#" MasterPageFile="~/smsMasterUser.master" AutoEventWireup="true" CodeFile="run_member.aspx.cs" Inherits="run_member" %>

<%@ Register Src="~/Member_Master.ascx" TagPrefix="uc1" TagName="Member_Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Member_Master runat="server" ID="Member_Master" />
</asp:Content>

