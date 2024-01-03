﻿global using AutoMapper;
global using FluentValidation;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.OpenApi.Models;
global using Serilog;
global using ToDoApp.Api;
global using ToDoApp.Api.Auth;
global using ToDoApp.Api.Features;
global using ToDoApp.Api.Features.Tasks.Contracts;
global using ToDoApp.Api.Features.Tasks.Mapping;
global using ToDoApp.Api.Logging;
global using ToDoApp.Bll;
global using ToDoApp.Bll.Features;
global using ToDoApp.Bll.Features.Exceptions;
global using ToDoApp.Bll.Features.Tasks;
global using ToDoApp.Bll.Features.Tasks.Create;
global using ToDoApp.Bll.Features.Tasks.Get;
global using ToDoApp.Storage.MongoDb.Extensions;
