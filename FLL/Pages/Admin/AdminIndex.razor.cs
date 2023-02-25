namespace FLL.Pages.Admin
{
    public partial class AdminIndex
    {
        protected override void OnInitialized()
        {
            Set.Set("Admin", true, FLL.Data.Types.AccessLevels.Admin);

            if (!ValidateAccess())
                return;
            base.OnInitialized();
        }



    }
}
