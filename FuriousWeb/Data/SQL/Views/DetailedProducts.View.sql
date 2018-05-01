CREATE VIEW DetailedProducts
	AS SELECT ProductId, Products.Code, Products.Name, Description, WarehouseId, Warehouses.Code as WarehouseCode, Quantity, Price 
	FROM Products 
	INNER JOIN Stock ON Products.Id = ProductId
	INNER JOIN Warehouses ON Warehouses.Id = WarehouseId
