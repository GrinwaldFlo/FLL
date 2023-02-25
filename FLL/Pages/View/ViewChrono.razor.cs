using FLL.Data.Models;
using FLL.Pages.Components;
using FLL.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace FLL.Pages.View
{
    public partial class ViewChrono : IDisposable
    {
        [Parameter] public int? ChronoID { get; set; }

        private Chrono? chrono = null;
        [Inject] ChronoService ChronoService { get; set; } = null!;

        private IDisposable? OnChronoChange = null;

        public void Dispose()
        {
            OnChronoChange?.Dispose();
        }

        protected override void OnInitialized()
        {
            Set.Set("View chrono", false, FLL.Data.Types.AccessLevels.Viewer);

            if (!ValidateAccess())
                return;
            base.OnInitialized();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (Set.Contest == null)
                return;
            chrono = ChronoService.GetChrono(Set.Contest, ChronoID);
            OnChronoChange = ChronoService.OnChange.Subscribe(async (arg) => await OnChange(arg));
        }

        private async Task OnChange(bool value)
        {
            if (value)
                await InvokeAsync(StateHasChanged);
        }

    }
}
