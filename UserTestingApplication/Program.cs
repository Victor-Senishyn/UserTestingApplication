using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using UserTestingApplication.Utilities;
using UserTestingApplication.Data;
using UserTestingApplication.Models;
using UserTestingApplication.Repositories.Inerfaces;
using UserTestingApplication.Repositories;
using UserTestingApplication.Services.Interfaces;
using UserTestingApplication.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AuthOptions>(
    builder.Configuration.GetSection(nameof(AuthOptions)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = HeaderNames.Authorization,
        Type = SecuritySchemeType.ApiKey,
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));

//
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
builder.Services.AddScoped<IApplicationUserTestRepository, ApplicationUserTestRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<ITestRepository, TestRepository>();

builder.Services.AddScoped<ITestService, TestService>();
//

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapIdentityApi<ApplicationUser>();

app.Run();
