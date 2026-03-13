using Infrastructure;
using InstagramInfrastructure;
using MetaService.Services;
using Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IInstagramService, InstagramService>();
builder.Services.AddTransient<ITelegramService, TelegramService>();

builder.Services.Configure<InstagramOptions>(
    builder.Configuration.GetSection("Instagram"));

builder.Services.AddHttpClient<InstagramClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Instagram:BaseUrl"]!);
});

builder.Services.AddHttpClient<TelegramClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Telegram:BaseUrl"]!);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => { policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReact");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
