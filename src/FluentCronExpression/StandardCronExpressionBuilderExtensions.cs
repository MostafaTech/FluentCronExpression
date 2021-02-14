using System;
using System.Collections.Generic;

namespace FluentCronExpression
{
    public static class StandardCronExpressionBuilderExtensions
    {

        public static StandardCronExpressionBuilder SetEveryDay(
            this StandardCronExpressionBuilder builder, int minute = 0, int hour = 0)
        {
            return builder.Reset().WithMinute(minute).WithHour(hour);
        }

        public static StandardCronExpressionBuilder SetEveryMonth(
            this StandardCronExpressionBuilder builder, int dayOfMonth = 1, int minute = 0, int hour = 0)
        {
            return builder.Reset().WithDayOfMonth(dayOfMonth).WithMinute(minute).WithHour(hour);
        }

        public static StandardCronExpressionBuilder SetEveryYear(
            this StandardCronExpressionBuilder builder, int atMonth = 1, int dayOfMonth = 1, int minute = 0, int hour = 0)
        {
            return builder.Reset().WithMonth(atMonth).WithDayOfMonth(dayOfMonth).WithMinute(minute).WithHour(hour);
        }

        public static StandardCronExpressionBuilder SetEveryDayOfWeek(
            this StandardCronExpressionBuilder builder, DayOfWeek dayOfWeek, int minute = 0, int hour = 0)
        {
            return builder.Reset().WithWeekDay(((int)dayOfWeek) + 1).WithMinute(minute).WithHour(hour);
        }

    }
}
