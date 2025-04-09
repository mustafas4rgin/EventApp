using AutoMapper;
using EventApp.API.Profiles;
using EventApp.Application.Providers;
using EventApp.Core.Registrations;
using EventApp.Infrastracture.Registrations;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
// Add services to the container.
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddBusinessService();

var validatorAssemblies = ValidatorAssemblyProvider.GetValidatorAssemblies();
foreach (var assemblyType in validatorAssemblies)
{
    builder.Services.AddValidatorsFromAssemblyContaining(assemblyType);
}
var dtoValidatorAssemblies = DTOValidatorAssemblyProvider.GetValidatorAssemblies();
foreach(var assemblyType in dtoValidatorAssemblies)
{
    builder.Services.AddValidatorsFromAssemblyContaining(assemblyType);
}

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
