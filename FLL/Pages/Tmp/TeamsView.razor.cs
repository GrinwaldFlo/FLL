using FLL.Pages.Components;
using FLL.Services;
using Microsoft.AspNetCore.Components;

namespace FLL.Pages.Tmp
{
    public partial class TeamsView : BasePage
    {
        [Parameter] public int RoundId { get; set; }

        [Inject] TeamsService Teams { get; set; } = null!;

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
