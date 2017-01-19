<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Basket.aspx.cs" Inherits="WebForms.Basket" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Basket</h1>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/FilmList.aspx">Film List</asp:HyperLink>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
            <Columns>
                <asp:BoundField DataField="Product" HeaderText="Product" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OnObjectCreating="ObjectDataSource1_ObjectCreating" SelectMethod="GetLineItems" TypeName="Core.Entity.Order"></asp:ObjectDataSource>
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Purchase" />
    
    </div>
    </form>
</body>
</html>
