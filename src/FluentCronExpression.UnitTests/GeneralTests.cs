using System;
using Xunit;

namespace FluentCronExpression.UnitTests
{
    public class GeneralTests
    {

        [Fact]
        public void valid_expression_in_initialize_should_work_ok()
        {
            var expression = "* * * * *";
            var cron = new StandardCronExpressionBuilder(expression);
            Assert.True(true);
        }

        [Fact]
        public void invalid_expression_in_initialize_should_work_ok()
        {
            var expression = "* * * *";
            Assert.Throws<ArgumentException>(() => new StandardCronExpressionBuilder(expression));
        }

        [Fact]
        public void validate_every1minute()
        {
            var cron = new StandardCronExpressionBuilder().WithMinutesEvery(1);
            Assert.Equal("*/1 * * * *", cron.Build());
        }

        [Fact]
        public void validate_every1stDayOfEveryMonth()
        {
            var cron = new StandardCronExpressionBuilder().SetEveryMonth();
            Assert.Equal("0 0 1 * *", cron.Build());
        }

        [Fact]
        public void validate_every1stDayOfEveryYear()
        {
            var cron = new StandardCronExpressionBuilder().SetEveryYear();
            Assert.Equal("0 0 1 JAN *", cron.Build());
        }

        [Fact]
        public void validate_every1stDayOfEveryWeek()
        {
            var cron = new StandardCronExpressionBuilder().SetEveryDayOfWeek(DayOfWeek.Sunday);
            Assert.Equal("0 0 * * SUN", cron.Build());
        }

        [Fact]
        public void validate_between0600to0659Every5MinutesDaily()
        {
            var cron = new StandardCronExpressionBuilder()
                .SetFromHourMinuteToHourMinuteString("06:00", "06:59")
                .WithMinutesEvery(5);
            Assert.Equal("0-59/5 6 * * *", cron.Build());
        }

    }
}
