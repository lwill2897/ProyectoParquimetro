-- BASE DE DATOS CREADA 05/07/2019
-- ELABORADA POR: WILL ALEJANDRO LARA
--				  RUDY ROBERTO ROSA
-- CLASE: PROGRAMACION DE NEGOCIOS

USE tempdb
GO

IF EXISTS(SELECT *FROM sys.databases WHERE NAME = 'Estacionamiento')
BEGIN 
	DROP DATABASE Estacionamiento
END
GO

CREATE DATABASE Estacionamiento
GO

USE Estacionamiento
GO

CREATE SCHEMA Parqueo
GO

CREATE TABLE Parqueo.Vehiculo
(
	Num_Placa NVARCHAR(7) NOT NULL
	CONSTRAINT PK_Vehiculo_Num_Placa PRIMARY KEY CLUSTERED,
	Hora_Ingreso TIME DEFAULT GETDATE() NOT NULL,
	Hora_Salida TIME,
	Tipo_Vehiculo INT NOT NULL
)
GO

CREATE TABLE Parqueo.Tipo_Vehiculo
(
	Id INT IDENTITY (1,1) NOT NULL
	CONSTRAINT PK_Tipo_Vehiculo_Id PRIMARY KEY CLUSTERED,
	Tipo NVARCHAR (10) NOT NULL
)
GO

CREATE TABLE Parqueo.Cobro
(
	Id INT IDENTITY (1,1) NOT NULL
	CONSTRAINT PK_Cobro_Id PRIMARY KEY CLUSTERED,
	Monto MONEY NOT NULL
)
GO

ALTER TABLE Parqueo.Vehiculo
	ADD CONSTRAINT FK_Vehiculo_Tipo_Vehiculo$Tipo_Vehiculo_Id
	FOREIGN KEY (Tipo_Vehiculo) REFERENCES Tipo_Vehiculo(Id)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
GO

INSERT INTO Parqueo.Tipo_Vehiculo(Tipo)
VALUES  ('Liviana'),
		('Pesada'),
		('Particular')
GO

SELECT * FROM Parqueo.Tipo_Vehiculo
GO

INSERT INTO Parqueo.Vehiculo(Num_Placa,Tipo_Vehiculo)
VALUES ('HND1234',12)
GO

SELECT * FROM Parqueo.Vehiculo
GO
