﻿using System;
using Microsoft.Extensions.DependencyInjection;

namespace SignalR.Chat.API.Data.DatabaseInitialization
{
    /// <summary>
    /// Database Initializer
    /// </summary>
    public static class DatabaseInitializer
    {
        public static async void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            // Should be uncomment when using UseSqlServer() settings or any other provider.
            // This is should not be used when UseInMemoryDatabase()
            // context.Database.Migrate();

            // TODO: Add your seed data here

            if (context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }
        }
    }
}