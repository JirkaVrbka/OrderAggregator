using OrderAggregator;
using OrderAggregator.Services;
using OrderAggregator.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


builder.Services.AddLogging();

// Add custom services
builder.Services.AddSingleton<IStorage, MemoryStorage>();
builder.Services.AddSingleton<ISender, ConsoleSender>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<ICollectorService, CollectorService>();
builder.Services.AddHostedService<OrderSender>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();

// Improvements:
//  - add cancellation tokens to async methods
