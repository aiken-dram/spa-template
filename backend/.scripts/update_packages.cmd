cd application
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package FluentValidation.DependencyInjectionExtensions
dotnet add package MediatR.Extensions.Microsoft.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Newtonsoft.Json
dotnet add package SPA.Shared.Application
cd ..

cd infrastructure
dotnet add package CsvHelper
dotnet add package IBM.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package RabbitMQ.Client
dotnet add package SPA.Shared.Infrastructure
dotnet add package Microsoft.AspNetCore.SignalR
dotnet add package Microsoft.AspNetCore.SignalR.Client
dotnet add package R.NET
cd ..

cd webapi
dotnet add package FluentValidation.AspNetCore
dotnet add package MediatR
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
dotnet add package Microsoft.AspNetCore.SpaServices.Extensions
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore
dotnet add package SPA.Shared.Application
dotnet add package Swashbuckle.AspNetCore
dotnet add package Microsoft.AspNetCore.SignalR
dotnet add package Serilog.Extensions.Logging.File
cd ..

cd workservice
dotnet add package Microsoft.Extensions.Hosting
cd ..

cd tests

cd application.integrationtests
dotnet add package Moq
dotnet add package FluentAssertions
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package coverlet.collector
cd ..

cd application.unittests
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Moq
dotnet add package FluentAssertions
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package coverlet.collector
cd ..

cd domain.unittests
dotnet add package FluentAssertions
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package coverlet.collector
cd ..

cd infrastructure.unittests
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Moq
dotnet add package FluentAssertions
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package coverlet.collector
cd ..

cd ..