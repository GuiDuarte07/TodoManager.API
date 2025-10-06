# 🧩 TodoManager.API

Backend do projeto **TodoManager**, uma API REST desenvolvida em **.NET 8** para gerenciar tarefas, autenticação de usuários e comunicação com o frontend.  
Toda a infraestrutura foi projetada para ser modular, segura e facilmente escalável, utilizando **Docker**, **PostgreSQL** e **Nginx**.

---

## ⚙️ Tecnologias Utilizadas

| Categoria | Tecnologias |
|------------|--------------|
| Linguagem | C# |
| Framework | .NET 8 (ASP.NET Core Web API) |
| Banco de Dados | PostgreSQL |
| ORM | Entity Framework Core |
| Testes | xUnit |
| Autenticação | ASP.NET Identity + JWT |
| Servidor Web | Nginx |
| HTTPS | Certbot (Let's Encrypt) |
| Containerização | Docker e Docker Compose |
| CI/CD | Testes/Build/Deploy via GitActions |
| Deploy | AWS EC2 |
| Ferramentas auxiliares | dotnet CLI, EF Core Tools |

---

## 🧱 Arquitetura e Decisões de Construção

* **API RESTful** organizada em camadas:
  * **Controllers:** expõem os endpoints HTTP.
  * **Services:** contêm as regras de negócio.
  * **Repositories:** isolam o acesso ao banco de dados.
  * **DTOs (Data Transfer Objects):** garantem segurança e clareza nas respostas.
* **Entity Framework Core** com **Migrations** para controle de versão do banco.
* **ASP.NET Identity** para autenticação e autorização com **JWT Tokens**.
* **xUnit** utilizado para testes de unidade.
* **Configurações externas** via `appsettings.json` e variáveis de ambiente.
* **CORS** configurado para comunicação segura com o frontend (Next.js).
* **Docker Compose** para orquestrar:
  * API (.NET)
  * Banco de dados (PostgreSQL)
  * Proxy reverso (Nginx + Certbot)
* **CI/CD :** Foi criado um workflow via GitActions para sempre que realizado um commit na master, o sistema realizar os testes e se passar, fazer o deploy direto da ec2 usando ssh. 
* **Observações:** Algumas decisões não ideias foram tomadas por se tratar de um projeto para avaliação de forma a facilitar o uso:
  * Arquivo .env com os dados de acesso ao servidor postgres fora do .gitignore
  * Arquivo appsettings.json fora do .gitignore
  * CORS configurado para sem restrições
    
---

## 🌐 Endpoints Principais

| Método | Endpoint | Descrição |
|---------|-----------|-----------|
| **POST** | `/api/auth/register` | Cria um novo usuário |
| **POST** | `/api/auth/login` | Realiza login e retorna JWT |
| **GET** | `/api/todoitems` | Lista tarefas do usuário |
| **POST** | `/api/todoitems` | Cria uma nova tarefa |
| **PUT** | `/api/todoitems/{id}` | Atualiza uma tarefa |
| **DELETE** | `/api/todoitems/{id}` | Exclui uma tarefa |


---

## ☁️ Deploy e Infraestrutura

### 🌍 Ambiente de Produção

| Serviço | Descrição |
|----------|------------|
| **AWS EC2** | Hospeda os containers da aplicação |
| **Nginx** | Proxy reverso para a API (porta 443 → 8081) |
| **Certbot** | Emite certificados SSL via Let's Encrypt |
| **Docker Compose** | Orquestra API, PostgreSQL e Nginx |
| **PostgreSQL** | Banco de dados em container persistente |
| **ASP.NET API** | Aplicação principal, servindo requisições REST |

---

## 🌐 Como Acessar a Aplicação

🔗 **Aplicação Online:**  
👉 [https://www.todomanager.shop](https://www.todomanager.shop)

🔗 **API (Backend):**  
👉 [https://api.todomanager.shop](https://api.todomanager.shop)

---

## 🧰 Como Rodar o Projeto Localmente

### 🔧 Pré-requisitos

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
- [Git](https://git-scm.com/)
- (Opcional) [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

### ▶️ Passos para execução

```bash
# 1. Clone o repositório
git clone https://github.com/GuiDuarte07/TodoManager.API.git

# 2. Acesse o diretório
cd TodoManager.API

# 5. Suba os containers
docker-compose up -d --build

# 6. Acesse a API localmente
http://localhost:8080/swagger
```

O servidor nginx precisa de um certificado pré-configurado para funcionar, isso foi feito no servidor EC2. 

Para rodar via Visual Studio é mais simples, basta acessar a solution da API e e clicar para executar via Docker Compose

<img width="761" height="73" alt="Image" src="https://github.com/user-attachments/assets/8b9f68d2-c4ac-4722-86ea-8e8676a140bb" />


### Tela do Swagger:

<img width="1254" height="1365" alt="Image" src="https://github.com/user-attachments/assets/7e025156-6fff-4831-a203-0611eb6ce47e" />


### Tela do Git Actions

<img width="1250" height="925" alt="Image" src="https://github.com/user-attachments/assets/0323104d-71d8-467d-8b8f-77d6b6ec3dbd" />


---

## 👨‍💻 Autor

**Guilherme Duarte**  
💼 Desenvolvedor Full Stack  

📧 **E-mail:** [guilhduart.abr@gmail.com](mailto:guilhduart.abr@gmail.com)  
🌐 **LinkedIn:** [linkedin.com/in/gui-duarte07](https://www.linkedin.com/in/gui-duarte07/)  
💻 **GitHub:** [github.com/GuiDuarte07](https://github.com/GuiDuarte07)
