﻿namespace ToDoApp.Api.Contracts.Responses;

internal record ErrorResponse(string ErrorMessage, string? RequestId = null);
