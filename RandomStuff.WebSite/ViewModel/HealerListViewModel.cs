using RandomStuff.Lib.Model;
using System.Collections.Generic;

namespace RandomStuff.WebSite.ViewModel
{
    public class HealerListViewModel
    {
        public HealerListViewModel(IEnumerable<Healer> healers)
        {
            Healers = healers;
        }

        public IEnumerable<Healer> Healers { get; set; }
    }
}
