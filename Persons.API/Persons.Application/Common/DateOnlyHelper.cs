namespace Persons.Application.Common
{
    public static class DateOnlyHelper
    {
        public static bool BeValid(DateOnly? dateOnly)
        {
            var value = dateOnly.ToString();
            return DateOnly.TryParse(value, out var date) && date.CompareTo(DateOnly.MinValue) > 0;
        }
    }
}
