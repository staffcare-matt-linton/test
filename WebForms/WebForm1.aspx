<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebForms.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Search "></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Characters only" ControlToValidate="TextBox1" ForeColor="Red" ValidationExpression="[a-zA-Z]">*</asp:RegularExpressionValidator>
    <asp:Button ID="Button1" runat="server" Text="Submit" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" DataKeyNames="Id,RowVersion" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="CostPrice" HeaderText="CostPrice" SortExpression="CostPrice" />
            <asp:BoundField DataField="RetailPrice" HeaderText="RetailPrice" SortExpression="RetailPrice" />
        </Columns>
    </asp:GridView>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Core.Entity.Product" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="SelectByName" TypeName="Core.DataAccess.EntityFramework.EfProductModel" UpdateMethod="Update" >
        <DeleteParameters>
            <asp:Parameter Name="id" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBox1" Name="partOfName" PropertyName="Text" Type="String" ConvertEmptyStringToNull="False" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <script src="~/Scripts/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="~/Scripts/Search.js" type="text/javascript"></script>
</asp:Content>
