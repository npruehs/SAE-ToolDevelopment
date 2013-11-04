// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Map.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LevelEditor.Model
{
    using System;

    /// <summary>
    /// Map data.
    /// </summary>
    public class Map
    {
        #region Constructors and Destructors

        /// <summary>
        /// Creates a new map with the specified width and height.
        /// </summary>
        /// <param name="width">Width of the map.</param>
        /// <param name="height">Height of the map.</param>
        /// <exception cref="ArgumentOutOfRangeException">Width or height is less than or equal to zero.</exception>
        public Map(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException("width", "Width must be greater than zero.");
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException("height", "Height must be greater than zero.");
            }

            this.Width = width;
            this.Height = height;

            this.Tiles = new MapTile[width, height];
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Height of this map, in tiles.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Tiles of this map.
        /// </summary>
        public MapTile[,] Tiles { get; private set; }

        /// <summary>
        /// Width of this map, in tiles.
        /// </summary>
        public int Width { get; private set; }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Map tile with the specified coordinates.
        /// </summary>
        /// <param name="x">X-coordinate of the map tile.</param>
        /// <param name="y">Y-coordinate of the map tile.</param>
        /// <returns>Map tile with the specified coordinates.</returns>
        public MapTile this[int x, int y]
        {
            get
            {
                return this.Tiles[x, y];
            }

            set
            {
                this.Tiles[x, y] = value;
            }
        }

        #endregion
    }
}