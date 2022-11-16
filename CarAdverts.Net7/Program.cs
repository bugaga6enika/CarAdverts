using CarAdverts.Application.Configurations;
using CarAdverts.Net7.Middlewares;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
IoC.RegisterServices(builder.Services);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureHttpJsonOptions((options) =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    using (var serviceScope = app.Services.CreateScope())
    { DataProvider.Seed(serviceScope.ServiceProvider); }

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionsGlobalHandlerMiddleware>();

//app.UseCors(c =>
//{
//    c.AllowAnyHeader();
//    c.AllowAnyMethod();
//    c.AllowAnyOrigin();
//});

app.Run();
