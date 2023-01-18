namespace FamilyTreeMongoApp.Core.Util;

public static class Extensions
{
    public static string Truncate(this string self, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(self) || self.Length <= maxLength)
        {
            return self;
        }

        return $"{self[..(maxLength - 3)]}...";
    }

    public static (TKey, TValue) ToTuple<TKey, TValue>(this KeyValuePair<TKey, TValue> self) =>
        (self.Key, self.Value);

    public static void Remove<TItem>(this ICollection<TItem> self, Func<TItem, bool> selector)
    {
        var toDelete = self.Where(selector).ToList();
        foreach (var del in toDelete)
        {
            self.Remove(del);
        }
    }
}