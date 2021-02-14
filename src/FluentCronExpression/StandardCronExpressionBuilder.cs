using System;
using System.Linq;
using System.Collections.Generic;

namespace FluentCronExpression
{
    public class StandardCronExpressionBuilder
    {
        private readonly StandardCronExpressionConfiguration _configuration;
        private (string Minute, string Hour, string DayOfMonth, string Month, string WeekDay) exp;

        #region Constructors

        public StandardCronExpressionBuilder() : this(null, null) { }
        public StandardCronExpressionBuilder(string expression) : this(expression, null) { }
        public StandardCronExpressionBuilder(StandardCronExpressionConfiguration configuration) : this(null, configuration) { }
        public StandardCronExpressionBuilder(string expression, StandardCronExpressionConfiguration configuration)
        {
            // set configuration or default
            _configuration = configuration ?? new StandardCronExpressionConfiguration();
            
            Reset(expression);
        }

        public StandardCronExpressionBuilder Reset(string expression = null)
        {
            // set default expression if its not provided
            if (string.IsNullOrWhiteSpace(expression))
                expression = "* * * * *";

            // validate initial expression
            var parts = expression.Split(' ');
            if (parts.Length != 5)
                throw new ArgumentException($"The value '{expression}' is not a valid cron expression.");

            // explode expression to a tuple object
            exp = (parts[0], parts[1], parts[2], parts[3], parts[4]);

            return this;
        }

        #endregion

        #region Getters

        public string GetMinute() => exp.Minute;
        public string GetHour() => exp.Hour;
        public string GetDayOfMonth() => exp.DayOfMonth;
        public string GetMonth() => exp.Month;
        public string GetWeek() => exp.WeekDay;
        public string Build() =>
            $"{exp.Minute} {exp.Hour} {exp.DayOfMonth} {exp.Month} {exp.WeekDay}";

        #endregion

        #region Minute

        public StandardCronExpressionBuilder WithMinute(int at)
        {
            guardAgainstInvalidMinuteValue(at);
            exp.Minute = at.ToString();
            return this;
        }
        public StandardCronExpressionBuilder WithMinutes(params int[] minutes)
        {
            foreach (var i in minutes)
                guardAgainstInvalidMinuteValue(i);

            exp.Minute = string.Join(",", minutes.Select(x => x.ToString()));
            return this;
        }
        public StandardCronExpressionBuilder WithMinutesBetween(int from, int to)
        {
            guardAgainstInvalidMinuteValue(from);
            guardAgainstInvalidMinuteValue(to);
            guardAgainstInvalidRange(from, to);
            exp.Minute = $"{from}-{to}";
            return this;
        }
        public StandardCronExpressionBuilder WithMinutesAll()
        {
            exp.Minute = "*";
            return this;
        }
        public StandardCronExpressionBuilder WithMinutesEvery(int interval)
        {
            guardAgainstInvalidIntervalValue(interval);
            exp.Minute =
                (exp.Minute.Contains("/") ? exp.Minute.Substring(0, exp.Minute.IndexOf("/")) : exp.Minute) +
                (interval > 0 ? "/" + interval.ToString() : "");

            return this;
        }

        #endregion

        #region Hour

        public StandardCronExpressionBuilder WithHour(int at)
        {
            guardAgainstInvalidHourValue(at);
            exp.Hour = at.ToString();
            return this;
        }
        public StandardCronExpressionBuilder WithHours(params int[] hours)
        {
            foreach (var i in hours)
                guardAgainstInvalidHourValue(i);

            exp.Hour = string.Join(",", hours.Select(x => x.ToString()));
            return this;
        }
        public StandardCronExpressionBuilder WithHoursBetween(int from, int to)
        {
            guardAgainstInvalidHourValue(from);
            guardAgainstInvalidHourValue(to);
            guardAgainstInvalidRange(from, to);
            exp.Hour = $"{from}-{to}";
            return this;
        }
        public StandardCronExpressionBuilder WithHoursAll()
        {
            exp.Hour = "*";
            return this;
        }
        public StandardCronExpressionBuilder WithHoursEvery(int interval)
        {
            guardAgainstInvalidIntervalValue(interval);
            exp.Hour =
                (exp.Hour.Contains("/") ? exp.Hour.Substring(0, exp.Hour.IndexOf("/")) : exp.Hour) +
                (interval > 0 ? "/" + interval.ToString() : "");

            return this;
        }

        #endregion

        #region Day Of Month

        public StandardCronExpressionBuilder WithDayOfMonth(int atDayOfMonth)
        {
            guardAgainstInvalidDayOfMonthValue(atDayOfMonth);
            exp.DayOfMonth = atDayOfMonth.ToString();
            return this;
        }
        public StandardCronExpressionBuilder WithDaysOfMonth(params int[] atDaysOfMonth)
        {
            foreach (var i in atDaysOfMonth)
                guardAgainstInvalidDayOfMonthValue(i);

            exp.DayOfMonth = string.Join(",", atDaysOfMonth.Select(x => x.ToString()));
            return this;
        }
        public StandardCronExpressionBuilder WithDaysOfMonthBetween(int from, int to)
        {
            guardAgainstInvalidDayOfMonthValue(from);
            guardAgainstInvalidDayOfMonthValue(to);
            guardAgainstInvalidRange(from, to);
            exp.DayOfMonth = $"{from}-{to}";
            return this;
        }
        public StandardCronExpressionBuilder WithDaysOfMonthAll()
        {
            exp.DayOfMonth = "*";
            return this;
        }
        public StandardCronExpressionBuilder WithDaysOfMonthEvery(int interval)
        {
            guardAgainstInvalidIntervalValue(interval);
            exp.DayOfMonth =
                (exp.DayOfMonth.Contains("/") ? exp.DayOfMonth.Substring(0, exp.DayOfMonth.IndexOf("/")) : exp.DayOfMonth) +
                (interval > 0 ? "/" + interval.ToString() : "");

            return this;
        }

        #endregion

        #region Month

        private Dictionary<int, string> mapMonths = new Dictionary<int, string>()
        {
            { 1, "JAN" }, { 2, "FEB" }, { 3, "MAR" },
            { 4, "APR" }, { 5, "MAY" }, { 6, "JUN" },
            { 7, "JUL" }, { 8, "AUG" }, { 9, "SEP" },
            { 10, "OCT" }, { 11, "NOV" }, { 12, "DEC" },
        };

        public StandardCronExpressionBuilder WithMonth(int at)
        {
            guardAgainstInvalidMonthValue(at);
            exp.Month = _configuration.UseMonthNames ? mapMonths[at] : at.ToString();
            return this;
        }
        public StandardCronExpressionBuilder WithMonths(params int[] months)
        {
            foreach (var i in months)
                guardAgainstInvalidMonthValue(i);

            exp.Month = string.Join(",", months.Select(x => _configuration.UseMonthNames ? mapMonths[x] : x.ToString()));
            return this;
        }
        public StandardCronExpressionBuilder WithMonthsBetween(int from, int to)
        {
            guardAgainstInvalidMonthValue(from);
            guardAgainstInvalidMonthValue(to);
            guardAgainstInvalidRange(from, to);
            exp.Month = _configuration.UseMonthNames ?
                $"{mapMonths[from]}-{mapMonths[to]}" :
                $"{from}-{to}";
            return this;
        }
        public StandardCronExpressionBuilder WithMonthsAll()
        {
            exp.Month = "*";
            return this;
        }

        #endregion

        #region Week

        private Dictionary<int, string> mapWeekDays = new Dictionary<int, string>()
        {
            { 1, "SUN" }, { 2, "MON" }, { 3, "TUE" }, { 4, "WED" }, { 5, "THR" }, { 6, "FRI" }, { 7, "SAT" }
        };

        public StandardCronExpressionBuilder WithWeekDay(int at)
        {
            guardAgainstInvalidWeekDayValue(at);
            exp.WeekDay = _configuration.UseWeekDayNames ? mapWeekDays[at] : at.ToString();
            return this;
        }
        public StandardCronExpressionBuilder WithWeekDays(params int[] weekDays)
        {
            foreach (var i in weekDays)
                guardAgainstInvalidWeekDayValue(i);

            exp.WeekDay = string.Join(",", weekDays.Select(x => _configuration.UseWeekDayNames ? mapWeekDays[x] : x.ToString()));
            return this;
        }
        public StandardCronExpressionBuilder WithWeekDaysBetween(int from, int to)
        {
            guardAgainstInvalidWeekDayValue(from);
            guardAgainstInvalidWeekDayValue(to);
            guardAgainstInvalidRange(from, to);
            exp.WeekDay = _configuration.UseWeekDayNames ?
                $"{mapWeekDays[from]}-{mapWeekDays[to]}" :
                $"{from}-{to}";
            return this;
        }
        public StandardCronExpressionBuilder WithWeekDaysAll()
        {
            exp.WeekDay = "*";
            return this;
        }

        #endregion

        #region Validations

        private void guardAgainstInvalidMinuteValue(int value)
        {
            if (value < 0 || value > 59)
                throw new ArgumentException($"Cron minute value '{value}' is not valid.");
        }
        private void guardAgainstInvalidHourValue(int value)
        {
            if (value < 0 || value > 24)
                throw new ArgumentException($"Cron hour value '{value}' is not valid.");
        }
        private void guardAgainstInvalidDayOfMonthValue(int value)
        {
            if (value < 1 || value > 31)
                throw new ArgumentException($"Cron day of month month value '{value}' is not valid.");
        }
        private void guardAgainstInvalidMonthValue(int value)
        {
            if (value < 1 || value > 12)
                throw new ArgumentException($"Cron month value '{value}' is not valid.");
        }
        private void guardAgainstInvalidWeekDayValue(int value)
        {
            if (value < 1 || value > 7)
                throw new ArgumentException($"Cron week day value '{value}' is not valid.");
        }
        private void guardAgainstInvalidIntervalValue(int value)
        {
            if (value < 0)
                throw new ArgumentException($"Cron interval value '{value}' is not valid.");
        }
        private void guardAgainstInvalidRange(int from, int to)
        {
            if (from > to)
                throw new ArgumentException($"Cron values from '{from}' and to '{to}' are not valid.");
        }

        #endregion

    }
}
