CREATE VIEW DetailedProducts
	AS SELECT ProductId, Code, Name, Description, WarehouseId, Quantity, Price 
	FROM Products INNER JOIN Stock ON Products.Id = ProductId
