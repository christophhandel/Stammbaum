namespace FamilyTreeMongoApp.Core.Util;

public interface IDateTimeProvider
{
    DateTime Now { get; }
}