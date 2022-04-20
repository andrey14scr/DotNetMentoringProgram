using System;
using FluentAssertions;
using Xunit;

namespace LeapYear.Tests;

public class LeapYearTest
{
    [Theory]
    [InlineData(1920)]
    [InlineData(2000)]
    [InlineData(1996)]
    public void Should_Be_Leap(int year)
    {
        var yearsService = new YearsService();

        var result = yearsService.GetYearsType(year);

        result.Should().Be(YearsType.Leap);
    }

    [Theory]
    [InlineData(1900)]
    [InlineData(2001)]
    [InlineData(2019)]
    public void Should_Be_Common(int year)
    {
        var yearsService = new YearsService();

        var result = yearsService.GetYearsType(year);

        result.Should().Be(YearsType.Common);
    }

    [Theory]
    [InlineData(-2001)]
    [InlineData(-100)]
    public void Should_Throw_ArgumentOutOfRangeException_If_Year_Is_Negative(int year)
    {
        var yearsService = new YearsService();

        var action = () => yearsService.GetYearsType(year);

        action.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }
}