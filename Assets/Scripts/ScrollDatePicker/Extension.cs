using Boo.Lang;

namespace ScrollDatePicker
{
    public static class Extension
    {
        public static void SetAsLastAndRemove<T>(this List<T> items)
        {
            items.Add(items[0]);
            items.RemoveAt(0);
        }

        public static void SetAsFirstAndRemove<T>(this List<T> items)
        {
            items.Insert(0, items[items.Count - 1]);
            items.RemoveAt(items.Count - 1);
        }
    }
}