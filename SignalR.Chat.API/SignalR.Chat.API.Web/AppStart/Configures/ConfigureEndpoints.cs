﻿using Microsoft.AspNetCore.Builder;

namespace SignalR.Chat.API.Web.AppStart.Configures
{
    /// <summary>
    /// Configure pipeline
    /// </summary>
    public static class ConfigureEndpoints
    {
        /// <summary>
        /// Configure Routing
        /// </summary>
        /// <param name="app"></param>
        public static void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}