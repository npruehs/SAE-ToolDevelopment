// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapTile.cs" company="Nick Pruehs">
//   Copyright 2013 Nick Pruehs.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace LevelEditor.Model
{
    /// <summary>
    /// Tile of a map.
    /// </summary>
    public class MapTile
    {
        #region Constructors and Destructors

        /// <summary>
        /// Constructs a new map tile without position or type.
        /// </summary>
        public MapTile()
        {
        }

        /// <summary>
        /// Constructs a new map tile with the specified position and type.
        /// </summary>
        /// <param name="x">X-coordinate of the map tile position.</param>
        /// <param name="y">Y-coordinate of the map tile position.</param>
        /// <param name="type">Type of the map tile.</param>
        public MapTile(int x, int y, string type)
            : this(new Vector2I(x, y), type)
        {
        }

        /// <summary>
        /// Constructs a new map tile with the specified position and type.
        /// </summary>
        /// <param name="position">Position of the map tile.</param>
        /// <param name="type">Type of the map tile.</param>
        public MapTile(Vector2I position, string type)
        {
            this.Position = position;
            this.Type = type;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Position of this tile on the map.
        /// </summary>
        public Vector2I Position { get; set; }

        /// <summary>
        /// Type of this tile (e.g. swamp, desert).
        /// </summary>
        public string Type { get; set; }

        #endregion
    }
}