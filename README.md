# AirWatch API

## Integrantes
- Vitor Dias dos Santos — RM: 565422
- Enrico Delesporte — RM: 565760
- Felipe Modesto — RM: 561810
---

## Domínio do Projeto

O domínio escolhido para o projeto foi **Monitoramento de Qualidade do Ar**.

O sistema foi modelado para representar uma plataforma completa de monitoramento ambiental, permitindo o gerenciamento de países, cidades, sensores IoT, leituras de qualidade do ar, usuários, configurações de alerta, eventos de alerta e logs de integração com APIs externas.

---

## SGBD Utilizado

**Oracle Database** — via provider `Oracle.EntityFrameworkCore`.

A connection string é configurada via User Secrets sob a chave `ConnectionStrings:DefaultConnection`. Credenciais reais não são commitadas no repositório.

---

## Arquitetura

O projeto segue os princípios de **Clean Architecture**, organizado em quatro camadas:

| Camada | Projeto | Responsabilidade |
|--------|---------|-----------------|
| Domain | AirWatch.Domain | Entidades e regras de negócio |
| Application | AirWatch.Application | DTOs e interfaces de repositório |
| Infrastructure | AirWatch.Infrastructure | DbContext, mapeamentos, migrations e implementações de repositório |
| API | AirWatch.API | Controllers, Program.cs e configuração de DI |

---

## Entidades Modeladas

- Country
- City
- User
- Sensor
- AirReading
- AlertConfig
- AlertEvent
- IntegrationLog

---

## Relacionamentos do Sistema

| Entidades | Cardinalidade |
|-----------|--------------|
| Country → City | (1) : (N) |
| City → User | (1) : (N) |
| City → Sensor | (1) : (N) |
| City → AirReading | (1) : (N) |
| City → AlertConfig | (1) : (N) |
| City → IntegrationLog | (1) : (N) |
| User → AlertConfig | (1) : (N) |
| Sensor → AirReading | (1) : (N) |
| AlertConfig → AlertEvent | (1) : (N) |
| AirReading → AlertEvent | (1) : (N) |

---

## Persistência com EF Core

### DbContext

O `AirWatchContext` está localizado em `AirWatch.Infrastructure/Persistence/` e expõe os seguintes DbSets:

- Countries
- Cities
- Users
- Sensors
- AirReadings
- AlertConfigs
- AlertEvents
- IntegrationLogs

### Mapeamento — Fluent API

Cada entidade possui sua própria classe de configuração (`IEntityTypeConfiguration<T>`) em `AirWatch.Infrastructure/Persistence/Configurations/`:

| Arquivo | Entidade |
|---------|----------|
| CountryConfiguration.cs | Country |
| CityConfiguration.cs | City |
| UserConfiguration.cs | User |
| SensorConfiguration.cs | Sensor |
| AirReadingConfiguration.cs | AirReading |
| AlertConfigConfiguration.cs | AlertConfig |
| AlertEventConfiguration.cs | AlertEvent |
| IntegrationLogConfiguration.cs | IntegrationLog |

### Migration

Uma migration inicial foi gerada e cobre o esquema completo:

```
InitialCreate
```

Para aplicar ao banco:

```bash
dotnet ef database update --project AirWatch.Infrastructure --startup-project AirWatch.API
```

---

## Repositórios

**Interfaces** (camada Application — `AirWatch.Application/Services/`):

- ICountryRepository
- ICityRepository
- IUserRepository
- ISensorRepository
- IAirReadingRepository
- IAlertConfigRepository
- IAlertEventRepository
- IIntegrationLogRepository

**Implementações** (camada Infrastructure — `AirWatch.Infrastructure/Repositories/`):

- CountryRepository
- CityRepository
- UserRepository
- SensorRepository
- AirReadingRepository
- AlertConfigRepository
- AlertEventRepository
- IntegrationLogRepository

---

## Injeção de Dependência

Registros realizados em `Program.cs`:

```csharp
builder.Services.AddDbContext<AirWatchContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISensorRepository, SensorRepository>();
builder.Services.AddScoped<IAirReadingRepository, AirReadingRepository>();
builder.Services.AddScoped<IAlertConfigRepository, AlertConfigRepository>();
builder.Services.AddScoped<IAlertEventRepository, AlertEventRepository>();
builder.Services.AddScoped<IIntegrationLogRepository, IntegrationLogRepository>();
```

---

## Endpoints da API

Todos os controllers estão em `AirWatch.API/Controllers/` e seguem o padrão `api/[controller]`.

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `api/{entidade}` | Lista todos os registros |
| GET | `api/{entidade}/{id:guid}` | Busca por ID |
| POST | `api/{entidade}` | Cria novo registro |
| PUT | `api/{entidade}/{id:guid}` | Atualiza registro |
| DELETE | `api/{entidade}/{id:guid}` | Remove registro |

### Rotas adicionais por entidade

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `api/cities/by-country/{countryId}` | Cidades de um país |
| GET | `api/sensors/by-city/{cityId}` | Sensores de uma cidade |
| GET | `api/airreadings/by-city/{cityId}` | Leituras de uma cidade |
| GET | `api/airreadings/by-sensor/{sensorId}` | Leituras de um sensor |
| GET | `api/alertconfigs/by-user/{userId}` | Configs de alerta de um usuário |
| GET | `api/alertconfigs/by-city/{cityId}` | Configs de alerta de uma cidade |
| GET | `api/alertevents/by-config/{alertConfigId}` | Eventos de uma config de alerta |
| GET | `api/integrationlogs/by-city/{cityId}` | Logs de uma cidade |
| PATCH | `api/alertevents/{id}/notify` | Marca evento como notificado |

---

## Fluxo de Requisições

O banco possui chaves estrangeiras, portanto os cadastros devem seguir a ordem de dependência entre as entidades. Tentar criar um registro antes de seu pai resulta em erro de FK.

### Hierarquia de dependências

```
Country
  └── City
        ├── User
        │     └── AlertConfig
        │               └── AlertEvent ←─── AirReading
        ├── Sensor
        │     └── AirReading
        └── IntegrationLog
```

### Ordem correta para cadastro

**1. Country** — não depende de nenhuma outra entidade
```
POST /api/countries
{
  "name": "Brasil",
  "isoCode": "BR",
  "continent": "América do Sul"
}
```

**2. City** — depende de Country
```
POST /api/cities
{
  "countryId": "<id do country>",
  "name": "São Paulo",
  "state": "SP",
  "latitude": -23.5505,
  "longitude": -46.6333,
  "status": "A"
}
```

**3. User** — depende de City
```
POST /api/users
{
  "cityId": "<id da city>",
  "name": "Carlos Oliveira",
  "email": "carlos@email.com",
  "passwordHash": "senha123",
  "role": "ADMIN",
  "isActive": "Y",
  "notifyEmail": "Y",
  "notifyPush": "N"
}
```

**4. Sensor** — depende de City
```
POST /api/sensors
{
  "cityId": "<id da city>",
  "name": "ESP32-SP-01",
  "type": "IOT",
  "source": "IOT_DEVICE",
  "status": "ACTIVE"
}
```

**5. AirReading** — depende de City e Sensor
```
POST /api/airreadings
{
  "cityId": "<id da city>",
  "sensorId": "<id do sensor>",
  "pm25": 42.7,
  "aqi": 120,
  "category": "BAD",
  "source": "IOT_DEVICE",
  "readingAt": "2026-05-31T10:00:00"
}
```

**6. AlertConfig** — depende de User e City
```
POST /api/alertconfigs
{
  "userId": "<id do user>",
  "cityId": "<id da city>",
  "pollutant": "PM25",
  "threshold": 35.4,
  "operator": ">=",
  "severity": "WARNING",
  "isActive": "Y"
}
```

**7. AlertEvent** — depende de AlertConfig e AirReading
```
POST /api/alertevents
{
  "alertConfigId": "<id da alertconfig>",
  "readingId": "<id da airreading>",
  "measuredValue": 42.7,
  "message": "PM2.5 acima do limite",
  "status": "PENDING",
  "notificationSent": "N",
  "eventAt": "2026-05-31T10:00:00"
}
```

**8. IntegrationLog** — depende de City (opcional)
```
POST /api/integrationlogs
{
  "cityId": "<id da city>",
  "apiName": "OpenWeather",
  "endpoint": "/api/air_pollution",
  "httpMethod": "GET",
  "httpStatus": 200,
  "result": "SUCCESS",
  "responseMs": 320
}
```

---

## Pré-requisitos

| Ferramenta | Versão mínima | Link |
|------------|--------------|------|
| .NET SDK | 8.0 | https://dotnet.microsoft.com/download |
| EF Core CLI | 8.0 | `dotnet tool install --global dotnet-ef` |
| Oracle Database | 19c+ | Acesso ao servidor Oracle (ex: oracle.fiap.com.br) |

Verificando as versões instaladas:

```bash
dotnet --version
dotnet ef --version
```

---

## Como Executar

### 1. Clone o repositório

```bash
git clone <url-do-repositorio>
cd AirWatch-dotnet
```

### 2. Configure a connection string via User Secrets

```bash
cd AirWatch.API
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "User Id=<usuario>;Password=<senha>;Data Source=oracle.fiap.com.br:1521/orcl;"
```

Para verificar:

```bash
dotnet user-secrets list
```

### 3. Aplique as migrations

```bash
dotnet ef database update --project AirWatch.Infrastructure --startup-project AirWatch.API
```

### 4. Execute a API

```bash
dotnet run --project AirWatch.API
```

### 5. Acesse o Swagger

Abra no navegador: `https://localhost:{porta}/swagger`

---

## Estrutura de Pastas

```
AirWatch-dotnet/
├── AirWatch.Domain/
│   ├── Common/BaseEntity.cs
│   └── Entities/          # Country, City, User, Sensor,
│                          # AirReading, AlertConfig, AlertEvent, IntegrationLog
├── AirWatch.Application/
│   ├── DTOs/              # Request e Response por entidade
│   └── Services/          # Interfaces de repositório
├── AirWatch.Infrastructure/
│   ├── Persistence/
│   │   ├── AirWatchContext.cs
│   │   └── Configurations/ # IEntityTypeConfiguration<T> por entidade
│   ├── Migrations/         # InitialCreate
│   └── Repositories/       # Implementações dos repositórios
└── AirWatch.API/
    ├── Controllers/        # Um controller por entidade
    ├── Program.cs          # DI e configuração
    └── appsettings.json
```
