# Gerador de Usuários

Aplicativo web desenvolvido em . NET 10 com um frontend em React + Vite, que possibilita a usuários logados a produção de arquivos Excel com dados inventados de usuários e a possibilidade de enviá-los via e-mail.

## Pré-requisitos

Antes de começar, certifique-se de ter instalado em sua máquina:

- [Node.js](https://nodejs.org/) (versão 18 ou superior)
- [.NET SDK 10](https://dotnet.microsoft.com/download/dotnet/10.0)

## Como executar o projeto

Este projeto consiste em duas partes: uma aplicação frontend em React + Vite e uma API backend em .NET 10.

### Frontend (React + Vite)

1. Navegue até o diretório do frontend:

```bash
cd site/frontend
```

2. Instale as dependências:

```bash
npm install
```

3. Inicie o servidor de desenvolvimento:

```bash
npm run dev
```

O frontend estará disponível em `http://localhost:5173` (porta padrão do Vite).

### Backend (API .NET 10)

1. Navegue até o diretório da API:

```bash
cd Api
```

2. Execute a API:

```bash
dotnet run
```

A API estará disponível em `https://localhost:7075` (verifique no terminal a porta exata)

### Configurar Email:
```json
"Email": {
  "SmtpServer": "smtp.gmail.com",
  "SmtpEmail": "seu-email@gmail.com",
  "SmtpPassword": "sua-senha-app",
  "SmtpPort": 587,
  "SmtpEnableSsl": true,
  ...
},
```

A senha do app pode ser gerada aqui: `https://myaccount.google.com/apppasswords`

Obs: será necessário ativar a autenticação de 2 fatores do gmail

## Tecnologias utilizadas

### Frontend
- React + Vite

### Backend
- .NET 10

### Banco de dados
- SQLite

## Autor

* **João Pedro Alves de Moraes**
