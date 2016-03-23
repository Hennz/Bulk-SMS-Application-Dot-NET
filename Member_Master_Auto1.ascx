<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Member_Master_Auto1.ascx.cs" Inherits="Member_Master_Auto1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<header>
<script type="text/javascript" >
    function pageLoad() {
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        }
        )
    }
    function DisableBackButton() {
        window.history.forward()
    }
    DisableBackButton();
    window.onload = DisableBackButton;
    window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
    window.onunload = function () { void (0) }
 </script>
    <h2 style="text-align:center">Register Members through Excel File</h2>
</header>
<div class="row">
	 <div class="col-md-10">
         <div class="form-group">
                <div class="col-md-6">
                    <asp:Label ID="lblgroup" Font-Bold="true" runat="server" Text="Group"></asp:Label>
      
                   <asp:DropDownList ID="ddgroup" runat="server" CssClass="form-control" AutoPostBack="true" ToolTip="Select Group in which you want to insert the members" OnSelectedIndexChanged="ddgroup_SelectedIndexChanged" >
                            </asp:DropDownList><asp:DropDownExtender ID="DropDownExtender1" TargetControlID="ddgroup" runat="server"></asp:DropDownExtender>
    
                    <asp:Label ID="lblcategory" Font-Bold="true" runat="server" Text="Category"></asp:Label>
      
                    <asp:DropDownList ID="ddcategory" CssClass="form-control" runat="server" AutoPostBack="true"  ToolTip="Select Category in which you want to insert the members" Enabled="False">
                            </asp:DropDownList><asp:DropDownExtender ID="DropDownExtender2" TargetControlID="ddcategory" runat="server"></asp:DropDownExtender>
                     <asp:Label ID="lblfileupload" Font-Bold="true"  runat="server" Text="File Upload"></asp:Label>
     
                               <span data-toggle="tooltip" title="Please save your Excel 2007 File AS Excel 2003 before uploading">  <asp:FileUpload ID="FileUploadToServer" CssClass="btn btn-primary btn-file" runat="server"  /></span>
                           <div style="padding-top:5px;"><asp:Button ID="btnUpload" runat="server"  CssClass="btn btn-primary btn-md" AutoPostBack="True" OnClick="btnUpload_Click"  Text="Upload"  />
                             <asp:Button ID="butlink" runat="server"  CssClass="btn btn-primary btn-md" Text="View Registered Memebers" OnClick="butlink_Click" /></div>
                    <div style="padding-top:5px;"><asp:Label ID="lblMsg1" runat="server" ForeColor="Green"></asp:Label></div>
                    <asp:GridView ID="GridView2" runat="server"  CssClass="table table-striped table-condensed table-bordered" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Missing Category Name">
                        <ItemTemplate>
                                <asp:Label ID="lblmemberid" runat="server" text='<%#Eval("Category_Name") %>'></asp:Label>
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
                     <asp:GridView ID="GridView3" runat="server"  CssClass="table table-striped table-condensed table-bordered" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"  Width="210px">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Member Name">
                        <ItemTemplate>
                                <asp:Label ID="lblmemberid" runat="server" text='<%#Eval("Member_Name") %>'></asp:Label>
                            </ItemTemplate>
  
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile">
                        <ItemTemplate>
                                <asp:Label ID="lblmemberid1" runat="server" text='<%#Eval("Member_Mobile") %>'></asp:Label>
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
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  CssClass="table table-striped table-condensed table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" Height="213px" Width="958px"  style="margin-right: 19px" >
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                            <asp:TemplateField HeaderText="MEMBER NAME">
                                <ItemTemplate>
                                <asp:Label ID="lblmembername" runat="server" text='<%#Eval("Member_Name") %>'></asp:Label>
                            </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOBILE NO.">
                                <ItemTemplate>
                                <asp:Label ID="lblmembermobile" runat="server" text='<%#Eval("Member_Mobile") %>'></asp:Label>
                            </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EMAIL">
                                <ItemTemplate>
                                <asp:Label ID="lblmemberemail" runat="server" text='<%#Eval("Member_Email") %>'></asp:Label>
                            </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ADDRESS">
                                <ItemTemplate>
                                <asp:Label ID="lblmemberaddress" runat="server" text='<%#Eval("Member_Address") %>'></asp:Label>
                            </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="STATUS">
                                <ItemTemplate>
                                <asp:Label ID="lblmemberstatus" runat="server" text='<%#Eval("Status") %>'></asp:Label>
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
         <div class="form-group">
                <div class="col-md-4">
                   <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn-link form-control" CausesValidation="false" OnClick="download">Download Specimen File</asp:LinkButton>

                  <asp:Label ID="lblMsg" runat="server"  ForeColor="Green" Text=""></asp:Label>
                 
                    <asp:Label ID="lblMsg2" runat="server" ForeColor="Green" Font-Size="Larger"></asp:Label>
       
           <span style="padding-left:30px;"><asp:Button ID="butinsert" runat="server" CssClass="btn btn-primary btn-md" OnClick="btninsert_Click" Text="INSERT" /></span>

            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False"  CssClass="table table-striped table-condensed table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>

                        <asp:TemplateField HeaderText="Member Name">
                        <ItemTemplate>
                                <asp:Label ID="lblmemberid2" runat="server" text='<%#Eval("Member_Name") %>'></asp:Label>
                            </ItemTemplate>
  
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile">
                        <ItemTemplate>
                                <asp:Label ID="lblmemberid3" runat="server" text='<%#Eval("Member_Mobile") %>'></asp:Label>
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
     
    </div>