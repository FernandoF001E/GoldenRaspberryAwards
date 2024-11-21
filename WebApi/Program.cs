using CsvHelper;
using Entity.Data;
using Infrastruture.Context;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AwardsContext>(options =>options.UseInMemoryDatabase("AwardsDB"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}

using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AwardsContext>();
        string filePath = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net8.0\\", "\\CSV\\Movielist.csv");

        using var reader = new StreamReader(filePath);

        int countLine = 0;
        int countId = 1;

        Award? award = null;

        while (!reader.EndOfStream)
        {
            char[] delimitadores = { ';' };

            string line = reader.ReadLine().ToString();
            var values = line.Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);

            if (countLine > 0)
            {
                bool isNumeric = int.TryParse(values[0], out _);

                if (isNumeric)
                {
                    if (values.Length == 4)
                    {
                        award = new Award
                        {
                            Id = countId,
                            Year = int.Parse(values[0]),
                            Title = values[1],
                            Studio = values[2],
                            Producers = values[3]
                        };
                    }
                    else if (values.Length == 5)
                    {
                        award = new Award
                        {
                            Id = countId,
                            Year = int.Parse(values[0]),
                            Title = values[1],
                            Studio = values[2],
                            Producers = values[3],
                            Winner = values[4]
                        };
                    }

                    dbContext.Awards.Add(award);
                    dbContext.SaveChanges();

                    countId++;
                }
            }
            countLine++;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message.ToString());
    }
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(options => options
    .SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API"));

app.Run();
