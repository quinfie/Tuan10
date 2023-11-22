CREATE DATABASE Multiple_Choice
USE Multiple_Choice

CREATE TABLE Cau_Hoi
(
	CauHoi NVARCHAR(100) PRIMARY KEY,
	LuaChon1 NVARCHAR(50),
	LuaChon2 NVARCHAR(50),
	LuaChon3 NVARCHAR(50),
	LuaChon4 NVARCHAR(50),
	DapAn NVARCHAR(50)
)

INSERT INTO Cau_Hoi VALUES
('100 + 10 = ?', '50', '110', '200', '10', '110'), 
('130 + 20 = ?', '10', '100', '150', '10', '150'), 
('200 + 10 = ?', '50', '110', '210', '10', '210'), 
('500 + 10 = ?', '50', '510', '200', '10', '510')

delete Cau_Hoi