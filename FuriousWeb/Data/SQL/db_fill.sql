INSERT INTO Warehouses (Code, Name, Address)
VALUES 
('WR1', 'Pagrindinis Sandelis', 'Naugarduko 48'),
('WR2', 'Atsarginis Sandelis', 'Naugarduko 49');

INSERT INTO Products(Code, Name, Description)
VALUES 
('PR1', 'Prekė 1', 'Prekės 1 aprašymas..'),
('PR2', 'Prekė 2', 'Prekės 2 aprašymas'),
('PR3', 'Prekė 3', 'Prekės 3 aprašymas..'),
('PR4', 'Prekė 4', 'Prekės 4 aprašymas'),
('PR5', 'Prekė 5', 'Prekės 5 aprašymas..'),
('PR6', 'Prekė 6', 'Prekės 6 aprašymas');

INSERT INTO Stock(WarehouseId, ProductId, Price, Quantity)
VALUES 
(1, 1, 5.99, 100),
(1, 2, 5.99, 100),
(1, 3, 13.99, 10),
(1, 4, 24.65, 10),
(1, 5, 119.99, 5);