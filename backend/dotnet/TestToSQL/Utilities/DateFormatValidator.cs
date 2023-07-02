using System.Globalization;

namespace TestToSQL.Utilities
{
    public class DateFormatValidator
    {
        public static bool IsValidDateFormat(string date)
        {
            string[] acceptedFormats = {
                "MM/dd/yyyy", "dd/MM/yyyy", "yyyy/MM/dd", "MM-dd-yyyy", "dd-MM-yyyy", "yyyy-MM-dd", "MMMM d, yyyy",
                "d", "D", "f", "F", "g", "G", "M", "m", "O", "o", "R", "r",
                "s", "t", "T", "u", "U", "Y", "y"
            };

            foreach (string format in acceptedFormats)
            {
                if (DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _))
                {
                    return true; // date is correct and can be sent!
                }
            }
            return false; // date is not in a valid format
        }
    }
}
