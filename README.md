# 📋 Sistema de Faturamento (Billing System)

## 📖 Sobre o Projeto
Sistema de gerenciamento de faturas com entidades para clientes, produtos e faturas. 
Desenvolvido em .NET com arquitetura em camadas.
Utilizando migrations, swagger e SQL Server.

### 🧩 Principais Entidades

Customer: Clientes do sistema

Product: Produtos/Serviços oferecidos

Billing: Faturas com linhas de detalhamento

BillingLine: Itens individuais das faturas

### Passo a Passo para clonar 
Clone o repositório

1.
git clone <url-do-repositorio>
cd BillingSystem


2.Build no projeto
 build

## 🧪 Testes Unitários
Tecnologias de Teste
xUnit - Framework de testes

Moq - Mocking de dependências

FluentAssertions - Asserts mais expressivos

Entity Framework InMemory - Banco em memória para testes

## ⚙️ Configuração
Connection String
Edite o appsettings.json para configurar o banco de dados:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=Nexer;Trusted_Connection=true;"
  }
}

# 🎯 Funcionalidades
## ✅ Implementadas
* CRUD de Clientes

* CRUD de Produtos

* Criação de Faturas

* Cálculo automático de totais

* Validações de negócio

* Testes unitários

###Requisitos

* A aplicação deverá ser desenvolvida usando .NET a partir da versão 5+;
* Modelagem de dados pode ser no banco de dados de sua preferência, podendo ser um banco relacional ou não relacional (mongodb, SQL Server, PostgreSQL, MySQL, etc);
* Persistência de dados no banco deverá ser feita utilizando o Entity Framework Core;
* O retorno da API deverá ser em formato JSON;
* Utilizar as requisições GET, POST, PUT ou DELETE, conforme a melhor prática;
* Criar o README do projeto descrevendo as tecnologias utilizadas, chamadas dos serviços e configurações necessário para executar a aplicação.

📞 Contato: isadoramelo995@gmail.com
