# 🗂️ TaskManager API

API RESTful desenvolvida para gerenciamento de projetos, tarefas e comentários, com foco em controle, histórico de alterações e desempenho. Esta aplicação foi construída com .NET 8, seguindo princípios de Clean Architecture, utilizando CQRS e testes unitários.

---

## 🚀 Como executar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- [Docker](https://www.docker.com/)  
- [Docker Compose](https://docs.docker.com/compose/install/)

### Execução via Docker com build + start

```bash
docker-compose up --build
```

### Execução via Docker em segundo plano

```bash
docker-compose up --build -d
```

### Execução via Docker sem o build

```bash
docker-compose up
```

### Parar a execução do container

```bash
docker-compose down
```

### Parar e remover tudo (container + volumes + redes)

```bash
docker-compose down -v
```

### Rebuild de Containers

```bash
docker-compose up --force-recreate --build
```

### Ver logs dos containers

```bash
docker-compose logs
```

### Executar comando em container

```bash
docker-compose exec nome-do-servico bash
```

> ⚡ **A API estará disponível em:**  
> **[http://localhost:5000](http://localhost:5000)**  
> O **Swagger** roda **diretamente na raiz `/`**, ou seja, basta acessar essa URL no navegador.

---

## 🧱 Aplicando as Migrations do Entity Framework

Antes de tudo, certifique-se de que o Entity Framework CLI está instalado globalmente:

```bash
dotnet tool install --global dotnet-ef
```

Se já estiver instalado, você pode atualizar para a versão mais recente:

```bash
dotnet tool update --global dotnet-ef
```

Antes de executar o projeto pela primeira vez, é necessário criar o banco de dados e aplicar as migrations para que as tabelas sejam geradas corretamente.

### Criar a migration inicial (caso ainda não exista)
```bash
dotnet ef migrations add InitialCreate --project ./src/TaskManager.Infrastructure --startup-project ./src/TaskManager.API
```

### Aplicar as migrations ao banco de dados
```bash
dotnet ef database update --project ./src/TaskManager.Infrastructure --startup-project ./src/TaskManager.API
```

> ⚠️ Esses comandos devem ser executados na raiz da solução. O `--project` aponta para onde estão as migrations e o `--startup-project` indica o projeto que contém o `Program.cs`.

---

## 👥 Usuários Cadastrados (Seed Inicial)

| ID | Nome             | Email                 | Função         | Login            | Senha     |
|----|------------------|-----------------------|----------------|------------------|-----------|
| 1  | João Silva       | joao@empresa.com      | Colaborador    | joao.silva       | senha123  |
| 2  | Maria Santos     | maria@empresa.com     | Colaborador    | maria.santos     | senha123  |
| 3  | Carlos Lima      | carlos@empresa.com    | Colaborador    | carlos.lima      | senha123  |
| 4  | Ana Souza        | ana@empresa.com       | Colaborador    | ana.souza        | senha123  |
| 5  | Fernanda Torres  | fernanda@empresa.com  | Gerente        | fernanda.torres  | senha123  |

> 🔑 Esses usuários são criados automaticamente ao aplicar o migration (seed).

---

## 📦 Funcionalidades

### Usuário  
Pessoa que utiliza o aplicativo detentor de uma conta.

### Projeto  
Entidade que contém várias tarefas. Um usuário pode criar, visualizar e gerenciar vários projetos.

### Tarefa  
Unidade de trabalho dentro de um projeto. Cada tarefa possui:  
- Título  
- Descrição  
- Data de vencimento  
- Status (pendente, em andamento, concluída)  
- Prioridade (baixa, média, alta)

---

## 📌 Funcionalidades Implementadas (Sprint 1)

- ✅ Listar todos os projetos de um usuário  
- ✅ Visualizar todas as tarefas de um projeto específico  
- ✅ Criar novos projetos  
- ✅ Criar novas tarefas  
- ✅ Atualizar tarefas (status e detalhes)  
- ✅ Remover tarefas  
- ✅ Adicionar comentários nas tarefas  
- ✅ Registro de histórico de alterações por meio do `IMediator`

---

## ⚖️ Regras de Negócio

1. **Prioridade de Tarefas**  
   - A prioridade é obrigatória na criação e imutável após.

2. **Remoção de Projetos**  
   - Projetos com tarefas pendentes não podem ser removidos.

3. **Histórico de Atualizações**  
   - Toda atualização de tarefa ou comentário gera um evento registrado no histórico.

4. **Limite de Tarefas por Projeto**  
   - Máximo de 20 tarefas por projeto.

5. **Relatórios de Desempenho**  
   - Relatórios acessíveis apenas para usuários com função `gerente`.

6. **Comentários nas Tarefas**  
   - Comentários devem ser registrados no histórico da tarefa.

---

## 🧪 Testes

- ✅ Testes unitários cobrindo regras de negócio com **>80% de cobertura**
- Bibliotecas utilizadas:  
  - `xUnit`  
  - `Moq`  
  - `Shouldly`

### Executar os testes

```bash
dotnet test
```

---

## 🧰 Tecnologias Utilizadas

- .NET 8  
- Entity Framework Core  
- Dapper (opcional)  
- MediatR  
- Docker  
- MySQL 8.0  
- xUnit, Moq, Shouldly  
- Clean Architecture + CQRS  

---

## 🔍 Fase 2: Perguntas para o PO

- Qual a regra de expiração para tarefas vencidas?  
- Comentários podem ser editados ou removidos?  
- O histórico deve ser visível para todos os usuários ou só para gerentes?  
- As tarefas podem ser atribuídas a múltiplos usuários?  
- Como devem ser calculadas as métricas de desempenho no relatório?  
- A criação de relatórios será assíncrona?  
- Haverá notificações por e-mail?

---

## 🌱 Fase 3: Melhorias Futuras

- 📦 Implementar autenticação via IdentityServer/Azure Entra ID  
- 🌐 Publicação em nuvem com CI/CD  
- 📈 Dashboard com gráficos de desempenho  
- 🧱 Aplicar padrão de Domain Events com Event Sourcing  
- 🕵️‍♂️ Log centralizado com Serilog + Elastic Stack  
- ⚙️ Separação entre comandos e queries com medição de performance  
- ☁️ Estratégia multi-tenant e suporte a escalabilidade horizontal  

---

## 📁 Estrutura do Projeto

```text
/src
  /Application
  /Domain
  /Infrastructure
  /Persistence
  /WebApi
/tests
  /Application.Tests
```

---

## 📃 Licença

Este projeto está licenciado sob a MIT License.
