<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Group_Master.ascx.cs" Inherits="Group_Master" %>
<header>
<script type="text/javascript" >
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
            <asp:Label ID="lblgroupname" Font-Bold="true" runat="server" Text="Group Name"></asp:Label>
            <asp:TextBox ID="txtgroupname" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtgroupname" ErrorMessage="Please enter a valid Group Name" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtgroupname" ErrorMessage="Please enter a valid Group Name" ForeColor="Red" ValidationExpression="^[a-zA-Z'.\s]{1,50}"></asp:RegularExpressionValidator>
            <div style="margin-left:100px;padding-bottom:5px;"><asp:Button ID="butgroupsubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-md" OnClick="butgroupsubmit_Click"/></div>
            <asp:GridView ID="GridView1" CssClass="table table-striped table-condensed table-bordered" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand" CellSpacing="2">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                  
                    <asp:TemplateField HeaderText="GROUP NAME">
                         <ItemTemplate>
                                <asp:Label ID="lblgroupname" runat="server" text='<%#Eval("Group_Name") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="STATUS">
                         <ItemTemplate>
                                <asp:Label ID="lblgroupstatus" runat="server" text='<%#Eval("Status") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EDIT">
                                <ItemTemplate>
                                <asp:LinkButton ID="lblgroupedit" runat="server" CausesValidation="False"  CommandName="editrow" CommandArgument='<%#Eval("Group_Id") %>' Text='Edit' ></asp:LinkButton>
                            </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CHANGE STATUS">
                        <ItemTemplate>
                                <asp:LinkButton ID="lblgroupchangestatus" runat="server" CausesValidation="False"  CommandName="changestatus" CommandArgument='<%#Eval("Group_Id") %>' Text='Change Status' ></asp:LinkButton>
                            </ItemTemplate>

                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" Wrap="True" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Wrap="True" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
       </div>
 </div>
</div> 