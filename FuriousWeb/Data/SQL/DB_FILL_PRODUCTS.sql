﻿INSERT INTO Products(Code, Name, Description, Price, Created_at)
VALUES 
('1', 'Prekė 1', 'Prekės 1 aprašymas', 100, '2018-05-31 12:20:08'),
('2', 'Prekė 2', 'Prekės 2 aprašymas', 50, '2018-05-31 12:20:08'),
('3', 'Prekė 3', 'Prekės 3 aprašymas', 100, '2018-05-31 12:20:08'),
('4', 'Prekė 4', 'Prekės 4 aprašymas', 60, '2018-05-31 12:20:08'),
('5', 'Prekė 5', 'Prekės 5 aprašymas', 800, '2018-05-31 12:20:08'),
('6', 'Prekė 6', 'Prekės 6 aprašymas', 100, '2018-05-31 12:20:08'),
('7', 'Prekė 7', 'Prekės 7 aprašymas', 100, '2018-05-31 12:20:08'),
('8', 'Prekė 8', 'Prekės 8 aprašymas', 10, '2018-05-31 12:20:08'),
('9', 'Prekė 9', 'Prekės 9 aprašymas', 110, '2018-05-31 12:20:08'),
('10', 'Prekė 10', 'Prekės 10 aprašymas', 120, '2018-05-31 12:20:08'),
('11', 'Prekė 11', 'Prekės 11 aprašymas', 60, '2018-05-31 12:20:08'),
('12', 'Prekė 12', 'Prekės 12 aprašymas', 70, '2018-05-31 12:20:08'),
('13', 'Prekė 13', 'Prekės 13 aprašymas', 80, '2018-05-31 12:20:08'),
('14', 'Prekė 14', 'Prekės 14 aprašymas', 250, '2018-05-31 12:20:08'),
('15', 'Prekė 15', 'Prekės 15 aprašymas', 30, '2018-05-31 12:20:08'),
('16', 'Prekė 16', 'Prekės 16 aprašymas', 40, '2018-05-31 12:20:08'),
('17', 'Prekė 17', 'Prekės 17 aprašymas', 70, '2018-05-31 12:20:08'),
('18', 'Prekė 18', 'Prekės 18 aprašymas', 80, '2018-05-31 12:20:08'),
('19', 'Prekė 19', 'Prekės 19 aprašymas', 85.50, '2018-05-31 12:20:08'),
('20', 'Prekė 20', 'Prekės 20 aprašymas', 20, '2018-05-31 12:20:08'),
('21', 'Prekė 21', 'Prekės 21 aprašymas', 30, '2018-05-31 12:20:08'),
('22', 'Prekė 22', 'Prekės 22 aprašymas', 40, '2018-05-31 12:20:08'),
('23', 'Prekė 23', 'Prekės 23 aprašymas', 50, '2018-05-31 12:20:08'),
('24', 'Prekė 24', 'Prekės 24 aprašymas', 80, '2018-05-31 12:20:08'),
('25', 'Prekė 25', 'Prekės 25 aprašymas', 90, '2018-05-31 12:20:08'),
('26', 'Prekė 26', 'Prekės 26 aprašymas', 20, '2018-05-31 12:20:08'),
('27', 'Prekė 27', 'Prekės 27 aprašymas', 10, '2018-05-31 12:20:08');



INSERT INTO Payments(Amount, Created_At, Code)
VALUES 
(3663, '2018-05-31T09:16:48.908030298Z', '98f78b51-7343-4d91-9c2c-6be4fe8c97cb'),
(3663, '2018-05-31T09:16:48.908030298Z', '98f78b51-7343-4d91-9c2c-6be4fe8c97cd'),
(3663, '2018-05-31T09:16:48.908030298Z', '98f78b51-7343-4d91-9c2c-6be4fe8c97cc'),
(3663, '2018-05-31T09:16:48.908030298Z', '98f78b51-7343-4d91-9c2c-6be4fe8c97ce'),
(3663, '2018-05-31T09:16:48.908030298Z', '98f78b51-7343-4d91-9c2c-6be4fe8c97cf'),
(3663, '2018-05-31T09:16:48.908030298Z', '98f78b51-7343-4d91-9c2c-6be4fe8c97cg'),
(3663, '2018-05-31T09:16:48.908030298Z', '98f78b51-7343-4d91-9c2c-6be4fe8c97ch'),
(3663, '2018-05-31T09:16:48.908030298Z', '98f78b51-7343-4d91-9c2c-6be4fe8c97cj'),
(3663, '2018-05-31T09:16:48.908030298Z', '98f78b51-7343-4d91-9c2c-6be4fe8c97ck'),
(3663, '2018-05-31T09:16:48.908030298Z', '98f78b51-7343-4d91-9c2c-6be4fe8c97cl'),
(3663, '2018-05-31T09:16:48.908030298Z', '98f78b51-7343-4d91-9c2c-6be4fe8c97cp'),
(3663, '2018-05-31T09:16:48.908030298Z', '98f78b51-7343-4d91-9c2c-6be4fe8c97co'),
(3663, '2018-05-31T09:16:48.908030298Z', '98f78b51-7343-4d91-9c2c-6be4fe8c97ci'),
(3663, '2018-05-31T09:16:48.908030298Z', '98f78b51-7343-4d91-9c2c-6be4fe8c97cu');


INSERT INTO Orders(UserID, PaymentID, Status, Created_at)
VALUES 
('2', 1, 1, '2018-05-31'),
('2', 2, 1, '2018-05-31'),
('2', 3, 1, '2018-05-31'),
('2', 4, 1, '2018-05-31'),
('2', 5, 1, '2018-05-31'),
('2', 6, 1, '2018-05-31'),
('2', 7, 1, '2018-05-31'),
('2', 8, 1, '2018-05-31'),
('2', 9, 1, '2018-05-31'),
('2', 10, 1, '2018-05-31'),
('2', 11, 1, '2018-05-31'),
('2', 12, 1, '2018-05-31'),
('2', 13, 1, '2018-05-31'),
('2', 14, 1, '2018-05-31');


INSERT INTO OrderDetails(OrderID, ProductID, Quantity)
VALUES
(14, 10, 1),
(14, 1, 1),
(14, 12, 3),
(13, 10, 1),
(12, 10, 1),
(11, 10, 1),
(10, 10, 1),
(1, 10, 1),
(2, 10, 1),
(3, 10, 1),
(4, 10, 1),
(5, 10, 1),
(6, 10, 1),
(7, 10, 1),
(8, 10, 1),
(9, 10, 1);