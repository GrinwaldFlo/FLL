
using FLL.Data;
using FLL.Data.Types;
using FLL.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System;

namespace FLL.Pages.Components
{

    public class BasePage : ComponentBase
    {
        [Inject] protected NavigationManager Nav { get; set; } = null!;
        [CascadingParameter] protected NavSettings Set { get; set; } = null!;
        [Inject] protected IWebHostEnvironment Env { get; set; } = null!;

        [Inject] protected DataService Data { get; set; } = null!;

        [Inject] protected IJSRuntime JSRuntime { get; set; } = null!;

        [Parameter] public string? GUID { get; set; }
        [Parameter] public string? ContestName { get; set; }

        

        

        public string? ErrorMessage { get; set; } = null;


        protected bool CheckAccessLevel()
        {
            if(GUID == null || ContestName == null) 
            {
                return Set.PageACL <= Set.UserACL;
            }
            Set.UserGUID = GUID;
            (Set.Contest, Set.UserACL) = Data.FindContest(GUID, ContestName);

            return Set.Contest != null && Set.PageACL <= Set.UserACL;
        }

        protected bool ValidateAccess()
        {
            if (!CheckAccessLevel())
            { 
                Nav.NavigateTo("/", true);
                return false;
            }
            return true;
        }

        protected async Task NavigateToNewTab(string url)
        {
            await JSRuntime.InvokeAsync<object>("open", url, "_blank");
        }
    }
}