<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Category_Master.ascx.cs" Inherits="Category_Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
</header>
<div class="row">
 <div class="form-group">
	 <div class="col-md-6">

            <asp:Label ID="lblgroupname" runat="server" Font-Bold="true" Text="Group Name"></asp:Label>
            <asp:DropDownList ID="dropdown" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            </asp:DropDownList><asp:DropDownExtender ID="DropDownExtender1" TargetControlID="dropdown" runat="server"></asp:DropDownExtender>
         
            <asp:Label ID="lblcategoryname" runat="server" Font-Bold="true" Text="Category Name"></asp:Label>
            <asp:TextBox ID="txtcategoryname" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcategoryname" ErrorMessage="Please enter a valid Category Name" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtcategoryname" ErrorMessage="Please enter a valid Category Name" ForeColor="Red" ValidationExpression="^[a-zA-Z'.\s]{1,50}"></asp:RegularExpressionValidator>
   
         <div style="margin-left:100px;padding-bottom:5px;">   <asp:Button ID="butcategorysubmit" CssClass="btn btn-primary btn-md" runat="server"  Text="Submit" OnClick="butcategorysubmit_Click"/></div>

            <asp:Label ID="lblgroupname0" runat="server" Font-Italic="true" Text="If you do not see your group in the list, reload the page or activate the group from group register page."></asp:Label>
            <asp:GridView ID="GridView1" runat="server"  CssClass="table table-striped table-condensed table-bordered"  AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    
                    <asp:TemplateField HeaderText="CATEGORY NAME">
                         <ItemTemplate>
                                <asp:Label ID="lblcategoryname" runat="server" text='<%#Eval("Category_Name") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="STATUS">
                         <ItemTemplate>
                                <asp:Label ID="lblcategorystatus" runat="server" text='<%#Eval("Status") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                          <asp:TemplateField HeaderText="EDIT">
                                <ItemTemplate>
                                <asp:LinkButton ID="lblcategoryedit" runat="server" CausesValidation="False"  CommandName="editrow" CommandArgument='<%#Eval("Category_Id") %>' Text='Edit' ></asp:LinkButton>
                            </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CHANGE STATUS">
                        <ItemTemplate>
                                <asp:LinkButton ID="lblcategorychangestatus" runat="server" CausesValidation="False"  CommandName="changestatus" CommandArgument='<%#Eval("Category_Id") %>' Text='Change Status' ></asp:LinkButton>
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