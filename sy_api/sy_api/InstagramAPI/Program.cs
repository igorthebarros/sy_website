using MetaService;
using MetaService.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<InstagramOptions>(
    builder.Configuration.GetSection("Instagram"));

builder.Services.AddHttpClient<MetaClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Instagram:BaseUrl"]!);
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
