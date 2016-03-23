<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuickSend_Master.ascx.cs" Inherits="QuickSend_Master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<header runat="server">
      
<link href="css/jquery.tagit.css" rel="stylesheet" type="text/css">
<link href="css/tagit.ui-zendesk.css" rel="stylesheet" type="text/css">
<script src="js/tag-it.js" type="text/javascript" charset="utf-8"></script>
<script type="text/javascript">
    function DisableBackButton() {
        window.history.forward()
    }
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }
    function swap() {
        var finaltext = "";
        if ($('#<%=chkheader.ClientID%>').is(':checked') && $('#<%=chkfooter.ClientID%>').is(':checked')) {
            finaltext = $('#<%=hdnheader.ClientID%>').val() + "\n " + $('#<%=txtmessage.ClientID%>').val() + "\n" + $('#<%=hdnfooter.ClientID%>').val();
        }
        else if ($('#<%=chkheader.ClientID%>').is(':checked') && !($('#<%=chkfooter.ClientID%>').is(':checked'))) {
            finaltext = $('#<%=hdnheader.ClientID%>').val() + "\n " + $('#<%=txtmessage.ClientID%>').val();
      }
      else if (!($('#<%=chkheader.ClientID%>').is(':checked')) && $('#<%=chkfooter.ClientID%>').is(':checked')) {
          finaltext = $('#<%=txtmessage.ClientID%>').val() + "\n" + $('#<%=hdnfooter.ClientID%>').val();
       }
       else {
           finaltext = $('#<%=txtmessage.ClientID%>').val();
       }
    $('#<%=txtfinalmessage.ClientID%>').val(finaltext);
    }

 </script>
<script type="text/javascript">
    function pageLoad() {
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('[data-toggle="tooltipforMembers"]').tooltip();
            $('[data-toggle="tooltipforMembersDeletion"]').tooltip();
            $('[data-toggle="tooltipforHeader"]').tooltip();
            $('[data-toggle="tooltipforFooter"]').tooltip();
        });
        $(function () {
            $('#singleFieldTagsforMembers').tagit({
                removeConfirmation: true,
                allowSpaces: true,
                readOnly: true,
                allowDuplicates: false,
                onTagClicked: function(event, ui) {
                    $('#singleFieldTagsforMembers').tagit('removeTagByLabel', ui.tagLabel);
                },
                beforeTagAdded:
                function (event, ui) {

                    var Number = ui.tagLabel;
                    var IndNum = /^([7-9]{1})([0-9]{9})$/;

                    if (IndNum.test(Number)) {
                        $('#<%=lblInvalidTagforMember.ClientID%>').empty();
                    }
                    else {
                        $('#<%=lblInvalidTagforMember.ClientID%>').text("Invalid Mobile Number");
                        return false;
                    }
                },
            });
        }

            );
        $(function () {
            $('#<%=txtmobile.ClientID%>').tagit({

                // This will make Tag-it submit a single form value, as a comma-delimited field.
                singleField: true,
                singleFieldNode: $('#<%=txtmobile.ClientID%>'),
                removeConfirmation: true,
                allowSpaces: true,
                allowDuplicates: false,
                beforeTagAdded:
                    
                 function (event, ui) {
                     
                        
                     var Number = ui.tagLabel;
                     var IndNum = /^([7-9]{1})([0-9]{9})$/;

                     if (IndNum.test(Number)) {
                         $('#<%=lblInvalidTag.ClientID%>').empty();
                     }
                     else {
                         $('#<%=lblInvalidTag.ClientID%>').text("Invalid Mobile Number");
                         return false;
                     }



                     /*  if ($.inArray(ui.tagLabel,sampletags ) == -1) {
                           $('#<=lblInvalidTag.ClientID%>').text("Invalid Tag");
                           return false;
                       }
                       else {
                           $('#<=lblInvalidTag.ClientID%>').empty();
                       }*/


                 },

            });


            //-------------------------------
            var eventTags = $('#eventTags');

            var addEvent = function (text) {
                $('#events_container').append(text + '<br>');
            };

            eventTags.tagit({

                beforeTagAdded: function (evt, ui) {
                    if (!ui.duringInitialization) {
                        addEvent('beforeTagAdded: ' + eventTags.tagit('tagLabel', ui.tag));
                    }
                },
                afterTagAdded: function (evt, ui) {
                    if (!ui.duringInitialization) {
                        addEvent('afterTagAdded: ' + eventTags.tagit('tagLabel', ui.tag));
                    }
                },
                beforeTagRemoved: function (evt, ui) {
                    addEvent('beforeTagRemoved: ' + eventTags.tagit('tagLabel', ui.tag));
                },
                afterTagRemoved: function (evt, ui) {
                    addEvent('afterTagRemoved: ' + eventTags.tagit('tagLabel', ui.tag));
                },
                onTagClicked: function (evt, ui) {
                    addEvent('onTagClicked: ' + eventTags.tagit('tagLabel', ui.tag));
                },
                onTagExists: function (evt, ui) {
                    addEvent('onTagExists: ' + eventTags.tagit('tagLabel', ui.existingTag));
                }
            });
        });
    }
</script>
    <script type="text/javascript">
        function getmobiles() {
            var arr = $('#<%=txtmobile.ClientID%>').tagit("assignedTags");
            var hidden = document.getElementById("<%=hdnmobiles.ClientID%>");
            hidden.value = arr.slice();

            var arrforMembers = $('#singleFieldTagsforMembers').tagit("assignedTags");
            var hiddenforMembers = document.getElementById("<%=hdnmobilesforMembers.ClientID%>");
            hiddenforMembers.value = arrforMembers.slice();
        }
    </script>

    <div style="text-align:center"><asp:Label ID="lblmsgreport" runat="server" ForeColor="Red" Visible="false"></asp:Label></div>
    <asp:UpdatePanel ID="optionpanel" runat="server">
        <ContentTemplate>
            <asp:LinkButton ID="linknumber" runat="server" OnClick="linknumber_Click" CausesValidation="False">Send message to a new number</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="linkmember" runat="server" OnClick="linkmember_Click" CausesValidation="False">Send message to saved member</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="linknumber" />
<asp:PostBackTrigger ControlID="linkmember"></asp:PostBackTrigger>
        </Triggers>
        <Triggers>
            <asp:PostBackTrigger ControlID="linkmember" />
        </Triggers>

    </asp:UpdatePanel>


</header>

<script type="text/javascript">
    function validateLimit(obj, divID) {

        objDiv = get_object(divID);

        if (this.id) obj = this;
        objDiv.innerHTML = obj.value.length + " characters";
    }

    function get_object(id) {

        var object = null;

        if (document.layers) {

            object = document.layers[id];

        } else if (document.all) {

            object = document.all[id];

        } else if (document.getElementById) {

            object = document.getElementById(id);

        }

        return object;

    }
    function isNumberKey(evt)
     {
         var charCode = (evt.which) ? evt.which : event.keyCode
         if (charCode > 31 && (charCode < 48 || charCode > 57))
             return false;

         return true;
     }
   
    </script>

<asp:UpdatePanel ID="memberpanel" runat="server" UpdateMode="Conditional" Visible="False">
    <ContentTemplate>
        <div class="row">
            <div class="col-md-6">
                <div>
             <div class="form-group">
	             <div class="col-md-6">
                    <asp:Label ID="membername" Font-Bold="true" runat="server" Text="Select Member Name"></asp:Label>
               <span data-toggle="tooltipforMembers" title="Select member(s) to whom you wish to send message. You can select multiple members" data-placement="right">     <asp:DropDownList ID="ddmember" runat="server" CssClass="form-control">
                    </asp:DropDownList><asp:DropDownExtender ID="DropDownExtender1" TargetControlID="ddmember" runat="server"></asp:DropDownExtender>
                   </span>
                    
                    <asp:Label ID="lblmobilebyname" Font-Bold="true" runat="server" Text=" Mobile Number"></asp:Label>
                      <span data-toggle="tooltipforMembersDeletion" title="To remove a number from the list, Click on it." data-placement="right"> <ul id="singleFieldTagsforMembers" ></ul>
                     <asp:Label ID="lblInvalidTagforMember" runat="server" ForeColor="Red"></asp:Label></span>
                 </div>
            </div>
                <div class="form-group">
                    <div class="col-md-6">
                        <asp:LinkButton ID="linknumber0" runat="server" OnClick="linknumber_Click" CausesValidation="False">OR Send message to a new number</asp:LinkButton>
                    </div>
                </div>
                    </div>
        </div>
    </div>
    </ContentTemplate>
    <Triggers>
            <asp:PostBackTrigger ControlID="linknumber0" />
        </Triggers>
   
</asp:UpdatePanel>
<asp:UpdatePanel ID="numberpanel" runat="server" UpdateMode="Conditional" Visible="False">
    <ContentTemplate>
        <div class="row">
            <div class="col-md-6">
                <div>
             <div class="form-group">
	             <div class="col-md-7">
                     <asp:Label ID="lblInvalidTag" runat="server" ForeColor="Red"></asp:Label>
                    <asp:RequiredFieldValidator ID="requirednumber" runat="server" ControlToValidate="txtmobile" ErrorMessage="Please enter a valid Mobile Number" ForeColor="Red"></asp:RequiredFieldValidator>
                     <br />
                    <asp:Label ID="lblmobile" runat="server" Font-Bold="true" Text="Enter a Mobile Number"></asp:Label>
          <span data-toggle="tooltip" title="Enter mobile number(without +91) and press enter to add multiple numbers" data-placement="right">+91<asp:TextBox ID="txtmobile" runat="server" CssClass="form-control" AutoCompleteType="Disabled"   ></asp:TextBox> <ul id="singleFieldTags"  onkeypress="return isNumberKey(event)" ></ul></span>
                      
                   <div> <asp:Label ID="lblhistory" runat="server" Font-Bold="true" Text="or Choose from your sent history"></asp:Label></div>
             
                    <asp:DropDownList ID="ddhistory" runat="server"  CssClass="form-control"  >
                    </asp:DropDownList><asp:DropDownExtender ID="DropDownExtender2" TargetControlID="ddhistory"  runat="server"></asp:DropDownExtender>
                     </div>
                 </div>
                    <div class="form-group">
                        <div class="col-md-5">
                        
                        <asp:LinkButton ID="linkmember0" runat="server" OnClick="linkmember_Click" CausesValidation="False">OR Send msg to saved member</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </ContentTemplate>
    <Triggers>
            <asp:PostBackTrigger ControlID="linkmember0" />
        </Triggers>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
	         <div class="row" style="padding-left:5px;">
                <div class="form-group">
                    <div class="col-md-9">
                                    <div class="col-md-8">
                                <asp:Label ID="lblmessage" runat="server" Font-Bold="true" Text="Message"></asp:Label>
                                 <asp:TextBox ID="txtmessage" runat="server" CssClass="form-control"  MaxLength="1000" AutoPostBack="false" onkeyup="return validateLimit(this, 'lblcount')" Placeholder="One message can contain 160 characters.Characters after exceeding limit will be sent in a new message" TextMode="MultiLine"></asp:TextBox>
                                 <div id="lblcount">
                                    0 characters
                                </div>
                   
                            
                                </div>
                                <div class="col-md-4">
                                    <br />
                                    <br />
                                    <div class="checkbox checkbox-primary">
                                       <span data-toggle="tooltipforHeader" title="Check it to insert header in your message. It is the text which will be inserted before your message text. To edit Header go to SMS settings" data-placement="top" > <asp:CheckBox ID="chkheader"  runat="server" Font-Bold="true" Text="Use Header" /></span>
                                        <br />
                                        <span data-toggle="tooltipforHeader" title="Check it to insert footer in your message. It is the text which will be inserted at the end of your message text. To edit Footer go to SMS settings"><asp:CheckBox ID="chkfooter" runat="server" Font-Bold="true" Text="Use Footer"/></span>
	                                </div>
                            </div>
                    </div>
                    <div class="col-md-3"></div>
                    <br />
                    <div class="col-md-6">
                        <div class="col-md-3">
                            
                             <div style="padding-left:8px;" ><input id="butsend" runat="server" data-toggle="modal" data-target="#finalmessage_popup" type="button" class="btn btn-primary btn-md forbutton" onclick="swap();" style="margin-left: 264px" value="Send" /></div>
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
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfinalmessage" ErrorMessage="Message cannot be blank" ForeColor="Red"></asp:RequiredFieldValidator>
         <br />
              <div style="text-align:center">   <asp:Button ID="butconfirmmsg" OnClick="txtsend_Click" OnClientClick="getmobiles();"  runat="server" CssClass="btn btn-primary btn-md" Text="Confirm and Send" /></div>
 
         </div>
</div>
             <div class="modal-footer">
                    
                 <a href="#" class="close reset" data-dismiss="modal" id="message_popout">[x] cancel</a>
        </div>
</div>
               </div>
    
</div>      
        <asp:HiddenField ID="hdnheader" runat="server" />  
        <asp:HiddenField ID="hdnfooter" runat="server" />
        <asp:HiddenField ID="hdnmobiles" runat="server" />
        <asp:HiddenField ID="hdnmobilesforMembers" runat="server" />
    </ContentTemplate>
    <Triggers>
            <asp:PostBackTrigger ControlID="butconfirmmsg" />
        </Triggers>
</asp:UpdatePanel>


        <script type="text/javascript">

            $(function () {
                $('#<%=ddhistory.ClientID%>').change(function () {
                    var ddlFruits = $('#<%=ddhistory.ClientID%>');
                    var selectedValue = ddlFruits.val();
                     $('#<%=txtmobile.ClientID%>').tagit('createTag', selectedValue);
                });
            });
            $(function () {
                $('#<%=ddmember.ClientID%>').change(function () {
                    var ddlmembers = $('#<%=ddmember.ClientID%>');
                    var selectedValue = ddlmembers.val();
                    $('#singleFieldTagsforMembers').tagit('createTag', selectedValue);
                });
            });
            </script>

