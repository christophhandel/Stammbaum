namespace FamilyTreeMongoApp.Core.Util;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}