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
    public void GetMin_PossibleData_ShouldGetMinimumValue(int[] numbers)
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
    public void GetMax_PossibleData_ShouldGetMaximumValue(int[] numbers)
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
    public void GetCount_PossibleData_ShouldGetCountValue(int[] numbers)
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
    public void GetAverage_PossibleData_ShouldGetAverageValue(int[] numbers)
    {
        var calcStats = new StatsCalculator(numbers);
        var expectedResult = (float)numbers.Average();

        var result = calcStats.GetAverage();

        Assert.Equal(result, expectedResult);
    }

    [Fact]
    public void Init_NullData_ShouldThrowArgumentNullException()
    {
        int[] data = null;

        var action = () => new StatsCalculator(data);

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void GetMinimum_EmptyData_ShouldThrowInvalidOperationException()
    {
        var data = Array.Empty<int>();
        var calcStats = new StatsCalculator(data);

        var action = () => calcStats.GetMinimum();

        action.Should().ThrowExactly<InvalidOperationException>();
    }

    [Fact]
    public void GetMaximum_EmptyData_ShouldThrowInvalidOperationException()
    {
        var data = Array.Empty<int>();
        var calcStats = new StatsCalculator(data);

        var action = () => calcStats.GetMaximum();

        action.Should().ThrowExactly<InvalidOperationException>();
    }

    [Fact]
    public void GetAverage_EmptyData_ShouldThrowInvalidOperationException()
    {
        var data = Array.Empty<int>();
        var calcStats = new StatsCalculator(data);

        var action = () => calcStats.GetAverage();

        action.Should().ThrowExactly<InvalidOperationException>();
    }
}