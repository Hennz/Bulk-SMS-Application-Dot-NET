<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Send_Master.ascx.cs" Inherits="Send_Master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<header>
<script type="text/javascript" >
    function pageLoad() {
        $(document).ready(function () {
            $('[data-toggle="tooltipforHeader"]').tooltip();
            $('[data-toggle="tooltipforFooter"]').tooltip();
        });
    }
    function DisableBackButton() {
        window.history.forward();
     //   window.open('run_home.aspx','_self');
    }
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }
    //window.onunload = DisableBackButton;
    
    function swap()
    {
        var finaltext="";
        if ($('#<%=chkheader.ClientID%>').is(':checked') && $('#<%=chkfooter.ClientID%>').is(':checked')){
             finaltext = $('#<%=hdnheader.ClientID%>').val() + "\n " + $('#<%=txtmessage.ClientID%>').val() + "\n" + $('#<%=hdnfooter.ClientID%>').val();
        }
      else if ($('#<%=chkheader.ClientID%>').is(':checked') && !($('#<%=chkfooter.ClientID%>').is(':checked'))) {
             finaltext = $('#<%=hdnheader.ClientID%>').val() + "\n " + $('#<%=txtmessage.ClientID%>').val();
        }
       else if (!($('#<%=chkheader.ClientID%>').is(':checked')) && $('#<%=chkfooter.ClientID%>').is(':checked')) {
            finaltext = $('#<%=txtmessage.ClientID%>').val() + "\n" + $('#<%=hdnfooter.ClientID%>').val();
        }
        else  {
             finaltext = $('#<%=txtmessage.ClientID%>').val();
        }
        $('#<%=txtfinalmessage.ClientID%>').val(finaltext);
    }
 </script>

</header>
<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
<div class="row" style="padding-left:5px;">
    <div class="form-group">
    	 <div class="col-md-6">
             <div class="col-md-12">
                 <div style="text-align:center"><asp:Label ID="lblmsgreport" runat="server" ForeColor="Red" Visible="False"></asp:Label></div>
                    <asp:Label ID="lblgroupname" Font-Bold="true" runat="server" Text="Group Name"></asp:Label>
                    <asp:DropDownList ID="dropdown" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged" >
                    </asp:DropDownList><asp:DropDownExtender ID="DropDownExtender1" TargetControlID="dropdown" runat="server"></asp:DropDownExtender>
                
                    <asp:Label ID="lblgroupname0" runat="server" Font-Italic="true" Text="If you do not see your group or category, reload the page or activate the group"></asp:Label>
           </div>
            <div class="col-md-12">
                  <div>  <asp:Label ID="lblcategoryname" Font-Bold="true" runat="server" Text="Category Name"></asp:Label></div>
              
                    <asp:DropDownList ID="dropdown1" CssClass="form-control" runat="server" Enabled="False" AutoPostBack="True"  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" >
                    </asp:DropDownList><asp:DropDownExtender ID="DropDownExtender2" TargetControlID="dropdown1" runat="server"></asp:DropDownExtender>
             </div>
                    
        </div>
        <div class="col-md-6">
            
        </div>
        <div class="col-md-9">
            <div class="col-md-8" >
                    <asp:Label ID="lblmessage" runat="server" Font-Bold="true" Text="Message"></asp:Label>
              
                    <asp:TextBox ID="txtmessage" runat="server" CssClass="form-control" AutoCompleteType="Disabled" MaxLength="1000" onkeyup="return validateLimit(this, 'lblcount')" Placeholder="One message can contain 160 characters.Characters after exceeding limit will be sent in a new message" TextMode="MultiLine"></asp:TextBox>
             
                    <div id="lblcount" style="float:right">
                        0 characters
                    </div>
            </div>
            <div class="col-md-4">
                <br />
                <br />
                <div class="checkbox checkbox-primary">
                <span data-toggle="tooltipforHeader" title="Check it to insert header in your message. It is the text which will be inserted before your message text. To edit Header go to SMS settings" data-placement="top" >    <asp:CheckBox ID="chkheader" runat="server" Font-Bold="true" Text="Use Header" /></span>
                    <br />
                <span data-toggle="tooltipforHeader" title="Check it to insert footer in your message. It is the text which will be inserted at the end of your message text. To edit Footer go to SMS settings">    <asp:CheckBox ID="chkfooter" runat="server" Font-Bold="true" Text="Use Footer"/></span>
	            </div>
                
            </div>
            </div>
        <div class="col-md-6">
             <div><asp:Label ID="lblMsg" runat="server" Font-Size="Larger" ForeColor="Green"></asp:Label>
                    <asp:CheckBox ID="cbselectall" runat="server" AutoPostBack="true" OnCheckedChanged="cbselectall_CheckedChanged" Text="Select all" /></div>
               <div style="text-align:center;">
    <asp:UpdateProgress id="UpdateProgress1" runat="server" associatedupdatepanelid="UpdatePanel1" dynamiclayout="true">
                        <progresstemplate>

                           Loading.....<img src="images/loading.gif">

                        </progresstemplate>
                    </asp:UpdateProgress>

                    </div>
            
            <input id="butsend" runat="server" type="button" data-toggle="modal"  data-target="#finalmessage_popup" onclick="swap();" style="margin-left: 264px;" class="btn btn-primary btn-md"  value="Send" />

             <div>
                     <asp:GridView ID="GridView1" runat="server"  CssClass="table table-striped table-condensed table-bordered" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkCtrl" runat="server" AutoPostBack="true" CausesValidation="False" OnCheckedChanged="chkCtrl_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Member Id" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblmemberid" runat="server" text='<%#Eval("Member_Id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Member Mobile" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblmembermobile" runat="server" text='<%#Eval("Member_Mobile") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MEMBER NAME">
                                <ItemTemplate>
                                    <asp:Label ID="lblmembername" runat="server" text='<%#Eval("Member_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                         <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                         <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                         
                         
                        
                        
                    </asp:GridView>
           
                       
        </div>
            </div>
    </div>
            <div id="finalmessage_popup" class="modal fade" role="dialog">
           <div class="modal-dialog modal-lg">
	        <div class="modal-content">
       
        <div class="modal-header" >
			            <h4 style="text-align:center">Confirm the message</h4>
			            <button type="button" class="close" data-dismiss="modal">&times;</button>
	    	         </div>
                <div class="modal-body">
                      <div class="form-group">
                         <asp:TextBox ID="txtfinalmessage" TextMode="MultiLine" Height="150px" CssClass="form-control" runat="server"></asp:TextBox> 
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfinalmessage" ForeColor="Red" ErrorMessage="Message cannot be empty"></asp:RequiredFieldValidator>
                        <br />
                        <div style="text-align:center"> <asp:Button ID="butconfirmmsg" OnClick="butsend_Click"  runat="server" CssClass="btn btn-primary btn-md" Text="Confirm and Send" /></div>
                       </div>
                    </div>
                     <div class="modal-footer">
                            <a href="#" class="close reset" data-dismiss="modal" id="message_popout">[x] cancel</a>
                     </div>

    
        <!--end-->   
                </div>
               </div>
                </div>
        <asp:HiddenField ID="hdnheader" runat="server" />  
        <asp:HiddenField ID="hdnfooter" runat="server" />  
    </ContentTemplate>
    <Triggers>
            <asp:PostBackTrigger ControlID="butconfirmmsg" />
        </Triggers>
</asp:UpdatePanel>
    <script type="text/javascript">
    function validateLimit(obj, divID) {

        objDiv = get_object(divID);

        if (this.id) obj = this;
        objDiv.innerHTML = obj.value.length + " characters";
  }

    function get_object(id) {

        var object= null;

        if (document.layers) {

            object = document.layers[id];

        } else if (document.all) {

            object = document.all[id];

        } else if (document.getElementById) {

            object = document.getElementById(id);

        }

        return object;

    }
    </script>
  