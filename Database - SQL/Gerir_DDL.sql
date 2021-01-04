-- DDL

-- Cria o Banco de dados
CREATE DATABASE Gerir
-- Usa o Banco de dados selecionado
USE Gerir

-- Cria a tabela usuarios
CREATE TABLE Usuarios(
	id UNIQUEIDENTIFIER PRIMARY KEY,
	nome VARCHAR(150) NOT NULL,
	email VARCHAR(150) NOT NULL,
	senha VARCHAR (150) NOT NULL,
	tipo VARCHAR (100) NOT NULL
)

-- Cria a tabela tarefas
CREATE TABLE Tarefas(
	id UNIQUEIDENTIFIER PRIMARY KEY,
	titulo VARCHAR(150) NOT NULL,
	descricao TEXT NOT NULL,
	categoria VARCHAR(150) NOT NULL,
	DataEntrega DATETIME NOT NULL,
	status bit,
	
	usuario_id UNIQUEIDENTIFIER
	FOREIGN KEY (usuario_id) REFERENCES Usuarios(id) ON DELETE CASCADE
)

