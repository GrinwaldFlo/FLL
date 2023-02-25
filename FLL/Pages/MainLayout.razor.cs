using FLL.Data;

namespace FLL.Pages
{
    public partial class MainLayout : IDisposable
    {
        private bool showTopMenu;
        private readonly NavSettings navSettings;

        private readonly IDisposable? showTopMenuSubscriber = null;

       

        public MainLayout()
        {
            navSettings= new NavSettings();

            showTopMenuSubscriber = navSettings.ShowTopMenu.Subscribe(x => ShowTopMenu(x));
        }

        private void ShowTopMenu(bool value)
        {
            showTopMenu = value;
            StateHasChanged();
        }

        public void Dispose()
        {
            showTopMenuSubscriber?.Dispose();
        }
    }
}
