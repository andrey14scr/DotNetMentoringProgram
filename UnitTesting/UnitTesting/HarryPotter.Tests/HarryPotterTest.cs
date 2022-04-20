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
    public void TestCost(int[] books, float expected)
    {
        var booksService = new BooksService();

        var actual = booksService.GetCost(books);

        Assert.Equal(expected, actual, 4);
    }

    [Fact]
    public void Should_Throw_ArgumentNullException_If_Books_Are_Null()
    {
        int[] data = null;
        var booksService = new BooksService();

        var action = () => booksService.GetCost(data);

        action.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Should_Throw_ArgumentOutOfRangeException_If_Books_Count_Is_More_Than_Five()
    {
        var data = new int[6];
        var booksService = new BooksService();

        var action = () => booksService.GetCost(data);

        action.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Should_Throw_ArgumentOutOfRangeException_If_Books_Count_Is_Less_Than_Five()
    {
        var data = new int[4];
        var booksService = new BooksService();

        var action = () => booksService.GetCost(data);

        action.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Should_Throw_ArgumentException_If_Books_Count_Is_Negative()
    {
        var data = new[] {1, -1, 1, -1, 1};
        var booksService = new BooksService();

        var action = () => booksService.GetCost(data);

        action.Should().ThrowExactly<ArgumentException>();
    }
}