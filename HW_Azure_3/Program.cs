
using Azure.Identity;
using HW_Azure_3.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace HW_Azure_3
{
    public class Program
    {
        private const string connectionStringConfiguration = "AzureSql";
        private const string url = "https://hw-keyvault-5.vault.azure.net/";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Database configuration
            builder.Services.AddDbContext<DataContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString(connectionStringConfiguration)));
            
            // Key vault configuration
            var keyVaultUrl = new Uri(url);
            builder.Configuration.AddAzureKeyVault(keyVaultUrl, new DefaultAzureCredential());

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapGet("/", () => Results.Redirect("/swagger"));

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
