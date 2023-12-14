using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace MeterApp.Components.Pages
{
    public partial class StallGas : IDisposable
    {
        [Parameter] public string StallCode { get; set; }
        string StallName { get; set; }
        AppDbContext MeterDbContext { get; set; }
        [Inject] public IDbContextFactory<AppDbContext> DbContextFactory { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }

        PaginationState pagination = new PaginationState { ItemsPerPage = 10 };

        protected override async Task OnInitializedAsync()
        {
            using (AppDbContext? dbContext = await DbContextFactory.CreateDbContextAsync())
            {
                var stall = await dbContext.Stalls.SingleOrDefaultAsync(s => s.StallCode == StallCode);
                StallName = stall?.Name;
            }
            MeterDbContext = await DbContextFactory.CreateDbContextAsync();
        }

        async Task ConfirmDelete()
        {
            // Show a prompt dialog with a message and an input field
            string input = await JSRuntime.InvokeAsync<string>("prompt", "Nhập 'DongY' để thực hiện:");

            // If the user input matches the expected value, do the action
            if (input == "DongY")
            {
                using AppDbContext? dbContext = await DbContextFactory.CreateDbContextAsync();
                var stall = await dbContext.Stalls.SingleOrDefaultAsync(s => s.StallCode == StallCode);
                stall.LastGasMeterId = default;
                stall.LastGasMeter = default;
                stall.LastGasMeterDate = default;
                stall.LatestGasMeterId = default;
                stall.LatestGasMeter = default;
                stall.LatestGasMeterDate = default;
                stall.UseGasMeter = false;

                await dbContext.GasMeters.Where(w => w.StallCode == StallCode).ExecuteDeleteAsync();

                await dbContext.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            MeterDbContext?.Dispose();
        }
    }
}
