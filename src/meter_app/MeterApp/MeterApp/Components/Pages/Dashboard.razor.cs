using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.EntityFrameworkCore;

namespace MeterApp.Components.Pages
{
    public partial class Dashboard : IDisposable
    {
        public AppDbContext MeterDbContext { get; set; }
        [Inject] public IDbContextFactory<AppDbContext> DbContextFactory { get; set; }

        PaginationState pagination = new PaginationState { ItemsPerPage = 10 };

        protected override async Task OnInitializedAsync()
        {
            MeterDbContext = await DbContextFactory.CreateDbContextAsync();
        }

        public void Dispose()
        {
            MeterDbContext?.Dispose();
        }
    }
}
