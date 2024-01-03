﻿global using AutoMapper;
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.OpenApi.Models;
global using Serilog;
global using ToDoApp.Api;
global using ToDoApp.Api.Mapping;
global using ToDoApp.Api.Options;
global using ToDoApp.Api.Tasks;
global using ToDoApp.Bll;
global using ToDoApp.Bll.Exceptions;
global using ToDoApp.Bll.Extensions;
global using ToDoApp.Bll.Models.Commands;
global using ToDoApp.Bll.Models.Queries;
global using ToDoApp.Bll.Models.Results;
global using ToDoApp.Storage.MongoDb.Extensions;
