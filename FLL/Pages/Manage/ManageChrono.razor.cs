using FLL.Data.Models;
using FLL.Pages.Components;
using FLL.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace FLL.Pages.Manage
{
    public partial class ManageChrono : IDisposable
    {
        private List<Chrono> chronos = new();
        [Inject] ChronoService ChronoService { get; set; } = null!;

        private IDisposable? OnChronoChange = null;

        public void Dispose()
        {
            OnChronoChange?.Dispose();
        }

        protected override void OnInitialized()
        {
            Set.Set("Manage chrono", true, FLL.Data.Types.AccessLevels.Manager);

            if (!ValidateAccess())
                return;
            base.OnInitialized();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (Set.Contest == null)
                return;
            chronos = ChronoService.GetChronos(Set.Contest);
            OnChronoChange = ChronoService.OnChange.Subscribe(async (arg) => await OnChange(arg));
        }

        private async Task OnChange(bool value)
        {
            if (value)
                await InvokeAsync(StateHasChanged);
        }

        private string GetPath(Chrono chrono)
        {
            return $"/view/chrono/{Set.Contest?.ShortName}/{Set.Contest?.ViewGuid}/{chrono.ID}";
        }

    }
}
