using FastEndpoints;
using RiverBooks.Books;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFastEndpoints();

// Add Module Services
builder.Services.AddBookServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(c =>
    //{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RiverBooks API V1");
    //    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    //});
}

app.UseHttpsRedirection();

app.UseFastEndpoints();

app.Run();

