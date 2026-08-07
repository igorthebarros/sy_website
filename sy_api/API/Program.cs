using Infrastructure.Instagram;
using Infrastructure.Telegram;
using MetaService.Services;
using Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IInstagramService, InstagramService>();
builder.Services.AddSingleton<ICurrentAlbumStore, CurrentAlbumStore>();
builder.Services.AddTransient<ITelegramService, TelegramService>();

builder.Services.Configure<InstagramOptions>(
    builder.Configuration.GetSection("Instagram"));

builder.Services.AddHttpClient<InstagramClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Instagram:BaseUrl"]!);
});

builder.Services.Configure<TelegramOptions>(
    builder.Configuration.GetSection("Telegram"));

builder.Services.AddHttpClient<TelegramClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Telegram:BaseUrl"]!);
});

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? Array.Empty<string>();

if (allowedOrigins.Length == 0 && builder.Environment.IsDevelopment())
{
    allowedOrigins = new[]
    {
        "http://localhost:3420",
        "http://localhost:5173",
        "https://localhost:3420",
        "https://localhost:5173"
    };
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        if (allowedOrigins.Length > 0)
        {
            policy.WithOrigins(allowedOrigins);
        }
        else
        {
            policy.SetIsOriginAllowed(_ => false);
        }

        policy.AllowAnyHeader()
            .AllowAnyMethod();
    });
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
