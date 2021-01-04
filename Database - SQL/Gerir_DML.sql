-- DML

USE Gerir

-- Insere um novo usuário na tabela Usuarios, passando os campos da tabela 
-- NEWID() - Gera um UNIQUEIDENTIFIER para o id
INSERT INTO Usuarios(
id,nome,email,senha,tipo)
VALUES (NEWID(), ' Fernando Henrique', 'fhguerra@outlook.com','123456', 'Comun')

-- Insere um novo usuário sem passsar os campos, somente valores
INSERT INTO Usuarios
VALUES (NEWID(),'Priscila Medeiros', 'primedeiros@gmail.com', '123456','Comun')

-- Altera todas as linhas
UPDATE Usuarios SET tipo = 'Comum' 

-- Altera somente a linha desejada usando a clausula WHERE
UPDATE USUARIOS SET nome = 'Fernando Henrique Guerra' WHERE id ='D833EEB7-0E68-4ACB-84A0-E0E9580B37C0'

SELECT * FROM Usuarios -- * retorna todos os campos
SELECT nome,email FROM Usuarios

-----------------------------------------------------------------------------------------

INSERT INTO Tarefas (id, titulo, descricao, categoria, DataEntrega, status, usuario_id)
VALUES(NEWID(),'Tarefa 1', ' Descrição da tarefa 1', 'Pessoal', '04-01-2021 18:00:00' , 0, 'D833EEB7-0E68-4ACB-84A0-E0E9580B37C0')

INSERT INTO Tarefas (id, titulo, descricao, categoria, DataEntrega, status, usuario_id)
VALUES(NEWID(),'Tarefa 1', ' Descrição da tarefa 1', 'Pessoal', '04-01-2021 18:00:00' , 0, 'CB162EFD-7B1E-431A-8313-DA58632C93EC')

INSERT INTO Tarefas (id, titulo, descricao, categoria, DataEntrega, status, usuario_id)
VALUES(NEWID(),'Tarefa 2', ' Descrição da tarefa 2', 'Pessoal', '04-01-2021 18:00:00' , 0, 'D833EEB7-0E68-4ACB-84A0-E0E9580B37C0')


SELECT * FROM Usuarios
SELECT * FROM Tarefas

-- INNER JOIN 
SELECT 
	U.id AS id_usuario,
	U.nome, 
	U.email,
	t.id AS id_Tarefa,
	t.titulo,
	t.descricao,
	t.categoria,
	t.status,
	t.DataEntrega
FROM Usuarios u
INNER JOIN Tarefas t ON t.usuario_id = u.id
WHERE u.id = 'D833EEB7-0E68-4ACB-84A0-E0E9580B37C0'


UPDATE Tarefas SET status = 1 where id = '1FD45AC7-B319-4244-AA9B-4FF9DBF410A9'


	
