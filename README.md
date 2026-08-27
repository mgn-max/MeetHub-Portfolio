# MeetHub

API para gerenciamento de reuniões desenvolvida como projeto pessoal de estudo e evolução em desenvolvimento backend com **C# e .NET**.

O projeto foi criado com o objetivo de aplicar conceitos de desenvolvimento de APIs, organização em camadas, autenticação, persistência de dados e implementação de regras de negócio em uma aplicação voltada ao gerenciamento de usuários, empresas e reuniões.

> **Status do projeto: Descontinuado**
>
> O desenvolvimento foi interrompido por decisão pessoal para priorizar minhas responsabilidades profissionais e preservar uma separação clara entre projetos pessoais e atividades relacionadas à empresa em que atuo atualmente.
>
> O repositório permanece público exclusivamente como **portfólio técnico**, representando o estágio alcançado pelo projeto até sua descontinuação.

## Tecnologias

* C#
* .NET 10
* ASP.NET Core Web API
* SQL Server
* Entity Framework Core
* JWT Authentication
* Swagger / Scalar
* Git

## Estrutura do Backend

O backend foi organizado em projetos com responsabilidades distintas:

```text
Backend/
├── Domain/
├── Application/
├── MeetingSystem.Infrastructure/
└── MeetingSystem.API/
```

### Domain

Responsável pelos principais elementos do domínio da aplicação, incluindo:

* Entidades
* Interfaces
* Enums
* Validadores
* Exceções
* Projeções

### Application

Camada responsável pela lógica de aplicação e pela comunicação entre o domínio e as demais partes do sistema.

### Infrastructure

Responsável pelo acesso e persistência dos dados.

Inclui:

* Entity Framework Core
* SQL Server
* Repositórios
* Migrations
* Configuração de acesso aos dados

### API

Responsável pela exposição dos endpoints e comunicação HTTP da aplicação.

A estrutura inclui:

* Controllers
* Services
* Middleware
* Mappers
* Models
* Configuração de autenticação

## Funcionalidades implementadas

Durante o desenvolvimento foram implementadas funcionalidades relacionadas a:

* Autenticação de usuários
* Autenticação baseada em JWT
* Cadastro e gerenciamento de usuários
* Cadastro e gerenciamento de empresas
* Associação de usuários a empresas
* Cadastro e gerenciamento de reuniões
* Persistência de dados em SQL Server
* Documentação e teste dos endpoints da API

## Principais Controllers

```text
AuthController
UserController
FirmController
FirmMembershipController
MeetingController
```

## Banco de Dados

O projeto utiliza **SQL Server** com **Entity Framework Core** para persistência dos dados.

A configuração utilizada durante o desenvolvimento local utiliza SQL Server LocalDB:

```text
MeetHubDb
```

As migrations do Entity Framework estão organizadas na camada de infraestrutura.

## Segurança

A API utiliza autenticação baseada em **JWT Bearer**.

Informações sensíveis, como chaves de autenticação, não são disponibilizadas no repositório público e devem ser configuradas localmente para execução da aplicação.

## Organização do Projeto

Durante o desenvolvimento, busquei separar as responsabilidades da aplicação em diferentes camadas, evitando concentrar acesso a dados, regras de negócio e exposição HTTP em um único projeto.

Essa estrutura foi utilizada principalmente como exercício prático de organização de aplicações backend e evolução dos meus conhecimentos em desenvolvimento com .NET.

## Sobre este repositório

Este repositório representa uma **versão pública e sanitizada do projeto original**, disponibilizada como parte do meu portfólio de desenvolvimento.

Embora o desenvolvimento tenha sido descontinuado, o código permanece disponível para demonstrar conhecimentos aplicados em:

* C# e .NET
* Desenvolvimento de APIs
* Orientação a objetos
* SQL Server
* Entity Framework Core
* Autenticação JWT
* Organização em camadas
* Regras de negócio
* Git
* Desenvolvimento backend
