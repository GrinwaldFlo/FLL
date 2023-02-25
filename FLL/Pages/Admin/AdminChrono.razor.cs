using FLL.Data.Models;
using FLL.Pages.Components;
using FLL.Services;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace FLL.Pages.Admin
{
    public partial class AdminChrono
    {
        private readonly GridEditor<Chrono> gridEditor = new();

        [Inject] ChronoService ChronoService { get; set; } = null!;

        protected override void OnInitialized()
        {
            Set.Set("Create chrono", true, FLL.Data.Types.AccessLevels.Admin);

            if (!ValidateAccess())
                return;
            base.OnInitialized();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var db = base.Data.Db;

            if(Set.Contest != null)
            {
                gridEditor.Init(db.Chronos.Where(x => x.Contest!.ID == Set.Contest.ID), db);
                gridEditor.AfterChange = AfterChange;
            }
        }

        private void AfterChange(GridEditor<Chrono>.ChangeType action, Chrono chrono) 
        {
            switch (action)
            {
                case GridEditor<Chrono>.ChangeType.Create:
                    chrono.ContestID = Set.Contest!.ID;
                    break;
                case GridEditor<Chrono>.ChangeType.Update:
                    break;
                case GridEditor<Chrono>.ChangeType.Delete:
                    break;
                default:
                    break;
            }
            ChronoService.AskUpdate = true;
        }


    }
}
