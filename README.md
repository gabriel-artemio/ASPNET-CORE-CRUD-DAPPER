# CRUD com ASP.NET Core, Dapper e MySQL

## Criando a base de dados para teste

Execute os comandos abaixo no MySQL para criar o banco de dados e a tabela `funcionario`:

```sql
CREATE DATABASE tutorial;

USE tutorial;

CREATE TABLE funcionario (
    id_funcionario INT PRIMARY KEY AUTO_INCREMENT,
    nm_funcionario VARCHAR(100),
    cargo_funcionario VARCHAR(100),
    cadastrado_em DATETIME DEFAULT CURRENT_TIMESTAMP
);
```

## Inserindo dados de teste

Após criar a tabela, execute os comandos abaixo para inserir alguns funcionários:

```sql
INSERT INTO funcionario (
    nm_funcionario,
    cargo_funcionario
) VALUES (
    'Ciclano da Silva',
    'Desenvolvedor'
);

INSERT INTO funcionario (
    nm_funcionario,
    cargo_funcionario
) VALUES (
    'Fulano Rocha',
    'Financeiro'
);
```

### Verificando os dados

Para verificar os registros inseridos:

```sql
SELECT
    id_funcionario,
    nm_funcionario,
    cargo_funcionario,
    cadastrado_em
FROM funcionario;
```
