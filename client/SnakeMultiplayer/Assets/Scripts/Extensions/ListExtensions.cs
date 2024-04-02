using System.Collections.Generic;

namespace Extensions
{
    public static class ListExtensions
    {
        public static TItem AddTo<TItem>(this TItem item, List<TItem> list)
        {
            list.Add(item);
            return item;
        }
    }
}