namespace FLL.Pages.Manage
{
    public partial class ManageIndex
    {
        protected override void OnInitialized()
        {
            Set.Set("Manage", true, FLL.Data.Types.AccessLevels.Manager);

            if (!ValidateAccess())
                return;
            base.OnInitialized();
        }



    }
}
