

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;

namespace FLL.Pages
{
    public partial class Index
    {
        private readonly Data.Models.Contest newContest = new();

        protected override void OnInitialized()
        {
            Set.Set("FLL - Create new contest", false, FLL.Data.Types.AccessLevels.None);
            base.OnInitialized();
        }

        private void UpdateTo(DateTimeOffset? date)
        {
            if (newContest.DateTo == null)
                newContest.DateTo = date;
        }

        async Task OnSubmit(Data.Models.Contest newContest)
        {
            try
            {
                var msg = newContest.IsValid;
                if (string.IsNullOrEmpty(msg))
                {
                    if(await Data.ContestExists(newContest.ShortName))
                    {
                        ErrorMessage = $"{newContest.ShortName} already exists";
                        return;
                    }

                    await Data.AddContest(newContest);
                    Nav.NavigateTo(newContest.UrlAdmin);
                }
                else
                {
                    ErrorMessage = msg;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message + " " + ex.InnerException?.Message;
            }
        }
    }
}
