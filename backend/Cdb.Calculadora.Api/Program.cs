using Cdb.Calculadora.Application.Behaviors;
using Cdb.Calculadora.Application.Commands;
using Cdb.Calculadora.Application.Validators;
using Cdb.Calculadora.Domain.Services;
using MediatR;
using FluentValidation;
using Cdb.Calculadora.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// 🔧 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔧 Controllers + FluentValidation
builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<CalcularInvestimentoCommandValidator>();

// 🔧 MediatR + Behavior de validação
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<CalcularInvestimentoCommand>());

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// 🔧 DI de serviço de cálculo
builder.Services.AddScoped<ICalculoCdbService, CalculoCdbService>();

// 🔧 CORS para permitir acesso do Angular (localhost:4200)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
// 🔧 Habilitar Swagger no dev
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }