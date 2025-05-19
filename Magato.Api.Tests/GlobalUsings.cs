// System
global using System.Net;
global using System.Net.Http.Headers;
global using System.Net.Http.Json;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
// Third-party
global using FluentAssertions;

global using Magato.Api.Controllers;
global using Magato.Api.Data;
global using Magato.Api.DTO;
global using Magato.Api.Models;
global using Magato.Api.Repositories;
// Project
global using Magato.Api.Repositories.Collections;
global using Magato.Api.Services;
global using Magato.Api.Services.Collections;
global using Magato.Api.Shared;
// Microsoft
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Testing;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;

global using Moq;

global using Xunit;
