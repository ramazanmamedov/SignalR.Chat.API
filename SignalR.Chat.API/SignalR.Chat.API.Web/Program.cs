﻿// ---------------------------------------
// Name: Microservice Template for ASP.NET Core API
// Author: Calabonga © Calabonga SOFT
// Version: 5.0.5
// Based on: .NET 5.0.x
// Created Date: 2019-10-06
// Updated Date 2021-06-04
// ---------------------------------------
// Contacts
// ---------------------------------------
// Blog: https://www.calabonga.net
// GitHub: https://github.com/Calabonga
// YouTube: https://youtube.com/sergeicalabonga
// ---------------------------------------
// Description:
// ---------------------------------------
// This template implements Web API functionality.
// ---------------------------------------


using System;
using SignalR.Chat.API.Data.DatabaseInitialization;
using SignalR.Chat.API.Entities.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System.Threading.Tasks;

namespace SignalR.Chat.API.Web
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                var webHost = CreateHostBuilder(args).Build();
                using (var scope = webHost.Services.CreateScope())
                {
                    DatabaseInitializer.Seed(scope.ServiceProvider);
                }

                Console.Title =
                    $"{AppData.ServiceName} v.{ThisAssembly.Git.SemVer.Major}.{ThisAssembly.Git.SemVer.Minor}.{ThisAssembly.Git.SemVer.Patch}";
                await webHost.RunAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}