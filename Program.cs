using App.Metrics;
using Microsoft.AspNetCore.Mvc;
using Prometheus.Metrics.Demo.infra;
using Prometheus.Metrics.Demo.repository;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration; 

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var metrics = new MetricsBuilder()
                    .Configuration.Configure(
                        options =>
                        {
                            options.AddServerTag();
                            options.AddEnvTag();
                            options.AddAppTag();
                        })
                    .OutputMetrics.AsPrometheusPlainText()
                    .Build(); // Cria/Constrói um metric root

builder.Services.AddMetrics(metrics); // Adiciona ao serviço
builder.Services.AddMetricsReportingHostedService(); // Serviço roda em backgroud para coletar as métricas
builder.Services.AddMetricsEndpoints(); // Adicionando endpoints de métricas
builder.Services.AddMetricsTrackingMiddleware(); // Adiciona os middleware de tracking
builder.Services.AddMvc().AddMetrics(); // Adiciona o metrics no mvc

var config = new ServerConfig();
configuration.Bind(config);
var mongoDbContext = new MongoDbContext(config.MongoDB);

var orderRepository = new OrderRepository(mongoDbContext);
builder.Services.AddSingleton<IOrderRepository>(orderRepository);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMetricsAllEndpoints();
app.UseMetricsAllMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
