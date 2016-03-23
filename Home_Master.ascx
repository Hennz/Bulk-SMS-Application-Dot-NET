<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Home_Master.ascx.cs" Inherits="Home_Master" %>
<header>
<script type="text/javascript">
    function DisableBackButton() {
        window.history.forward()
    }
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }
</script>
    <style type="text/css">
#hidden
{
 display:none;
}
</style>
</header>
<div class="row">
		<div class="col-md-9">
			<div>
         <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Welcome"></asp:Label>
          <asp:Label ID="lbluser" runat="server" Font-Bold="true" Text="Label"></asp:Label>
        </div>
            <div class="form-group">
			     <div class="col-md-3">
                    <div style="padding-top:10px;"><asp:Button ID="butgroup" runat="server" CssClass="btn btn-primary btn-md" Width="200px"  CausesValidation="false" OnClick="butgroup_Click" Text="Register/View Groups"  /></div>
                    <div style="padding-top:5px;"><asp:Button ID="butuser"  runat="server" CssClass="btn btn-primary btn-md" Width="200px" CausesValidation="false" Text="Register/View Users"  OnClick="butuser_Click" Visible="False" /></div>
                    <div style="padding-top:5px;"><asp:Button ID="butcategory" runat="server" CssClass="btn btn-primary btn-md" Width="200px" CausesValidation="false"  Text="Register/View Categories" OnClick="butcategory_Click" /></div>

                         <div style="padding-top:5px;">               <input id="butmember" data-toggle="collapse" runat="server" data-target="#collapse1" Class="btn btn-primary btn-md" style="width:200px;"  type="button" value="Register/View Members" /></div>

                                <div id="collapse1" class="panel-collapse collapse">
                                  <ul class="list-group">
                                    <li class="list-group-item">
                                        <asp:LinkButton ID="LinkButton1" OnClick="butmember_Click" runat="server">Register Manually</asp:LinkButton></li>
                                    <li class="list-group-item">
                                        <asp:LinkButton ID="LinkButton2" OnClick="LinkButton2_Click" runat="server">Through Excel File</asp:LinkButton></li>
        
                                  </ul>
                                </div>

                     
                    <div style="padding-top:5px;"><asp:Button ID="butactiveusers" runat="server" CssClass="btn btn-primary btn-md" Width="200px" CausesValidation="false" Text="View Online Users" OnClick="butactiveusers_Click" Visible="False" /></div>
                    <div style="padding-top:20px;"><asp:Button ID="buttransact" runat="server" CssClass="btn btn-primary btn-md" CausesValidation="false"  Width="200px" Text="New/View Transanctions"  OnClick="buttransact_Click" Visible="False" /></div>
                 </div>

            </div>
                <div class="form-group">
			         <div class="col-md-3">
                        <div style="padding-top:5px;" ><asp:Button ID="butsend" runat="server" CssClass="btn btn-primary btn-md" CausesValidation="false" Width="200px" Text="Send a new message" OnClick="butsend_Click" /></div>
                        <div style="padding-top:5px;"><asp:Button ID="butquicksend" runat="server" CssClass="btn btn-primary btn-md" Width="200px" CausesValidation="false" OnClick="butquicksend_Click" Text="Quick Send"/></div>
                        <div style="padding-top:5px;"><asp:Button ID="butexcelSend" runat="server" CssClass="btn btn-primary btn-md" Width="200px" CausesValidation="false" OnClick="butexcelSend_Click" Text="Send through Excel"/></div>
                      </div>

                </div>
            <div class="form-group">
			         <div class="col-md-3">
            <div style="padding-top:5px;"><asp:Button ID="butsentlog" CssClass="btn btn-primary btn-md" runat="server" CausesValidation="false"  Width="200px" Text="View Sent Messages" OnClick="butsentlog_Click" /></div>
<div style="padding-top:5px;"><asp:Button ID="butquicksendlog" runat="server" Width="200px" CssClass="btn btn-primary btn-md"  CausesValidation="false" OnClick="butquicksendlog_Click" Text="Quick Send Log"/></div>
                         </div></div>
             <div class="form-group">
                 </div>
        </div>
    </div>