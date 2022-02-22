CREATE DATABASE ProductDB;
GO
USE ProductDB;
GO
CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    Name VARCHAR (50) NOT NULL,
    Color VARCHAR (50),
    Branch VARCHAR (50),
    Price decimal(12,2)
);
GO
CREATE INDEX idx_name
ON Products (Name);
GO
CREATE INDEX idx_color
ON Products (Color);
GO
CREATE INDEX idx_branch
ON Products (Branch);
GO
CREATE INDEX idx_price
ON Products (Price);
GO
INSERT INTO Products(Name,Color,Branch,Price) values 
('IPhone X','Blue','Vietnam',950),
('Samsung 10','Gold','Hongkong',840),
('Huawei Plus','White','Taiwan',650),
('Xiaomi Mi 9','Blue','China',470),
('HTC U11+ Plus','Silver','Vietnam',380),
('LG G7 ThinQ','White','Japan',240);