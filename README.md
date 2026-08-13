# CRUD com ASP.NET Core, Dapper e MySQL

API REST desenvolvida com **ASP.NET Core**, utilizando **Dapper** para acesso ao banco de dados **MySQL**.

O projeto implementa um CRUD completo de funcionários, utilizando uma arquitetura simples com separação de responsabilidades entre **Controller, BLL e DAL**.

## 🚀 Tecnologias utilizadas

* .NET / ASP.NET Core
* C#
* Dapper
* MySQL
* REST API
* Swagger

## 📁 Estrutura do projeto

```text
ApiWeb_Dapper
│
├── Controllers
│   └── FuncionariosController.cs
│
├── Models
│   └── Funcionario.cs
│
├── BLL
│   └── FuncionarioBLL.cs
│
├── DAL
│   └── FuncionarioDAL.cs
│
├── Program.cs
└── appsettings.json
```

### Responsabilidade das camadas

**Controller**

Responsável por receber as requisições HTTP e retornar as respostas da API.

**BLL — Business Logic Layer**

Responsável pelas regras de negócio e validações da aplicação.

**DAL — Data Access Layer**

Responsável pelo acesso ao banco de dados e execução dos comandos SQL através do Dapper.

**Model**

Contém as classes que representam as entidades utilizadas pela aplicação.

## 🗄️ Criando a base de dados

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

## 🧪 Inserindo dados de teste

Após criar a tabela, execute os comandos abaixo:

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

## 🔎 Verificando os dados

Para consultar os registros cadastrados:

```sql
SELECT
    id_funcionario,
    nm_funcionario,
    cargo_funcionario,
    cadastrado_em
FROM funcionario
ORDER BY nm_funcionario;
```

## 🔌 Connection String

Configure a conexão com o MySQL no arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "MySql": "Server=localhost;Database=tutorial;Uid=root;Pwd=sua_senha;"
  }
}
```

Altere os valores de acordo com a configuração do seu ambiente.

## 📡 Endpoints

### Listar funcionários

```http
GET /api/Funcionarios
```

Retorna todos os funcionários cadastrados.

### Buscar funcionário por ID

```http
GET /api/Funcionarios/byId/{id}
```

Exemplo:

```http
GET /api/Funcionarios/byId/1
```

### Cadastrar funcionário

```http
POST /api/Funcionarios
```

Exemplo de JSON:

```json
{
  "nm_funcionario": "João da Silva",
  "cargo_funcionario": "Desenvolvedor"
}
```

### Alterar funcionário

```http
PUT /api/Funcionarios/{id}
```

Exemplo:

```http
PUT /api/Funcionarios/1
```

```json
{
  "nm_funcionario": "João da Silva",
  "cargo_funcionario": "Desenvolvedor Sênior"
}
```

### Excluir funcionário

```http
DELETE /api/Funcionarios/{id}
```

Exemplo:

```http
DELETE /api/Funcionarios/1
```

## 📋 Operações disponíveis

| Método | Endpoint                      | Descrição                   |
| ------ | ----------------------------- | --------------------------- |
| GET    | `/api/Funcionarios`           | Lista todos os funcionários |
| GET    | `/api/Funcionarios/byId/{id}` | Consulta um funcionário     |
| POST   | `/api/Funcionarios`           | Cadastra um funcionário     |
| PUT    | `/api/Funcionarios/{id}`      | Altera um funcionário       |
| DELETE | `/api/Funcionarios/{id}`      | Exclui um funcionário       |

## 🧩 Dapper

O projeto utiliza o **Dapper** como micro-ORM para realizar o acesso aos dados.

Exemplo de consulta:

```csharp
var funcionarios = _dbConnection.Query<Funcionario>(
    @"SELECT
        id_funcionario,
        nm_funcionario,
        cargo_funcionario,
        cadastrado_em
      FROM funcionario
      ORDER BY nm_funcionario"
);
```

As consultas SQL ficam concentradas na camada **DAL**, mantendo os Controllers e as regras de negócio independentes do acesso direto ao banco.

## 🏗️ Fluxo da aplicação

```text
Cliente
   │
   ▼
Controller
   │
   ▼
BLL
   │
   ▼
DAL
   │
   ▼
Dapper
   │
   ▼
MySQL
```

## ▶️ Executando o projeto

### 1. Clone o repositório

```bash
git clone URL_DO_REPOSITORIO
```

### 2. Configure o MySQL

Crie o banco `tutorial` e a tabela `funcionario` utilizando o script apresentado neste README.

### 3. Configure a conexão

Edite o `appsettings.json` com as informações do seu banco MySQL.

### 4. Execute a aplicação

```bash
dotnet restore
dotnet build
dotnet run
```

### 5. Teste a API

Com a aplicação em execução, utilize o Swagger ou ferramentas como:

* Swagger
* Postman
* Insomnia

## 📚 Objetivo do projeto

Este projeto foi desenvolvido com objetivo de estudo e demonstração de uma **API REST utilizando ASP.NET Core, Dapper e MySQL**, aplicando uma separação básica entre as camadas de apresentação, regras de negócio e acesso a dados.

## 🔮 Próximos passos

Algumas possíveis evoluções para o projeto:

* [ ] Validações utilizando Data Annotations ou FluentValidation
* [ ] Autenticação com JWT
* [ ] Controle de usuários e permissões
* [ ] Paginação
* [ ] Tratamento global de exceções
* [ ] Testes unitários
* [ ] Testes de integração
* [ ] Docker
* [ ] Repository Pattern
* [ ] Documentação completa da API

## 📄 Licença

Este projeto é destinado a fins de estudo e demonstração.
