namespace FLL.Pages.View
{
    public partial class ViewIndex
    {
        protected override void OnInitialized()
        {
            Set.Set("View", true, FLL.Data.Types.AccessLevels.Viewer);

            if (!ValidateAccess())
                return;
            base.OnInitialized();
        }



    }
}
