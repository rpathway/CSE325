// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine($"The current time is {DateTime.Now}");

DateTime today = DateTime.Now;
DateTime christmasTime = DateTime.Parse($"12/25/{today.Year}");
TimeSpan christmasDay = christmasTime.Subtract(today);

Console.WriteLine($"There are {christmasDay.Days} days until the next Christmas.");