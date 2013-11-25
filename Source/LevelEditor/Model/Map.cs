// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Map.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LevelEditor.Model
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Map data.
    /// </summary>
    [XmlRoot(Namespace = "http://www.npruehs.de/teaching")]
    public class Map
    {
        #region Constructors and Destructors

        /// <summary>
        /// Parameter-less constructor provided for deserialization.
        /// </summary>
        public Map()
        {
        }

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

            this.Tiles = new MapTile[width * height];
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Height of this map, in tiles.
        /// </summary>
        [XmlAttribute]
        public int Height { get; set; }

        /// <summary>
        /// Tiles of this map.
        /// </summary>
        public MapTile[] Tiles { get; set; }

        /// <summary>
        /// Width of this map, in tiles.
        /// </summary>
        [XmlAttribute]
        public int Width { get; set; }

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
                return this.Tiles[(y * this.Width) + x];
            }

            set
            {
                this.Tiles[(y * this.Width) + x] = value;
            }
        }

        /// <summary>
        /// Map tile with the specified index.
        /// </summary>
        /// <param name="index">Index of the map tile.</param>
        /// <returns>Map tile with the specified index.</returns>
        public MapTile this[int index]
        {
            get
            {
                return this.Tiles[index];
            }

            set
            {
                this.Tiles[index] = value;
            }
        }

        /// <summary>
        /// Map tile with the specified position.
        /// </summary>
        /// <param name="position">Position of the map tile.</param>
        /// <returns>Map tile with the specified coordinates.</returns>
        public MapTile this[Vector2I position]
        {
            get
            {
                return this[position.X, position.Y];
            }

            set
            {
                this[position.X, position.Y] = value;
            }
        }

        #endregion
    }
}