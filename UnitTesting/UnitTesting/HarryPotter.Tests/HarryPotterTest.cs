using System;
using FluentAssertions;
using Xunit;

namespace HarryPotter.Tests;

public class HarryPotterTest
{
    [Theory]
    [InlineData(new[] { 0, 0, 0, 0, 0 }, 0)]
    [InlineData(new[] { 1, 0, 0, 0, 0 }, 8)]
    [InlineData(new[] { 1, 1, 0, 0, 0 }, 15.2)]
    [InlineData(new[] { 1, 1, 1, 0, 0 }, 21.6)]
    [InlineData(new[] { 1, 1, 1, 1, 1 }, 30)]
    [InlineData(new[] { 2, 1, 0, 0, 0 }, 23.2)]
    [InlineData(new[] { 2, 1, 1, 0, 0 }, 29.6)]
    [InlineData(new[] { 2, 2, 2, 1, 1 }, 51.6)]
    [InlineData(new[] { 5, 5, 4, 5, 4 }, 141.6)]
    public void GetCost_PossibleCombination_ShouldGetCost(int[] books, float expected)
    {
        var booksService = new BooksService();

        var actual = booksService.GetCost(books);

        Assert.Equal(expected, actual, 4);
    }

    [Fact]
    public void GetCost_NullData_ShouldThrowArgumentNullException()
    {
        int[] data = null;
        var booksService = new BooksService();

        var action = () => booksService.GetCost(data);

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void GetCost_TooBigData_ShouldThrowArgumentOutOfRangeException()
    {
        var data = new int[6];
        var booksService = new BooksService();

        var action = () => booksService.GetCost(data);

        action.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void GetCost_TooSmallData_ShouldThrowArgumentOutOfRangeException()
    {
        var data = new int[4];
        var booksService = new BooksService();

        var action = () => booksService.GetCost(data);

        action.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void GetCost_NegativeData_ShouldThrowArgumentException()
    {
        var data = new[] {1, -1, 1, -1, 1};
        var booksService = new BooksService();

        var action = () => booksService.GetCost(data);

        action.Should().ThrowExactly<ArgumentException>();
    }
}