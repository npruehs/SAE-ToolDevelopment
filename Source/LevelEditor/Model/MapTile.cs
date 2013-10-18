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