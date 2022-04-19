using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace CalcStats.Test;

public class CalcStatsTest
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 })]
    [InlineData(new[] { 6, 5, 4, 3, 2, 1 })]
    [InlineData(new[] { -1, -2, -3, -4, -5, -6 })]
    [InlineData(new[] { -6, -5, -4, -3, -2, -1 })]
    [InlineData(new[] { 1, 0, -1, 2, 0, -2 })]
    [InlineData(new[] { 3, 1, -1, 2, -4, 5 })]
    [InlineData(new[] { 3, 3, 3 })]
    [InlineData(new[] { 4 })]
    [InlineData(new[] { 0 })]
    public void ShouldGetMinimumValue(int[] numbers)
    {
        var calcStats = new StatsCalculator(numbers);
        var expectedResult = numbers.Min();

        var result = calcStats.GetMinimum();

        Assert.Equal(result, expectedResult);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 })]
    [InlineData(new[] { 6, 5, 4, 3, 2, 1 })]
    [InlineData(new[] { -1, -2, -3, -4, -5, -6 })]
    [InlineData(new[] { -6, -5, -4, -3, -2, -1 })]
    [InlineData(new[] { 1, 0, -1, 2, 0, -2 })]
    [InlineData(new[] { 3, 1, -1, 2, -4, 5 })]
    [InlineData(new[] { 3, 3, 3 })]
    [InlineData(new[] { 4 })]
    [InlineData(new[] { 0 })]
    public void ShouldGetMaximumValue(int[] numbers)
    {
        var calcStats = new StatsCalculator(numbers);
        var expectedResult = numbers.Max();

        var result = calcStats.GetMaximum();

        Assert.Equal(result, expectedResult);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 })]
    [InlineData(new[] { 1, 2, 3, 4 })]
    [InlineData(new[] { 1, 2, 3 })]
    [InlineData(new[] { 1 })]
    [InlineData(new[] { 0 })]
    [InlineData(new int[] { } )]
    public void ShouldGetCountValue(int[] numbers)
    {
        var calcStats = new StatsCalculator(numbers);
        var expectedResult = numbers.Length;

        var result = calcStats.GetCount();

        Assert.Equal(result, expectedResult);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 })]
    [InlineData(new[] { 6, 5, 4, 3, 2, 1 })]
    [InlineData(new[] { -1, -2, -3, -4, -5, -6 })]
    [InlineData(new[] { -6, -5, -4, -3, -2, -1 })]
    [InlineData(new[] { 1, 0, -1, 2, 0, -2 })]
    [InlineData(new[] { 3, 1, -1, 2, -4, 5 })]
    [InlineData(new[] { 3, 3, 3 })]
    [InlineData(new[] { 4 })]
    [InlineData(new[] { 0 })]
    public void ShouldGetAverageValue(int[] numbers)
    {
        var calcStats = new StatsCalculator(numbers);
        var expectedResult = (float)numbers.Average();

        var result = calcStats.GetAverage();

        Assert.Equal(result, expectedResult);
    }

    [Fact]
    public void Should_Throw_ArgumentNullException_If_Numbers_Are_Null()
    {
        int[] data = null;

        var action = () => new StatsCalculator(data);

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void GetMinimum_Should_Throw_InvalidOperationException_If_Numbers_Are_Empty()
    {
        var data = Array.Empty<int>();
        var calcStats = new StatsCalculator(data);

        var action = () => calcStats.GetMinimum();

        action.Should().ThrowExactly<InvalidOperationException>();
    }

    [Fact]
    public void GetMaximum_Should_Throw_InvalidOperationException_If_Numbers_Are_Empty()
    {
        var data = Array.Empty<int>();
        var calcStats = new StatsCalculator(data);

        var action = () => calcStats.GetMaximum();

        action.Should().ThrowExactly<InvalidOperationException>();
    }

    [Fact]
    public void GetAverage_Should_Throw_InvalidOperationException_If_Numbers_Are_Empty()
    {
        var data = Array.Empty<int>();
        var calcStats = new StatsCalculator(data);

        var action = () => calcStats.GetAverage();

        action.Should().ThrowExactly<InvalidOperationException>();
    }
}