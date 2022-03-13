using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HomeTask.Models;
using HomeTask.Services;
using Moq;
using Xunit;

namespace HomeTask.Test
{
    public class DifferentModesTest
    {
        const int EntriesCount = 6;
        const string Path = "c:\\RootDirectoryToTest";

        [Fact]
        public void TestCountIfCompareByName()
        {
            // Arrange
            var root = Init();
            var visitor = new FileSystemVisitor(root, (e1, e2) => string.Compare(e1.Name, e2.Name, StringComparison.Ordinal));

            // Act
            var result = visitor.ToList();
            Clear(root);

            // Assert
            Assert.Equal(EntriesCount, result.Count);
        }

        [Fact]
        public void TestCountIfCompareByCreationDate()
        {
            // Arrange
            var root = Init();
            var visitor = new FileSystemVisitor(root, (e1, e2) => e1.CreationDate.CompareTo(e2.CreationDate));

            // Act
            var result = visitor.ToList();
            Clear(root);

            // Assert
            Assert.Equal(EntriesCount, result.Count);
        }

        [Fact]
        public void TestComparingByName()
        {
            // Arrange
            var root = Init();
            var visitor = new FileSystemVisitor(root, (e1, e2) => string.Compare(e1.Name, e2.Name, StringComparison.Ordinal));

            // Act
            var result = visitor.ToList();
            var expected = visitor.OrderBy(e => e.Name).ToList();
            Clear(root);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestComparingByCreationDate()
        {
            // Arrange
            var root = Init();
            var visitor = new FileSystemVisitor(root, (e1, e2) => e1.CreationDate.CompareTo(e2.CreationDate));

            // Act
            var result = visitor.ToList();
            var expected = visitor.OrderBy(e => e.CreationDate).ToList();
            Clear(root);

            // Assert
            Assert.Equal(expected, result);
        }

        private static string Init()
        {
            return new TempFolderCreator().CreateForTest(Path, EntriesCount);
        }

        private static void Clear(string root)
        {
            Directory.Delete(root, true);
        }
    }
}