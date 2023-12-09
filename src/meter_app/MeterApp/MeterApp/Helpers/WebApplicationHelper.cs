using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MeterApp.Helpers
{
    public static class WebApplicationHelper
    {
        public static async ValueTask InitializeDatabaseAsync(this WebApplication webApplication)
        {
            using IServiceScope scope = webApplication.Services.CreateScope();
            using AppDbContext appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            appContext.Database.Migrate();
        }
    }
}
