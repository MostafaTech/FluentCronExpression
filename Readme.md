# Fluent Cron Expression
A utility to build standard cron expressions fluently based on crontab

## Install
```
PM> Install-Package FluentCronExpression
```

## Usage
You can browse tests for complete usage.

```csharp
// initialize cron expression builder
using FluentCronExpression;
var cron = new StandardCronExpressionBuilder();

// every one minute => */1 * * * *
var exp = cron.WithMinutesEvery(1).Build();

// 1st day of every month => 0 0 1 * *
var exp = cron.WithDayOfMonth(1).WithMinute(0).WithHour(0).Build();
// or use extension methods
var exp = cron.SetEveryMonth().Build();

// every monday => 0 0 * * SUN
var exp = cron.WithWeekDay(2).WithMinute(0).WithHour(0).Build();
// or use extension methods
var exp = cron.SetEveryDayOfWeek(DayOfWeek.Monday).Build();

// between 06:00 to 06:59 every 5 minutes daily => 0-59/5 6 * * *
var exp = cron.WithMinutesBetween(0, 59).WithHoursBetween(6, 6).WithMinutesEvery(5).Build();
// or use extension methods
var exp = cron.SetFromHourMinuteToHourMinuteString("06:00", "06:59").WithMinutesEvery(5).Build();
```