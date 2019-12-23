using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        [Test]
        public void SearchEngine_SearchOptionsNull_ThrowsException()
        {
            // Arrange
            var searchEngine = new SearchEngine(new List<Shirt>());
            SearchOptions searchOptions = null;

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => searchEngine.Search(null));
        }

        [Test]
        public void SearchEngine_SizesInSearchOptionsNull_ThrowsException()
        {
            // Arrange 
            var searchEngine = new SearchEngine(new List<Shirt>());

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => searchEngine.Search(new SearchOptions { Sizes = null }));
        }

        [Test]
        public void SearchEngine_ColorsInSearchOptionsNull_ThrowsException()
        {
            // Arrange 
            var searchEngine = new SearchEngine(new List<Shirt>());

            // Act + Assert
            Assert.Throws<ArgumentNullException>(() => searchEngine.Search(new SearchOptions { Colors = null }));
        }

        [Test]
        public void SearchEngine_SearchForYellowAndAnySizeShirts_ReturnsNothing()
        {
            // Arrange
            var shirts = GetShirts();
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Yellow }
            };

            // Act
            var results = searchEngine.Search(searchOptions);

            // Assert
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchEngine_SearchForBlueAndAnySizeShirts_ReturnsOnlyBlueShirt()
        {
            // Arrange
            var shirts = GetShirts();
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Blue }
            };

            // Act
            var results = searchEngine.Search(searchOptions);

            // Assert
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchEngine_SearchForRedAndSmallSizeShirts_ReturnsRedAndSmallShirt()
        {
            // Arrange
            var shirts = GetShirts();

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            // Act
            var results = searchEngine.Search(searchOptions);

            // Assert
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchEngine_SearchingForAnyColorAndAnySizeShirts_ReturnInputShirtResult()
        {
            // Arrange
            var shirts = GetShirts();
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions();

            // Act
            var results = searchEngine.Search(searchOptions);

            // Assert
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchEngine_SearchingForNonMatchingSize_ReturnNoResults()
        {
            var shirts = GetShirts();
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size> { Size.Medium }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchEngine_SearchingForNonMatchingColor_ReturnNoResults()
        {
            // Arrange
            var shirts = GetShirts();
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Yellow },
            };

            // Act
            var results = searchEngine.Search(searchOptions);

            // Asset
            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchEngine_SearchingForMultipleColorsAndMultipleSizes_ReturnResults()
        {
            var shirts = GetMultipleColorAndSizeShirts();
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Black },
                Sizes = new List<Size> { Size.Small, Size.Medium }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        private List<Shirt> GetShirts()
        {
            return new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, Color.Blue)
            };
        }

        private List<Shirt> GetMultipleColorAndSizeShirts()
        {
            return new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Blue - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, Color.Blue)
            };
        }
    }
}
