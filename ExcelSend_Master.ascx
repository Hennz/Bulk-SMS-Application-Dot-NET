<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExcelSend_Master.ascx.cs" Inherits="ExcelSend_Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<header>
<script type="text/javascript" >
    function pageLoad() {
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        }
        )}
    function DisableBackButton() {
        window.history.forward()
    }
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }

    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }

 </script>
    <h2 style="text-align:center">Send Message through Excel File</h2>
</header>
<div class="row">
	 <div class="col-md-10">
         <div class="form-group">
             <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                 <ContentTemplate>
                     <div class="col-md-6">
                            <asp:Label ID="lblgroup" Font-Bold="true" runat="server" Text="Group"></asp:Label>
      
                           <asp:DropDownList ID="ddgroup" runat="server" CssClass="form-control" AutoPostBack="true" ToolTip="Select Group in which you want to insert the members" OnSelectedIndexChanged="ddgroup_SelectedIndexChanged" >
                                    </asp:DropDownList><asp:DropDownExtender ID="DropDownExtender1" TargetControlID="ddgroup" runat="server"></asp:DropDownExtender>
    
                            <asp:Label ID="lblcategory" Font-Bold="true" runat="server" Text="Category"></asp:Label>
      
                            <asp:DropDownList ID="ddcategory" CssClass="form-control" runat="server"  ToolTip="Select Category in which you want to insert the members" Enabled="False">
                                    </asp:DropDownList><asp:DropDownExtender ID="DropDownExtender2" TargetControlID="ddcategory" runat="server"></asp:DropDownExtender>
                         <div class="checkbox checkbox-primary" style="float:right">
                           <asp:CheckBox ID="chkheader" runat="server" Font-Bold="true" Text="Use Header" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="chkfooter" runat="server" Font-Bold="true" Text="Use Footer"/>
	                     </div>

                             <asp:Label ID="lblfileupload" Font-Bold="true"  runat="server" Text="File Upload"></asp:Label>
                           
                             <span data-toggle="tooltip" title="Please save your Excel 2007 File AS Excel 2003 before uploading">  <asp:FileUpload ID="FileUploadToServer" CssClass="btn btn-primary btn-file" runat="server"  /></span>
                            <div style="padding-top:5px;"><asp:Button ID="btnUpload" runat="server"  CssClass="btn btn-primary btn-md" AutoPostBack="True" OnClick="btnUpload_Click"  Text="Upload"  />
                            <a href="run_excelSend.aspx" runat="server" id="refresh" visible="false">Upload again</a>           
                            </div>
                           
                             <asp:UpdateProgress id="UpdateProgress1" runat="server" associatedupdatepanelid="UpdatePanel1" >
                            
                                    <progresstemplate>
                                        <div class="divWaiting">
                                            <asp:Label ID="lblWait" runat="server" Text=" Please wait... " />
                                       <asp:Image ID="Image1" ImageAlign="Middle" ImageUrl="images/loadingnew.gif" runat="server" />
                                         </div>
                                    </progresstemplate> 
                                 
                          </asp:UpdateProgress>
                    </div>

                </ContentTemplate>
                 <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="ddgroup" />
                 </Triggers>
                <Triggers>
                     <asp:PostBackTrigger ControlID="btnUpload" />
                 </Triggers>
                 </asp:UpdatePanel>
             </div>
         <div class="form-group">
                <div class="col-md-4">
                   <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn-link form-control" CausesValidation="false" OnClick="download">Download Specimen File</asp:LinkButton>

                  <asp:Label ID="lblMsg" runat="server"  ForeColor="Green" Text=""></asp:Label>

                </div>
             </div>
         </div>
    <div class="col-md-10">
        <div class="col-md-6">
            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:UpdateProgress id="UpdateProgress3" runat="server" associatedupdatepanelid="UpdatePanel1" >
                            
                                    <progresstemplate>
                                        <div class="divWaiting">
                                            <asp:Label ID="lblWaitgrid" runat="server" Text=" Please wait... " />
                                       <asp:Image ID="Image1grid" ImageAlign="Middle" ImageUrl="images/loadingnew.gif" runat="server" />
                                         </div>
                                    </progresstemplate> 
                                 
                          </asp:UpdateProgress>
                        <div style="padding-top:5px;"><asp:Label ID="lblMsg1" runat="server" ForeColor="Green"></asp:Label></div>
                         <asp:GridView ID="GridView3" runat="server"  CssClass="table table-striped table-condensed table-bordered" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" >
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Member Name">
                        <ItemTemplate>
                                <asp:Label ID="lblmemberid" runat="server" text='<%#Eval("Member_Name") %>'></asp:Label>
                            </ItemTemplate>
  
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile Num.">
                        <ItemTemplate>
                                <asp:Label ID="lblmemberid1" runat="server" text='<%#Eval("Member_Mobile") %>'></asp:Label>
                            </ItemTemplate>
  

                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Row No.">
                        <ItemTemplate>
                                <asp:Label ID="lblmemberid2" runat="server" text='<%#Eval("Row_Number") %>'></asp:Label>
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

                </ContentTemplate>
                 
                </asp:UpdatePanel>
        </div>
        <div class="col-md-6">
                          <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                              <ContentTemplate>
                                    <asp:UpdateProgress id="UpdateProgress2" runat="server" associatedupdatepanelid="UpdatePanel2" >
                            
                                    <progresstemplate>
                                        <div class="divWaiting">
                                            <asp:Label ID="lblWaitnew" runat="server" Text=" Please wait... " />
                                       <asp:Image ID="Image1new" ImageAlign="Middle" ImageUrl="images/loadingnew.gif" runat="server" />
                                         </div>
                                    </progresstemplate> 
                                 
                          </asp:UpdateProgress>
                    <asp:Label ID="lblMsg2" runat="server" ForeColor="Green" Font-Size="Larger"></asp:Label>
                  <asp:GridView ID="GridView1" runat="server" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table table-striped table-condensed table-bordered" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand" AllowPaging="True" PageSize="20">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Party Name">
                        <ItemTemplate>
                                <asp:Label ID="lblmembername" runat="server" text='<%#Eval("Member_Name") %>'></asp:Label>
                            </ItemTemplate>
  
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Row No.">
                        <ItemTemplate>
                                <asp:Label ID="lblmemberrow" runat="server" text='<%#Eval("Row_Number") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type Mob. Number">
                        <ItemTemplate>
                            <asp:TextBox ID="txtmob" onkeypress="return isNumberKey(event)" MaxLength="10" runat="server" ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

            <asp:TemplateField HeaderText="Send Msg">
                  <ItemTemplate>
                                <asp:LinkButton ID="lblsendmsg" runat="server" CausesValidation="False"  CommandName="sendmessage" CommandArgument='<%#Eval("Row_number") %>' Text='Send Message' ></asp:LinkButton>
                            </ItemTemplate>
            </asp:TemplateField>
                    <asp:TemplateField HeaderText="Msg Status">
                        <ItemTemplate>
                                <asp:Label ID="lblmembermsgstatus" runat="server" Font-Bold="true" text='Not Sent'></asp:Label>
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
                        </ContentTemplate>
                              <Triggers>
                                  <asp:AsyncPostBackTrigger ControlID="GridView1" />
                              </Triggers>
             </asp:UpdatePanel>      
        </div>
    </div>
    </div>