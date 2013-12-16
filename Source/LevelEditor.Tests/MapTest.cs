// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapTest.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace LevelEditor.Tests
{
    using System;

    using LevelEditor.Model;

    using NUnit.Framework;

    public class MapTest
    {
        #region Constants

        private const int Height = 16;

        private const int Width = 32;

        #endregion

        #region Fields

        private Map map;

        #endregion

        #region Public Methods and Operators

        public void SetUp()
        {
            this.map = new Map();
        }

        [Test]
        public void TestMapConstructor()
        {
            // Create new map.
            this.map = new Map(Width, Height);

            // Check width and height.
            Assert.AreEqual(Width, this.map.Width);
            Assert.AreEqual(Height, this.map.Height);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNegativeWidth()
        {
            this.map = new Map(-Width, Height);
        }

        [Test]
        public void TestTileIndexer()
        {
            const int X = 1;
            const int Y = 2;

            var mapTile = new MapTile(X, Y, "Desert");
            this.map[X, Y] = mapTile;

            Assert.AreEqual(mapTile, this.map[X, Y]);
        }

        #endregion
    }
}