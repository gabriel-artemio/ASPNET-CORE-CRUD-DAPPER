# CRUD com ASP.NET Core, Dapper e MySQL

## Criando a base de dados para teste:

create database tutorial;
use tutorial;

create table funcionario(
    id_funcionario int primary key auto_increment,
    nm_funcionario varchar(100),
    cargo_funcionario varchar(100),
    cadastrado_em datetime default current_timestamp
);

## Inserindo os dados

INSERT INTO `tutorial`.`funcionario` (`nm_funcionario`, `cargo_funcionario`) VALUES ('Ciclano da Silva', 'Desenvolvedor');
INSERT INTO `tutorial`.`funcionario` (`nm_funcionario`, `cargo_funcionario`) VALUES ('Fulano Rocha', 'Financeiro');