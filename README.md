# TasksSupero

WebService simples para gerenciamento tarefas

Base MySQL

Criação tabela CREATE TABLE Tarefa(
Id char(36) not null,
Title varchar(50),
Description varchar(250),
Complete boolean,
primary key (Id)
);

Para rodar o sistema: - Subir webapi(utilizado Owin para selfhosting) - Abir index.html na pasta Front
