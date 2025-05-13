// <copyright file="DependencyInjections.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Text;
using System.Text.Json.Serialization;

using FluentValidation;
using FluentValidation.AspNetCore;

using Magato.Api.Data;
using Magato.Api.Repositories;
using Magato.Api.Repositories.Collections;
using Magato.Api.Services;
using Magato.Api.Services.Collections;
using Magato.Api.Validators;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Magato.Api;
public static class DependencyInjection
{
    public static IServiceCollection AddMagatoServices(
        this IServiceCollection services,
        IConfiguration cfg,
        IWebHostEnvironment env)
    {
        if (!env.IsEnvironment("Testing"))
        {
            services.AddDbContext<ApplicationDbContext>(o =>
                o.UseSqlServer(cfg.GetConnectionString("DefaultConnection")));
        }

        // ---------- MVC ----------
        services.AddControllers()
                .AddJsonOptions(o =>
                    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        // ---------- Repositories & Services ----------
        // Collection aggregate
        services.AddScoped<ICollectionRepository, CollectionRepository>();
        services.AddScoped<IColorRepository, ColorRepository>();
        services.AddScoped<IMaterialRepository, MaterialRepository>();
        services.AddScoped<ISketchRepository, SketchRepository>();
        services.AddScoped<ILookbookRepository, LookbookRepository>();
        services.AddScoped<ICollectionReader, CollectionReader>();
        services.AddScoped<ICollectionWriter, CollectionWriter>();
        services.AddScoped<IColorWriter, ColorWriter>();
        services.AddScoped<IMaterialWriter, MaterialWriter>();
        services.AddScoped<ISketchWriter, SketchWriter>();
        services.AddScoped<ILookbookWriter, LookbookWriter>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IPageContentRepository, PageContentRepository>();
        services.AddScoped<IPageContentService, PageContentService>();
        services.AddScoped<IBlogPostRepository, BlogPostRepository>();
        services.AddScoped<IBlogPostService, BlogPostService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductInquiryRepository, ProductInquiryRepository>();
        services.AddScoped<IProductInquiryService, ProductInquiryService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        // ---------- Hosted Services ----------
        services.AddHostedService<ContactCleanupService>();

        // ---------- File Hadeling
        services.AddHttpContextAccessor();
        services.AddScoped<IFileStorageService, LocalFileStorageService>();

        // ---------- Validators ----------
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CollectionDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UserRegisterValidator>();
        services.AddValidatorsFromAssemblyContaining<UserLoginValidator>();
        services.AddValidatorsFromAssemblyContaining<PageContentValidator>();
        services.AddValidatorsFromAssemblyContaining<BlogPostValidator>();
        services.AddValidatorsFromAssemblyContaining<ProductValidator>();
        services.AddValidatorsFromAssemblyContaining<ProductInquiryValidator>();
        services.AddValidatorsFromAssemblyContaining<SocialMediaLinkDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<LookbookImageDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CategoryDtoValidator>();

        // ---------- Auth ----------
        services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(cfg["Jwt:Key"] ?? "supersecretkey123")),
                    };
                });

        services.AddAuthorization(o =>
        {
            o.AddPolicy("Admin", p => p.RequireRole("Admin"));
        });

        // ---------- Swagger ----------
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Magato API", Version = "v1" });
            c.UseInlineDefinitionsForEnums();
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n" +
                              "Skriv 'Bearer {token}' i fältet nedan.\r\n\r\nExempel: 'Bearer abc123xyz'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                    },
                    new List<string>()
                },
            });
        });

        // ---------- CORS ----------
        services.AddCors(o =>
        {
            o.AddPolicy("AllowFrontend", p =>
            {
                p.WithOrigins("http://localhost:3000")
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowCredentials();
            });
        });

        // ---------- OpenAPI helper ----------
        services.AddOpenApi();

        return services;
    }
}
