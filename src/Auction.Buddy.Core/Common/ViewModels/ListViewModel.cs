using System;
using System.Collections.Generic;
using System.Linq;

namespace Auction.Buddy.Core.Common.ViewModels
{
    public class ListViewModel<TItem>
    {
        public TItem[] Items { get; set; }

        public ListViewModel(IEnumerable<TItem> items = null)
        {
            Items = items?.ToArray() ?? Array.Empty<TItem>();
        }
    }
}