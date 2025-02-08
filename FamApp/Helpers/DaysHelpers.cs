namespace FamApp.Helpers
{
    public class DaysHelpers
    {
        public static (string, string) DaysLeft (DateTime deadline)
        {
            DateTime today = DateTime.Now;
            int daysDifference = (int)(deadline - today).TotalDays;

            string message = "";
            if (daysDifference >= 0)
                message += $"Zbývá {daysDifference} ";
            else
                message += $"Zpožděno o {Math.Abs(daysDifference)} ";

            if (Math.Abs(daysDifference) >= 5 || daysDifference == 0)
                message += "dní";
            else if (Math.Abs(daysDifference) >= 2)
                message += "dny";
            else
                message += "den";

            string bg = DaysBgAlertColor(daysDifference);
            return (message, bg);
        }

        public static string DaysBgAlertColor (int daysDifference)
        {
            if (daysDifference > 3)
                return "bg-dark";
            else if (daysDifference > 0)
                return "bg-warning";
            else
                return "bg-danger";
        }

    }
}
