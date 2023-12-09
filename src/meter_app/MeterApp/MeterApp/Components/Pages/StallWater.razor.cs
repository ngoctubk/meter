using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.Components;

namespace MeterApp.Components.Pages
{
    public partial class StallWater
    {
        [Parameter] public string StallCode { get; set; }
        [Inject] public AppDbContext MeterDbContext { get; set; }

        PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
    }
}
