<%@ Page Language="C#" AutoEventWireup="true" 
    CodeBehind="FilmList.aspx.cs" Inherits="WebForms.FilmList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body>

    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Search "></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Characters only" ControlToValidate="TextBox1" ForeColor="Red" ValidationExpression="[a-zA-Z]">*</asp:RegularExpressionValidator>
            <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
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
              
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Core.Entity.Product" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="SelectByName" TypeName="Core.DataAccess.EntityFramework.EfProductModel" UpdateMethod="Update">
            <DeleteParameters>
                <asp:Parameter Name="id" Type="String" />
            </DeleteParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="TextBox1" Name="partOfName" PropertyName="Text" Type="String" ConvertEmptyStringToNull="False" />
            </SelectParameters>
        </asp:ObjectDataSource>

    </div>
    </form>
    <script src="~/Scripts/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="~/Scripts/Search.js" type="text/javascript"></script>
</body>
</html>
