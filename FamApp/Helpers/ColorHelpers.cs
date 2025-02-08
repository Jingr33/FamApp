using System.Globalization;

namespace FamApp.Helpers
{
    public class ColorHelpers
    {
        public static string ConvertHexToRgba (string hex, double alpha)
        {
            if (hex == null)
                return "inherit";

            if (hex.StartsWith("#"))
                hex = hex.Substring(1);

            if (hex.Length == 6)
            {
                int r =Convert.ToInt32(hex.Substring(0, 2), 16);
                int g = Convert.ToInt32(hex.Substring(2, 2), 16);
                int b = Convert.ToInt32(hex.Substring(4, 2), 16);
                return $"rgba({r}, {g}, {b}, {alpha.ToString("0.00", CultureInfo.InvariantCulture)})";
            }

            return hex;
        }
    }
}
