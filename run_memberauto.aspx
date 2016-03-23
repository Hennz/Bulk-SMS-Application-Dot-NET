<%@ Page Title="" Language="C#" MasterPageFile="~/smsMasterUser.master" AutoEventWireup="true" CodeFile="run_memberauto.aspx.cs" Inherits="run_memberauto" %>

<%@ Register Src="~/Member_Master_Auto1.ascx" TagPrefix="uc1" TagName="Member_Master_Auto1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Member_Master_Auto1 runat="server" ID="Member_Master_Auto1" />
</asp:Content>

