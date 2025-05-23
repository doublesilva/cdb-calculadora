# CDB Calculadora 📈💰

Esta aplicação permite simular investimentos em CDB (Certificado de Depósito Bancário) com base em um valor inicial e prazo em meses. Retorna os valores brutos e líquidos, considerando a alíquota de imposto regressiva.

---

## ✨ Tecnologias Utilizadas

### Backend (.NET 9)

* ASP.NET Core 9 (API REST)
* CQRS com MediatR
* FluentValidation para regras de negócio
* Pipeline de validação (ValidationBehavior)
* Clean Architecture (Domain, Application, Api)
* xUnit + Moq + FluentAssertions (testes)
* Cobertura de código com Coverlet + ReportGenerator
* Docker + Docker Compose

### Frontend (Angular)

* Angular Standalone Components
* FormsModule para formulários
* HttpClientModule para comunicação com a API

---

## ⚙️ Arquitetura da Solução

```
CdbCalculadora
├── backend
│   ├── Cdb.Calculadora.Api
│   ├── Cdb.Calculadora.Application
│   ├── Cdb.Calculadora.Domain
│   └── Cdb.Calculadora.Tests
├── frontend
│   └── cdb-calculadora-app (Angular)
├── docker-compose.yml
└── README.md
```

* **Domain**: regras de negócio puras
* **Application**: comandos, DTOs, validadores, interfaces de serviços
* **Api**: entrada HTTP, MediatR, Swagger, Middleware
* **Tests**: testes unitários e de integração com >90% de cobertura
* **Angular**: app que consome a API com `InvestimentoService`

---

## ⛓ Executar Localmente com Docker Compose

```bash
docker-compose up --build
```

Acesse:

* API: [http://localhost:5000/swagger](http://localhost:5000/swagger)
* Frontend: [http://localhost:4200](http://localhost:4200)

---

## ✅ Rodar Testes e Gerar Cobertura (via Dockerfile)

Ao buildar a imagem de testes, o Dockerfile roda:

```Dockerfile
RUN dotnet test ./Cdb.Calculadora.Tests --collect:"XPlat Code Coverage"
RUN dotnet tool install --global dotnet-reportgenerator-globaltool
RUN reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"/app/TestResults" -reporttypes:Html
```

O relatório gerado é salvo no container em `/app/TestResults/index.html`.

Para copiar localmente:

```bash
docker cp <container_id>:/app/TestResults ./relatorio-teste
```

---

## 📊 Cobertura de Testes

* Linhas de código cobertas: ✔✔✔ > 90%
* Testes unitários para:

  * Cálculo de CDB com alíquota regressiva
  * Handler MediatR com validação
  * Controller de API
  * Middleware de exceção
* Testes de integração com `WebApplicationFactory`

---

## 📚 Licença

MIT License

---

Desenvolvido por Diego Silva da Silva ❤️
