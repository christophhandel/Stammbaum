using FamilyTreeMongoApp.Core.Util;

namespace FamilyTreeMongoApp.Test;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}