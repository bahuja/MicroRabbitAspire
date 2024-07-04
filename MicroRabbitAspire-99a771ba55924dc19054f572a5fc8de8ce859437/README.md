# [.NET Core Microservices with RabbitMQ](https://github.com/dpedwards/dotnet-core-micro-rabbit)

[![.NET 8](https://img.shields.io/badge/dotnet-8-blue.svg)](https://dotnet.microsoft.com/download)
[![swagger](https://img.shields.io/badge/swagger-lightgreen.svg)](https://rubygems.org/gems/minimal-mistakes-jekyll)
[![RabbitMQ](https://img.shields.io/badge/RabbitMQ-orange.svg)](https://www.rabbitmq.com/download.html)
[![Aspire](https://img.shields.io/badge/Aspire-blue.svg)](https://learn.microsoft.com/en-us/dotnet/aspire/)

.NET Core Microservice architecture design pattern with RabbitMQ messaging

![](MicroRabbit/images/.NET_Core_Microservices_(RabbitMQ_EventBus).png)

## Projects

- MicroRabbit.Domain.Core
- MicroRabbit.Infra.Bus
- MicroRabbit.Infra.IoC
- MicroRabbit.Banking.ApiNew
- MicroRabbit.Banking.Application
- MicroRabbit.Banking.Data
- MicroRabbit.Banking.Domain
- MicroRabbit.Transfer.ApiNew
- MicroRabbit.Transfer.Application
- MicroRabbit.Transfer.Data
- MicroRabbit.Transfer.Domain
- MicroRabbit.MVCNew

## Notable features

- Microservice architecture design pattern
- RabbitMQ messaging

## Project notes

- **MicroRabbit.Banking.Api** project is listening on localhost port `5001` (https) and `5000` (http)
![](MicroRabbit/images/Banking_Microservice_Swagger_UI.png)

- **MicroRabbit.Transfer.Api** project is listening on localhost port `5003` (https) and `5002` (http)
![](MicroRabbit/images/Transfer_Microservice_Swagger_UI.png)

- **MicroRabbit.MVC** project is listeing on localhost port `5005` (https) and `5004` (http)
![](MicroRabbit/images/Banking_Microservice_MVC.png)

### Installation

Check if .NET 8 + and PODMAN/DOCKER is installed on your machine. 

---


### Requirements

- [Visual Studio](https://visualstudio.microsoft.com/de/vs/) or [Visual Studio Code](https://code.visualstudio.com/)
- [.NET 8](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/de-de/sql-server/sql-server-downloads)

### Packages:

- [Microsoft.NETCore.App](https://dotnet.microsoft.com/)
- [MediatR](https://github.com/jbogard/MediatR)
- [Microsoft.Extensions.DependencyInjection](https://dotnet.microsoft.com/apps/aspnet)
- [Newtonsoft.Json](https://www.newtonsoft.com/json)
- [RabbitMQ.Client](https://www.rabbitmq.com/dotnet.html)
- [Microsoft.AspNetCore.Razor.Design](https://dotnet.microsoft.com/apps/aspnet)
- [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [Microsoft.EntityFrameworkCore](https://docs.microsoft.com/de-de/ef/core/)
- [Microsoft.EntityFrameworkCore.Design](https://docs.microsoft.com/de-de/ef/core/)
- [Microsoft.EntityFrameworkCore.SqlServer](https://docs.microsoft.com/de-de/ef/core/)
- [Microsoft.EntityFrameworkCore.Tools](https://docs.microsoft.com/de-de/ef/core/)

---

## License

MIT License

Copyright (c) 2019 Davain Pablo Edwards

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

