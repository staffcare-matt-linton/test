DECLARE @id int
SET @id = 1

select orderId, id as [Account id], name from Accounts
inner join Orders on Orders.Account_Id = Accounts.Id
where OrderId = @id;

select LineItems.Id as LineItemId, LineItems.Quantity, Products.Id as ProductId, 
Products.Name, Products.CostPrice, Products.RetailPrice
from Products
inner join LineItems on LineItems.Product_Id = Products.Id
where LineItems.Order_OrderId = @id;