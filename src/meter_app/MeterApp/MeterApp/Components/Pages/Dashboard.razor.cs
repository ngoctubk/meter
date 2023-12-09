using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;

namespace MeterApp.Components.Pages
{
    public partial class Dashboard
    {
        [Inject] public AppDbContext MeterDbContext { get; set; }

        PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
    }
}
