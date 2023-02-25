using FLL.Data.Types;
using System.Reactive.Subjects;

namespace FLL.Data
{

    public class NavSettings
    {
        public AccessLevels UserACL { get;  set; }

        public string? UserGUID { get; set; }
        public AccessLevels PageACL { get; private set; }
        public Subject<bool> ShowTopMenu { get; } = new();
        public string? PageName { get; private set; }

        public Data.Models.Contest? Contest { get; set; }

        public void Set(string pagename, bool showTopMenu, AccessLevels pageACL)
        {
            PageName = pagename;
            ShowTopMenu.OnNext(showTopMenu);
            PageACL = pageACL;
        }

    }
}
