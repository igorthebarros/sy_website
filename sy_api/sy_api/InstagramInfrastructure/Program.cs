using Microsoft.Extensions.Configuration;

Console.WriteLine("Meta Repository is running!");

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var tst = configuration["Instagram:Token"];
